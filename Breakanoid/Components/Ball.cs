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

            CollideWithWalls();

            base.Update(gameTime);
        }

        public void CollideWith(BaseComponent component)
        {
            var normal = Physics.GetNormal(Sprite.Destination, component.Sprite.Destination);

            Velocity += -2.0f * (Vector2.Dot(Velocity, normal) * normal);
        }

        public void CollideWithWalls()
        {
            var normal = Physics.GetWallNormal(Sprite.Destination, Game.GraphicsDevice.Viewport.Bounds);
            if (normal == Vector2.Zero) return;

            Velocity += -2.0f * (Vector2.Dot(Velocity, normal) * normal);
        }
    }
}
