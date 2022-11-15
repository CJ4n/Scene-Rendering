//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Media.Media3D;
using ObjLoader.Loader.Data;
namespace Filling_Triangular_Mesh
{
    public class PolygonFiller
    {
        private Bitmap bitmap;
        private MyColor[,] texture;
        private Bitmap _colors;
        int bitmapWidth;
        private List<MyFace> grid;

        public PolygonFiller(Bitmap bitmap, List<MyFace> grid, MyColor[,] texture, Bitmap colors)
        {
            this.bitmap = bitmap;
            this.grid = grid;
            this.texture = texture;
            _colors = colors;
            bitmapWidth = bitmap.Width;

        }
        public void ChangeTexture(MyColor[,] texture)
        {
            this.texture = texture;
        }
        public void ChangeTexture(List<MyFace> grid)
        {
            this.grid = grid;
        }

        public void FillGridWithTriangles(float kd, float ks, int m, bool interpolateNormalVector, Vector3 lightSource, int u)
        {
            using (var snoop = new BmpPixelSnoop(bitmap))
            {
                using (var cs = new BmpPixelSnoop(_colors))
                {
                    if (u == -1)
                        Parallel.For(0, grid.Count, i =>
                        {
                            var triangle = new List<Point> { new Point((int)grid[i].vertices[0].X, (int)grid[i].vertices[0].Y),
                                                 new Point((int)grid[i].vertices[1].X, (int)grid[i].vertices[1].Y),
                                                 new Point((int)grid[i].vertices[2].X, (int)grid[i].vertices[2].Y)};

                            var gen = new ColorGenerator(grid[i], ks, kd, m, interpolateNormalVector, lightSource, texture);

                            FillTriangle(triangle, gen, snoop);
                        });

                    //if (u == -1)
                    //{
                    //for (int i = 0; i < grid.Count; ++i)
                    //{
                    //    var triangle = new List<Point> { new Point((int)grid[i].vertices[0].X, (int)grid[i].vertices[0].Y),
                    //                         new Point((int)grid[i].vertices[1].X, (int)grid[i].vertices[1].Y),
                    //                         new Point((int)grid[i].vertices[2].X, (int)grid[i].vertices[2].Y)};
                    //    var gen = new ColorGenerator(grid[i], ks, kd, m, interpolateNormalVector, lightSource, texture, cs);

                    //    FillTriangle(triangle, gen, snoop);
                    //}

                    //}
                    else
                    {
                        int i = u;
                        var triangle = new List<Point> { new Point((int)grid[i].vertices[0].X, (int)grid[i].vertices[0].Y),
                                                 new Point((int)grid[i].vertices[1].X, (int)grid[i].vertices[1].Y),
                                                 new Point((int)grid[i].vertices[2].X, (int)grid[i].vertices[2].Y)};
                        var gen = new ColorGenerator(grid[i], ks, kd, m, interpolateNormalVector, lightSource, texture);

                        FillTriangle(triangle, gen, snoop);
                    }


                }
            }
        }

        private void FillTriangle(List<Point> triangle, ColorGenerator colorGenerator, BmpPixelSnoop snoop)
        {
            var scanLine = new ScanLine(triangle);

            foreach (var (xList, y) in scanLine.GetIntersectionPoints())
            {
                FillRow(xList, y, colorGenerator, snoop);
            }
        }
        private void FillRow(List<int> xList, int y, ColorGenerator colorGenerator, BmpPixelSnoop snoop)
        {
            for (int i = 0; i < xList.Count - 1; ++i)
            {
                int endCol = Math.Min(xList[i + 1], bitmapWidth);
                for (int x = xList[i]; x < endCol; ++x)
                {
                    Color color = colorGenerator.ComputeColor(x, y);
                    snoop.SetPixel(x, y, color);
                }
            }
        }
    }
}
