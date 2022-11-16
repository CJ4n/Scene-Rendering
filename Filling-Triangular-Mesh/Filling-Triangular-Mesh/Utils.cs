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
            v.X /= magnitude;
            v.Y /= magnitude;
            v.Z /= magnitude;
            //return new Vector3(v.X / magnitude, v.Y / magnitude, v.Z / magnitude);
        }
        public static bool AreTwoDoublesClose(double a, double b)
        {
            return Math.Abs(a - b) < Utils.Eps;
        }
        public static double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);

            if (cA != rB)
            {
                throw new Exception("wrong matrix dimensions!");
            }
            else
            {
                double temp = 0;
                double[,] kHasil = new double[rA, cB];

                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        kHasil[i, j] = temp;
                    }
                }

                return kHasil;
            }
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
    }
}

