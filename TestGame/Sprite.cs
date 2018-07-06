using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame
{
    public class Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _size;

        public Texture2D Texture
        {
            get => _texture;
            set
            {
                _texture = value;
                Origin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);
            }
        }

        public float Rotation { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;

                var offset = Texture != null ? Origin / 2 : Vector2.Zero;
                Bounds = new Rectangle((_position + offset).ToPoint(), _size.ToPoint());
            }
        }

        public Vector2 Size
        {
            get => _size;
            set
            {
                _size = value;

                var offset = Texture != null ? Origin / 2 : Vector2.Zero;
                Bounds = new Rectangle((_position + offset).ToPoint(), _size.ToPoint());
            }
        }

        public Rectangle Bounds { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Bounds, null, Color.White, Rotation, Origin, SpriteEffects.None, 0);
        }
    }
}
