using ObjLoader.Loader.Data;
using ObjLoader.Loader.Loaders;

namespace Filling_Triangular_Mesh
{
    public partial class Form1 : Form
    {
        private string _pathToColorMap = "..\\..\\..\\..\\..\\colorMap1.jpg";
        private string _pathToObjFile = "..\\..\\..\\..\\..\\SemiSphere.obj";
        private string _pathToNormalMap = "..\\..\\..\\..\\..\\brickwall_normal.jpg";

        private Vector3 _lightSource;
        private PointF _origin = new PointF(300, 300);
        private int _radius = 1000;
        private int _angle = 0;
        private int _radiusIncrement = -10;
        private int _angleIncrement = 3;
        private int _maxSpiralRadius = 2000;
        private int _minSpiralRadious = 40;

        private Bitmap _drawArea;
        private PolygonFiller _fillPolygon;
        private List<MyFace> _faces;
        private Vector3[,] _normalMap = null;
        private MyColor[,] _colorMap;
        private Color _lighColor = Color.White;
        private int _objectBasicDim = 600;

        public Form1()
        {
            InitializeComponent();
            this.zLabel.Text = "z: " + this.zTrackBar.Value.ToString();
            this.mLabel.Text = "m: " + this.mTrackBar.Value.ToString();
            this.ksLabel.Text = "ks: " + (this.ksTrackBar.Value / 100.0).ToString();
            this.kdLabel.Text = "kd: " + (this.kdTrackBar.Value / 100.0).ToString();
            _lightSource = new Vector3(_radius, 300, 2500);
            _drawArea = new Bitmap(Canvas.Width * 1, Canvas.Height * 2, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Canvas.Image = _drawArea;

            var colorMapBitmap = GetBitampFromFile(_pathToColorMap);
            _colorMap = ConvertBitmapToArray(colorMapBitmap);
            GetAndSetObj();
            PaintScene();
        }
        private void GetAndSetObj()
        {
            var _result = LoadObjFile();
            _faces = GetAllFaces(_result);
            _fillPolygon = new PolygonFiller(_drawArea, _faces, _colorMap, _lighColor, _normalMap);
        }
        private Bitmap GetBitampFromFile(string path)
        {
            Bitmap bitmap = new Bitmap(path);
            Rectangle cloneRect = new Rectangle(0, 0, Math.Min(_drawArea.Width, bitmap.Width), Math.Min(_drawArea.Height, bitmap.Height));
            Bitmap bmp = bitmap.Clone(cloneRect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            bitmap.Dispose();
            return bmp;
        }
        private MyColor[,] ConvertBitmapToArray(Bitmap bitmap)
        {
            MyColor[,] result = new MyColor[bitmap.Width, bitmap.Height];
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    var color = bitmap.GetPixel(x, y);
                    result[x, y] = new MyColor(color.R / 255.0, color.G / 255.0, color.B / 255.0);
                }
            }
            return result;
        }
        private Vector3 RgbToNormalVectorsArray(Color c)
        {
            Vector3 v = new Vector3(2.0 * c.R / 255.0, 2.0 * c.G / 255.0, c.B / 255.0);
            return v;
        }
        private void ModifyNormalVectors()
        {
            var normalMapBitmap = GetBitampFromFile(_pathToNormalMap);

            _normalMap = new Vector3[normalMapBitmap.Width, normalMapBitmap.Height];
            for (int col = 0; col < normalMapBitmap.Width; col++)
            {
                for (int row = 0; row < normalMapBitmap.Height; row++)
                {
                    _normalMap[col, row] = RgbToNormalVectorsArray(normalMapBitmap.GetPixel(col, row));
                }
            }
            normalMapBitmap.Dispose();
        }
        private void PaintScene()
        {
            float ks = (float)(this.ksTrackBar.Value / 100.0);
            float kd = (float)(this.kdTrackBar.Value / 100.0);
            int m = this.mTrackBar.Value;
            bool interpolateNormalVector = this.normalRadioButton.Checked;

            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                g.Clear(Color.LightBlue);
                g.FillEllipse(Brushes.Red, (int)_lightSource.X, (int)_lightSource.Y, 50, 50);
            }

            _fillPolygon.FillEachFace(kd, ks, m, interpolateNormalVector, _lightSource);

            if (paintTriangulationCheckBox.Checked)
            {
                DrawTriangulation();
            }
            Canvas.Refresh();
        }
        private List<MyFace> GetAllFaces(LoadResult data)
        {
            float maxX = data.Vertices.Max(x => x.X);
            float maxY = data.Vertices.Max(x => x.Y);
            float maxZ = data.Vertices.Max(x => x.Z);
            float minX = data.Vertices.Min(x => x.X);
            float minY = data.Vertices.Min(x => x.Y);
            float minZ = data.Vertices.Min(x => x.Z);

            var scaleVectorLambda = (Vector3 v) =>
            {
                v.X = (v.X - minX) / (maxX - minX) * _objectBasicDim;
                v.Y = (v.Y - minY) / (maxY - minY) * _objectBasicDim;
                v.Z = (v.Z - minZ) / (maxZ - minZ) * _objectBasicDim / 2;
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
                var ToPointF = (double x, double y) => { return new PointF((float)x, (float)y); };
                foreach (var f in _faces)
                {
                    Pen pen = new Pen(Brushes.Black, 1);
                    g.DrawLine(pen, ToPointF(f.vertices[0].X, f.vertices[0].Y), ToPointF(f.vertices[1].X, f.vertices[1].Y));
                    g.DrawLine(pen, ToPointF(f.vertices[1].X, f.vertices[1].Y), ToPointF(f.vertices[2].X, f.vertices[2].Y));
                    g.DrawLine(pen, ToPointF(f.vertices[2].X, f.vertices[2].Y), ToPointF(f.vertices[0].X, f.vertices[0].Y));
                }
            }
        }
        private LoadResult LoadObjFile()
        {
            var fileStream = new FileStream(_pathToObjFile, FileMode.Open);
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            var result = objLoader.Load(fileStream);
            return result;
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

            _lightSource.X = x + _origin.X;
            _lightSource.Y = y + _origin.Y;
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
            _colorMap = ConvertBitmapToArray(texture);
            _fillPolygon.ColorMap = _colorMap;
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
            _colorMap = ConvertBitmapToArray(bmp);
            _fillPolygon.ColorMap = _colorMap;
        }
        private void loadObjFileButton_Click(object sender, EventArgs e)
        {
            var status = this.openFileDialog1.ShowDialog();
            if (status != DialogResult.OK)
            {
                return;
            }
            _pathToObjFile = this.openFileDialog1.FileName;
            GetAndSetObj();
        }
        private void changeLightColorButton_Click(object sender, EventArgs e)
        {
            var status = this.lightColorDialog.ShowDialog();
            if (status != DialogResult.OK)
            {
                return;
            }
            _lighColor = this.lightColorDialog.Color;
            _fillPolygon.LighColor = _lighColor;
        }
        private void loadNormalMapButton_Click(object sender, EventArgs e)
        {
            var status = this.openFileDialog1.ShowDialog();
            if (status != DialogResult.OK)
            {
                _normalMap = null;
                _fillPolygon.NormalMap = _normalMap;
            }
            else
            {
                _pathToNormalMap = this.openFileDialog1.FileName;
                this.modifyNormalMapcheckBox.Checked = true;

            }
            modifyNormalMapcheckBox_CheckedChanged(null, null);
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
            _fillPolygon.NormalMap = _normalMap;
        }
    }
}