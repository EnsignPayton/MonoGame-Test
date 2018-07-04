using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TestGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private KeyboardState _keyboardState;
        private MouseState _mouseState;
        private SpriteBatch _spriteBatch;

        private Song _song;
        private SoundEffect _brotherEffect;
        private SoundEffectInstance _brotherInstance;

        private Texture2D _flagTexture;
        private Texture2D _hulkTexture;
        private Vector2 _hulkPosition = Vector2.Zero;

        public TestGame()
        {
            _graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _song = Content.Load<Song>("song");
            _brotherEffect = Content.Load<SoundEffect>("brother1");

            _flagTexture = Content.Load<Texture2D>("flag");
            _hulkTexture = Content.Load<Texture2D>("hulk");

            // Play and repeat
            MediaPlayer.Play(_song);
            MediaPlayer.MediaStateChanged += (s, e) => MediaPlayer.Play(_song);

            _brotherInstance = _brotherEffect.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            if (_keyboardState.IsKeyDown(Keys.A) || _keyboardState.IsKeyDown(Keys.Left))
            {
                _hulkPosition.X -= deltaTime * 32.0f;
            }

            if (_keyboardState.IsKeyDown(Keys.D) || _keyboardState.IsKeyDown(Keys.Right))
            {
                _hulkPosition.X += deltaTime * 32.0f;
            }

            if (_keyboardState.IsKeyDown(Keys.Space) ||
                _mouseState.LeftButton == ButtonState.Pressed ||
                _mouseState.RightButton == ButtonState.Pressed)
            {
                _brotherInstance.Play();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_flagTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            _spriteBatch.Draw(_hulkTexture, _hulkPosition, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
