using ObjLoader.Loader.Data;

namespace SceneRendering
{
    public class MyFace
    {
        public List<Vector3> vertices; // 3 vertices of face
        public List<Vector3> normals; // 3 normals coresponding to vertices
        public List<int> ids;
        public MyFace(List<Vector3> vertices, List<Vector3> normals, List<int> ids)
        {
            this.vertices = vertices;
            this.normals = normals;
            this.ids = ids;
        }
    }
    public struct MyColor
    {
        public double R;
        public double G;
        public double B;
        public MyColor(double r, double g, double b)
        {

            if (r > 1)
            {
                r = 1;
            }
            if (g > 1)
            {
                g = 1;
            }
            if (b > 1)
            {
                b = 1;
            }
            //if ((r < 0 || r > 1) || (g < 0 || g > 1) || (b < 0 || b > 1))
            //{
            //    throw new Exception("color outside of range [0,1]");
            //}
            R = r;
            G = g;
            B = b;
        }
        public static implicit operator MyColor(Vector3 x)
        {
            return new MyColor(x.X, x.Y, x.Z);
        }
    }

    public class SceneObject
    {
        public string Name { get; set; }
        public bool Animatable = false;
        public bool Animate { get; set; }
        public int Scale { get; set; }
        public System.Numerics.Vector3 Offset { get; set; }
        public List<MyFace> FacesCamera { get; set; }
        public List<MyFace> FacesWorld { get; set; }
        public PolygonFiller PolygonFiller { get; set; }
    }

    // 0 - stationary cam, 1 - stationary-tracking object cam, 2 - object following cam
    public class Camera
    {
        public string Name { get; set; }
        static public int CurrentCameraIndex = 0;
        public int TargetObjectIdx = -1;
        public System.Numerics.Vector3 CamPosition { get; set; }
        public System.Numerics.Vector3 CamTarget { get; set; }
    }

    public class Light
    {
        public Vector3 Position { get; set; }
        public bool Enabled { get; set; }
        public bool IsSpotLight { get; set; }
        public Vector3 DirectionOfLight { get; set; }
    }
}
