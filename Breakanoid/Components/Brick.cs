using Microsoft.Xna.Framework.Graphics;

namespace Breakanoid.Components
{
    public class Brick : BaseComponent
    {
        public Brick(MainGame game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            Sprite.Texture = Game.Content.Load<Texture2D>("brick");
            Sprite.SizeToTexture();
            base.LoadContent();
        }
    }
}
