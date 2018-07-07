using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakanoid
{
    public class Sprite
    {
        private Vector2 _size;
        private Vector2 _position;

        public Texture2D Texture { get; set; }
        public Rectangle Destination { get; set; }
        public Rectangle? Source { get; set; }
        public Color Overlay { get; set; } = Color.White;
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
        public float LayerDepth { get; set; }

        public bool IsVisible { get; set; } = true;
        public bool CalculateDestination { get; set; } = true;

        public Vector2 Size
        {
            get => _size;
            set
            {
                _size = value;
                SetDestination();
            }
        }

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                SetDestination();
            }
        }

        public void SizeToTexture()
        {
            Size = new Vector2(Texture.Width, Texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsVisible)
            {
                spriteBatch.Draw(Texture, Destination, Source, Overlay, Rotation, Origin, Effects, LayerDepth);
            }
        }

        private void SetDestination()
        {
            if (CalculateDestination)
            {
                Destination = new Rectangle(_position.ToPoint(), _size.ToPoint());
            }
        }
    }
}
