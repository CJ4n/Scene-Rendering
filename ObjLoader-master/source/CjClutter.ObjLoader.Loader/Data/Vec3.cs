using ObjLoader.Loader.Data.VertexData;
namespace ObjLoader.Loader.Data
{
    public struct Vector3
    {


        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Vector3(double x, double y, double z) : this()
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector3 operator *(double s, Vector3 v)
        {
            return new Vector3(s * v.X, s * v.Y, s * v.Z);
        }
        public static implicit operator Vector3(Normal x)
        {
            return new Vector3(x.X, x.Y, x.Z);
        }
        public static implicit operator Vector3(Vertex x)
        {
            return new Vector3(x.X, x.Y, x.Z);
        }


    }
}