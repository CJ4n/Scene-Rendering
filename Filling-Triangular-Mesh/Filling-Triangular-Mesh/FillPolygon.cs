//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Media.Media3D;
namespace Filling_Triangular_Mesh
{
    public class FillPolygon
    {
        private Bitmap bitmap;
        private readonly byte[] buffer;
        //private readonly List<List<Point>> grid;
        private readonly List<MyFace> grid;
        //public FillPolygon(Bitmap bitmap, List<List<Point>> grid)
        //{
        //    this.bitmap = bitmap;
        //    //this.buffer = new byte[bitmap.PixelHeight * bitmap.BackBufferStride];
        //    this.grid = grid;
        //}

        //public void FillGridByTriangles()
        //{
        //    //currentRandom = new Random();
        //    //defaultGenerator = new ColorGenerator(Kd, Ks, M);
        //    try
        //    {
        //        //bitmap.Lock();
        //        for (int i = 0; i < grid.Count; ++i)
        //        {
        //            for (int j = 0; j < grid[i].Count - 1; ++j)
        //            {
        //                var lowerTriangle = new List<Point> { grid[i][0], grid[i][1], grid[i][2] };
        //                FillTriangle(lowerTriangle);

        //                //var upperTriangle = new List<Point> { grid[i][j], grid[i][j + 1], grid[i + 1][j + 1] };
        //                //FillTriangle(upperTriangle);
        //            }
        //        }
        //        //bitmap.AddDirtyRect(dirtyRect);
        //    }
        //    finally
        //    {
        //        //bitmap.Unlock();
        //    }
        //}
        public FillPolygon(Bitmap bitmap, List<MyFace> grid)
        {
            this.bitmap = bitmap;
            //this.buffer = new byte[bitmap.PixelHeight * bitmap.BackBufferStride];
            this.grid = grid;
        }

        public void FillGridByTriangles()
        {
            for (int i = 0; i < grid.Count; ++i)
            {
                var triangle = new List<Point> { new Point((int)grid[i].vertices[0].X, (int)grid[i].vertices[0].Y),
                                                 new Point((int)grid[i].vertices[1].X, (int)grid[i].vertices[1].Y),
                                                 new Point((int)grid[i].vertices[2].X, (int)grid[i].vertices[2].Y)};
                //var triangle = new List<Point> { new Point((int)Math.Round(grid[i].vertices[0].X), (int)Math.Round(grid[i].vertices[0].Y)),
                //                                 new Point((int)Math.Round(grid[i].vertices[1].X), (int)Math.Round(grid[i].vertices[1].Y)),
                //                                 new Point((int)Math.Round(grid[i].vertices[2].X), (int)Math.Round(grid[i].vertices[2].Y))};
                var gen = new ColorGenerator(grid[i]);

                FillTriangle(triangle, gen);

            }

        }

        public void FillTriangle(List<Point> triangle, ColorGenerator colorGenerator)
        {
            var scanLine = new ScanLine(triangle);
            //var generator = GetGenerator(triangle);
            foreach (var (xList, y) in scanLine.GetIntersectionPoints())
            {
                FillRow(xList, y, colorGenerator);
            }
        }

        private void FillRow(List<int> xList, int y, ColorGenerator colorGenerator)
        {
            Color color = Color.Yellow;
            //var gen = new ColorGenerator(null);
            //int rowShift = y * bitmap.BackBufferStride;
            for (int i = 0; i < xList.Count - 1; ++i)
            {
                //int currShift = rowShift + xList[i] * 4;
                //IntPtr pBackBuffer = bitmap.BackBuffer + currShift;
                int endCol = Math.Min(xList[i + 1], bitmap.Width);
                //if (xList[i] == Geometry.Infinity)
                //{


                //}
                //else
                for (int x = xList[i]; x < endCol; ++x)
                {

                    //bitmap.SetPixel(x, y, color);
                    bitmap.SetPixel(x, y, colorGenerator.ComputeColor(x, y));
                    //var (R, G, B) = GetColors(colorGenerator, currShift, x, y);
                    //byte A = buffer[currShift + 3];
                    //unsafe
                    //{
                    //*((int*)pBackBuffer) = (A << 24) | (R << 16) | (G << 8) | B;
                    //pBackBuffer += 4;
                    //}
                    //currShift += 4;
                }
                //color = Color.Red;
            }
        }
    }
}
