//using System.Windows.Media;
//using System.Windows.Media.Imaging.WriteableBitmap;
//using Pen = System.Drawing.Pen;
//using System.Windows.UI.Xaml.Media.Imaging;
//using static System.Windows.Media.Imaging.WriteableBitmap;
//using System.Windows.Media.Imaging;
//using System.Drawing.Imaging;
//using System.Windows.Media;
using ObjLoader.Loader.Data;
using ObjLoader.Loader.Loaders;

namespace Filling_Triangular_Mesh
{

    //skalowanie R,G -> <-1,1>
    // skalowanie B-> <0,1>
    public partial class Form1 : Form
    {
        private Bitmap _drawArea;
        private Bitmap _texture;
        FillPolygon fillPolygon;
        LoadResult result;
        Vector3 lightSource = new Vector3(300, 0, 0);
        Color selectedColor;
        //string pathToTexture = "C:\\Users\\YanPC\\Downloads\\1234.jpg";
        string pathToTexture = "..\\..\\..\\..\\..\\1234.jpg";
        //string pathToObjFile = "D:\\JAN_CICHOMSKI\\STUDIA\\STUDIA_SEMESTR_5_2022_ZIMA\\Grafika Komputerowa 1\\lab2\\Filling-Triangular-Mesh\\hemi.obj";
        string pathToObjFile = "..\\..\\..\\..\\..\\smoothsphere.obj";
        public Form1()
        {
            InitializeComponent();
            result = LoadObjFile(pathToObjFile);
            _drawArea = new Bitmap(Canvas.Width * 1, Canvas.Height * 2, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Canvas.Image = _drawArea;

            Bitmap bmp = GetBitampFromFile(pathToTexture);

            var myColorArray = ConvertBitmapToArray(bmp);

            var faces = GetAllFaces(result);

            fillPolygon = new FillPolygon(_drawArea, faces, myColorArray);
            PaintScene();
        }

        private Bitmap GetBitampFromFile(string path)
        {

            Bitmap texture = new Bitmap(path);
            Rectangle cloneRect = new Rectangle(0, 0, Math.Min(_drawArea.Width, texture.Width), Math.Min(_drawArea.Height, texture.Height));
            Bitmap bmp = texture.Clone(cloneRect, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
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
            }
            fillPolygon.FillGridWithTriangles(ks, kd, m, interpolateNormalVector, lightSource);
            if (paintTriangulationCheckBox.Checked)
            {
                DrawTriangulation(result);
            }
            Canvas.Refresh();

        }
        private List<MyFace> GetAllFaces(LoadResult data)
        {
            List<MyFace> faces = new List<MyFace>();
            foreach (var t in data.Groups)
            {
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
            var rse = RotatePoint(lightSource, new PointF(0, 0), 2);
            lightSource.X = (float)rse.Item1;
            lightSource.Y = (float)rse.Item2;
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
            lightSource.Z = z / 10;
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
            var texture = GetBitampFromFile(pathToTexture);
            var colorArray = ConvertBitmapToArray(texture);
            fillPolygon.ChangeTexture(colorArray);
        }

        private void constColorRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.constColorRadioButton.Checked == false)
            //{
            //    return;
            //}
            //Color c;
            //if (this.colorDialog.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}
            //c = this.colorDialog.Color;
            //Bitmap bmp = new Bitmap(_drawArea.Width, _drawArea.Height);
            //Graphics g = Graphics.FromImage(bmp);
            //g.Clear(c);
            //g.Dispose();
            //var colorArray = ConvertBitmapToArray(bmp);
            //fillPolygon.ChangeTexture(colorArray);
        }

        private void constColorRadioButton_Click(object sender, EventArgs e)
        {
            if (this.constColorRadioButton.Checked == false)
            {
                return;
            }
            Color c;
            if (this.colorDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            c = this.colorDialog.Color;
            Bitmap bmp = new Bitmap(_drawArea.Width, _drawArea.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(c);
            g.Dispose();
            var colorArray = ConvertBitmapToArray(bmp);
            fillPolygon.ChangeTexture(colorArray);
            //constColorRadioButton_CheckedChanged(sender, e);
        }
    }
}