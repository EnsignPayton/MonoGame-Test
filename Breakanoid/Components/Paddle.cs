using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakanoid.Components
{
    public class Paddle : BaseComponent
    {
        private readonly InputState _inputState;
        public Paddle(MainGame game, InputState inputState) : base(game)
        {
            _inputState = inputState;
            Walls = game.GraphicsDevice.Viewport.Bounds;
        }

        public float Speed { get; set; }
        public Rectangle Walls { get; set; }

        protected override void LoadContent()
        {
            Sprite.Texture = Game.Content.Load<Texture2D>("brick");
            Sprite.SizeToTexture();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (_inputState.KeyDown(Keys.Left) && Sprite.Position.X > Walls.Left)
            {
                Sprite.Position = new Vector2(Sprite.Position.X - deltaTime * Speed, Sprite.Position.Y);
            }

            if (_inputState.KeyDown(Keys.Right) && Sprite.Position.X + Sprite.Size.X < Walls.Right)
            {
                Sprite.Position = new Vector2(Sprite.Position.X + deltaTime * Speed, Sprite.Position.Y);
            }

            base.Update(gameTime);
        }
    }
}
