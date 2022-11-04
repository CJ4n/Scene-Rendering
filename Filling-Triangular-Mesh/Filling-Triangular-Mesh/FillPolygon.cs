//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Media.Media3D;
using ObjLoader.Loader.Data;
namespace Filling_Triangular_Mesh
{
    public class FillPolygon
    {
        private Bitmap bitmap;
        private MyColor[,] texture;
        int bitmapWidth, bitmapStride;

        private readonly byte[] buffer;
        private readonly List<MyFace> grid;
        System.Drawing.Imaging.BitmapData bitmapData;

        public FillPolygon(Bitmap bitmap, List<MyFace> grid, MyColor[,] texture)
        {
            this.bitmap = bitmap;
            this.grid = grid;
            this.texture = texture;
        }

        public void FillGridWithTriangles(float ks, float kd, int m, bool interpolateNormalVector, Vector3 lightSource)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            bitmapData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr ptr = bitmapData.Scan0;

            int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            bitmapWidth = bitmap.Width;
            bitmapStride = bitmapData.Stride;
            //Parallel.For(0, grid.Count, i =>
            //{
            //    var triangle = new List<Point> { new Point((int)grid[i].vertices[0].X, (int)grid[i].vertices[0].Y),
            //                                     new Point((int)grid[i].vertices[1].X, (int)grid[i].vertices[1].Y),
            //                                     new Point((int)grid[i].vertices[2].X, (int)grid[i].vertices[2].Y)};
            //    var gen = new ColorGenerator(grid[i], ks, kd, m, interpolateNormalVector, lightSource, texture);
            //    FillTriangle(triangle, gen, rgbValues);
            //});


            for (int i = 0; i < grid.Count; ++i)
            {
                var triangle = new List<Point> { new Point((int)grid[i].vertices[0].X, (int)grid[i].vertices[0].Y),
                                                 new Point((int)grid[i].vertices[1].X, (int)grid[i].vertices[1].Y),
                                                 new Point((int)grid[i].vertices[2].X, (int)grid[i].vertices[2].Y)};
                var gen = new ColorGenerator(grid[i], ks, kd, m, interpolateNormalVector, lightSource, texture);
                FillTriangle(triangle, gen, rgbValues);
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(bitmapData);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(bitmap, 0, 0);
        }

        public void FillTriangle(List<Point> triangle, ColorGenerator colorGenerator, byte[] rgbValues)
        {
            var scanLine = new ScanLine(triangle);

            //Parallel.ForEach(scanLine.GetIntersectionPoints(), a =>
            //{

            //    FillRow(a.xList, a.y, colorGenerator, rgbValues);
            //});
            foreach (var (xList, y) in scanLine.GetIntersectionPoints())
            {
                FillRow(xList, y, colorGenerator, rgbValues);
            }
        }
        private void FillRow(List<int> xList, int y, ColorGenerator colorGenerator, byte[] rgbValues)
        {
            //int p = y * bitmapData.Stride;
            int p = y * bitmapStride;
            for (int i = 0; i < xList.Count - 1; ++i)
            {
                //int endCol = Math.Min(xList[i + 1], bitmap.Width);
                int endCol = Math.Min(xList[i + 1], bitmapWidth);
                p += xList[i] * 3;
                for (int x = xList[i]; x < endCol; ++x)
                {

                    Color color = colorGenerator.ComputeColor(x, y);
                    rgbValues[p] = color.R;
                    rgbValues[p + 1] = color.G;
                    rgbValues[p + 2] = color.B;
                    p += 3;

                    //bitmap.SetPixel(x, y, colorGenerator.ComputeColor(x, y));
                }
            }
        }
    }
}
