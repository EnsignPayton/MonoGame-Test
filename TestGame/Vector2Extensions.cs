using System;
using Microsoft.Xna.Framework;

namespace TestGame
{
    public static class Vector2Extensions
    {
        public static Vector2 AddX(this Vector2 value, float add)
        {
            return new Vector2(value.X + add, value.Y);
        }

        public static Vector2 SubX(this Vector2 value, float sub)
        {
            return new Vector2(value.X - sub, value.Y);
        }

        public static Vector2 AddY(this Vector2 value, float add)
        {
            return new Vector2(value.X, value.Y + add);
        }

        public static Vector2 SubY(this Vector2 value, float sub)
        {
            return new Vector2(value.X, value.Y - sub);
        }
    }
}
