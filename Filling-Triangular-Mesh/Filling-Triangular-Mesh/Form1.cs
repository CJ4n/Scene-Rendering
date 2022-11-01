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


    public partial class Form1 : Form
    {
        private Bitmap _drawArea;
        float minX;
        float minY;
        float minZ;
        FillPolygon fillPolygon;
        LoadResult result;
        public Form1()
        {
            InitializeComponent();
            result = LoadObjFile("C:\\Users\\YanPC\\Desktop\\Filling-Triangular-Mesh\\hemi.obj");
            _drawArea = new Bitmap(Canvas.Width * 1, Canvas.Height * 2);

            //minX = result.Vertices.Min(x => x.X);
            //minY = result.Vertices.Min(x => x.Y);
            //minZ = result.Vertices.Min(x => x.Z);

            //result.Vertices.All((v) =>
            //{
            //    v.X = (float)((v.X - minX) * 300.0 + 100);
            //    v.Y = (float)((v.Y - minY) * 300.0 + 100);
            //    v.Z = (float)((v.Z - minZ) * 300.0 + 100);
            //    return true;
            //});

            //minX = result.Normals.Min(x => x.X);
            //minY = result.Normals.Min(x => x.Y);
            //minZ = result.Normals.Min(x => x.Z);
            //result.Normals.All((v) =>
            //{
            //    v.X = (float)((v.X - minX) * 300.0 + 100);
            //    v.Y = (float)((v.Y - minY) * 300.0 + 100);
            //    v.Z = (float)((v.Z - minZ) * 300.0 + 100);
            //    return true;
            //});

            var aaa = result.Vertices.Min(x => x.Z);
            //var count = result.Vertices.Select(x =>
            //{
            //    if (x.Z == aaa)
            //    {
            //        return true;
            //    }
            //    return false;
            //}).Count();
            //int count = 0;
            //foreach (var v in result.Vertices)
            //{
            //    if (v.Z == aaa)
            //        count++;
            //}
            Canvas.Image = _drawArea;
            var faces = GetAllFaces(result);
            //var triangles = GetTriangles(result);

            fillPolygon = new FillPolygon(_drawArea, faces);

            //PaintScene(true);

        }

        private void PaintScene(bool paintTriangulation = true)
        {
            float ks = (float)(this.ksTrackBar.Value / 100.0);
            float kd = (float)(this.kdTrackBar.Value / 100.0);
            int m = this.ksTrackBar.Value;
            fillPolygon.FillGridWithTriangles(ks, kd, m, Canvas);
            DrawTriangulation(result);
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
        //private List<List<Point>> GetTriangles(LoadResult data)
        //{
        //    List<List<Point>> triangles = new List<List<Point>>();
        //    foreach (var t in data.Groups)
        //    {
        //        foreach (var f in t.Faces)
        //        {
        //            var triangle = new List<Point>();
        //            triangle.Add(ToPoint(data.Vertices[f[0].VertexIndex - 1].X, data.Vertices[f[0].VertexIndex - 1].Y));
        //            triangle.Add(ToPoint(data.Vertices[f[1].VertexIndex - 1].X, data.Vertices[f[1].VertexIndex - 1].Y));
        //            triangle.Add(ToPoint(data.Vertices[f[2].VertexIndex - 1].X, data.Vertices[f[2].VertexIndex - 1].Y));
        //            triangles.Add(triangle);
        //        }
        //    }
        //    return triangles;
        //}


        PointF ToPointF(double x, double y)
        {
            return new PointF((float)x, (float)y);
        }
        //Point ToPoint(double x, double y)
        //{
        //    return new Point((int)x, (int)y);
        //}
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
            PaintScene(true);
        }

        private void ksTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PaintScene(true);

        }

        private void mTrackBar_ValueChanged(object sender, EventArgs e)
        {
            PaintScene(true);

        }
    }
}