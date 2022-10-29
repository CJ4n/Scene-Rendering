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
        public Form1()
        {
            InitializeComponent();
            var result = LoadObjFile("C:\\Users\\YanPC\\Desktop\\Filling-Triangular-Mesh\\sphere.obj");
            _drawArea = new Bitmap(Canvas.Width * 1, Canvas.Height * 2);
            minX = result.Vertices.Min(x => x.X);
            minY = result.Vertices.Min(x => x.Y);
            result.Vertices.All(v =>
            {
                v.X = (float)((v.X - minX) * 300.0 + 100);
                v.Y = (float)((v.Y - minY) * 300.0 + 150);
                return true;
            });


            Canvas.Image = _drawArea;
            var faces = GetAllFaces(result);
            //var triangles = GetTriangles(result);

            var fillPolygon = new FillPolygon(_drawArea, faces);
            fillPolygon.FillGridByTriangles();
            DrawTriangulation(result);


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
                    Vector3 n0 = data.Normals[f[0].VertexIndex - 1];
                    Vector3 n1 = data.Normals[f[1].VertexIndex - 1];
                    Vector3 n2 = data.Normals[f[2].VertexIndex - 1];
                    List<Vector3> normals = new List<Vector3>();
                    normals.Add(n0);
                    normals.Add(n1);
                    normals.Add(n2);
                    List<Vector3> vertices = new List<Vector3>();
                    vertices.Add(v0);
                    vertices.Add(v1);
                    vertices.Add(v2);
                    faces.Add(new MyFace(vertices, normals));
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
                Canvas.Refresh();
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



    }
}