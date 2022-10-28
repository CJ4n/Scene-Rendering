using static System.Math;
namespace Filling_Triangular_Mesh
{
    public static class PointGeometry
    {
        public static bool ArePointsIntersecting(this Point p1, Point p2)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y)
                //<= Configuration.VertexRadius * Configuration.VertexRadius;
                <= 5.0 * 5.0;
        }

        public static Point Multiply(this Point p, double a)
        {
            return new Point((int)(p.X * a), (int)(p.Y * a));
        }

        public static double Magnitude(this Point p)
        {
            return Math.Sqrt(p.X * p.X + p.Y * p.Y);
        }

        public static Point Difference(this Point p, Point d)
        {
            return new Point(p.X - d.X, p.Y - d.Y);
        }

        public static double Distance(this Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public static double Angle(Point p1, Point p2)
        {
            double xDiff = p2.X - p1.X;
            double yDiff = p2.Y - p1.Y;

            return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
        }

        public static double CrossProduct(Point p1, Point p2)
        {
            return p1.X * p2.Y - p1.Y * p2.X;
        }

        public static double DotProduct(Point p1, Point p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }

        // https://math.stackexchange.com/questions/175896/finding-a-point-along-a-line-https://stackoverflow.com/questions/13302396/given-two-points-find-a-third-point-on-the-line?rq=1a-certain-distance-away-from-another-point/175906
        public static Point SameLinePoint(Point p1, double distanceRatio, Point p2)
        {
            return new Point((int)((1 - distanceRatio) * p1.X + distanceRatio * p2.X), (int)((1 - distanceRatio) * p1.Y + distanceRatio * p2.Y));
        }

        public static (int x, int y) GetIntCoordinates(this Point p)
        {
            return ((int)p.X, (int)p.Y);
        }

        public static double Slope(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) < Geometry.Eps ? Geometry.Infinity : (p2.Y - p1.Y) / (p2.X - p1.X);
        }
    }

    static class Geometry
    {
        public const double Eps = 1e-8;
        public const double Infinity = 1 / Eps;

        public static double Atan2_2PI(int y, int x)
        {
            double value = Atan2(y, x) * 180 / PI;
            return value < 0 ? value + 360 : value;
        }

        // https://stackoverflow.com/a/14998816/6841224
        // The function counts the number of sides of the polygon that:
        //  - intersect the Y coordinate of the point (the first if() condition) 
        //  - are to the left of it (the second if() condition).
        // If the number of such sides is odd, then the point is inside the polygon
        public static bool IsPointInsidePolygon(Point p, List<Point> polygon)
        {
            if (polygon.Count < 3)
            {
                return true;
            }

            bool result = false;
            int j = polygon.Count - 1;
            for (int i = 0; i < polygon.Count; ++i)
            {
                if ((polygon[j].Y < p.Y && polygon[i].Y >= p.Y) ||
                    (polygon[i].Y < p.Y && polygon[j].Y >= p.Y))
                {
                    if (polygon[i].X + (p.Y - polygon[i].Y) * (polygon[j].X - polygon[i].X)
                        / (double)(polygon[j].Y - polygon[i].Y) < p.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }

            return result;
        }

        //public static void ComputeNormalVector(this Vector3D v)
        //{
        //    v.Normalize();
        //    v.X = 2 * v.X - 1;
        //    v.Y = 2 * v.Y - 1;
        //    v.Z = 2 * v.Z - 1;
        //}
    }
}
