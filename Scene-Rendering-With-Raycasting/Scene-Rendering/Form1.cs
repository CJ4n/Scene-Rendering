using ObjLoader.Loader.Loaders;
using System.Numerics;
using Vector3 = ObjLoader.Loader.Data.Vector3;

namespace SceneRendering
{
    public partial class Form1 : Form
    {
        //TODO:
        // 2. doda� nowe kamery
        // 3. pozwoli� na r�zne skalowanie r�znych obiekt�w
        // 4. r�zne kolory/tekst�ry r�znych obiekt�w
        // 5. zmienic alg rysowania pod rysowanie tylko trojk�t�w
        // 8. ma byc sterowanie obiektem
        // 9. z coord po transoformacjach jest moco malo zmienny, maly przedzial przyjmowanych wartosci
        // 12. dodac 3rd person camera
        private class SceneObject
        {
            public bool Animatable = false;
            public bool Animate { get; set; }
            public int Scale { get; set; }
            public List<MyFace> FacesCamera { get; set; }
            public List<MyFace> FacesWorld { get; set; }
            public PolygonFiller PolygonFiller { get; set; }
        }

        // 0 - stationary cam, 1 - stationary-tracking object cam, 2 - object following cam
        private class Camera
        {
            public string Name { get; set; }
            static public int CurrentCameraIndex = 0;
            public int TargetObjectIdx = -1;
            public System.Numerics.Vector3 CamPosition { get; set; }
            public System.Numerics.Vector3 CamTarget { get; set; }
        }

        // paths to files
        private List<string> _pathsToObjFiles = new List<string>();
        //private string _pathToColorMap = "..\\..\\..\\..\\..\\colorMap1.jpg";
        private string _pathToObjFile = "..\\..\\..\\..\\..\\monkey.obj";
        private string _pathToObjFileSecond = "..\\..\\..\\..\\..\\fulltorust.obj";
        private string _pathToPlaneObj = "..\\..\\..\\..\\..\\coords.obj";

        private Vector3 _lightSource = new Vector3(1000, 300, 2500);
        private Vector3 _lightSourceCamera = new Vector3(1000, 300, 2500);
        private PointF _origin = new PointF(Constants.ObjectBasicDim / 2, Constants.ObjectBasicDim / 2);
        private int _radius = 1000;
        private int _angle = 0;
        private int _radiusIncrement = -10;
        private int _angleIncrement = 3;
        private int _maxSpiralRadius = 2000;
        private int _minSpiralRadious = 40;

        private Bitmap _drawArea;
        //private MyColor[,] _colorMap;
        private Color _lighColor = Color.White;
        private MyColor _objectColor;

        private double[,] _zBuffer;

        private List<SceneObject> _objects = new List<SceneObject>();
        private List<Camera> _cameras = new List<Camera>();

        public Form1()
        {
            InitializeComponent();

            this.zLabel.Text = "z: " + this.zTrackBar.Value.ToString();
            this.mLabel.Text = "m: " + this.mTrackBar.Value.ToString();
            this.ksLabel.Text = "ks: " + (this.ksTrackBar.Value / 100.0).ToString();
            this.kdLabel.Text = "kd: " + (this.kdTrackBar.Value / 100.0).ToString();

            _drawArea = new Bitmap(Canvas.Width * 1, Canvas.Height * 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Canvas.Image = _drawArea;

            _zBuffer = new double[Canvas.Width, Canvas.Height];

            _pathsToObjFiles.Add(_pathToObjFile);
            SceneObject obj1 = new SceneObject();
            obj1.Animate = true;
            obj1.Animatable = true;
            obj1.Scale = Constants.ObjectBasicDim;
            _objects.Add(obj1);

            _pathsToObjFiles.Add(_pathToObjFileSecond);
            SceneObject obj2 = new SceneObject();
            obj2.Animate = false;
            obj2.Scale = Constants.ObjectBasicDim;
            _objects.Add(obj2);

            _pathsToObjFiles.Add(_pathToPlaneObj);
            SceneObject obj3 = new SceneObject();
            obj3.Animate = false;
            obj3.Scale = Constants.ObjectBasicDim * 5;
            _objects.Add(obj3);

            var colorTmp = Color.OrangeRed;
            _objectColor = new MyColor(colorTmp.R / 255f, colorTmp.G / 255f, colorTmp.B / 255f);

            //var colorMapBitmap = GetBitampFromFile(_pathToColorMap);
            //_colorMap = Utils.ConvertBitmapToArray(colorMapBitmap);
            GetAndSetObj();

            Camera cam1 = new Camera();
            cam1.CamTarget = GetWorldObjectsCenter(Camera.CurrentCameraIndex);
            cam1.CamPosition = new System.Numerics.Vector3(400, 400, 1500);
            cam1.Name = "StationaryTrackingCamera";
            cam1.TargetObjectIdx = 0;
            _cameras.Add(cam1);

            Camera cam2 = new Camera();
            cam2.CamTarget = new System.Numerics.Vector3(0, 0, 0);
            cam2.CamPosition = new System.Numerics.Vector3(1000, 1000, 1500);
            cam2.Name = "StationaryCamera";
            cam2.TargetObjectIdx = -1;
            _cameras.Add(cam2);

            Camera cam3 = new Camera();
            cam3.CamTarget = new System.Numerics.Vector3(0, 0, 0);
            cam3.CamPosition = new System.Numerics.Vector3(1000, 1000, 1500);
            cam3.Name = "3rdPersonCamera";
            cam3.TargetObjectIdx = -1;
            _cameras.Add(cam3);

            this.comboBox1.DataSource = _cameras.Select(x => x.Name).ToList();

            var tmp = _cameras[Camera.CurrentCameraIndex].CamPosition;
            this.xNumericUpDown.Value = (decimal)tmp.X;
            this.yNumericUpDown.Value = (decimal)tmp.Y;
            this.zNumericUpDown.Value = (decimal)tmp.Z;
            this.zNumericUpDown.Update();

            PaintScene();
        }

        System.Numerics.Vector3 GetWorldObjectsCenter(int objectIdx)
        {
            double x = 0, y = 0, z = 0;
            int count = 0;
            foreach (var face in _objects[objectIdx].FacesWorld)
            {
                foreach (var vertex in face.vertices)
                {
                    x += vertex.X;
                    y += vertex.Y;
                    z += vertex.Z;
                }
                count += 3;
            }

            return new System.Numerics.Vector3((float)x / count, (float)y / count, (float)z / count);
        }
        void InitZBuffer()
        {
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                g.Clear(Color.LightBlue);
            }
            for (int x = 0; x < Canvas.Width; x++)
                for (int y = 0; y < Canvas.Height; y++)
                {
                    _zBuffer[x, y] = double.MaxValue;
                }
        }
        private void PaintScene()
        {
            InitZBuffer();
            RotateScene();
            float ks = (float)(this.ksTrackBar.Value / 100.0);
            float kd = (float)(this.kdTrackBar.Value / 100.0);
            float ka = (float)(this.kaTrackBar.Value / 100.0);
            int m = this.mTrackBar.Value;
            //bool interpolateNormalVector = this.interpolateNormalRadioButton.Checked;
            Constants.SHADER shader = this.interpolateConstradioButton.Checked ? Constants.SHADER.CONST : this.interpolateNormalRadioButton.Checked ? Constants.SHADER.PHONG : Constants.SHADER.GOURAUD;
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                g.Clear(Color.FromArgb(255,(int)(255.0f*Constants.LightIntensity), (int)(255.0f * Constants.LightIntensity), (int)(255.0f * Constants.LightIntensity)));
                g.FillEllipse(Brushes.Yellow, (int)_lightSourceCamera.X, (int)_lightSourceCamera.Y, 50, 50);
            }
            if (this.paintObjectsCheckBox.Checked)
            {
                foreach (var obj in _objects)
                {

                    obj.PolygonFiller.FillEachFace(ka, kd, ks, m, shader, _lightSource, _zBuffer);
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
            var result = LoadObjFile();
            for (int idx = 0; idx < result.Count; idx++)
            {
                var faces = GetAllFaces(result[idx], idx);
                var faces2 = GetAllFaces(result[idx], idx);
                _objects[idx].FacesCamera = faces;
                _objects[idx].FacesWorld = faces2;

                var polygonFiller = new PolygonFiller(_drawArea, faces, faces2, _objectColor, _lighColor/*, _normalMap*/);
                _objects[idx].PolygonFiller = polygonFiller;
            }
        }
        //private Bitmap GetBitampFromFile(string path)
        //{
        //    Bitmap bitmap = new Bitmap(path);
        //    Rectangle cloneRect = new Rectangle(0, 0, Math.Min(_drawArea.Width, bitmap.Width), Math.Min(_drawArea.Height, bitmap.Height));
        //    Bitmap bmp = bitmap.Clone(cloneRect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        //    bitmap.Dispose();
        //    return bmp;
        //}
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
                v.X = (v.X - minX) / (maxX - minX) * _objects[idx].Scale + Constants.XOffset + (float)0.1 * idx * Constants.Offset;
                v.Y = (v.Y - minY) / (maxY - minY) * _objects[idx].Scale + Constants.YOffset;
                v.Z = (v.Z - minZ) / (maxZ - minZ) * _objects[idx].Scale / 2;
                return v;
            };

            List<MyFace> faces = new List<MyFace>();
            var group = data.Groups.First(); // only one object in obj file
            foreach (var f in group.Faces)
            {
                List<Vector3> normals = new List<Vector3>();
                List<Vector3> vertices = new List<Vector3>();
                List<int> ids = new List<int>();

                for (int i = 0; i < f.Count; i++)
                {
                    Vector3 v = scaleVectorLambda(data.Vertices[f[i].VertexIndex - 1]);
                    Vector3 n = data.Normals[f[i].NormalIndex - 1];
                    normals.Add(n);
                    vertices.Add(v);
                    ids.Add(f[i].VertexIndex);

                }
                faces.Add(new MyFace(vertices, normals, ids));
            }
            return faces;
        }
        private void DrawTriangulation()
        {
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                for (int idx = 0; idx < _objects.Count; idx++)
                {
                    var ToPointF = (double x, double y, double z) =>
                    {
                        return new PointF((float)x, (float)y);
                    };
                    foreach (var f in _objects[idx].FacesCamera)
                    {
                        Pen pen = new Pen(Brushes.Black, 1);
                        for (int i = 0; i < f.vertices.Count - 1; i++)
                        {
                            g.DrawLine(pen, ToPointF(f.vertices[i].X, f.vertices[i].Y, f.vertices[i].Z), ToPointF(f.vertices[i + 1].X, f.vertices[i + 1].Y, f.vertices[i + 1].Z));

                        }
                        g.DrawLine(pen, ToPointF(f.vertices[f.vertices.Count - 1].X, f.vertices[f.vertices.Count - 1].Y, f.vertices[f.vertices.Count - 1].Z), ToPointF(f.vertices[0].X, f.vertices[0].Y, f.vertices[0].Z));
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
        private void rotateObjectsPoint(List<Vector3> cameraPoint, List<Vector3> worldPoint, Matrix4x4 rotationMat, Matrix4x4 viewMat, Matrix4x4 perspectiveMat, int idx)
        {
            for (int i = 0; i < worldPoint.Count; i++)
            {
                System.Numerics.Vector4 p;
                p.X = (float)worldPoint[i].X;
                p.Y = (float)worldPoint[i].Y;
                p.Z = (float)worldPoint[i].Z;
                p.W = 1;

                if (_objects[idx].Animate == true)
                {
                    p = Vector4.Transform(p, rotationMat);
                    worldPoint[i].X = p.X / p.W;
                    worldPoint[i].Y = p.Y / p.W;
                    worldPoint[i].Z = p.Z / p.W;
                }
                p = Vector4.Transform(p, viewMat);
                p = Vector4.Transform(p, perspectiveMat);
                if (p.W == 0) { throw new Exception("unexpecatde happend"); }
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
                p.Z = (p.Z + 1) / 2;

                cameraPoint[i].X = p.X;
                cameraPoint[i].Y = p.Y;
                cameraPoint[i].Z = p.Z;
            }

        }
        // cameraNormal==worldNormal
        private void rotateNormalVector(List<Vector3> cameraNormal, List<Vector3> worldNormal, Matrix4x4 rotationMat, int idx)
        {
            if (_objects[idx].Animate == false)
            {
                return;
            }

            for (int i = 0; i < worldNormal.Count; i++)
            {
                System.Numerics.Vector3 p;
                p.X = (float)worldNormal[i].X;
                p.Y = (float)worldNormal[i].Y;
                p.Z = (float)worldNormal[i].Z;
                var after = System.Numerics.Vector3.TransformNormal(p, rotationMat);
                cameraNormal[i].X = after.X;
                cameraNormal[i].Y = after.Y;
                cameraNormal[i].Z = after.Z;
                worldNormal[i].X = after.X;
                worldNormal[i].Y = after.Y;
                worldNormal[i].Z = after.Z;
            }
        }
        private void rotateSun(Vector3 cameraPoint, Vector3 worldPoint, Matrix4x4 rotationMat, Matrix4x4 viewMat, Matrix4x4 perspectiveMat)
        {
            System.Numerics.Vector4 p;
            p.X = (float)worldPoint.X;
            p.Y = (float)worldPoint.Y;
            p.Z = (float)worldPoint.Z;
            p.W = 1;

            //p = Vector4.Transform(p, rotationMat);
            //worldPoint.X = p.X / p.W;
            //worldPoint.Y = p.Y / p.W;
            //worldPoint.Z = p.Z / p.W;
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
            p.Z = (p.Z + 1) / 2;

            cameraPoint.X = p.X;
            cameraPoint.Y = p.Y;
            cameraPoint.Z = p.Z;

        }
        private void UpdataCameraTarget(int idx)
        {
            _cameras[idx].CamTarget = GetWorldObjectsCenter(_cameras[idx].TargetObjectIdx);
        }
        void RotateScene()
        {
            var rotationMat =
                  System.Numerics.Matrix4x4.CreateRotationX((float)(Constants.Angle * Math.PI / 180.0));

            System.Numerics.Vector3 camUpVec;

            camUpVec.X = 0;
            camUpVec.Y = 0;
            camUpVec.Z = 1; // something is working when == 1
            for (int idx = 0; idx < _cameras.Count; idx++)
            {
                if (_cameras[idx].TargetObjectIdx != -1)
                {
                    if (_cameras[idx].TargetObjectIdx < _objects.Count)
                        UpdataCameraTarget(idx);
                }
            }

            var viewMat = Matrix4x4.CreateLookAt(_cameras[Camera.CurrentCameraIndex].CamPosition, _cameras[Camera.CurrentCameraIndex].CamTarget, camUpVec);
            float fieldOfView, aspecetRatio, nearPlaneDist, farPlaneDist;
            fieldOfView = (float)((double)this.FOVTrackBar.Value * Math.PI / 180.0);
            aspecetRatio = _drawArea.Width / _drawArea.Height;
            nearPlaneDist = 2500;
            farPlaneDist = 20000;
            var perspectiveMat = System.Numerics.Matrix4x4.CreatePerspectiveFieldOfView(fieldOfView, aspecetRatio, nearPlaneDist, farPlaneDist);
            for (int idx = 0; idx < _objects.Count; idx++)
            {
                for (int f = 0; f < _objects[idx].FacesWorld.Count; f++)
                {
                    rotateObjectsPoint(_objects[idx].FacesCamera[f].vertices, _objects[idx].FacesWorld[f].vertices, rotationMat, viewMat, perspectiveMat, idx);
                    rotateNormalVector(_objects[idx].FacesCamera[f].normals, _objects[idx].FacesWorld[f].normals, rotationMat, idx);
                }

                Constants.MaxZ = Math.Max(Constants.MaxZ, _objects[idx].FacesCamera.Max(x => (x.vertices.Max(xx => (xx.Z)))));
                Constants.MinZ = Math.Min(Constants.MinZ, _objects[idx].FacesCamera.Min(x => (x.vertices.Min(xx => (xx.Z)))));
                _objects[idx].PolygonFiller.Faces = _objects[idx].FacesCamera;
            }
            //rotateSun(_lightSourceCamera, _lightSource, rotationMat, viewMat, perspectiveMat);
        }
        //purely debug featuer, not intented for any particular usage
        private void CameraPositionChanged(object sender, EventArgs e)
        {
            _cameras[Camera.CurrentCameraIndex].CamPosition =
                new System.Numerics.Vector3((float)this.xNumericUpDown.Value, (float)this.yNumericUpDown.Value, (float)this.zNumericUpDown.Value);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.animateLightCheckBox.Checked)
            {
                //if (_radius < _minSpiralRadious || _radius > _maxSpiralRadius)
                //{
                //    _radiusIncrement = -_radiusIncrement;
                //}

                double x = _radius * Math.Cos(_angle * Math.PI / 180);
                double y = _radius * Math.Sin(_angle * Math.PI / 180);
                _angle += _angleIncrement;
                //_radius += _radiusIncrement;
                _lightSource.X = x + _origin.X;
                _lightSource.Y = y + _origin.Y;
                _lightSourceCamera.X = x + _origin.X;
                _lightSourceCamera.Y = y + _origin.Y;
            }
            if (this.animateObjectCheckBox.Checked)
            {
                //Constants.Angle += 3;
            }

            if (Constants.LightIntensity + Constants.LightIntensityChangeRate > 1 || Constants.LightIntensity + Constants.LightIntensityChangeRate < 0)
            {
                Constants.LightIntensityChangeRate = -Constants.LightIntensityChangeRate;
            }
            Constants.LightIntensity += Constants.LightIntensityChangeRate;
            PaintScene();
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
            foreach (var obj in _objects)
            {
                if(obj.Animatable)
                obj.Animate = !obj.Animate;
            }
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
        //private void bitmapColorRadioButton_Click(object sender, EventArgs e)
        //{
        //    if (this.bitmapColorRadioButton.Checked == false)
        //    {
        //        return;
        //    }
        //    var status = this.openFileDialog1.ShowDialog();
        //    if (status != DialogResult.OK)
        //    {
        //        return;
        //    }
        //    _pathToColorMap = this.openFileDialog1.FileName;
        //    var texture = GetBitampFromFile(_pathToColorMap);
        //    _colorMap = Utils.ConvertBitmapToArray(texture);
        //    for (int idx = 0; idx < _objects.Count; idx++)
        //    {
        //        _objects[idx].PolygonFiller.ColorMap = _colorMap;
        //    }
        //    PaintScene();
        //}
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
            MyColor myCol = new MyColor(c.R / 255f, c.G / 255f, c.B / 255f);
            //Bitmap bmp = new Bitmap(_drawArea.Width, _drawArea.Height);
            //using (Graphics g = Graphics.FromImage(bmp))
            //{
            //    g.Clear(c);
            //    g.Dispose();
            //}
            //_colorMap = Utils.ConvertBitmapToArray(bmp);
            foreach (var obj in _objects)
            {
                obj.PolygonFiller.ObjectColor = myCol;
            }
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

            foreach (var obj in _objects)
            {
                obj.PolygonFiller.LighColor = _lighColor;
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
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Camera.CurrentCameraIndex = this.comboBox1.SelectedIndex;
            var tmp = _cameras[Camera.CurrentCameraIndex].CamPosition;
            this.xNumericUpDown.Value = (decimal)tmp.X;
            this.yNumericUpDown.Value = (decimal)tmp.Y;
            this.zNumericUpDown.Value = (decimal)tmp.Z;
        }
    }
}