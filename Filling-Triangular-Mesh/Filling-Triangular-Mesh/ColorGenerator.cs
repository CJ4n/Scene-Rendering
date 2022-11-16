using ObjLoader.Loader.Data;

namespace Filling_Triangular_Mesh
{
    public class ColorGenerator
    {
        private MyFace _face;
        private double _ks;
        private double _kd;
        private int _m;
        private bool interpolateNormalVector;
        private Vector3 lightSourceVector;
        private MyColor[,] colorMap;
        private MyColor lightColor;
        private Vector3[,] normalMap;
        private Vector3 V;
        private Vector3 v1Color;
        private Vector3 v2Color;
        private Vector3 v3Color;
        public ColorGenerator(MyFace face, float ks, float kd, int m, bool interpolateNormalVector,
            Vector3 lightSourceVector, MyColor[,] colorMap, Color lightColor, Vector3[,] normalMap = null)
        {
            this._face = face;
            this._ks = ks;
            this._kd = kd;
            this._m = m;
            this.interpolateNormalVector = interpolateNormalVector;
            this.lightSourceVector = lightSourceVector;
            this.colorMap = colorMap;
            this.lightColor = new MyColor(lightColor.R / 255.0, lightColor.G / 255.0, lightColor.B / 255.0);
            this.normalMap = normalMap;
            this.V = new Vector3(0, 0, 1);

            this.v1Color = GetColorInVetex(0);
            this.v2Color = GetColorInVetex(1);
            this.v3Color = GetColorInVetex(2);
        }

        private Vector3 ModifyNormalVector(Vector3 normalVersor, int x, int y)
        {
            Vector3 Ntextrue = normalMap[x, y];
            Utils.Normalize(Ntextrue);
            Vector3 B;
            if (Utils.AreTwoDoublesClose(normalVersor.X, 0) && Utils.AreTwoDoublesClose(normalVersor.Y, 0)
                && Utils.AreTwoDoublesClose(normalVersor.Z, 1))
            {
                B = new Vector3(0, 1, 0);
            }
            else
            {
                B = Utils.CrossProduct(normalVersor, new Vector3(0, 0, 1));
            }
            Utils.Normalize(B);

            Vector3 T = Utils.CrossProduct(B, normalVersor);
            Utils.Normalize(T);

            double X = T.X * Ntextrue.X + B.X * Ntextrue.Y + normalVersor.X * Ntextrue.Z;
            double Y = T.Y * Ntextrue.X + B.Y * Ntextrue.Y + normalVersor.Y * Ntextrue.Z;
            double Z = T.Z * Ntextrue.X + B.Z * Ntextrue.Y + normalVersor.Z * Ntextrue.Z;

            Vector3 N = new Vector3(X, Y, Z);
            Utils.Normalize(N);
            return N;
        }

        public Vector3 GetColorInVetex(int idx)
        {
            Vector3 L = lightSourceVector - new Vector3(_face.vertices[idx].X, _face.vertices[idx].Y, _face.vertices[idx].Z);
            Utils.Normalize(L);

            Vector3 normalVersor = _face.normals[idx];
            Utils.Normalize(normalVersor);

            if (normalMap != null)
            {
                normalVersor = ModifyNormalVector(normalVersor, (int)_face.vertices[idx].X, (int)_face.vertices[idx].Y);
            }
            Vector3 R = 2 * Utils.DotProduct(normalVersor, L) * normalVersor - L;
            //R = Utils.Normalize(R);

            double cosVR = Math.Max(0, Utils.CosBetweenVersors(V, R));
            double cosNL = Math.Max(0, Utils.CosBetweenVersors(normalVersor, L));

            var objectColor = colorMap[(int)_face.vertices[idx].X, (int)_face.vertices[idx].Y];

            double r = _kd * lightColor.R * objectColor.R * cosNL + _ks * lightColor.R * objectColor.R * Math.Pow(cosVR, _m);
            double g = _kd * lightColor.G * objectColor.G * cosNL + _ks * lightColor.G * objectColor.G * Math.Pow(cosVR, _m);
            double b = _kd * lightColor.B * objectColor.B * cosNL + _ks * lightColor.B * objectColor.B * Math.Pow(cosVR, _m);
            return new Vector3(r, g, b);
        }

        private Color ComputeColorInterpolateNormalVector(int x, int y)
        {
            Vector3 XYZ = BarycentricInterpolation(_face.vertices[0], _face.vertices[1], _face.vertices[2], x, y);

            Vector3 L = lightSourceVector - new Vector3(x, y, XYZ.Z);
            Utils.Normalize(L);

            Vector3 normalVector = BarycentricInterpolation(_face.normals[0], _face.normals[1], _face.normals[2], x, y);

            Utils.Normalize(normalVector);
            if (normalMap != null)
            {
                normalVector = ModifyNormalVector(normalVector, x, y);
            }

            Vector3 R = 2 * Utils.DotProduct(normalVector, L) * normalVector - L;
            //Utils.Normalize(R);

            double cosVR = Math.Max(0, Utils.CosBetweenVersors(V, R));
            double cosNL = Math.Max(0, Utils.CosBetweenVersors(normalVector, L));

            var objectColor = colorMap[x, y];
            double r = _kd * lightColor.R * objectColor.R * cosNL + _ks * lightColor.R * objectColor.R * Math.Pow(cosVR, _m);
            double g = _kd * lightColor.G * objectColor.G * cosNL + _ks * lightColor.G * objectColor.G * Math.Pow(cosVR, _m);
            double b = _kd * lightColor.B * objectColor.B * cosNL + _ks * lightColor.B * objectColor.B * Math.Pow(cosVR, _m);
            MyColor myColor = new MyColor(r, g, b);
            Color color = Color.FromArgb(255, (int)(myColor.R * 255), (int)(myColor.G * 255), (int)(myColor.B * 255));
            return color;
        }
        private Color ComputeColorInterpolateColor(int x, int y)
        {
            MyColor myColor = BarycentricInterpolation(v1Color, v2Color, v3Color, x, y);
            Color color = Color.FromArgb(255, (int)(myColor.R * 255), (int)(myColor.G * 255), (int)(myColor.B * 255));
            return color;
        }
        public Color ComputeColor(int x, int y)
        {
            if (interpolateNormalVector)
            {
                return ComputeColorInterpolateNormalVector(x, y);
            }
            else
            {
                return ComputeColorInterpolateColor(x, y);
            }
        }
        public Vector3 BarycentricInterpolation(Vector3 v1, Vector3 v2, Vector3 v3, int x, int y)
        {
            Vector3 v = new Vector3(x, y, 0);
            var area12 = TrinagelArea(_face.vertices[0] - v, _face.vertices[1] - v);
            var area23 = TrinagelArea(_face.vertices[1] - v, _face.vertices[2] - v);
            var area31 = TrinagelArea(_face.vertices[2] - v, _face.vertices[0] - v);
            var sum = area12 + area23 + area31;
            return area12 * v3 / sum + area23 * v1 / sum + area31 * v2 / sum;
        }
        public double TrinagelArea(Vector3 v1, Vector3 v2)
        {
            double Z = v1.X * v2.Y - v2.X * v1.Y;
            return Math.Sqrt(Z * Z) / 2;
        }
    }
}
