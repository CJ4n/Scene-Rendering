using ObjLoader.Loader.Data;
namespace Filling_Triangular_Mesh
{
    public static class PointGeometry
    {
        public const double Eps = 1e-8;
        public const double Infinity = 1 / Eps;
        public static double DotProduct(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static double CosBetweenVectors(Vector3 v1, Vector3 v2)
        {

            return DotProduct(v1 / Magnitude(v1), v2 / Magnitude(v2));
        }
        public static double Magnitude(Vector3 v)
        {
            return Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }
        public static Vector3 Normalize(Vector3 v)
        {
            var magnitude = Magnitude(v);
            return new Vector3(v.X / magnitude, v.Y / magnitude, v.Z / magnitude);
        }

        public static double Slope(PointF p1, PointF p2)
        {
            return Math.Abs(p1.X - p2.X) < PointGeometry.Eps ? PointGeometry.Infinity : (double)(p2.Y - p1.Y) / (double)(p2.X - p1.X);
        }
    }
    //static class Geometry
    //{
    //    public const double Eps = 1e-8;
    //    public const double Infinity = 1 / Eps;

    //    public static double Atan2_2PI(int y, int x)
    //    {
    //        double value = Atan2(y, x) * 180 / PI;
    //        return value < 0 ? value + 360 : value;
    //    }

    //    // https://stackoverflow.com/a/14998816/6841224
    //    // The function counts the number of sides of the polygon that:
    //    //  - intersect the Y coordinate of the point (the first if() condition) 
    //    //  - are to the left of it (the second if() condition).
    //    // If the number of such sides is odd, then the point is inside the polygon
    //    public static bool IsPointInsidePolygon(Point p, List<Point> polygon)
    //    {
    //        if (polygon.Count < 3)
    //        {
    //            return true;
    //        }

    //        bool result = false;
    //        int j = polygon.Count - 1;
    //        for (int i = 0; i < polygon.Count; ++i)
    //        {
    //            if ((polygon[j].Y < p.Y && polygon[i].Y >= p.Y) ||
    //                (polygon[i].Y < p.Y && polygon[j].Y >= p.Y))
    //            {
    //                if (polygon[i].X + (p.Y - polygon[i].Y) * (polygon[j].X - polygon[i].X)
    //                    / (double)(polygon[j].Y - polygon[i].Y) < p.X)
    //                {
    //                    result = !result;
    //                }
    //            }
    //            j = i;
    //        }

    //        return result;
    //    }

    //public static void ComputeNormalVector(this Vector3D v)
    //{
    //    v.Normalize();
    //    v.X = 2 * v.X - 1;
    //    v.Y = 2 * v.Y - 1;
    //    v.Z = 2 * v.Z - 1;
    //}
    //}
}

