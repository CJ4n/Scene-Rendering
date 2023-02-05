using ObjLoader.Loader.Data;

namespace SceneRendering
{
    public class ColorGenerator
    {
        private MyFace _face;
        private MyFace _faceWorld;
        private double _ka;
        private double _ks;
        private double _kd;
        private int _m;
        private List<Light> _lightSourcePoint;
        private MyColor _lightColor;
        private Vector3 _V;
        private Vector3 _v1Color; // color in vertex 1
        private Vector3 _v2Color; // color in vertex 2
        private Vector3 _v3Color; // color in vertex 3
        private Vector3 _R;
        private MyColor _objectColor;
        private Constants.SHADER _shader;
        public ColorGenerator(MyFace face, MyFace faceWorld, float ka, float ks, float kd, int m, Constants.SHADER shader,
             List<Light> lightSource, MyColor color, Color lightColor)
        {
            this._face = face;
            this._faceWorld = faceWorld;
            this._ks = ks;
            this._kd = kd;
            this._m = m;
            this._ka = ka;
            this._shader = shader;
            this._lightSourcePoint = lightSource;
            this._objectColor = color;
            this._lightColor = new MyColor(lightColor.R / 255.0, lightColor.G / 255.0, lightColor.B / 255.0);
            this._R = new Vector3(0, 0, 0);

            //this._V = new Vector3(0, 0, -1);
            double x = (_faceWorld.vertices[0].X + _faceWorld.vertices[1].X + _faceWorld.vertices[2].X) / 3;
            double y = (_faceWorld.vertices[0].Y + _faceWorld.vertices[1].Y + _faceWorld.vertices[2].Y) / 3;
            double z = (_faceWorld.vertices[0].Z + _faceWorld.vertices[1].Z + _faceWorld.vertices[2].Z) / 3;
            this._V = new Vector3(Constants.camPositoin.X - x, Constants.camPositoin.Y - y, Constants.camPositoin.Z - z);
            Utils.Normalize(_V);

            this._v1Color = GetColorInVetex(0);
            this._v2Color = GetColorInVetex(1);
            this._v3Color = GetColorInVetex(2);
            Utils.Normalize(_face.normals[0]);
            Utils.Normalize(_face.normals[1]);
            Utils.Normalize(_face.normals[2]);
        }
        public Color ComputeColor(int x, int y)
        {
            if (_shader == Constants.SHADER.PHONG)
            {
                return PhongeShader(x, y);
            }
            else if (_shader == Constants.SHADER.GOURAUD)
            {
                return GouradudShader(x, y);
            }
            else //if(_shader==Constants.SHADER.CONST)
            {
                return ConstantShader(x, y);
            }
        }

        private Vector3 GetColorInVetex(int idx)
        {
            if ((int)_face.vertices[idx].X < 0 || (int)_face.vertices[idx].Y < 0)
            {
                return new Vector3(0, 0, 0);
            }

            double r = 0, g = 0, b = 0;
            Vector3 _L = new Vector3(0, 0, 0);

            foreach (var light in _lightSourcePoint)
            {
                if (light.Enabled == false)
                {
                    continue;
                }

                _L.X = light.Position.X - _faceWorld.vertices[idx].X;
                _L.Y = light.Position.Y - _faceWorld.vertices[idx].Y;
                _L.Z = light.Position.Z - _faceWorld.vertices[idx].Z;
                Utils.Normalize(_L);

                //if (light.IsSpotLight)
                //{
                //    double cosDL = Math.Max(0, Utils.CosBetweenVersors(-light.DirectionOfLight, _L));
                //    int p = 1;
                //    double _kspotlight = 0.5;
                //    r += _lightColor.R * _objectColor.R * _kspotlight * Math.Pow(cosDL, p);
                //    g += _lightColor.G * _objectColor.G * _kspotlight * Math.Pow(cosDL, p);
                //    b += _lightColor.B * _objectColor.B * _kspotlight * Math.Pow(cosDL, p);
                //    continue;
                //}

                Vector3 normalVersor = _face.normals[idx];

                double dotProduct = Utils.DotProduct(normalVersor, _L);
                _R.X = 2 * dotProduct * normalVersor.X - _L.X;
                _R.Y = 2 * dotProduct * normalVersor.Y - _L.Y;
                _R.Z = 2 * dotProduct * normalVersor.Z - _L.Z;

                double cosVR = Math.Max(0, Utils.CosBetweenVersors(_V, _R));
                double cosVR_m = Math.Pow(cosVR, _m);
                double cosNL = Math.Max(0, Utils.CosBetweenVersors(normalVersor, _L));

                if (light.IsSpotLight)
                {
                    double cosDL = Math.Max(0, Utils.CosBetweenVersors(-light.DirectionOfLight, _L));
                    int p = 1;
                    double cosDL_p = Math.Pow(cosDL, p);
                    r += _lightColor.R * _objectColor.R * (_kd * cosNL + _ks * cosVR_m) * cosDL_p;
                    g += _lightColor.G * _objectColor.G * (_kd * cosNL + _ks * cosVR_m) * cosDL_p;
                    b += _lightColor.B * _objectColor.B * (_kd * cosNL + _ks * cosVR_m) * cosDL_p;
                }
                else
                {
                    r += _lightColor.R * _objectColor.R * (_kd * cosNL + _ks * cosVR_m);
                    g += _lightColor.G * _objectColor.G * (_kd * cosNL + _ks * cosVR_m);
                    b += _lightColor.B * _objectColor.B * (_kd * cosNL + _ks * cosVR_m);
                }
            }
            r = _ka + Constants.LightIntensity * r;
            g = _ka + Constants.LightIntensity * g;
            b = _ka + Constants.LightIntensity * b;

            r = System.Math.Clamp(r, 0, 1);
            g = System.Math.Clamp(g, 0, 1);
            b = System.Math.Clamp(b, 0, 1);
            return new Vector3(r, g, b);
        }
        //ComputeColorInterpolateNormalVector
        private Color PhongeShader(int x, int y)
        {
            Vector3 XYZ = BarycentricInterpolation(_faceWorld.vertices[0], _faceWorld.vertices[1], _faceWorld.vertices[2], x, y);

            double r = 0, g = 0, b = 0;
            Vector3 _L = new Vector3(0, 0, 0);
            foreach (var light in _lightSourcePoint)
            {
                if (light.Enabled == false)
                {
                    continue;
                }
                _L.X = light.Position.X - (double)x;
                _L.Y = light.Position.Y - (double)y;
                _L.Z = light.Position.Z - (double)XYZ.Z;
                Utils.Normalize(_L);

                Vector3 normalVector = BarycentricInterpolation(_face.normals[0], _face.normals[1], _face.normals[2], x, y);
                Utils.Normalize(normalVector);
                double dotProduct = Utils.DotProduct(normalVector, _L);
                _R.X = 2 * dotProduct * normalVector.X - _L.X;
                _R.Y = 2 * dotProduct * normalVector.Y - _L.Y;
                _R.Z = 2 * dotProduct * normalVector.Z - _L.Z;

                double cosVR = Math.Max(0, Utils.CosBetweenVersors(_V, _R));
                double cosVR_m = Math.Pow(cosVR, _m);
                double cosNL = Math.Max(0, Utils.CosBetweenVersors(normalVector, _L));

                if (light.IsSpotLight)
                {
                    double cosDL = Math.Max(0, Utils.CosBetweenVersors(-light.DirectionOfLight, _L));
                    int p = 1;
                    double cosDL_p = Math.Pow(cosDL, p);
                    r += _lightColor.R * _objectColor.R * (_kd * cosNL + _ks * cosVR_m) * cosDL_p;
                    g += _lightColor.G * _objectColor.G * (_kd * cosNL + _ks * cosVR_m) * cosDL_p;
                    b += _lightColor.B * _objectColor.B * (_kd * cosNL + _ks * cosVR_m) * cosDL_p;
                }
                else
                {

                    r += _lightColor.R * _objectColor.R * (_kd * cosNL + _ks * cosVR_m);
                    g += _lightColor.G * _objectColor.G * (_kd * cosNL + _ks * cosVR_m);
                    b += _lightColor.B * _objectColor.B * (_kd * cosNL + _ks * cosVR_m);
                }
            }

            r = _ka + Constants.LightIntensity * r;
            g = _ka + Constants.LightIntensity * g;
            b = _ka + Constants.LightIntensity * b;


            r = System.Math.Clamp(r, 0, 1);
            g = System.Math.Clamp(g, 0, 1);
            b = System.Math.Clamp(b, 0, 1);
            double z = ZValue(x, y);
            z = (z - Constants.MinZ) / (Constants.MaxZ - Constants.MinZ);
            return GetColorFromRGBAndZ(r, g, b, z);
        }
        //ComputeColorInterpolateColor
        private Color GouradudShader(int x, int y)
        {
            MyColor myColor = BarycentricInterpolation(_v1Color, _v2Color, _v3Color, x, y);
            double z = ZValue(x, y);
            z = (z - Constants.MinZ) / (Constants.MaxZ - Constants.MinZ);
            return GetColorFromRGBAndZ(myColor.R, myColor.G, myColor.B, z);
        }

        private Color ConstantShader(int x, int y)
        {
            double r = (_v1Color.X + _v2Color.X + _v3Color.X) / 3.0;
            double g = (_v1Color.Y + _v2Color.Y + _v3Color.Y) / 3.0;
            double b = (_v1Color.Z + _v2Color.Z + _v3Color.Z) / 3.0;
            double z = ZValue(x, y);
            r = System.Math.Clamp(r, 0, 1);
            g = System.Math.Clamp(g, 0, 1);
            b = System.Math.Clamp(b, 0, 1);
            z = (z - Constants.MinZ) / (Constants.MaxZ - Constants.MinZ);
            return GetColorFromRGBAndZ(r, g, b, z);
        }

        private Color GetColorFromRGBAndZ(double r, double g, double b, double z)
        {
            if (Constants.Fog)
            {
                return Color.FromArgb(255, (int)(r * 255 * (1 - z) + 255 * z), (int)(g * 255 * (1 - z) + 255 * z), (int)(b * 255 * (1 - z) + 255 * z));
            }
            else
            {
                return Color.FromArgb(255, (int)(r * 255), (int)(g * 255), (int)(b * 255));
            }
        }

        public double ZValue(int x, int y)
        {
            return BarycentricInterpolation(_face.vertices[0], _face.vertices[1], _face.vertices[2], x, y).Z;
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
