using ObjLoader.Loader.Data.VertexData;
namespace ObjLoader.Loader.Data
{
    public class Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
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