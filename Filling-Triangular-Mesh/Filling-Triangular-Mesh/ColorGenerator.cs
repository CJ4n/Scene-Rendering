using ObjLoader.Loader.Data;
namespace Filling_Triangular_Mesh
{
    public struct MyFace
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
        double kd;
        double ks;
        MyColor lightColor;
        MyColor objectColor;
        int m;
        Vector3 V;
        Vector3 R;
        Vector3 lightVersor;
        Vector3 v1Color;
        Vector3 v2Color;
        Vector3 v3Color;
        List<Vector3> vertices;
        MyFace f;

        //List<MyColor> colorsInVertices;
        public ColorGenerator(MyFace face)
        {
            m = 100;
            lightColor = new MyColor(0, 1, 0);
            objectColor = new MyColor(1, 1, 0.8);
            //lightVersor = PointGeometry.Normalize(new Vector3(0, 1, 0));
            lightVersor = new Vector3(0, -1, 0);
            //lightVersor = new Vector3(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2);
            V = new Vector3(0, 0, 1);
            kd = 0.7;
            ks = 0.3;
            if (face.ids[0] == 53 && face.ids[1] == 60 && face.ids[2] == 61)
            {
                int asdads = 3;
            }
            v1Color = GetColorInVetex(face.normals[0]);
            v2Color = GetColorInVetex(face.normals[1]);
            v3Color = GetColorInVetex(face.normals[2]);
            vertices = face.vertices;
            f = face;
        }

        public Vector3 GetColorInVetex(Vector3 normalVersor)
        {
            double cosVR;
            //Vector3 normalVersor = PointGeometry.Normalize(normalVector);
            Vector3 R = 2 * PointGeometry.DotProduct(normalVersor, lightVersor) * (normalVersor - lightVersor);
            if (R.X == 0 && R.Y == 0 && R.Z == 0)
            {
                cosVR = 1;
            }
            else
            {
                cosVR = PointGeometry.CosBetweenVectors(V, R);

            }
            if (cosVR < 0)
            {
                cosVR = 0;
            }
            double cosNL = PointGeometry.CosBetweenVectors(normalVersor, lightVersor);
            if (cosNL < 0)
            {
                cosNL = 0;
            }
            double r = kd * lightColor.R * objectColor.R * cosNL + ks * lightColor.R * objectColor.R * Math.Pow(cosVR, m);
            double g = kd * lightColor.G * objectColor.G * cosNL + ks * lightColor.G * objectColor.G * Math.Pow(cosVR, m);
            double b = kd * lightColor.B * objectColor.B * cosNL + ks * lightColor.B * objectColor.B * Math.Pow(cosVR, m);
            return new Vector3(r, g, b);
        }
        public Color ComputeColor(int x, int y)
        {
            //Vector3 v1Color = GetColorInVetex(v1);
            //Vector3 v2Color = GetColorInVetex(v2);
            //Vector3 v3Color = GetColorInVetex(v3);
            MyColor myColor = BarycentricInterpolation(v1Color, v2Color, v3Color, x, y);
            int aaa = 3;
            Color color = Color.FromArgb((int)(myColor.R * 255), (int)(myColor.G * 255), (int)(myColor.B * 255));
            return color;
        }

        public Vector3 BarycentricInterpolation(Vector3 v1, Vector3 v2, Vector3 v3, int x, int y)
        {
            // trzeba by jakos zainterpolowca Z
            //double z = fun(v1, v2, v3, x, y);            
            double z = FindIntersectionOfPlaneAndLine(vertices[0], vertices[1], vertices[2], x, y);
            Vector3 v = new Vector3(x, y, z);
            //var cos12 = PointGeometry.DotProduct(v1-v, v2-v);// wzgledten (x,y,z)
            //var cos23 = PointGeometry.DotProduct(v2 - v, v3 - v);
            //var cos31 = PointGeometry.DotProduct(v3 - v, v1 - v);
            var area12 = TrinagelArea(vertices[0] - v, vertices[1] - v);
            var area23 = TrinagelArea(vertices[1] - v, vertices[2] - v);
            var area31 = TrinagelArea(vertices[2] - v, vertices[0] - v);
            var sum = area12 + area23 + area31;
            var area = TrinagelArea(vertices[0] - vertices[2], vertices[1] - vertices[2]);
            // why sum!=area????
            //var res = Geometry.IsPointInsidePolygon(new Point(x, y), new List<Point> { new Point((int)vertices[0].X+1, (int)vertices[0].Y+1),
            //new Point((int)vertices[1].X+1, (int)vertices[1].Y+1),
            //new Point((int)vertices[2].X+1, (int)vertices[2].Y+1)});
            //sum = area;
            return area12 * v3 / sum + area23 * v1 / sum + area31 * v2 / sum;
        }
        public double TrinagelArea(Vector3 v1, Vector3 v2)
        {
            double a1 = v1.X;
            double a2 = v1.Y;
            double a3 = v1.Z;
            double b1 = v2.X;
            double b2 = v2.Y;
            double b3 = v2.Z;
            //double x3 = v3.X;
            //double y3 = v3.Y;
            //double z3 = v3.Z;
            //return (x1 * y2 * z3) + (x2 * y3 * z1) + (x3 * y1 * z2) - (z1 * y2 * x3) - (z2 * y3 * x1) - (z3 * y1 * x2);
            Vector3 v = new Vector3(a2 * b3 - b2 * a3, a1 * b3 - b1 * a3, a1 * b2 - b1 * a2);
            return PointGeometry.Magnitude(v) / 2;
        }
        public double FindIntersectionOfPlaneAndLine(Vector3 v1, Vector3 v2, Vector3 v3, int x0, int y0)
        {// Only 'z' is returned
            double x1 = v1.X;
            double y1 = v1.Y;
            double z1 = v1.Z;
            double x2 = v2.X;
            double y2 = v2.Y;
            double z2 = v2.Z;
            double x3 = v3.X;
            double y3 = v3.Y;
            double z3 = v3.Z;



            double W = (x1 * y2 * z3) + (x2 * y3 * z1) + (x3 * y1 * z2) - (z1 * y2 * x3) - (z2 * y3 * x1) - (z3 * y1 * x2);
            //if (W == 0) return 0;
            double WA = (y2 * z3) + (y3 * z1) + (y1 * z2) - (z1 * y2) - (z2 * y3) - (z3 * y1);
            double WB = (x1 * z3) + (x2 * z1) + (x3 * z2) - (z1 * x3) - (z2 * x1) - (z3 * x2);
            double WC = (x1 * y2) + (x2 * y3) + (x3 * y1) - (y2 * x3) - (y3 * x1) - (y1 * x2);
            double A = WA / W;
            double B = WB / W;
            double C = WC / W;

            int a1 = x0;
            int b1 = y0;
            int c1 = 0;
            int a2 = x0;
            int b2 = y0;
            int c2 = 1;
            double t = (-A * a1 - B * b1 + 1) / C / c2;

            double x = a1 + (a2 - a1) * t;
            double y = b1 + (b2 - b1) * t;
            double z = c1 + (c2 - c1) * t;
            return z;
        }

    }
}
