﻿using ObjLoader.Loader.Data;

namespace SceneRendering
{
    public class PolygonFiller
    {
        private Bitmap _drawarea; // canvas to draw on
        public Bitmap Drawarea
        {
            get { return _drawarea; }
            set { _drawarea = value; }
        }
        private MyColor[,] _colorMap; // colors in pixels
        public MyColor[,] ColorMap
        {
            get { return _colorMap; }
            set { _colorMap = value; }
        }
        private Color _lighColor;
        public Color LighColor
        {
            get { return _lighColor; }
            set { _lighColor = value; }
        }
        private List<MyFace> _faces;
        public List<MyFace> Faces
        {
            get { return _faces; }
            set { _faces = value; }
        }
        private Vector3[,] _normalMap; // normal vectors in pixels (normal vector modification)
        public Vector3[,] NormalMap
        {
            get { return _normalMap; }
            set { _normalMap = value; }
        }
        private int _bitmapWidth;
        private int _bitmapHeight;

        public PolygonFiller(Bitmap drawarea, List<MyFace> faces, MyColor[,] colorMap, Color lightColor, Vector3[,] normalMap)
        {
            this._drawarea = drawarea;
            this._faces = faces;
            this._colorMap = colorMap;
            _bitmapWidth = drawarea.Width;
            _bitmapHeight = drawarea.Height;
            _lighColor = lightColor;
            this._normalMap = normalMap;
        }

        public void FillEachFace(float ka, float kd, float ks, int m, bool interpolateNormalVector, Vector3 lightSource, int objectIdx = 0)
        {
            using (var snoop = new BmpPixelSnoop(_drawarea))
            {
                Parallel.For(0, _faces.Count, i =>
                {
                    var polygon = new List<Point> { new Point((int)_faces[i].vertices[0].X, (int)_faces[i].vertices[0].Y),
                                                 new Point((int)_faces[i].vertices[1].X, (int)_faces[i].vertices[1].Y),
                                                 new Point((int)_faces[i].vertices[2].X, (int)_faces[i].vertices[2].Y)};
                    var colorGenerator = new ColorGenerator(_faces[i], ka, ks, kd, m, interpolateNormalVector, lightSource, _colorMap, _lighColor, _normalMap);
                    FillPolygon(polygon, colorGenerator, snoop, objectIdx);
                });
            }
        }

        public void FillEachFace(float ka, float kd, float ks, int m, bool interpolateNormalVector, Vector3 lightSource, List<Point> polygon)
        {
            using (var snoop = new BmpPixelSnoop(_drawarea))
            {
                FillPolygon(polygon, null, snoop, 0);
            }
        }

        private void FillPolygon(List<Point> polygon, ColorGenerator colorGenerator, BmpPixelSnoop snoop, int objectIdx)
        {
            var scanLine = new ScanLine(polygon);

            foreach (var (xList, y) in scanLine.GetIntersectionPoints())
            {
                FillRow(xList, y, colorGenerator, snoop, objectIdx);
            }
        }
        private void FillRow(List<int> xList, int y, ColorGenerator colorGenerator, BmpPixelSnoop snoop, int objectIdx)
        {
            for (int i = 0; i < xList.Count - 1; ++i)
            {
                int endCol = Math.Min(xList[i + 1], _bitmapWidth);
                for (int x = xList[i]; x < endCol; ++x)
                {
                    if (x < 0 || y < 0 ||
                        x + objectIdx * Constants.Offset + Constants.XOffset >= _bitmapWidth ||
                        y + Constants.YOffset >= _bitmapHeight)
                    {
                        return;
                    }
                    Color color;
                    if (colorGenerator == null)
                    {
                        var tmp = _colorMap[x, y];
                        color = Color.FromArgb(255, (int)(tmp.R * 255), (int)(tmp.G * 255), (int)(tmp.B * 255));
                    }
                    else
                    {
                        color = colorGenerator.ComputeColor(x, y);
                    }
                    snoop.SetPixel(x + objectIdx * Constants.Offset + Constants.XOffset, y + Constants.YOffset, color);
                }
            }
        }
    }
}