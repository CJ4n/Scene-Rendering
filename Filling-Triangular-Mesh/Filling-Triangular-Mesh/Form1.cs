using ObjLoader.Loader.Data;
using ObjLoader.Loader.Loaders;
//using System.Windows.Media;
//using Color = System.Drawing.Color;
//using MathNet.Numerics;
namespace Filling_Triangular_Mesh
{

    //skalowanie R,G -> <-1,1>
    // skalowanie B-> <0,1>
    public partial class Form1 : Form
    {
        private Bitmap _drawArea;
        private PolygonFiller _fillPolygon;
        //private LoadResult _result;
        private Vector3 _lightSource;
        int radius = 3000;
        int angle = 0;
        int radiusIncrement = -2;
        int angleIncrement = 3;
        int maxSpiralRadius = 5000;
        int minSpiralRadious = 40;
        private Color _lighColor = Color.White;
        private PointF _origin = new PointF(300, 300);
        private string _pathToTexture = "..\\..\\..\\..\\..\\1234.jpg";
        private string _pathToObjFile = "..\\..\\..\\..\\..\\smoothv2.obj";
        //string _pathToObjFile = "..\\..\\..\\..\\..\\SemiTorus.obj";
        private string _pathToNormalMap;// = "..\\..\\..\\..\\..\\brickwall_normal.jpg";
        private List<MyFace> _faces;
        private Vector3[,] _normalMap = null;
        private MyColor[,] _colorMap;
        private int _objectBasicDim = 600;
        public Form1()
        {
            InitializeComponent();

            _lightSource = new Vector3(radius, 300, 2500);
            _drawArea = new Bitmap(Canvas.Width * 1, Canvas.Height * 2, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Canvas.Image = _drawArea;
            Bitmap bmp = new Bitmap(_drawArea.Width, _drawArea.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(255, 255, 255, 255));
            g.Dispose();

            _colorMap = ConvertBitmapToArray(bmp);
            GetAndSetObj();
            PaintScene();
        }
        private void GetAndSetObj()
        {
            var _result = LoadObjFile(_pathToObjFile);
            _faces = GetAllFaces(_result);
            _fillPolygon = new PolygonFiller(_drawArea, _faces, _colorMap, _lighColor, _normalMap);
        }
        private Bitmap GetBitampFromFile(string path)
        {
            Bitmap texture = new Bitmap(path);
            Rectangle cloneRect = new Rectangle(0, 0, Math.Min(_drawArea.Width, texture.Width), Math.Min(_drawArea.Height, texture.Height));
            Bitmap bmp = texture.Clone(cloneRect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
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
        public Vector3 RgbToNormalVectorsArray(Color c)
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
            bool interpolateNormalVector = false;
            if (this.normalRadioButton.Checked == true)
            {
                interpolateNormalVector = true;
            }
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                g.Clear(System.Drawing.Color.LightBlue);
                g.FillEllipse(Brushes.Red, (int)_lightSource.X, (int)_lightSource.Y, 50, 50);
            }

            _fillPolygon.FillGridWithTriangles(kd, ks, m, interpolateNormalVector, _lightSource);

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
        public void DrawTriangulation()
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
        public LoadResult LoadObjFile(string path)
        {
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            var fileStream = new FileStream(path, FileMode.Open);
            var result = objLoader.Load(fileStream);
            return result;
        }
        private void KdTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PaintScene();
        }
        private void KsTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PaintScene();
        }
        private void MTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PaintScene();
        }
        private void ColorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            PaintScene();
        }
        private void NormalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            PaintScene();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (radius < minSpiralRadious || radius > maxSpiralRadius)
            {
                radiusIncrement = -radiusIncrement;
            }
            double x = radius * Math.Cos(angle * Math.PI / 180);
            double y = radius * Math.Sin(angle * Math.PI / 180);
            angle += angleIncrement;
            radius += radiusIncrement;

            _lightSource.X = x + _origin.X;
            _lightSource.Y = y + _origin.Y;
            PaintScene();
        }
        private void ZTrackBar_ValueChanged(object sender, EventArgs e)
        {
            var z = (double)zTrackBar.Value;
            _lightSource.Z = z;
            if (animationCheckBox.Checked == false)
            {
                PaintScene();
            }
        }
        private void AnimationCheckBox_CheckedChanged(object sender, EventArgs e)
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
        private void PaintTriangulationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PaintScene();
        }
        private void TextureColorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.textureColorRadioButton.Checked == false)
            {
                return;
            }
            var texture = GetBitampFromFile(_pathToTexture);
            _colorMap = ConvertBitmapToArray(texture);
            _fillPolygon.ColorMap = _colorMap;
        }
        private void ConstColorRadioButton_Click(object sender, EventArgs e)
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
        private void SelectObjButton_Click(object sender, EventArgs e)
        {
            var status = this.openFileDialog1.ShowDialog();
            if (status != DialogResult.OK)
            {
                return;
            }
            _pathToObjFile = this.openFileDialog1.FileName;
            GetAndSetObj();
        }
        private void ChangeLightColorButton_Click(object sender, EventArgs e)
        {
            var status = this.lightColorDialog.ShowDialog();
            if (status != DialogResult.OK)
            {
                return;
            }
            _lighColor = this.lightColorDialog.Color;
            _fillPolygon.LighColor = _lighColor;
        }
        private void ModifyNormalVectorsButton_Click(object sender, EventArgs e)
        {
            var status = this.openFileDialog1.ShowDialog();
            if (status != DialogResult.OK)
            {
                _normalMap = null;
                _fillPolygon.NormalMap = _normalMap;
                return;
            }
            _pathToNormalMap = this.openFileDialog1.FileName;
            ModifyNormalVectors();
            _fillPolygon.NormalMap = _normalMap;
        }
    }
}