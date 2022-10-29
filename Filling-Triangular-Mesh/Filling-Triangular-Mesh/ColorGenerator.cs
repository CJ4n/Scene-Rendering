using ObjLoader.Loader.Data;
namespace Filling_Triangular_Mesh
{
    public struct MyFace
    {
        public List<Vector3> vertices; // 3 vertices of face
        public List<Vector3> normals; // 3 normals coresponding to vertices

        public MyFace(List<Vector3> vertices, List<Vector3> normals)
        {
            this.vertices = vertices;
            this.normals = normals;
        }
    }
    public struct MyColor
    {
        public double R;
        public double G;
        public double B;
        public MyColor(double r, double g, double b)
        {
            if ((r < 0 || r > 1) || (g < 0 || g > 1) || (b < 0 || b > 1))
            {
                throw new Exception("color outside of range [0,1]");
            }
            R = r;
            G = g;
            B = b;
        }
        public static implicit operator MyColor(Vector3 x)
        {
            return new MyColor(x.X, x.Y, x.Z);
        }
    }

    public class ColorGenerator
    {
        float kd;
        float ks;
        MyColor lightColor;
        MyColor objectColor;
        int m;
        Vector3 V;
        Vector3 R;
        Vector3 lightVersor;
        Vector3 v1Color;
        Vector3 v2Color;
        Vector3 v3Color;
        //List<MyColor> colorsInVertices;
        public ColorGenerator(MyFace face)
        {
            m = 50;
            lightColor = new MyColor(1, 1, 1);
            objectColor = new MyColor(0.5, 0.2, 0.4);
            lightVersor = new Vector3(0, 1, 0);
            V = new Vector3(0, 0, 1);

            Vector3 v1Color = GetColorInVetex(face.vertices[0]);
            Vector3 v2Color = GetColorInVetex(face.vertices[1]);
            Vector3 v3Color = GetColorInVetex(face.vertices[2]);
            //foreach (var face in faces)
            //{
            //    for (int i = 0; i < 3; ++i)
            //    {

            //        Vector3 R = 2 * PointGeometry.DotProduct(face.normals[i], face.vertices[i]) * (face.normals[i] - face.vertices[i]);
            //        double cosVR = PointGeometry.CosBetweenVersors(V, R);
            //        if (cosVR < 0)
            //        {
            //            cosVR = 0;
            //        }
            //        double cosNL = PointGeometry.CosBetweenVersors(face.normals[i], face.vertices[i]);
            //        if (cosNL < 0)
            //        {
            //            cosNL = 0;
            //        }
            //        double r = kd * lightColor.R * objectColor.R * cosNL + ks * lightColor.R * objectColor.R * Math.Pow(cosVR, m);
            //    }
            //}
        }

        public Vector3 GetColorInVetex(Vector3 normalVector)
        {
            Vector3 normalVersor = PointGeometry.Normalize(normalVector);
            Vector3 R = 2 * PointGeometry.DotProduct(normalVersor, lightVersor) * (normalVersor - lightVersor);
            double cosVR = PointGeometry.CosBetweenVersors(V, R);
            if (cosVR < 0)
            {
                cosVR = 0;
            }
            double cosNL = PointGeometry.CosBetweenVersors(normalVersor, lightVersor);
            if (cosNL < 0)
            {
                cosNL = 0;
            }
            double r = kd * lightColor.R * objectColor.R * cosNL + ks * lightColor.R * objectColor.R * Math.Pow(cosVR, m);
            double g = kd * lightColor.G * objectColor.G * cosNL + ks * lightColor.G * objectColor.G * Math.Pow(cosVR, m);
            double b = kd * lightColor.B * objectColor.B * cosNL + ks * lightColor.B * objectColor.B * Math.Pow(cosVR, m);
            return new Vector3(r, g, b);
        }
        public MyColor ComputeColor(int x, int y)
        {
            //Vector3 v1Color = GetColorInVetex(v1);
            //Vector3 v2Color = GetColorInVetex(v2);
            //Vector3 v3Color = GetColorInVetex(v3);
            MyColor color = BarycentricInterpolation(v1Color, v2Color, v3Color, x, y);
            return color;
        }

        public Vector3 BarycentricInterpolation(Vector3 v1, Vector3 v2, Vector3 v3, int x, int y)
        {
            // trzeba by jakos zainterpolowca Z
            var cos12 = PointGeometry.DotProduct(v1, v2);// wzgledten (x,y,z)
            var cos23 = PointGeometry.DotProduct(v2, v3);
            var cos31 = PointGeometry.DotProduct(v3, v1);
            return cos12 * v3 + cos23 * v1 + cos31 * v2;
        }

    }
}
