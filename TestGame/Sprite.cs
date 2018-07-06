using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
    public class Sprite
    {
        private Vector2 _position;
        private Vector2 _size;

        public Texture2D Texture { get; set; }

        public float Rotation { get; set; }

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                Bounds = new Rectangle(_position.ToPoint(), _size.ToPoint());
            }
        }

        public Vector2 Size
        {
            get => _size;
            set
            {
                _size = value;
                Bounds = new Rectangle(_position.ToPoint(), _size.ToPoint());
            }
        }

        public Rectangle Bounds { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, null, Color.White, Rotation, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
