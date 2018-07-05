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

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            if (_inputState.KeyDown(Keys.A) || _inputState.KeyDown(Keys.Left))
            {
                _position.X -= deltaTime * 32.0f;
            }

            if (_inputState.KeyDown(Keys.D) || _inputState.KeyDown(Keys.Right))
            {
                _position.X += deltaTime * 32.0f;
            }

            if (_inputState.KeyDown(Keys.S) || _inputState.KeyDown(Keys.Down))
            {
                _position.Y += deltaTime * 32.0f;
            }

            if (_inputState.KeyDown(Keys.W) || _inputState.KeyDown(Keys.Up))
            {
                _position.Y -= deltaTime * 32.0f;
            }

            if (_inputState.KeyPressed(Keys.Space) ||
                _inputState.MousePressed(MouseButton.LeftButton) ||
                _inputState.MousePressed(MouseButton.RightButton))
            {
                _fireEffectInstance.Play();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(_sprite, _position, Color.White);

            base.Draw(gameTime);
        }
    }
}
