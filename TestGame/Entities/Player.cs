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
        private Texture2D _sprite;
        private Vector2 _position;
        private Point _size;
        private Rectangle _bounds;

        public Player(Game game) : base(game)
        {
        }

        public new TestGame Game => (TestGame) base.Game;

        protected override void LoadContent()
        {
            _fireEffect = Game.Content.Load<SoundEffect>("brother1");
            _fireEffectInstance = _fireEffect.CreateInstance();
            _sprite = Game.Content.Load<Texture2D>("hulk");
            _size = new Point(_sprite.Width >> 1, _sprite.Height >> 1);
            UpdateBounds();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (Game.InputState.KeyDown(Keys.A) || Game.InputState.KeyDown(Keys.Left))
            {
                _position.X -= deltaTime * 32.0f;
                UpdateBounds();
            }

            if (Game.InputState.KeyDown(Keys.D) || Game.InputState.KeyDown(Keys.Right))
            {
                _position.X += deltaTime * 32.0f;
                UpdateBounds();
            }

            if (Game.InputState.KeyDown(Keys.S) || Game.InputState.KeyDown(Keys.Down))
            {
                _position.Y += deltaTime * 32.0f;
                UpdateBounds();
            }

            if (Game.InputState.KeyDown(Keys.W) || Game.InputState.KeyDown(Keys.Up))
            {
                _position.Y -= deltaTime * 32.0f;
                UpdateBounds();
            }

            if (Game.InputState.KeyPressed(Keys.Space) ||
                Game.InputState.MousePressed(MouseButton.LeftButton) ||
                Game.InputState.MousePressed(MouseButton.RightButton))
            {
                _fireEffectInstance.Play();
            }

            if (Game.InputState.KeyPressed(Keys.Q))
            {
                _size = new Point(_size.X << 1, _size.Y << 1);
                UpdateBounds();
            }

            if (Game.InputState.KeyPressed(Keys.E))
            {
                _size = new Point(_size.X >> 1, _size.Y >> 1);
                UpdateBounds();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.SpriteBatch.Draw(_sprite, _bounds, Color.White);

            base.Draw(gameTime);
        }

        private void UpdateBounds()
        {
            _bounds = new Rectangle(_position.ToPoint(), _size);
        }
    }
}
