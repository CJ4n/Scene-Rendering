using ObjLoader.Loader.Data;

namespace Filling_Triangular_Mesh
{
    public class ColorGenerator
    {
        private MyFace _face;
        private double _ks;
        private double _kd;
        private int _m;
        private bool _interpolateNormalVector;
        private Vector3 _lightSourcePoint;
        private MyColor[,] _colorMap;
        private MyColor _lightColor;
        private Vector3[,] _normalMap;
        private Vector3 _V;
        private Vector3 _v1Color; // color in vertex 1
        private Vector3 _v2Color; // color in vertex 2
        private Vector3 _v3Color; // color in vertex 3
        private Vector3 _R; // for memrory allocation optimazation purpose
        private Vector3 _L; // for memrory allocation optimazation purpose

        public ColorGenerator(MyFace face, float ks, float kd, int m, bool interpolateNormalVector,
            Vector3 lightSourceVector, MyColor[,] colorMap, Color lightColor, Vector3[,] normalMap = null)
        {
            this._face = face;
            this._ks = ks;
            this._kd = kd;
            this._m = m;
            this._interpolateNormalVector = interpolateNormalVector;
            this._lightSourcePoint = lightSourceVector;
            this._colorMap = colorMap;
            this._lightColor = new MyColor(lightColor.R / 255.0, lightColor.G / 255.0, lightColor.B / 255.0);
            this._normalMap = normalMap;
            this._V = new Vector3(0, 0, 1);
            this._L = new Vector3(0, 0, 0);
            this._R = new Vector3(0, 0, 0);
            this._v1Color = GetColorInVetex(0);
            this._v2Color = GetColorInVetex(1);
            this._v3Color = GetColorInVetex(2);
            Utils.Normalize(_face.normals[0]);
            Utils.Normalize(_face.normals[1]);
            Utils.Normalize(_face.normals[2]);
        }
        public Color ComputeColor(int x, int y)
        {
            if (_interpolateNormalVector)
            {
                return ComputeColorInterpolateNormalVector(x, y);
            }
            else
            {
                return ComputeColorInterpolateColor(x, y);
            }
        }
        private Vector3 ModifyNormalVector(Vector3 normalVersor, int x, int y)
        {
            Vector3 Ntextrue = _normalMap[x, y];
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
        private Vector3 GetColorInVetex(int idx)
        {
            _L.X = _lightSourcePoint.X - _face.vertices[idx].X;
            _L.Y = _lightSourcePoint.Y - _face.vertices[idx].Y;
            _L.Z = _lightSourcePoint.Z - _face.vertices[idx].Z;
            Utils.Normalize(_L);

            Vector3 normalVersor = _face.normals[idx];

            if (_normalMap != null)
            {
                normalVersor = ModifyNormalVector(normalVersor, (int)_face.vertices[idx].X, (int)_face.vertices[idx].Y);
            }
            double dotProduct = Utils.DotProduct(normalVersor, _L);
            _R.X = 2 * dotProduct * normalVersor.X - _L.X;
            _R.Y = 2 * dotProduct * normalVersor.Y - _L.Y;
            _R.Z = 2 * dotProduct * normalVersor.Z - _L.Z;
            //R = Utils.Normalize(R);

            double cosVR = Math.Max(0, Utils.CosBetweenVersors(_V, _R));
            double cosNL = Math.Max(0, Utils.CosBetweenVersors(normalVersor, _L));

            var objectColor = _colorMap[(int)_face.vertices[idx].X, (int)_face.vertices[idx].Y];

            double r = _kd * _lightColor.R * objectColor.R * cosNL + _ks * _lightColor.R * objectColor.R * Math.Pow(cosVR, _m);
            double g = _kd * _lightColor.G * objectColor.G * cosNL + _ks * _lightColor.G * objectColor.G * Math.Pow(cosVR, _m);
            double b = _kd * _lightColor.B * objectColor.B * cosNL + _ks * _lightColor.B * objectColor.B * Math.Pow(cosVR, _m);
            return new Vector3(r, g, b);
        }
        private Color ComputeColorInterpolateNormalVector(int x, int y)
        {
            Vector3 XYZ = BarycentricInterpolation(_face.vertices[0], _face.vertices[1], _face.vertices[2], x, y);

            _L.X = _lightSourcePoint.X - (double)x;
            _L.Y = _lightSourcePoint.Y - (double)y;
            _L.Z = _lightSourcePoint.Z - (double)XYZ.Z;
            Utils.Normalize(_L);

            Vector3 normalVector = BarycentricInterpolation(_face.normals[0], _face.normals[1], _face.normals[2], x, y);
            Utils.Normalize(normalVector);
            if (_normalMap != null)
            {
                normalVector = ModifyNormalVector(normalVector, x, y);
            }
            double dotProduct = Utils.DotProduct(normalVector, _L);
            _R.X = 2 * dotProduct * normalVector.X - _L.X;
            _R.Y = 2 * dotProduct * normalVector.Y - _L.Y;
            _R.Z = 2 * dotProduct * normalVector.Z - _L.Z;
            //Utils.Normalize(_R);

            double cosVR = Math.Max(0, Utils.CosBetweenVersors(_V, _R));
            double cosNL = Math.Max(0, Utils.CosBetweenVersors(normalVector, _L));

            var objectColor = _colorMap[x, y];
            double r = _kd * _lightColor.R * objectColor.R * cosNL + _ks * _lightColor.R * objectColor.R * Math.Pow(cosVR, _m);
            double g = _kd * _lightColor.G * objectColor.G * cosNL + _ks * _lightColor.G * objectColor.G * Math.Pow(cosVR, _m);
            double b = _kd * _lightColor.B * objectColor.B * cosNL + _ks * _lightColor.B * objectColor.B * Math.Pow(cosVR, _m);
            r = Math.Min(r, 1);
            g = Math.Min(g, 1);
            b = Math.Min(b, 1);
            Color color = Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));
            return color;
        }
        private Color ComputeColorInterpolateColor(int x, int y)
        {
            MyColor myColor = BarycentricInterpolation(_v1Color, _v2Color, _v3Color, x, y);
            Color color = Color.FromArgb(255, (int)(myColor.R * 255), (int)(myColor.G * 255), (int)(myColor.B * 255));
            return color;
        }
        private Vector3 BarycentricInterpolation(Vector3 v1, Vector3 v2, Vector3 v3, int x, int y)
        {
            Vector3 v = new Vector3(x, y, 0);
            var area12 = Utils.TrinagelArea(_face.vertices[0], _face.vertices[1], v);
            var area23 = Utils.TrinagelArea(_face.vertices[1], _face.vertices[2], v);
            var area31 = Utils.TrinagelArea(_face.vertices[2], _face.vertices[0], v);
            var sum = area12 + area23 + area31;
            area12 /= sum;
            area23 /= sum;
            area31 /= sum;

            v.X = area12 * v3.X + area23 * v1.X + area31 * v2.X;
            v.Y = area12 * v3.Y + area23 * v1.Y + area31 * v2.Y;
            v.Z = area12 * v3.Z + area23 * v1.Z + area31 * v2.Z;
            return v;
        }
    }
}
