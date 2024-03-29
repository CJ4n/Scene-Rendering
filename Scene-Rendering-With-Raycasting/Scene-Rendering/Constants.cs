﻿using ObjLoader.Loader.Data;

namespace SceneRendering
{
    public static class Constants
    {
        static public int ObjectBasicDim = 500;

        static public int XOffset = -50;
        static public int YOffset = -50;

        static public int Offset = ObjectBasicDim + XOffset;

        static public float Angle = 3;

        static public int MaxVisionRange = 1;

        static public double MinZ = 1;
        static public double MaxZ = 0;
        static public float LightIntensity = 1; // 1 - day, 0 - night
        static public float LightIntensityChangeRate = 0.01f;
        public enum SHADER { CONST, GOURAUD, PHONG };
        static public SHADER Shader = SHADER.PHONG;
        public static Vector3 camPositoin = new Vector3(0, 0, 0);

        public static bool Fog = true;
        public static bool DayAndNight = true;
    }
}
