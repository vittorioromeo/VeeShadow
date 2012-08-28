#region
using System.Collections.Generic;
using SFMLStart.Vectors;

#endregion

namespace VeeShadow
{
    public struct AABBHull
    {
        public SSVector2I HalfSize;
        public SSVector2I Position;
        #region Shortcut Properties
        public int X { get { return Position.X; } }
        public int Y { get { return Position.Y; } }
        public int Left { get { return Position.X - HalfSize.X; } }
        public int Right { get { return Position.X + HalfSize.X; } }
        public int Top { get { return Position.Y - HalfSize.Y; } }
        public int Bottom { get { return Position.Y + HalfSize.Y; } }
        #endregion
        public AABBHull(SSVector2I mPosition, SSVector2I mHalfSize)
        {
            Position = mPosition;
            HalfSize = mHalfSize;
        }

        public IEnumerable<SSVector2I[]> GetEdges()
        {
            yield return new[] {new SSVector2I(Left, Top), new SSVector2I(Right, Top)};
            yield return new[] {new SSVector2I(Right, Top), new SSVector2I(Right, Bottom)};
            yield return new[] {new SSVector2I(Right, Bottom), new SSVector2I(Left, Bottom)};
            yield return new[] {new SSVector2I(Left, Bottom), new SSVector2I(Left, Top)};
        }
    }
}