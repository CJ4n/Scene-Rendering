using ObjLoader.Loader.Loaders;
namespace Filling_Triangular_Mesh
{
    public partial class Form1 : Form
    {
        private Bitmap _drawArea;

        public Form1()
        {
            InitializeComponent();
            var result = LoadObjFile("C:\\Users\\YanPC\\Desktop\\Filling-Triangular-Mesh\\untitled.obj");
            _drawArea = new Bitmap(Canvas.Width * 3, Canvas.Height * 3);
            Canvas.Image = _drawArea;
            DrawTriangulation(result);

        }

        PointF ToPoint(double x, double y)
        {
            return new PointF((float)x * (200) + 300, (float)y * (200) + 250);
        }
        public void DrawTriangulation(LoadResult data)
        {
            using (Graphics g = Graphics.FromImage(_drawArea))
            {
                foreach (var t in data.Groups)
                {
                    foreach (var f in t.Faces)
                    {
                        Pen pen = new Pen(Brushes.Black, 3);
                        g.DrawLine(pen, ToPoint(data.Vertices[f[0].VertexIndex - 1].X, data.Vertices[f[0].VertexIndex - 1].Y),
                            ToPoint(data.Vertices[f[1].VertexIndex - 1].X, data.Vertices[f[1].VertexIndex - 1].Y));

                        g.DrawLine(pen, ToPoint(data.Vertices[f[1].VertexIndex - 1].X, data.Vertices[f[1].VertexIndex - 1].Y),
                            ToPoint(data.Vertices[f[2].VertexIndex - 1].X, data.Vertices[f[2].VertexIndex - 1].Y));

                        g.DrawLine(pen, ToPoint(data.Vertices[f[2].VertexIndex - 1].X, data.Vertices[f[2].VertexIndex - 1].Y),
                            ToPoint(data.Vertices[f[0].VertexIndex - 1].X, data.Vertices[f[0].VertexIndex - 1].Y));
                    }
                }
                foreach (var p in data.Vertices)
                {
                    g.FillEllipse(Brushes.Red, (int)p.X * (200) + 300, (int)p.Y * (200) + 250, 10, 10);

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