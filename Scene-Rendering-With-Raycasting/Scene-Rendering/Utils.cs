using ObjLoader.Loader.Data;

namespace SceneRendering
{
    public static class Utils
    {
        public const double Eps = 1e-8;
        public const double Infinity = 1 / Eps;
        public static double DotProduct(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static double CosBetweenVersors(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }
        public static double Magnitude(Vector3 v)
        {
            return Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }
        public static void Normalize(Vector3 v)
        {
            var magnitude = Magnitude(v);
            if (magnitude == 0)
            {
                return;
            }
            v.X /= magnitude;
            v.Y /= magnitude;
            v.Z /= magnitude;
        }
        //public static bool AreTwoDoublesClose(double a, double b)
        //{
        //    return Math.Abs(a - b) < Utils.Eps;
        //}
        //public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
        //{
        //    double a1 = v1.X;
        //    double a2 = v1.Y;
        //    double a3 = v1.Z;
        //    double b1 = v2.X;
        //    double b2 = v2.Y;
        //    double b3 = v2.Z;
        //    return new Vector3(a2 * b3 - b2 * a3, a1 * b3 - b1 * a3, a1 * b2 - b1 * a2); ;
        //}
        public static double Slope(Vector3 p1, Vector3 p2)
        {
            return Math.Abs((int)p1.X - (int)p2.X) < Utils.Eps ? Utils.Infinity : (double)((int)p2.Y - (int)p1.Y) / (double)((int)p2.X - (int)p1.X);
        }
        public static double TrinagelArea(Vector3 v1, Vector3 v2, Vector3 p)
        {
            double Z = (v1.X - p.X) * (v2.Y - p.Y) - (v2.X - p.X) * (v1.Y - p.Y);
            return Math.Sqrt(Z * Z) / 2;
        }

        //public static MyColor[,] ConvertBitmapToArray(Bitmap bitmap)
        //{
        //    MyColor[,] result = new MyColor[bitmap.Width, bitmap.Height];
        //    for (int x = 0; x < bitmap.Width; x++)
        //    {
        //        for (int y = 0; y < bitmap.Height; y++)
        //        {
        //            var color = bitmap.GetPixel(x, y);
        //            result[x, y] = new MyColor(color.R / 255.0, color.G / 255.0, color.B / 255.0);
        //        }
        //    }
        //    return result;
        //}
        //public static Vector3 RgbToNormalVectorsArray(Color c)
        //{
        //    return new Vector3(2.0 * c.R / 255.0, 2.0 * c.G / 255.0, c.B / 255.0);
        //}


    }
}

