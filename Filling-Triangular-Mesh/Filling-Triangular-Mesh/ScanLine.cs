namespace SceneRendering
{
    public class ScanLine
    {
        private List<AETPointer> _AET;
        private Stack<int> _sortedIdx;
        private List<Point> _polygon;

        public ScanLine(List<Point> polygon)
        {
            _AET = new List<AETPointer>();
            this._polygon = polygon;

            _sortedIdx = new Stack<int>(polygon.Select((point, idx) => new KeyValuePair<Point, int>(point, idx)).
                OrderByDescending(pair => pair.Key.Y).
                Select(pair => pair.Value));
        }
        public List<(List<int>, int)> GetIntersectionPoints()
        {
            List<(List<int> xList, int y)> outList = new List<(List<int>, int)>();
            int yMin = (int)_polygon[_sortedIdx.Peek()].Y;
            int yMax = (int)_polygon[_sortedIdx.Last()].Y;
            var mockPointer = new AETPointer(0, 0, 0);

            for (int y = yMin + 1; y <= yMax; ++y)
            {
                while (_sortedIdx.Count > 0 && _polygon[_sortedIdx.Peek()].Y == y - 1)
                {
                    var idx = _sortedIdx.Pop();
                    var currentPoint = _polygon[idx];
                    var prevPoint = _polygon[(idx - 1 + _polygon.Count) % _polygon.Count];
                    if (prevPoint.Y > currentPoint.Y)
                    {
                        _AET.Add(new AETPointer(prevPoint.Y, currentPoint.X, Utils.Slope(currentPoint, prevPoint)));
                    }
                    else if (prevPoint.Y < currentPoint.Y)
                    {
                        mockPointer.x = prevPoint.X;
                        mockPointer.yMax = (int)prevPoint.Y;
                        _AET.Remove(mockPointer);
                    }

                    var nextPoint = _polygon[(idx + 1) % _polygon.Count];
                    if (nextPoint.Y > currentPoint.Y)
                    {
                        _AET.Add(new AETPointer(nextPoint.Y, currentPoint.X, Utils.Slope(currentPoint, nextPoint)));
                    }
                    else if (nextPoint.Y < currentPoint.Y)
                    {
                        mockPointer.x = nextPoint.X;
                        mockPointer.yMax = (int)nextPoint.Y;
                        _AET.Remove(mockPointer);
                    }
                }

                outList.Add((_AET.Select(ptr => ptr.X).OrderBy(x => x).ToList(), y));

                _AET.RemoveAll((ptr) => ptr.yMax <= y);
                foreach (var ptr in _AET)
                {
                    ptr.Update();
                }
            }
            return outList;
        }
    }
    public class AETPointer : IComparable<AETPointer>
    {
        public int yMax;
        public double x;
        private double _slope;

        public AETPointer(double yMax, double x, double slope)
        {
            this.yMax = (int)yMax;
            this.x = x;
            this._slope = 1.0 / slope;
        }
        public int X { get => (int)Math.Round(x); }
        public void Update()
        {
            x = _slope == Utils.Infinity ? Utils.Infinity : x + _slope;
        }
        public int CompareTo(AETPointer other)
        {
            var xCmp = x.CompareTo(other.x);
            return xCmp == 0 ? yMax.CompareTo(other.yMax) : xCmp;
        }
    }
}
