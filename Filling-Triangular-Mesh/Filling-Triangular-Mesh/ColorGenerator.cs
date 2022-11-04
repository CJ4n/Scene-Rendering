﻿using ObjLoader.Loader.Data;
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
        int m;
        MyColor lightColor;
        Vector3 lightSourceVersor;
        //MyColor objectColor;
        MyColor[,] texture;
        Vector3 V;
        Vector3 v1Color;
        Vector3 v2Color;
        Vector3 v3Color;
        MyFace face;
        bool interpolateNormalVector;
        public ColorGenerator(MyFace face, float ks, float kd, int m, bool interpolateNormalVector, Vector3 lightSourceVector, MyColor[,] texture)
        {
            this.kd = kd;
            this.ks = ks;
            this.m = m;
            this.lightColor = new MyColor(1, 1, 1);
            this.lightSourceVersor = PointGeometry.Normalize(lightSourceVector);
            //this.objectColor = new MyColor(1, 1, 0.8);
            this.V = new Vector3(0, 0, 1);
            this.face = face;
            this.interpolateNormalVector = interpolateNormalVector;
            this.texture = texture;
            this.v1Color = GetColorInVetex(face.normals[0], 0);
            this.v2Color = GetColorInVetex(face.normals[1], 1);
            this.v3Color = GetColorInVetex(face.normals[2], 2);
        }

        public Vector3 GetColorInVetex(Vector3 normalVersor, int idx)
        {
            double cosVR;
            Vector3 R = 2 * PointGeometry.DotProduct(normalVersor, lightSourceVersor) * (normalVersor - lightSourceVersor);
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
            double cosNL = PointGeometry.CosBetweenVectors(normalVersor, lightSourceVersor);
            if (cosNL < 0)
            {
                cosNL = 0;
            }
            //var objectColor = new MyColor(0, 0, 1);
            var objectColor = texture[(int)face.vertices[idx].X, (int)face.vertices[idx].Y];
            double r = kd * lightColor.R * objectColor.R * cosNL + ks * lightColor.R * objectColor.R * Math.Pow(cosVR, m);
            double g = kd * lightColor.G * objectColor.G * cosNL + ks * lightColor.G * objectColor.G * Math.Pow(cosVR, m);
            double b = kd * lightColor.B * objectColor.B * cosNL + ks * lightColor.B * objectColor.B * Math.Pow(cosVR, m);
            return new Vector3(r, g, b);
        }
        public Color ComputeColor(int x, int y)
        {
            MyColor myColor;
            if (interpolateNormalVector)
            {
                //Vector3 normalVector = BarycentricInterpolation(face.normals[0], face.normals[1], face.normals[2], x, y);
                //myColor = GetColorInVetex(normalVector);
                //myColor = GetColorInVetex(normalVector);
                double cosVR;
                Vector3 normalVector = BarycentricInterpolation(face.normals[0], face.normals[1], face.normals[2], x, y);
                Vector3 R = 2 * PointGeometry.DotProduct(PointGeometry.Normalize(normalVector), lightSourceVersor) *
                    (PointGeometry.Normalize(normalVector) - lightSourceVersor);
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
                double cosNL = PointGeometry.CosBetweenVectors(PointGeometry.Normalize(normalVector), lightSourceVersor);
                if (cosNL < 0)
                {
                    cosNL = 0;
                }
                var objectColor = texture[x, y];
                double r = kd * lightColor.R * objectColor.R * cosNL + ks * lightColor.R * objectColor.R * Math.Pow(cosVR, m);
                double g = kd * lightColor.G * objectColor.G * cosNL + ks * lightColor.G * objectColor.G * Math.Pow(cosVR, m);
                double b = kd * lightColor.B * objectColor.B * cosNL + ks * lightColor.B * objectColor.B * Math.Pow(cosVR, m);
                myColor = new MyColor(r, g, b);
            }
            else
            {
                myColor = BarycentricInterpolation(v1Color, v2Color, v3Color, x, y);
            }



            Color color = Color.FromArgb((int)(myColor.R * 255), (int)(myColor.G * 255), (int)(myColor.B * 255));
            return color;
        }

        //public Color ComputeColor(int x, int y)
        //{
        //    MyColor myColor;
        //    if (interpolateNormalVector)
        //    {
        //        //Vector3 normalVector = BarycentricInterpolation(face.normals[0], face.normals[1], face.normals[2], x, y);
        //        //myColor = GetColorInVetex(normalVector);
        //        double cosVR;
        //        Vector3 normalVector = BarycentricInterpolation(face.normals[0], face.normals[1], face.normals[2], x, y);
        //        Vector3 R = 2 * PointGeometry.DotProduct(PointGeometry.Normalize(normalVector), lightSourceVersor) *
        //            (PointGeometry.Normalize(normalVector) - lightSourceVersor);
        //        if (R.X == 0 && R.Y == 0 && R.Z == 0)
        //        {
        //            cosVR = 1;
        //        }
        //        else
        //        {
        //            cosVR = PointGeometry.CosBetweenVectors(V, R);
        //        }
        //        if (cosVR < 0)
        //        {
        //            cosVR = 0;
        //        }
        //        double cosNL = PointGeometry.CosBetweenVectors(PointGeometry.Normalize(normalVector), lightSourceVersor);
        //        if (cosNL < 0)
        //        {
        //            cosNL = 0;
        //        }
        //        var objectColor = texture[x, y];
        //        double r = kd * lightColor.R * objectColor.R * cosNL + ks * lightColor.R * objectColor.R * Math.Pow(cosVR, m);
        //        double g = kd * lightColor.G * objectColor.G * cosNL + ks * lightColor.G * objectColor.G * Math.Pow(cosVR, m);
        //        double b = kd * lightColor.B * objectColor.B * cosNL + ks * lightColor.B * objectColor.B * Math.Pow(cosVR, m);
        //        myColor = new MyColor(r, g, b);
        //    }
        //    else
        //    {
        //        myColor = BarycentricInterpolation(v1Color, v2Color, v3Color, x, y);
        //    }



        //    Color color = Color.FromArgb((int)(myColor.R * 255), (int)(myColor.G * 255), (int)(myColor.B * 255));
        //    return color;
        //}

        public Vector3 BarycentricInterpolation(Vector3 v1, Vector3 v2, Vector3 v3, int x, int y)
        {
            double z = FindIntersectionOfPlaneAndLine(face.vertices[0], face.vertices[1], face.vertices[2], x, y);
            Vector3 v = new Vector3(x, y, z);
            var area12 = TrinagelArea(face.vertices[0] - v, face.vertices[1] - v);
            var area23 = TrinagelArea(face.vertices[1] - v, face.vertices[2] - v);
            var area31 = TrinagelArea(face.vertices[2] - v, face.vertices[0] - v);
            var sum = area12 + area23 + area31;
            var area = TrinagelArea(face.vertices[0] - face.vertices[2], face.vertices[1] - face.vertices[2]);
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
            Vector3 v = new Vector3(a2 * b3 - b2 * a3, a1 * b3 - b1 * a3, a1 * b2 - b1 * a2);
            return PointGeometry.Magnitude(v) / 2;
        }

        // Only 'z' is returned
        // Find intersestion of plane (v1,v2,v3) - vertices, and line defined by two points (x0,y0,0) and (x0,y0,1), that is perpendiuclar to (X,Y) plane
        public double FindIntersectionOfPlaneAndLine(Vector3 v1, Vector3 v2, Vector3 v3, int x0, int y0)
        {
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
            if (W == 0) return 1000;
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
