using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakanoid.Components
{
    public class Ball : BaseComponent
    {
        public Ball(MainGame game) : base(game)
        {
            DefaultSize = new Point(32);
        }

        public Vector2 Velocity { get; set; }

        protected override void LoadContent()
        {
            Sprite.Texture = Game.Content.Load<Texture2D>("ball");
            Sprite.SizeToTexture();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            Sprite.Position +=  Velocity * deltaTime;

            // TODO: Handle collisions

            base.Update(gameTime);
        }
    }
}
