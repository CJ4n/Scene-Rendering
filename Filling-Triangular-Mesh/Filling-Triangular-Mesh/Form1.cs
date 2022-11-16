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
        private Bitmap _color;
        private PolygonFiller _fillPolygon;
        private LoadResult _result;
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
        private Vector3[,] _normalMap=null;
        private MyColor[,] _myColorArray;

        private int _objectWidth = 600;
        private int _objectHeight = 600;

        public Form1()
        {
            InitializeComponent();
          _lightSource  = new Vector3(radius, 300, 2500);
            _drawArea = new Bitmap(Canvas.Width * 1, Canvas.Height * 2, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Canvas.Image = _drawArea;
            //Bitmap bmp = GetBitampFromFile(pathToTexture);
            Bitmap bmp = new Bitmap(_drawArea.Width, _drawArea.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(255, 255, 255, 255));
            g.Dispose();
            _color = bmp;
            _myColorArray = ConvertBitmapToArray(bmp);

            GetAndSetObj();
            PaintScene();
        }
        private void GetAndSetObj()
        {
            _result = LoadObjFile(_pathToObjFile);
            _faces = GetAllFaces(_result);
            _fillPolygon = new PolygonFiller(_drawArea, _faces, _myColorArray, _color, _lighColor, _normalMap);
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
            bool xx = false;

            _fillPolygon.FillGridWithTriangles(kd, ks, m, interpolateNormalVector, _lightSource, -1);

            if (paintTriangulationCheckBox.Checked)
            {
                DrawTriangulation(_result);
            }
            Canvas.Refresh();

        }
        
        private List<MyFace> GetAllFaces(LoadResult data)
        {
            float maxX;
            float maxY;
            float maxZ;
            float minX;
            float minY;
            float minZ;
            List<MyFace> faces = new List<MyFace>();
            foreach (var t in data.Groups)
            {
                maxX = data.Vertices.Max(x => x.X);
                maxY = data.Vertices.Max(x => x.Y);
                maxZ = data.Vertices.Max(x => x.Z);
                minX = data.Vertices.Min(x => x.X);
                minY = data.Vertices.Min(x => x.Y);
                minZ = data.Vertices.Min(x => x.Z);
                foreach (var f in t.Faces)
                {
                    Vector3 v0 = data.Vertices[f[0].VertexIndex - 1];
                    Vector3 v1 = data.Vertices[f[1].VertexIndex - 1];
                    Vector3 v2 = data.Vertices[f[2].VertexIndex - 1];
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
            }
            return faces;
        }

        PointF ToPointF(double x, double y)
        {
            return new PointF((float)x, (float)y);
        }
        public void DrawTriangulation(LoadResult data)
        {
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                foreach (var t in data.Groups)
                {
                    foreach (var f in t.Faces)
                    {
                        Pen pen = new Pen(Brushes.Black, 1);

                        g.DrawLine(pen, ToPointF(data.Vertices[f[0].VertexIndex - 1].X, data.Vertices[f[0].VertexIndex - 1].Y),
                          ToPointF(data.Vertices[f[1].VertexIndex - 1].X, data.Vertices[f[1].VertexIndex - 1].Y));

                        g.DrawLine(pen, ToPointF(data.Vertices[f[1].VertexIndex - 1].X, data.Vertices[f[1].VertexIndex - 1].Y),
                            ToPointF(data.Vertices[f[2].VertexIndex - 1].X, data.Vertices[f[2].VertexIndex - 1].Y));

                        g.DrawLine(pen, ToPointF(data.Vertices[f[2].VertexIndex - 1].X, data.Vertices[f[2].VertexIndex - 1].Y),
                            ToPointF(data.Vertices[f[0].VertexIndex - 1].X, data.Vertices[f[0].VertexIndex - 1].Y));
                    }
                }
                foreach (var p in data.Vertices)
                {
                    g.FillEllipse(Brushes.Green, (float)p.X * (200) + 300, (float)p.Y * (200) + 250, 2, 2);
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

        private void kdTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PaintScene();
        }

        private void ksTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PaintScene();
        }

        private void mTrackBar_ValueChanged(object sender, EventArgs e)
        {
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
            //var rse = RotatePoint(lightSource, origin, 3);
            if (radius < minSpiralRadious|| radius > maxSpiralRadius)
            {
                radiusIncrement = -radiusIncrement;
            }
           
            //lightSource.X = (float)rse.Item1;
            //lightSource.Y = (float)rse.Item2;
            double x = radius * Math.Cos(angle * Math.PI / 180);
            double y = radius * Math.Sin(angle * Math.PI / 180);
            angle += angleIncrement;
            radius  += radiusIncrement;

            _lightSource.X = x + _origin.X;
            _lightSource.Y = y + _origin.Y;
            PaintScene();
        }
        (double, double) RotatePoint(Vector3 pointToRotate, PointF centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return
                 ((cosTheta * (pointToRotate.X - centerPoint.X) - sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                  (sinTheta * (pointToRotate.X - centerPoint.X) + cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y));
        }

        private void zTrackBar_ValueChanged(object sender, EventArgs e)
        {
            var z = (double)zTrackBar.Value;
            _lightSource.Z = z;
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

        private void textureColorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.textureColorRadioButton.Checked == false)
            {
                return;
            }
            var texture = GetBitampFromFile(_pathToTexture);
            var colorArray = ConvertBitmapToArray(texture);
            _fillPolygon.ChangeTexture(colorArray);
        }
        private void constColorRadioButton_Click(object sender, EventArgs e)
        {
            if (this.constColorRadioButton.Checked == false)
            {
                return;
            }
            Color c;
            if (this.surfaceColorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            c = this.surfaceColorDialog.Color;
            Bitmap bmp = new Bitmap(_drawArea.Width, _drawArea.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(c);
            g.Dispose();
            var colorArray = ConvertBitmapToArray(bmp);
            _fillPolygon.ChangeTexture(colorArray);
        }
        private void selectObjButton_Click(object sender, EventArgs e)
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
            _fillPolygon.ChangeLighColor(_lighColor);
        }

        private void modifyNormalVectorsButton_Click(object sender, EventArgs e)
        {
            var status = this.openFileDialog1.ShowDialog();

            if (status != DialogResult.OK)
            {
                return;
            }

            _pathToNormalMap = this.openFileDialog1.FileName;
            ModifyNormalVectors();
            GetAndSetObj();
        }
    }
}