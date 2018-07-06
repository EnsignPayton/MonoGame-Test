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

            if (Game.InputState.KeysDown(false, Keys.A, Keys.Left))
            {
                Sprite.Position = Sprite.Position.SubX(deltaTime * Velocity);
            }

            if (Game.InputState.KeysDown(false, Keys.D, Keys.Right))
            {
                Sprite.Position = Sprite.Position.AddX(deltaTime * Velocity);
            }

            if (Game.InputState.KeysDown(false, Keys.S, Keys.Down))
            {
                Sprite.Position = Sprite.Position.AddY(deltaTime * Velocity);
            }

            if (Game.InputState.KeysDown(false, Keys.W, Keys.Up))
            {
                Sprite.Position = Sprite.Position.SubY(deltaTime * Velocity);
            }

            if (Game.InputState.KeyPressed(Keys.Space) ||
                Game.InputState.MultiMousePressed(false, MouseButton.LeftButton, MouseButton.RightButton))
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

            if (Game.InputState.KeyDown(Keys.Z))
            {
                Sprite.Rotation += deltaTime;
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
