using ObjLoader.Loader.Loaders;
using System.Numerics;
using Vector3 = ObjLoader.Loader.Data.Vector3;

namespace SceneRendering
{
    public partial class Form1 : Form
    {


        private string _pathToColorMap = "..\\..\\..\\..\\..\\colorMap1.jpg";
        private string _pathToObjFile = "..\\..\\..\\..\\..\\fulltorust.obj";
        private string _pathToObjFileSecond = "..\\..\\..\\..\\..\\fulltorust.obj";
        private string _pathToNormalMap = "..\\..\\..\\..\\..\\brickwall_normal.jpg";

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


        private PolygonFiller cloudGenerator;
        private MyColor[,] cloudeColorMap;
        private List<Point> cloude;
        private int zCloude = 250;

        private PolygonFiller shadowGenerator;
        private MyColor[,] shadowColorMap;

        public Form1()
        {
            InitializeComponent();
            this.zLabel.Text = "z: " + this.zTrackBar.Value.ToString();
            this.mLabel.Text = "m: " + this.mTrackBar.Value.ToString();
            this.ksLabel.Text = "ks: " + (this.ksTrackBar.Value / 100.0).ToString();
            this.kdLabel.Text = "kd: " + (this.kdTrackBar.Value / 100.0).ToString();
            _drawArea = new Bitmap(Canvas.Width * 1, Canvas.Height * 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Canvas.Image = _drawArea;
            ZBuffer = new double[Canvas.Width, Canvas.Height];

            _pathsToObjFiles.Add(_pathToObjFile);
            _pathsToObjFiles.Add(_pathToObjFileSecond);

            var colorMapBitmap = GetBitampFromFile(_pathToColorMap);
            _colorMap = Utils.ConvertBitmapToArray(colorMapBitmap);
            GetAndSetObj();
            InitCloude();
            InitShadow();
            PaintScene();
        }



        double[,] ZBuffer;
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
            if (this.paintCloudeCheckBox.Checked)
            {
                PaintCloude();
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

        private void ModifyNormalVectors()
        {
            var normalMapBitmap = GetBitampFromFile(_pathToNormalMap);

            _normalMap = new Vector3[normalMapBitmap.Width, normalMapBitmap.Height];
            for (int col = 0; col < normalMapBitmap.Width; col++)
            {
                for (int row = 0; row < normalMapBitmap.Height; row++)
                {
                    _normalMap[col, row] = Utils.RgbToNormalVectorsArray(normalMapBitmap.GetPixel(col, row));
                }
            }
            normalMapBitmap.Dispose();
        }

        private List<MyFace> GetAllFaces(LoadResult data, int idx)
        {
            float maxX = data.Vertices.Max(x => x.X);
            float maxY = data.Vertices.Max(x => x.Y);
            float maxZ = data.Vertices.Max(x => x.Z);
            float minX = data.Vertices.Min(x => x.X);
            float minY = data.Vertices.Min(x => x.Y);
            float minZ = data.Vertices.Min(x => x.Z);

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
            camUpVec.Z = 1;

            var viewMat = Matrix4x4.CreateLookAt(camPosition, camTarget, camUpVec);
            float fieldOfView, aspecetRatio, nearPlaneDist, farPlaneDist;
            fieldOfView = (float)((double)this.FOVTrackBar.Value * Math.PI / 180.0);
            aspecetRatio = _drawArea.Width / _drawArea.Height;
            nearPlaneDist = 10;
            farPlaneDist = 1000;
            var perspectiveMat = System.Numerics.Matrix4x4.CreatePerspectiveFieldOfView(fieldOfView, aspecetRatio, nearPlaneDist, farPlaneDist);
            int idx = 0;
            for (int faces = 0; faces < _listOfObjects.Count; faces++)
            {
                var rotatePoint = (Vector3 v, Vector3 original) =>
                {

                    System.Numerics.Vector4 p;
                    p.X = (float)original.X;
                    p.Y = (float)original.Y;
                    p.Z = (float)original.Z;
                    p.W = 1;
                    p = Vector4.Transform(p, rotationMat);
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


                    v.X = p.X;
                    v.Y = p.Y;
                    v.Z = p.Z;

                };

                var rotateNormalVector = (Vector3 v, Vector3 original) =>
                {
                    System.Numerics.Vector3 p;
                    p.X = (float)original.X;
                    p.Y = (float)original.Y;
                    p.Z = (float)original.Z;
                    var after = System.Numerics.Vector3.TransformNormal(p, rotationMat);
                    v.X = after.X;
                    v.Y = after.Y;
                    v.Z = after.Z;
                };
                for (int f = 0; f < _listOfObjects[faces].Count; f++)
                {

                    rotatePoint(_listOfObjects[faces][f].vertices[0], _listOfObjectsCOPY[faces][f].vertices[0]);
                    rotatePoint(_listOfObjects[faces][f].vertices[1], _listOfObjectsCOPY[faces][f].vertices[1]);
                    rotatePoint(_listOfObjects[faces][f].vertices[2], _listOfObjectsCOPY[faces][f].vertices[2]);
                    rotateNormalVector(_listOfObjects[faces][f].normals[0], _listOfObjectsCOPY[faces][f].normals[0]);
                    rotateNormalVector(_listOfObjects[faces][f].normals[1], _listOfObjectsCOPY[faces][f].normals[1]);
                    rotateNormalVector(_listOfObjects[faces][f].normals[2], _listOfObjectsCOPY[faces][f].normals[2]);
                }
                _polygonFillers[idx].Faces = _listOfObjects[faces];
                idx++;
            }
        }

        private int cloudIncremetn = 5;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_radius < _minSpiralRadious || _radius > _maxSpiralRadius)
            {
                _radiusIncrement = -_radiusIncrement;
            }

            double x = _radius * Math.Cos(_angle * Math.PI / 180);
            double y = _radius * Math.Sin(_angle * Math.PI / 180);
            _angle += _angleIncrement;
            _radius += _radiusIncrement;
            Constants.Angle += 5;
            _lightSource.X = x + _origin.X;
            _lightSource.Y = y + _origin.Y;

            for (int i = 0; i < cloude.Count(); i++)
            {
                var p = new Point(cloude[i].X + cloudIncremetn, cloude[i].Y);
                cloude[i] = p;
            }
            if (cloude[0].X < 30 || cloude[0].X > 1000)
            {
                cloudIncremetn = -cloudIncremetn;
            }
            PaintScene();
        }
        private void zTrackBar_ValueChanged(object sender, EventArgs e)
        {
            var z = (double)zTrackBar.Value;
            _lightSource.Z = z;
            this.zLabel.Text = "z: " + this.zTrackBar.Value.ToString();

            if (animationCheckBox.Checked == false)
            {
                PaintScene();
            }
        }
        private void animationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (animationCheckBox.Checked)
            {
                animationTimer.Start();
            }
            else
            {
                animationTimer.Stop();
            }
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
        private void loadNormalMapButton_Click(object sender, EventArgs e)
        {
            var status = this.openFileDialog1.ShowDialog();
            if (status != DialogResult.OK)
            {
                _normalMap = null;
                foreach (var polygonFiller in _polygonFillers)
                {
                    polygonFiller.NormalMap = _normalMap;
                }
            }
            else
            {
                _pathToNormalMap = this.openFileDialog1.FileName;
                this.modifyNormalMapcheckBox.Checked = true;

            }
            modifyNormalMapcheckBox_CheckedChanged(null, null);
            PaintScene();
        }
        private void modifyNormalMapcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.modifyNormalMapcheckBox.Checked)
            {
                ModifyNormalVectors();
            }
            else
            {
                _normalMap = null;
            }
            foreach (var polygonFiller in _polygonFillers)
            {
                polygonFiller.NormalMap = _normalMap;
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
            shadowColorMap = Utils.ConvertBitmapToArray(colorMapBitmapShadow);
            shadowGenerator.ColorMap = shadowColorMap;
            PaintScene();
        }
        private void InitCloude()
        {
            var colorMapBitmapCloude = new Bitmap(_drawArea.Width, _drawArea.Height);
            using (Graphics g = Graphics.FromImage(colorMapBitmapCloude))
            {
                g.Clear(Color.Blue);
            }
            cloudeColorMap = Utils.ConvertBitmapToArray(colorMapBitmapCloude);
            cloudGenerator = new PolygonFiller(_drawArea, null, cloudeColorMap, _lighColor, null);
            cloude = new List<Point>();
            cloude.Add(new Point(250, 200));
            cloude.Add(new Point(100, 500));
            cloude.Add(new Point(300, 500));
            cloude.Add(new Point(400, 200));
            cloude.Add(new Point(200, 100));
        }

        private void InitShadow()
        {
            var colorMapBitmapShadow = new Bitmap(_drawArea.Width, _drawArea.Height);
            using (Graphics g = Graphics.FromImage(colorMapBitmapShadow))
            {
                var ambient = (int)((double)kaTrackBar.Value / 100.0 * 255.0);
                g.Clear(Color.FromArgb(255, ambient, ambient, ambient));
            }
            shadowColorMap = Utils.ConvertBitmapToArray(colorMapBitmapShadow);
            shadowGenerator = new PolygonFiller(_drawArea, null, shadowColorMap, _lighColor, null);
        }
        private void PaintCloude()
        {
            float ks = (float)(this.ksTrackBar.Value / 100.0);
            float kd = (float)(this.kdTrackBar.Value / 100.0);
            float ka = (float)(this.kaTrackBar.Value / 100.0);
            int m = this.mTrackBar.Value;
            bool interpolateNormalVector = this.normalRadioButton.Checked;

            cloudGenerator.FillEachFace(ka, kd, ks, m, interpolateNormalVector, _lightSource, cloude);
            PaintShadow();
        }
        private void PaintShadow()
        {
            if (zCloude > _lightSource.Z)
            {
                return;
            }
            float ks = (float)(this.ksTrackBar.Value / 100.0);
            float kd = (float)(this.kdTrackBar.Value / 100.0);
            float ka = (float)(this.kaTrackBar.Value / 100.0);
            int m = this.mTrackBar.Value;
            bool interpolateNormalVector = this.normalRadioButton.Checked;

            List<Point> shadow = new List<Point>();
            foreach (var c in cloude)
            {
                double hs = _lightSource.Z;
                double hc = zCloude;
                int x = (int)(_lightSource.X + hs / hc * (-_lightSource.X + c.X));
                int y = (int)(_lightSource.Y + hs / hc * (-_lightSource.Y + c.Y));

                shadow.Add(new Point(x, y));
            }
            shadowGenerator.FillEachFace(ka, kd, ks, m, interpolateNormalVector, _lightSource, shadow);
        }

        private void paintObjectsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PaintScene();
        }

        private void paintCloudeCheckBox_CheckedChanged(object sender, EventArgs e)
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