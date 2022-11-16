using ObjLoader.Loader.Data;

namespace Filling_Triangular_Mesh
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
        private Color lighColor;
        public Color LighColor
        {
            get { return lighColor; }
            set { lighColor = value; }
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

        public PolygonFiller(Bitmap drawarea, List<MyFace> faces, MyColor[,] colorMap, Color lightColor, Vector3[,] normalMap)
        {
            this._drawarea = drawarea;
            this._faces = faces;
            this._colorMap = colorMap;
            _bitmapWidth = drawarea.Width;
            lighColor = lightColor;
            this._normalMap = normalMap;
        }

        public void FillGridWithTriangles(float kd, float ks, int m, bool interpolateNormalVector, Vector3 lightSource)
        {
            using (var snoop = new BmpPixelSnoop(_drawarea))
            {
                Parallel.For(0, _faces.Count, i =>
                {
                    var triangle = new List<Point> { new Point((int)_faces[i].vertices[0].X, (int)_faces[i].vertices[0].Y),
                                                 new Point((int)_faces[i].vertices[1].X, (int)_faces[i].vertices[1].Y),
                                                 new Point((int)_faces[i].vertices[2].X, (int)_faces[i].vertices[2].Y)};
                    var gen = new ColorGenerator(_faces[i], ks, kd, m, interpolateNormalVector, lightSource, _colorMap, lighColor, _normalMap);
                    FillTriangle(triangle, gen, snoop);
                });

                //for (int i = 0; i < grid.Count; ++i)
                //{
                //    var triangle = new List<Point> { new Point((int)grid[i].vertices[0].X, (int)grid[i].vertices[0].Y),
                //                         new Point((int)grid[i].vertices[1].X, (int)grid[i].vertices[1].Y),
                //                         new Point((int)grid[i].vertices[2].X, (int)grid[i].vertices[2].Y)};
                //    var gen = new ColorGenerator(grid[i], ks, kd, m, interpolateNormalVector, lightSource, texture, ,lighColor);

                //    FillTriangle(triangle, gen, snoop);
                //}
            }
        }

        private void FillTriangle(List<Point> triangle, ColorGenerator colorGenerator, BmpPixelSnoop snoop)
        {
            var scanLine = new ScanLine(triangle);

            foreach (var (xList, y) in scanLine.GetIntersectionPoints())
            {
                FillRow(xList, y, colorGenerator, snoop);
            }
        }
        private void FillRow(List<int> xList, int y, ColorGenerator colorGenerator, BmpPixelSnoop snoop)
        {
            for (int i = 0; i < xList.Count - 1; ++i)
            {
                int endCol = Math.Min(xList[i + 1], _bitmapWidth);
                for (int x = xList[i]; x < endCol; ++x)
                {
                    Color color = colorGenerator.ComputeColor(x, y);
                    snoop.SetPixel(x, y, color);
                }
            }
        }
    }
}
