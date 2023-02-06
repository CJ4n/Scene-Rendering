using ObjLoader.Loader.Loaders;
using System.Numerics;
using Vector3 = ObjLoader.Loader.Data.Vector3;

namespace SceneRendering
{
    public partial class Form1 : Form
    {
        // paths to files
        private List<string> _pathsToObjFiles = new List<string>();
        private string _pathToMonkey = "..\\..\\..\\..\\..\\monkey.obj";
        private string _pathToTorus = "..\\..\\..\\..\\..\\fulltorust.obj";
        private string _pathToCoords = "..\\..\\..\\..\\..\\coords.obj";

        private List<Light> _lightSource = new List<Light>();
        private PointF _origin = new PointF(Constants.ObjectBasicDim / 2, Constants.ObjectBasicDim / 2);
        private int _radius = 1000;
        private int _angle = 0;
        private int _angleIncrement = 3;

        private Bitmap _drawArea;
        private Color _lighColor = Color.White;
        private MyColor _objectColor;

        private double[,] _zBuffer;

        private List<SceneObject> _objects = new List<SceneObject>();
        private List<Camera> _cameras = new List<Camera>();

        System.Numerics.Vector3 translation = new System.Numerics.Vector3(0, 0, 0);

        private int stationaryCameraIdx = 1;
        private int stationaryTrackingCameraIdx = 0;
        private int thirdPersonCameraIdx = 2;
        private int CameraTargetObjectIdx = 0;

        private bool oscilate = true;
        System.Numerics.Vector3 oscilation = new System.Numerics.Vector3(0, 0, 20);

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

            _pathsToObjFiles.Add(_pathToTorus);
            SceneObject obj2 = new SceneObject();
            obj2.Name = "torus";
            obj2.Animate = true;
            obj2.Animatable = true;
            obj2.Scale = Constants.ObjectBasicDim;
            obj2.Offset = new System.Numerics.Vector3(400, 400, 0);

            _pathsToObjFiles.Add(_pathToMonkey);
            SceneObject obj1 = new SceneObject();
            obj1.Name = "monkey";
            obj1.Animate = false;
            obj1.Animatable = false;
            obj1.Scale = Constants.ObjectBasicDim;
            obj1.Offset = new System.Numerics.Vector3(0, 0, 0);

            _pathsToObjFiles.Add(_pathToTorus);
            SceneObject obj4 = new SceneObject();
            obj4.Name = "torusv2";
            obj4.Animate = false;
            obj4.Scale = Constants.ObjectBasicDim;
            obj4.Offset = new System.Numerics.Vector3(-400, -400, 0);

            _pathsToObjFiles.Add(_pathToTorus);
            SceneObject obj5 = new SceneObject();
            obj5.Name = "torusv3";
            obj5.Animate = false;
            obj5.Scale = Constants.ObjectBasicDim;
            obj5.Offset = new System.Numerics.Vector3(400, -400, 0);

            _objects.Add(obj2);
            _objects.Add(obj1);
            _objects.Add(obj4);
            _objects.Add(obj5);

            //_objects.Add(obj3);
            //_pathsToObjFiles.Add(_pathToCoords);
            //SceneObject obj3 = new SceneObject();
            //obj3.Name = "coords";
            //obj3.Animate = false;
            //obj3.Scale = Constants.ObjectBasicDim * 5;
            //obj3.Offset = new System.Numerics.Vector3(0, 0, 0);


            var colorTmp = Color.OrangeRed;
            _objectColor = new MyColor(colorTmp.R / 255f, colorTmp.G / 255f, colorTmp.B / 255f);

            GetAndSetObj();

            Camera cam1 = new Camera();
            cam1.CamTarget = GetWorldObjectsCenter(CameraTargetObjectIdx);
            cam1.CamPosition = new System.Numerics.Vector3(400, 400, 1500);
            cam1.Name = "StationaryTrackingCamera";
            cam1.TargetObjectIdx = CameraTargetObjectIdx;
            _cameras.Add(cam1);

            Camera cam2 = new Camera();
            cam2.CamTarget = new System.Numerics.Vector3(0, 0, 0);
            cam2.CamPosition = new System.Numerics.Vector3(1000, 1000, 1500);
            cam2.Name = "StationaryCamera";
            cam2.TargetObjectIdx = -1;
            _cameras.Add(cam2);

            Camera cam3 = new Camera();
            var position = GetThirdPersonCameraPosition();
            var target = GetThirdPersonCameraTarget();
            cam3.CamTarget = target;
            cam3.CamPosition = position;
            cam3.Name = "3rdPersonCamera";
            cam3.TargetObjectIdx = -1;
            _cameras.Add(cam3);

            this.comboBox1.DataSource = _cameras.Select(x => x.Name).ToList();

            var tmp = _cameras[Camera.CurrentCameraIndex].CamPosition;
            this.xNumericUpDown.Value = (decimal)tmp.X;
            this.yNumericUpDown.Value = (decimal)tmp.Y;
            this.zNumericUpDown.Value = (decimal)tmp.Z;
            this.zNumericUpDown.Update();
            //Constants.camPositoin = new Vector3(_cameras[Camera.CurrentCameraIndex].CamPosition.X,
            //    _cameras[Camera.CurrentCameraIndex].CamPosition.Y, _cameras[Camera.CurrentCameraIndex].CamPosition.Z);
            Constants.camPositoin = Utils.NumericsVec3ToVec3(_cameras[Camera.CurrentCameraIndex].CamPosition);

            Light light1 = new Light();
            light1.Position = new Vector3(1000, 300, 2500);
            light1.Enabled = true;
            light1.IsSpotLight = false;
            _lightSource.Add(light1);

            Light light2 = new Light();
            light2.Position = new Vector3(-1000, 300, 2500);
            light2.Enabled = true;
            light2.IsSpotLight = false;
            _lightSource.Add(light2);

            Light light3 = new Light();
            light3.Position = Utils.NumericsVec3ToVec3(GetWorldObjectsCenter(CameraTargetObjectIdx));
            light3.Enabled = true;
            light3.IsSpotLight = true;
            light3.DirectionOfLight = new Vector3(1, 0, 0);
            _lightSource.Add(light3);


            this.animationTimer.Enabled = true;
            PaintScene();
        }

        System.Numerics.Vector3 GetThirdPersonCameraPosition()
        {
            return GetWorldObjectsCenter(CameraTargetObjectIdx) + new System.Numerics.Vector3(-600, 0, 400);
        }

        System.Numerics.Vector3 GetThirdPersonCameraTarget()
        {
            return GetWorldObjectsCenter(CameraTargetObjectIdx) + new System.Numerics.Vector3(10000, 0, 300);
        }
        System.Numerics.Vector3 GetWorldObjectsCenter(int objectIdx)
        {
            //double x = 0, y = 0, z = 0;
            //int count = 0;
            double maxX = float.MinValue, maxY = float.MinValue, maxZ = float.MinValue;
            double minX = float.MaxValue, minY = float.MaxValue, minZ = float.MaxValue;
            foreach (var face in _objects[objectIdx].FacesWorld)
            {
                foreach (var vertex in face.vertices)
                {

                    maxX = (vertex.X > maxX) ? vertex.X : maxX;
                    maxY = (vertex.Y > maxY) ? vertex.Y : maxY;
                    maxZ = (vertex.Z > maxZ) ? vertex.Z : maxZ;
                    minX = (vertex.X < minX) ? vertex.X : minX;
                    minY = (vertex.Y < minY) ? vertex.Y : minY;
                    minZ = (vertex.Z < minZ) ? vertex.Z : minZ;
                    //x += vertex.X;  
                    //y += vertex.Y;
                    //z += vertex.Z;
                }
                //count += 3;
            }
            //var data = _objects[idx].FacesWorld.Min(x=>x.)

            //float maxX = data.Vertices.Max(x => x.X);
            //float maxY = data.Vertices.Max(x => x.Y);
            //float maxZ = data.Vertices.Max(x => x.Z);
            //float minX = data.Vertices.Min(x => x.X);
            //float minY = data.Vertices.Min(x => x.Y);
            //float minZ = data.Vertices.Min(x => x.Z);

            //return new System.Numerics.Vector3((float)x / count, (float)y / count, (float)z / count);
            return new System.Numerics.Vector3((float)((minX + maxX) / 2.0f), (float)((minY + maxY) / 2.0f), (float)((minZ + maxZ) / 2.0f));
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
            Constants.SHADER shader = this.interpolateConstradioButton.Checked ? Constants.SHADER.CONST : this.interpolateNormalRadioButton.Checked ? Constants.SHADER.PHONG : Constants.SHADER.GOURAUD;
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                g.Clear(Color.FromArgb(255, (int)(255.0f * Constants.LightIntensity), (int)(255.0f * Constants.LightIntensity), (int)(255.0f * Constants.LightIntensity)));
                //g.FillEllipse(Brushes.Yellow, (int)_lightSourceCamera.X, (int)_lightSourceCamera.Y, 50, 50);
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
                v.X = (v.X - minX) / (maxX - minX) * _objects[idx].Scale + _objects[idx].Offset.X;// + Constants.XOffset + (float)0.1 * idx * Constants.Offset;
                v.Y = (v.Y - minY) / (maxY - minY) * _objects[idx].Scale + _objects[idx].Offset.Y;// + Constants.YOffset;
                v.Z = (v.Z - minZ) / (maxZ - minZ) * _objects[idx].Scale / 2 + _objects[idx].Offset.Z;
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
        private void rotateObjectsPoint(List<Vector3> cameraPoint, List<Vector3> worldPoint, Matrix4x4 rotationMat,
            Matrix4x4 translationMat, Matrix4x4 oscilationMat, Matrix4x4 transleteToOriginMat, Matrix4x4 transleteFromOriginMat,
            Matrix4x4 viewMat, Matrix4x4 perspectiveMat, int idx)
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
                    p = Vector4.Transform(p, transleteToOriginMat);
                    p = Vector4.Transform(p, rotationMat);
                    p = Vector4.Transform(p, transleteFromOriginMat);

                    if (oscilate)
                    {
                        p = Vector4.Transform(p, oscilationMat);
                    }

                    p = Vector4.Transform(p, translationMat);
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
        private void UpdataCameraTarget(int idx)
        {
            _cameras[idx].CamTarget = GetWorldObjectsCenter(_cameras[idx].TargetObjectIdx);
        }
        void RotateScene()
        {
            var rotationMat =
                  System.Numerics.Matrix4x4.CreateRotationY((float)(Constants.Angle * Math.PI / 180.0));

            var translationMat = Matrix4x4.CreateTranslation(translation);
            var oscilationMat = Matrix4x4.CreateTranslation(oscilation);

            oscilation = -oscilation;
            translation.X = 0;
            translation.Y = 0;
            translation.Z = 0;
            System.Numerics.Vector3 camUpVec;

            camUpVec.X = 0;
            camUpVec.Y = 0;
            camUpVec.Z = 1;
            if (stationaryTrackingCameraIdx == Camera.CurrentCameraIndex)
            {
                UpdataCameraTarget(stationaryTrackingCameraIdx);
            }

            Matrix4x4 viewMat;
            if (thirdPersonCameraIdx == Camera.CurrentCameraIndex)
            {
                var position = GetThirdPersonCameraPosition();
                this.xNumericUpDown.Value = (decimal)position.X;
                this.yNumericUpDown.Value = (decimal)position.Y;
                this.zNumericUpDown.Value = (decimal)position.Z;
                var target = GetThirdPersonCameraTarget();
                viewMat = Matrix4x4.CreateLookAt(position, target, camUpVec);
            }
            else
            {
                viewMat = Matrix4x4.CreateLookAt(_cameras[Camera.CurrentCameraIndex].CamPosition, _cameras[Camera.CurrentCameraIndex].CamTarget, camUpVec);
            }
            float fieldOfView, aspecetRatio, nearPlaneDist, farPlaneDist;
            fieldOfView = (float)((double)this.FOVTrackBar.Value * Math.PI / 180.0);
            aspecetRatio = _drawArea.Width / _drawArea.Height;
            nearPlaneDist = 10;
            farPlaneDist = 20000;
            var perspectiveMat = System.Numerics.Matrix4x4.CreatePerspectiveFieldOfView(fieldOfView, aspecetRatio, nearPlaneDist, farPlaneDist);
            for (int idx = 0; idx < _objects.Count; idx++)
            {
                System.Numerics.Vector3 center = new System.Numerics.Vector3(0, 0, 0);
                if (_objects[idx].Animate == true)
                {
                    center = GetWorldObjectsCenter(idx);
                }
                Matrix4x4 transleteToOriginMat = Matrix4x4.CreateTranslation(-center);
                Matrix4x4 transleteFromOriginMat = Matrix4x4.CreateTranslation(center);
                for (int f = 0; f < _objects[idx].FacesWorld.Count; f++)
                {
                    rotateObjectsPoint(_objects[idx].FacesCamera[f].vertices, _objects[idx].FacesWorld[f].vertices, rotationMat, translationMat,
                        oscilationMat, transleteToOriginMat, transleteFromOriginMat, viewMat, perspectiveMat, idx);
                    rotateNormalVector(_objects[idx].FacesCamera[f].normals, _objects[idx].FacesWorld[f].normals, rotationMat, idx);
                }

                Constants.MaxZ = Math.Max(Constants.MaxZ, _objects[idx].FacesCamera.Max(x => (x.vertices.Max(xx => (xx.Z)))));
                Constants.MinZ = Math.Min(Constants.MinZ, _objects[idx].FacesCamera.Min(x => (x.vertices.Min(xx => (xx.Z)))));
                _objects[idx].PolygonFiller.Faces = _objects[idx].FacesCamera;
            }
        }
        //purely debug featuer, not intented for any particular usage
        private void CameraPositionChanged(object sender, EventArgs e)
        {
            _cameras[Camera.CurrentCameraIndex].CamPosition =
                new System.Numerics.Vector3((float)this.xNumericUpDown.Value, (float)this.yNumericUpDown.Value, (float)this.zNumericUpDown.Value);

            Constants.camPositoin = new Vector3((float)this.xNumericUpDown.Value, (float)this.yNumericUpDown.Value, (float)this.zNumericUpDown.Value);
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
                _lightSource[0].Position.X = x + _origin.X;
                _lightSource[0].Position.Y = y + _origin.Y;
                //_lightSourceCamera.X = x + _origin.X;
                //_lightSourceCamera.Y = y + _origin.Y;
            }
            //if (this.animateObjectCheckBox.Checked)
            //{
            //    //Constants.Angle += 3;
            //}
            if (Constants.DayAndNight)
            {

                if (Constants.LightIntensity + Constants.LightIntensityChangeRate > 1 || Constants.LightIntensity + Constants.LightIntensityChangeRate < 0)
                {
                    Constants.LightIntensityChangeRate = -Constants.LightIntensityChangeRate;
                }
                Constants.LightIntensity += Constants.LightIntensityChangeRate;
            }
            else
            {
                Constants.LightIntensity = 1;
            }

            if (this._lightSource[2].Enabled)
            {
                this._lightSource[2].Position = Utils.NumericsVec3ToVec3(GetWorldObjectsCenter(CameraTargetObjectIdx));
            }
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
            _lightSource[0].Position.Z = z;

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
                if (obj.Animatable)
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
            Constants.camPositoin = new Vector3(tmp.X, tmp.Y, tmp.Z);
            this.xNumericUpDown.Value = (decimal)tmp.X;
            this.yNumericUpDown.Value = (decimal)tmp.Y;
            this.zNumericUpDown.Value = (decimal)tmp.Z;
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int val = 40;
            int degree = 15;
            if (e.KeyChar == 'w')
            {
                translation.X += val;
            }
            else if (e.KeyChar == 's')
            {
                translation.X += -val;
            }
            else if (e.KeyChar == 'd')
            {
                translation.Y += +val;
            }
            else if (e.KeyChar == 'a')
            {
                translation.Y += -val;
            }
            else if (e.KeyChar == 'm')
            {
                double angle = degree; // Angle of rotation in degrees

                double radians = angle * Math.PI / 180.0; // Convert to radian
                double x = _lightSource[2].DirectionOfLight.X;
                double y = _lightSource[2].DirectionOfLight.Y;
                double newX = x * Math.Cos(radians) - y * Math.Sin(radians);
                double newY = x * Math.Sin(radians) + y * Math.Cos(radians);
                _lightSource[2].DirectionOfLight.X = newX;
                _lightSource[2].DirectionOfLight.Y = newY;

            }
            else if (e.KeyChar == 'b')
            {
                double angle = -degree; // Angle of rotation in degrees

                double radians = angle * Math.PI / 180.0; // Convert to radian
                double x = _lightSource[2].DirectionOfLight.X;
                double y = _lightSource[2].DirectionOfLight.Y;
                double newX = x * Math.Cos(radians) - y * Math.Sin(radians);
                double newY = x * Math.Sin(radians) + y * Math.Cos(radians);
                _lightSource[2].DirectionOfLight.X = newX;
                _lightSource[2].DirectionOfLight.Y = newY;
            }
            else if (e.KeyChar == 'h')
            {
                double angle = degree; // Angle of rotation in degrees

                double radians = angle * Math.PI / 180.0; // Convert to radian
                double x = _lightSource[2].DirectionOfLight.X;
                double y = _lightSource[2].DirectionOfLight.Z;
                double newX = x * Math.Cos(radians) - y * Math.Sin(radians);
                double newY = x * Math.Sin(radians) + y * Math.Cos(radians);
                _lightSource[2].DirectionOfLight.X = newX;
                _lightSource[2].DirectionOfLight.Z = newY;
            }
            else if (e.KeyChar == 'n')
            {
                double angle = -degree; // Angle of rotation in degrees

                double radians = angle * Math.PI / 180.0; // Convert to radian
                double x = _lightSource[2].DirectionOfLight.X;
                double y = _lightSource[2].DirectionOfLight.Z;
                double newX = x * Math.Cos(radians) - y * Math.Sin(radians);
                double newY = x * Math.Sin(radians) + y * Math.Cos(radians);
                _lightSource[2].DirectionOfLight.X = newX;
                _lightSource[2].DirectionOfLight.Z = newY;
            }
        }
        private void oscilationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            oscilate = !oscilate;
        }
        private void light1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _lightSource[0].Enabled = !_lightSource[0].Enabled;
        }
        private void light2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _lightSource[1].Enabled = !_lightSource[1].Enabled;
        }
        private void light3reflectorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _lightSource[2].Enabled = !_lightSource[2].Enabled;
        }
        private void fogCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Constants.Fog = !Constants.Fog;
        }
        private void dayAndNightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Constants.DayAndNight = !Constants.DayAndNight;
        }
    }
}
