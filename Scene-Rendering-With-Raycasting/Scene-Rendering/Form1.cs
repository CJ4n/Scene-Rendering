using ObjLoader.Loader.Loaders;
using System.Numerics;
using Vector3 = ObjLoader.Loader.Data.Vector3;

namespace SceneRendering
{
    public partial class Form1 : Form
    {
        //TODO:
        // 1. zrobiæ ³adny uklad wspolzednych
        // 2. dodaæ nowe kamery
        // 3. pozwoliæ na rózne skalowanie róznych obiektów
        // 4. rózne kolory/tekstóry róznych obiektów

        private string _pathToColorMap = "..\\..\\..\\..\\..\\colorMap1.jpg";
        private string _pathToObjFile = "..\\..\\..\\..\\..\\fulltorust.obj";
        private string _pathToObjFileSecond = "..\\..\\..\\..\\..\\fulltorust.obj";
        private string _pathToPlaneObj = "..\\..\\..\\..\\..\\plane.obj";

        //private string _pathToNormalMap = "..\\..\\..\\..\\..\\brickwall_normal.jpg";

        private List<string> _pathsToObjFiles = new List<string>();

        private Vector3 _lightSource = new Vector3(1000, 300, 2500);
        private PointF _origin = new PointF(Constants.ObjectBasicDim / 2, Constants.ObjectBasicDim / 2);
        private int _radius = 1000;
        private int _angle = 0;
        private int _radiusIncrement = -10;
        private int _angleIncrement = 3;
        private int _maxSpiralRadius = 2000;
        private int _minSpiralRadious = 40;

        private Bitmap _drawArea;
        private List<PolygonFiller> _polygonFillers = new List<PolygonFiller>();
        private List<List<MyFace>> _listOfObjects = new List<List<MyFace>>();
        private List<List<MyFace>> _listOfObjectsCOPY = new List<List<MyFace>>();
        private Vector3[,] _normalMap = null;
        private MyColor[,] _colorMap;
        private Color _lighColor = Color.White;

        double[,] ZBuffer;

        // 0 - stationary cam, 1 - stationary-tracking object cam, 2 - object following cam
        private List<System.Numerics.Vector3> _camPositions = new List<System.Numerics.Vector3>();
        private List<System.Numerics.Vector3> _camTargerts = new List<System.Numerics.Vector3>();
        private int curCameraIdx_ = 0;

        private List<bool> _animateObject = new List<bool>();

        public Form1()
        {
            InitializeComponent();

            _camPositions.Add(new System.Numerics.Vector3(0, 0, 0));
            _camTargerts.Add(new System.Numerics.Vector3(0, 0, 0));
            this.zLabel.Text = "z: " + this.zTrackBar.Value.ToString();
            this.mLabel.Text = "m: " + this.mTrackBar.Value.ToString();
            this.ksLabel.Text = "ks: " + (this.ksTrackBar.Value / 100.0).ToString();
            this.kdLabel.Text = "kd: " + (this.kdTrackBar.Value / 100.0).ToString();
            _drawArea = new Bitmap(Canvas.Width * 1, Canvas.Height * 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Canvas.Image = _drawArea;
            ZBuffer = new double[Canvas.Width, Canvas.Height];

            _pathsToObjFiles.Add(_pathToObjFile);
            _animateObject.Add(true);
            _pathsToObjFiles.Add(_pathToObjFileSecond);
            _animateObject.Add(false);
            _pathsToObjFiles.Add(_pathToPlaneObj);
            _animateObject.Add(false);

            var colorMapBitmap = GetBitampFromFile(_pathToColorMap);
            _colorMap = Utils.ConvertBitmapToArray(colorMapBitmap);
            GetAndSetObj();
            PaintScene();
        }
        void zBuffer()
        {
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                g.Clear(Color.LightBlue);
            }
            for (int x = 0; x < Canvas.Width; x++)
                for (int y = 0; y < Canvas.Height; y++)
                {
                    ZBuffer[x, y] = double.MaxValue;
                }
        }
        private void PaintScene()
        {
            zBuffer();
            RotateScene();
            float ks = (float)(this.ksTrackBar.Value / 100.0);
            float kd = (float)(this.kdTrackBar.Value / 100.0);
            float ka = (float)(this.kaTrackBar.Value / 100.0);
            int m = this.mTrackBar.Value;
            bool interpolateNormalVector = this.normalRadioButton.Checked;

            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                g.Clear(Color.LightBlue);
            }
            if (this.paintObjectsCheckBox.Checked)
            {
                foreach (var polygonFiler in _polygonFillers)
                {

                    polygonFiler.FillEachFace(ka, kd, ks, m, interpolateNormalVector, _lightSource, ZBuffer);
                }
            }
            if (paintTriangulationCheckBox.Checked)
            {
                DrawTriangulation();
            }
            Canvas.Refresh();
        }
        private void GetAndSetObj()
        {
            _polygonFillers.Clear();
            _listOfObjects.Clear();
            _listOfObjectsCOPY.Clear();
            var result = LoadObjFile();
            int i = 0;
            foreach (var loadResult in result)
            {
                var faces = GetAllFaces(loadResult, i);
                var faces2 = GetAllFaces(loadResult, i);
                _listOfObjects.Add(faces);
                _listOfObjectsCOPY.Add(faces2);
                var polygonFiller = new PolygonFiller(_drawArea, faces, _colorMap, _lighColor, _normalMap);
                _polygonFillers.Add(polygonFiller);
                i++;
            }
        }
        private Bitmap GetBitampFromFile(string path)
        {
            Bitmap bitmap = new Bitmap(path);
            Rectangle cloneRect = new Rectangle(0, 0, Math.Min(_drawArea.Width, bitmap.Width), Math.Min(_drawArea.Height, bitmap.Height));
            Bitmap bmp = bitmap.Clone(cloneRect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            bitmap.Dispose();
            return bmp;
        }
        private List<MyFace> GetAllFaces(LoadResult data, int idx)
        {
            float maxX = data.Vertices.Max(x => x.X);
            float maxY = data.Vertices.Max(x => x.Y);
            float maxZ = data.Vertices.Max(x => x.Z);
            float minX = data.Vertices.Min(x => x.X);
            float minY = data.Vertices.Min(x => x.Y);
            float minZ = data.Vertices.Min(x => x.Z);

            if (maxZ == minZ)
            {

                maxZ *= 2;
                if (maxZ == 0)
                {
                    maxZ = 1;
                }
            }
            var scaleVectorLambda = (Vector3 v) =>
            {
                v.X = (v.X - minX) / (maxX - minX) * Constants.ObjectBasicDim + Constants.XOffset + (float)0.1 * idx * Constants.Offset;
                v.Y = (v.Y - minY) / (maxY - minY) * Constants.ObjectBasicDim + Constants.YOffset;
                v.Z = (v.Z - minZ) / (maxZ - minZ) * Constants.ObjectBasicDim / 2;
                return v;
            };

            List<MyFace> faces = new List<MyFace>();
            var group = data.Groups.First(); // only one object in obj file
            foreach (var f in group.Faces)
            {
                Vector3 v0 = scaleVectorLambda(data.Vertices[f[0].VertexIndex - 1]);
                Vector3 v1 = scaleVectorLambda(data.Vertices[f[1].VertexIndex - 1]);
                Vector3 v2 = scaleVectorLambda(data.Vertices[f[2].VertexIndex - 1]);

                Vector3 n0 = data.Normals[f[0].NormalIndex - 1];
                Vector3 n1 = data.Normals[f[1].NormalIndex - 1];
                Vector3 n2 = data.Normals[f[2].NormalIndex - 1];

                List<Vector3> normals = new List<Vector3>();
                normals.Add(n0);
                normals.Add(n1);
                normals.Add(n2);

                List<Vector3> vertices = new List<Vector3>();
                vertices.Add(v0);
                vertices.Add(v1);
                vertices.Add(v2);

                List<int> ids = new List<int>();
                ids.Add(f[0].VertexIndex);
                ids.Add(f[1].VertexIndex);
                ids.Add(f[2].VertexIndex);

                faces.Add(new MyFace(vertices, normals, ids));
            }
            return faces;
        }
        private void DrawTriangulation()
        {

            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                foreach (var faces in _listOfObjects)
                {
                    var ToPointF = (double x, double y, double z) =>
                    {
                        return new PointF((float)x, (float)y);
                    };
                    foreach (var f in faces)
                    {
                        Pen pen = new Pen(Brushes.Black, 1);

                        g.DrawLine(pen, ToPointF(f.vertices[0].X, f.vertices[0].Y, f.vertices[0].Z), ToPointF(f.vertices[1].X, f.vertices[1].Y, f.vertices[1].Z));
                        g.DrawLine(pen, ToPointF(f.vertices[1].X, f.vertices[1].Y, f.vertices[1].Z), ToPointF(f.vertices[2].X, f.vertices[2].Y, f.vertices[2].Z));
                        g.DrawLine(pen, ToPointF(f.vertices[2].X, f.vertices[2].Y, f.vertices[2].Z), ToPointF(f.vertices[0].X, f.vertices[0].Y, f.vertices[0].Z));
                    }
                }
            }
        }
        private List<LoadResult> LoadObjFile()
        {
            List<LoadResult> loadResults = new List<LoadResult>();
            foreach (var path in _pathsToObjFiles)
            {
                var fileStream = new FileStream(path, FileMode.Open);
                var objLoaderFactory = new ObjLoaderFactory();
                var objLoader = objLoaderFactory.Create();
                var result = objLoader.Load(fileStream);
                loadResults.Add(result);
                fileStream.Close();
            }

            return loadResults;
        }
        private void kdTrackBar_ValueChanged(object sender, EventArgs e)
        {
            this.kdLabel.Text = "kd: " + (this.kdTrackBar.Value / 100.0).ToString();
            PaintScene();
        }
        private void ksTrackBar_ValueChanged(object sender, EventArgs e)
        {
            this.ksLabel.Text = "ks: " + (this.ksTrackBar.Value / 100.0).ToString();
            PaintScene();
        }
        private void mTrackBar_ValueChanged(object sender, EventArgs e)
        {
            this.mLabel.Text = "m: " + this.mTrackBar.Value.ToString();
            PaintScene();
        }
        private void colorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            PaintScene();
        }
        private void normalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            PaintScene();
        }
        private void rotatePoint(List<Vector3> v, List<Vector3> original, Matrix4x4 rotationMat, Matrix4x4 viewMat, Matrix4x4 perspectiveMat, int idx)
        {
            for (int i = 0; i < original.Count; i++)
            {
                System.Numerics.Vector4 p;
                p.X = (float)original[i].X;
                p.Y = (float)original[i].Y;
                p.Z = (float)original[i].Z;
                p.W = 1;

                if (_animateObject[idx] == true)
                {
                    p = Vector4.Transform(p, rotationMat);
                }
                p = Vector4.Transform(p, viewMat);
                p = Vector4.Transform(p, perspectiveMat);
                p.X /= p.W;
                p.Y /= p.W;
                p.Z /= p.W;
                if (p.X < -1) p.X = -1;
                if (p.Y < -1) p.Y = -1;
                if (p.Z < -1) p.Z = -1;

                if (p.X > 1) p.X = 1;
                if (p.Y > 1) p.Y = 1;
                if (p.Z > 1) p.Z = 1;
                p.X = (p.X + 1) / 2 * (_drawArea.Width - 1);
                p.Y = (p.Y + 1) / 2 * (_drawArea.Height - 1);
                p.Z = (p.Z + 1) / 2 * (3000 - 1);

                v[i].X = p.X;
                v[i].Y = p.Y;
                v[i].Z = p.Z;
            }
        }
        private void rotateNormalVector(List<Vector3> v, List<Vector3> original, Matrix4x4 rotationMat, int idx)
        {
            if (_animateObject[idx] == true)
            {
                return;
            }

            for (int i = 0; i < original.Count; i++)
            {
                System.Numerics.Vector3 p;
                p.X = (float)original[i].X;
                p.Y = (float)original[i].Y;
                p.Z = (float)original[i].Z;
                var after = System.Numerics.Vector3.TransformNormal(p, rotationMat);
                v[i].X = after.X;
                v[i].Y = after.Y;
                v[i].Z = after.Z;
            }
        }
        void RotateScene()
        {
            var rotationMat =
                  System.Numerics.Matrix4x4.CreateRotationX((float)(Constants.Angle * Math.PI / 180.0));

            System.Numerics.Vector3 camPosition, camTarget, camUpVec;
            camPosition.X = (float)this.xNumericUpDown.Value;
            camPosition.Y = (float)this.yNumericUpDown.Value;
            camPosition.Z = (float)this.zNumericUpDown.Value;

            camTarget.X = 0;
            camTarget.Y = 0;
            camTarget.Z = 0;

            camUpVec.X = 0;
            camUpVec.Y = 0;
            camUpVec.Z = -1;
            var viewMat = Matrix4x4.CreateLookAt(camPosition, camTarget, camUpVec);
            float fieldOfView, aspecetRatio, nearPlaneDist, farPlaneDist;
            fieldOfView = (float)((double)this.FOVTrackBar.Value * Math.PI / 180.0);
            aspecetRatio = _drawArea.Width / _drawArea.Height;
            nearPlaneDist = 10;
            farPlaneDist = 1000;
            var perspectiveMat = System.Numerics.Matrix4x4.CreatePerspectiveFieldOfView(fieldOfView, aspecetRatio, nearPlaneDist, farPlaneDist);
            for (int idx = 0; idx < _listOfObjects.Count; idx++)
            {
                for (int f = 0; f < _listOfObjects[idx].Count; f++)
                {
                    rotatePoint(_listOfObjects[idx][f].vertices, _listOfObjectsCOPY[idx][f].vertices, rotationMat, viewMat, perspectiveMat, idx);
                    rotateNormalVector(_listOfObjects[idx][f].normals, _listOfObjectsCOPY[idx][f].normals, rotationMat, idx);
                }
                _polygonFillers[idx].Faces = _listOfObjects[idx];
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.animateLightCheckBox.Checked)
            {
                if (_radius < _minSpiralRadious || _radius > _maxSpiralRadius)
                {
                    _radiusIncrement = -_radiusIncrement;
                }

                double x = _radius * Math.Cos(_angle * Math.PI / 180);
                double y = _radius * Math.Sin(_angle * Math.PI / 180);
                _angle += _angleIncrement;
                _radius += _radiusIncrement;
                _lightSource.X = x + _origin.X;
                _lightSource.Y = y + _origin.Y;
            }
            if (this.animateObjectCheckBox.Checked)
            {
                Constants.Angle += 3;
            }
            PaintScene();
        }
        private void zTrackBar_ValueChanged(object sender, EventArgs e)
        {
            var z = (double)zTrackBar.Value;
            _lightSource.Z = z;
            this.zLabel.Text = "z: " + this.zTrackBar.Value.ToString();

            if (animateObjectCheckBox.Checked == false)
            {
                PaintScene();
            }
        }
        private void animationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //if (animationCheckBox.Checked)
            //{
            //    animationTimer.Start();
            //}
            //else
            //{
            //    animationTimer.Stop();
            //}
        }
        private void paintTriangulationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PaintScene();
        }
        private void bitmapColorRadioButton_Click(object sender, EventArgs e)
        {
            if (this.bitmapColorRadioButton.Checked == false)
            {
                return;
            }
            var status = this.openFileDialog1.ShowDialog();
            if (status != DialogResult.OK)
            {
                return;
            }
            _pathToColorMap = this.openFileDialog1.FileName;
            var texture = GetBitampFromFile(_pathToColorMap);
            _colorMap = Utils.ConvertBitmapToArray(texture);
            foreach (var polygonFiller in _polygonFillers)
            {
                polygonFiller.ColorMap = _colorMap;
            }
            PaintScene();
        }
        private void constColorRadioButton_Click(object sender, EventArgs e)
        {
            if (this.constColorRadioButton.Checked == false)
            {
                return;
            }
            if (this.surfaceColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Color c = this.surfaceColorDialog.Color;
            Bitmap bmp = new Bitmap(_drawArea.Width, _drawArea.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(c);
                g.Dispose();
            }
            _colorMap = Utils.ConvertBitmapToArray(bmp);
            foreach (var polygonFiller in _polygonFillers)
            {
                polygonFiller.ColorMap = _colorMap;
            }
            PaintScene();

        }
        private void loadObjFileButton_Click(object sender, EventArgs e)
        {
            var status = this.openFileDialog1.ShowDialog();
            if (status != DialogResult.OK)
            {
                return;
            }
            //_pathToObjFile = this.openFileDialog1.FileName;
            var pathToObj = this.openFileDialog1.FileName;
            _pathsToObjFiles.Add(pathToObj);
            GetAndSetObj();
            PaintScene();
        }
        private void changeLightColorButton_Click(object sender, EventArgs e)
        {
            var status = this.lightColorDialog.ShowDialog();
            if (status != DialogResult.OK)
            {
                return;
            }
            _lighColor = this.lightColorDialog.Color;
            foreach (var polygonFiller in _polygonFillers)
            {
                polygonFiller.LighColor = _lighColor;
            }
            PaintScene();
        }
        private void kaTrackBar_ValueChanged(object sender, EventArgs e)
        {
            this.kaLabel.Text = "ka: " + this.kaTrackBar.Value.ToString();
            //InitShadow();

            var colorMapBitmapShadow = new Bitmap(_drawArea.Width, _drawArea.Height);
            using (Graphics g = Graphics.FromImage(colorMapBitmapShadow))
            {
                var ambient = (int)((double)kaTrackBar.Value / 100.0 * 255.0);
                g.Clear(Color.FromArgb(255, ambient, ambient, ambient));
            }
            PaintScene();
        }
        private void paintObjectsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PaintScene();
        }
        private void clearSceneButton_Click(object sender, EventArgs e)
        {
            _pathsToObjFiles.Clear();
            _polygonFillers.Clear();
            _listOfObjects.Clear();
            _listOfObjectsCOPY.Clear();
        }
    }
}