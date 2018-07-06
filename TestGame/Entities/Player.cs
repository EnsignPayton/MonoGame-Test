using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestGame.Input;

namespace TestGame.Entities
{
    public class Player : DrawableGameComponent
    {
        private SoundEffect _fireEffect;
        private SoundEffectInstance _fireEffectInstance;

        public Player(Game game) : base(game)
        {
        }

        public new TestGame Game => (TestGame) base.Game;

        public float Velocity { get; set; } = 150.0f;
        public Sprite Sprite { get; private set; }

        protected override void LoadContent()
        {
            _fireEffect = Game.Content.Load<SoundEffect>("brother1");
            _fireEffectInstance = _fireEffect.CreateInstance();

            Sprite = new Sprite
            {
                Texture = Game.Content.Load<Texture2D>("hulk"),
                Size = new Vector2(256)
            };

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (Game.InputState.KeyDown(Keys.A) || Game.InputState.KeyDown(Keys.Left))
            {
                Sprite.Position = new Vector2(Sprite.Position.X - deltaTime * Velocity, Sprite.Position.Y);
            }

            if (Game.InputState.KeyDown(Keys.D) || Game.InputState.KeyDown(Keys.Right))
            {
                Sprite.Position = new Vector2(Sprite.Position.X + deltaTime * Velocity, Sprite.Position.Y);
            }

            if (Game.InputState.KeyDown(Keys.S) || Game.InputState.KeyDown(Keys.Down))
            {
                Sprite.Position = new Vector2(Sprite.Position.X, Sprite.Position.Y + deltaTime * Velocity);
            }

            if (Game.InputState.KeyDown(Keys.W) || Game.InputState.KeyDown(Keys.Up))
            {
                Sprite.Position = new Vector2(Sprite.Position.X, Sprite.Position.Y - deltaTime * Velocity);
            }

            if (Game.InputState.KeyPressed(Keys.Space) ||
                Game.InputState.MousePressed(MouseButton.LeftButton) ||
                Game.InputState.MousePressed(MouseButton.RightButton))
            {
                _fireEffectInstance.Play();
            }

            if (Game.InputState.KeyPressed(Keys.Q))
            {
                Sprite.Size = Sprite.Size * 2;
            }

            if (Game.InputState.KeyPressed(Keys.E))
            {
                Sprite.Size = Sprite.Size / 2;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(Game.SpriteBatch);

            base.Draw(gameTime);
        }
    }
}
