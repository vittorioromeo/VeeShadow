#region
using System.Collections.Generic;
using SFMLStart.Utilities;
using SFMLStart.Vectors;

#endregion

namespace VeeShadow
{
    public class ShadowCaster
    {
        public ShadowCaster(SSVector2I mPosition, int mMultiplier = 1)
        {
            Position = mPosition;
            Multiplier = mMultiplier;
            Polygons = new List<PolygonI>();
        }

        public SSVector2I Position { get; set; }
        public int Multiplier { get; set; }
        public List<PolygonI> Polygons { get; private set; }

        private SSVector2I GetProjection(SSVector2I mPoint)
        {
            var lightToPoint = mPoint - Position;
            return mPoint + lightToPoint*Multiplier;
        }
        private bool ShouldEdgeCast(SSVector2I mPoint1, SSVector2I mPoint2)
        {
            var startToEnd = mPoint2 - mPoint1;
            var normal = new SSVector2I(startToEnd.Y, -1*startToEnd.X);
            var lightToStart = mPoint1 - Position;

            return normal.GetDotProduct(lightToStart) > 0;
        }
        private void CastEdgeShadow(SSVector2I mPoint1, SSVector2I mPoint2)
        {
            var polygon = new PolygonI(mPoint1, GetProjection(mPoint1), GetProjection(mPoint2), mPoint2);
            Polygons.Add(polygon);
        }

        public void CalculateShadows(IEnumerable<AABBHull> mHulls)
        {
            Polygons.Clear();
            foreach (var hull in mHulls)
                foreach (var edge in hull.GetEdges())
                    if (ShouldEdgeCast(edge[0], edge[1]))
                        CastEdgeShadow(edge[0], edge[1]);
        }
    }
}