using Microsoft.Xna.Framework;

namespace Breakanoid
{
    public static class Physics
    {
        public static Vector2 GetNormal(Rectangle rect1, Rectangle rect2)
        {
            var relativeCenter = rect2.Center - rect1.Center;

            // Lengths of intersection along the bounds of rect1
            var deltaX = relativeCenter.X >= 0 ? rect1.Right - rect2.Left : rect2.Right - rect1.Left;
            var deltaY = relativeCenter.Y >= 0 ? rect1.Bottom - rect2.Top : rect2.Bottom - rect1.Top;

            if (deltaY > deltaX)
            {
                // More vertical bounds intersection => normal is horizontal
                return relativeCenter.X >= 0 ? Vector2.UnitX : -Vector2.UnitX;
            }
            else
            {
                // Otherwise, normal is vertical
                return relativeCenter.Y >= 0 ? Vector2.UnitY : -Vector2.UnitY;
            }
        }

        public static Vector2 GetWallNormal(Rectangle obj, Rectangle walls)
        {
            if (obj.Left < walls.Left)
                return Vector2.UnitX;
            if (obj.Right > walls.Right)
                return -Vector2.UnitX;
            if (obj.Top < walls.Top)
                return Vector2.UnitY;

            return Vector2.Zero;
        }
    }
}
