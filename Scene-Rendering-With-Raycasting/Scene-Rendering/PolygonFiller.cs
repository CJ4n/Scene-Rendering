using Vector3 = ObjLoader.Loader.Data.Vector3;

namespace SceneRendering
{
    public class PolygonFiller
    {
        public Bitmap Drawarea { get; set; } // canvas to draw on

        //private MyColor[,] _colorMap; // colors in pixels
        //public MyColor[,] ColorMap
        //{
        //    get { return _colorMap; }
        //    set { _colorMap = value; }
        //}
        public Color LighColor { get; set; }
        public List<MyFace> Faces { get; set; }
        private int _bitmapWidth;
        private int _bitmapHeight;
        public MyColor ObjectColor { get; set; }

        private int _width;
        private int _height;

        private Mutex[,] _mutex;

        public PolygonFiller(Bitmap drawarea, List<MyFace> faces, MyColor color, Color lightColor/*, Vector3[,] normalMap*/)
        {
            this.Drawarea = drawarea;
            this.Faces = faces;
            //this._colorMap = colorMap;
            this._bitmapWidth = drawarea.Width;
            this._bitmapHeight = drawarea.Height;
            this.LighColor = lightColor;
            this.ObjectColor = color;
            _width = Drawarea.Width;
            _height = Drawarea.Height;
            _mutex = new Mutex[_width, _height];
            //for (int col = 0; col < _width; col++)
            //{
            //    for (int row = 0; row < _height; row++)
            //    {
            //        _mutex[col, row] = new Mutex();
            //    }
            //}
            //this.ColorMap = colorMap;
            //this._normalMap = normalMap;
        }
        public void FillEachFace(float ka, float kd, float ks, int m, bool interpolateNormalVector, Vector3 lightSource, double[,] ZBuffer)
        {
            if (_width != Drawarea.Width || _height == Drawarea.Height)
            {
                _width = Drawarea.Width;
                _height = Drawarea.Height;
                _mutex = new Mutex[_width, _height];
            }
            using (var snoop = new BmpPixelSnoop(Drawarea))
            {
                //for (int i = 0; i < Faces.Count; i++)
                {
                    Parallel.For(0, Faces.Count, i =>
                    {
                        //var polygon = new List<Point> { new Point((int)Faces[i].vertices[0].X, (int)Faces[i].vertices[0].Y),
                        //                         new Point((int)Faces[i].vertices[1].X, (int)Faces[i].vertices[1].Y),
                        //                         new Point((int)Faces[i].vertices[2].X, (int)Faces[i].vertices[2].Y)};
                        var colorGenerator = new ColorGenerator(Faces[i], ka, ks, kd, m, interpolateNormalVector, lightSource, ObjectColor, LighColor/*, _normalMap*/);
                        FillPolygon(Faces[i].vertices, colorGenerator, snoop, ZBuffer);
                    });
                }
            }
        }

        private void FillPolygon(List<Vector3> polygon, ColorGenerator colorGenerator, BmpPixelSnoop snoop, double[,] ZBuffer)
        {
            var scanLine = new ScanLine(polygon);

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
                    double z = colorGenerator.ZValue(x, y);
                    if (x < 0 || y < 0 || x >= _bitmapWidth || y >= +_bitmapHeight)
                    {
                        continue;
                    }
                    //_mutex[x, y].WaitOne();
                    if (z <= ZBuffer[x, y])
                    {
                        Color color = colorGenerator.ComputeColor(x, y);
                        snoop.SetPixel(x, y, color);
                        ZBuffer[x, y] = z;
                    }
                    //_mutex[x, y].ReleaseMutex();
                }
            }
        }
    }
}

