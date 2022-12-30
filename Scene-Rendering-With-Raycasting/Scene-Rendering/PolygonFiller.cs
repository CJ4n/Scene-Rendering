using Vector3 = ObjLoader.Loader.Data.Vector3;

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
        public void FillEachFace(float ka, float kd, float ks, int m, bool interpolateNormalVector, Vector3 lightSource, double[,] ZBuffer)
        {
            using (var snoop = new BmpPixelSnoop(_drawarea))
            {


                for (int i = 0; i < _faces.Count; i++)
                {
                    //Parallel.For(0, _faces.Count, i =>
                    //{
                    var polygon = new List<Point> { new Point((int)_faces[i].vertices[0].X, (int)_faces[i].vertices[0].Y),
                                                 new Point((int)_faces[i].vertices[1].X, (int)_faces[i].vertices[1].Y),
                                                 new Point((int)_faces[i].vertices[2].X, (int)_faces[i].vertices[2].Y)};
                    var colorGenerator = new ColorGenerator(_faces[i], ka, ks, kd, m, interpolateNormalVector, lightSource, _colorMap, _lighColor, _normalMap);
                    FillPolygon(polygon, colorGenerator, snoop, ZBuffer);
                    //});
                }
            }
        }

        public void FillEachFace(float ka, float kd, float ks, int m, bool interpolateNormalVector, Vector3 lightSource, List<Point> polygon)
        {
            using (var snoop = new BmpPixelSnoop(_drawarea))
            {
                FillPolygon(polygon, null, snoop, null);
            }
        }

        private void FillPolygon(List<Point> polygon, ColorGenerator colorGenerator, BmpPixelSnoop snoop, double[,] ZBuffer)
        {
            var scanLine = new ScanLine(polygon);

            Parallel.ForEach(scanLine.GetIntersectionPoints(), xList => { FillRow(xList.Item1, xList.Item2, colorGenerator, snoop, ZBuffer); });

            foreach (var (xList, y) in scanLine.GetIntersectionPoints())
            {
                FillRow(xList, y, colorGenerator, snoop, ZBuffer);
            }
        }


        private void FillRow(List<int> xList, int y, ColorGenerator colorGenerator, BmpPixelSnoop snoop, double[,] ZBuffer)
        {
            for (int i = 0; i < xList.Count - 1; ++i)
            {
                int endCol = Math.Min(xList[i + 1], _bitmapWidth);
                for (int x = xList[i]; x < endCol; ++x)
                {
                    if (x < 0 || y < 0 || x >= _bitmapWidth || y >= _bitmapHeight)
                    {
                        return;
                    }
                    if (colorGenerator == null)
                    {
                        var tmp = _colorMap[x, y];
                        Color color = Color.FromArgb(255, (int)(tmp.R * 255), (int)(tmp.G * 255), (int)(tmp.B * 255));
                        snoop.SetPixel(x, y, color);
                        return;
                    }
                    else
                    {
                        double z = colorGenerator.ZValue(x, y);
                        if (z <= ZBuffer[x, y])
                        {
                            Color color = colorGenerator.ComputeColor(x, y);
                            snoop.SetPixel(x, y, color);
                            ZBuffer[x, y] = z;
                        }
                    }
                }
            }
        }
    }
}

