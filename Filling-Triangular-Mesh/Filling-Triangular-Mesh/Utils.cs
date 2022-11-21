using ObjLoader.Loader.Data;

namespace Filling_Triangular_Mesh
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
        public static bool AreTwoDoublesClose(double a, double b)
        {
            return Math.Abs(a - b) < Utils.Eps;
        }
        public static Vector3 CrossProduct(Vector3 v1, Vector3 v2)
        {
            double a1 = v1.X;
            double a2 = v1.Y;
            double a3 = v1.Z;
            double b1 = v2.X;
            double b2 = v2.Y;
            double b3 = v2.Z;
            Vector3 v = new Vector3(a2 * b3 - b2 * a3, a1 * b3 - b1 * a3, a1 * b2 - b1 * a2);
            return v;
        }
        public static double Slope(PointF p1, PointF p2)
        {
            return Math.Abs(p1.X - p2.X) < Utils.Eps ? Utils.Infinity : (double)(p2.Y - p1.Y) / (double)(p2.X - p1.X);
        }
        public static double TrinagelArea(Vector3 v1, Vector3 v2, Vector3 p)
        {
            double Z = (v1.X - p.X) * (v2.Y - p.Y) - (v2.X - p.X) * (v1.Y - p.Y);
            return Math.Sqrt(Z * Z) / 2;
        }
    }
}

