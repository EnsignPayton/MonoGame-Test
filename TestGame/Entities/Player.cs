using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TestGame.Input;

namespace TestGame.Entities
{
    public class Player : DrawableGameComponent
    {
        private readonly InputState _inputState;
        private readonly SpriteBatch _spriteBatch;

        private SoundEffect _fireEffect;
        private SoundEffectInstance _fireEffectInstance;
        private Texture2D _sprite;
        private Vector2 _position;
        private Point _size;
        private Rectangle _bounds;

        public Player(Game game, InputState inputState, SpriteBatch spriteBatch) : base(game)
        {
            _inputState = inputState;
            _spriteBatch = spriteBatch;
        }

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

            if (_inputState.KeyDown(Keys.A) || _inputState.KeyDown(Keys.Left))
            {
                _position.X -= deltaTime * 32.0f;
                UpdateBounds();
            }

            if (_inputState.KeyDown(Keys.D) || _inputState.KeyDown(Keys.Right))
            {
                _position.X += deltaTime * 32.0f;
                UpdateBounds();
            }

            if (_inputState.KeyDown(Keys.S) || _inputState.KeyDown(Keys.Down))
            {
                _position.Y += deltaTime * 32.0f;
                UpdateBounds();
            }

            if (_inputState.KeyDown(Keys.W) || _inputState.KeyDown(Keys.Up))
            {
                _position.Y -= deltaTime * 32.0f;
                UpdateBounds();
            }

            if (_inputState.KeyPressed(Keys.Space) ||
                _inputState.MousePressed(MouseButton.LeftButton) ||
                _inputState.MousePressed(MouseButton.RightButton))
            {
                _fireEffectInstance.Play();
            }

            if (_inputState.KeyPressed(Keys.Q))
            {
                _size = new Point(_size.X << 1, _size.Y << 1);
                UpdateBounds();
            }

            if (_inputState.KeyPressed(Keys.E))
            {
                _size = new Point(_size.X >> 1, _size.Y >> 1);
                UpdateBounds();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(_sprite, _bounds, Color.White);

            base.Draw(gameTime);
        }

        private void UpdateBounds()
        {
            _bounds = new Rectangle(_position.ToPoint(), _size);
        }
    }
}
