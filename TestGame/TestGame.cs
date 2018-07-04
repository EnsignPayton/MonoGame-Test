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
        private SpriteBatch _spriteBatch;

        private KeyboardState _keyboardState;
        private KeyboardState _oldKeyboardState;
        private MouseState _mouseState;
        private MouseState _oldMouseState;

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

            _oldKeyboardState = Keyboard.GetState();
            _oldMouseState = Mouse.GetState();

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

            if (_keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (_keyboardState.IsKeyDown(Keys.A) || _keyboardState.IsKeyDown(Keys.Left))
            {
                _hulkPosition.X -= deltaTime * 32.0f;
            }

            if (_keyboardState.IsKeyDown(Keys.D) || _keyboardState.IsKeyDown(Keys.Right))
            {
                _hulkPosition.X += deltaTime * 32.0f;
            }

            if (_keyboardState.IsKeyDown(Keys.S) || _keyboardState.IsKeyDown(Keys.Down))
            {
                _hulkPosition.Y += deltaTime * 32.0f;
            }

            if (_keyboardState.IsKeyDown(Keys.W) || _keyboardState.IsKeyDown(Keys.Up))
            {
                _hulkPosition.Y -= deltaTime * 32.0f;
            }

            if ((_keyboardState.IsKeyDown(Keys.Space) && _oldKeyboardState.IsKeyUp(Keys.Space)) ||
                (_mouseState.LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton == ButtonState.Released) ||
                (_mouseState.RightButton == ButtonState.Pressed && _oldMouseState.RightButton == ButtonState.Released))
            {
                _brotherInstance.Play();
            }

            //Change window mode only on key press, not hold
            if (_keyboardState.IsKeyDown(Keys.F11) && !_oldKeyboardState.IsKeyDown(Keys.F11))
            {
                if (Window.IsBorderless == false)
                {
                    //Set window size to resolution of monitor
                    _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    //Set position to top left corner, avoids offset by taskbars
                    this.Window.Position = new Point(0, 0);
                    Window.IsBorderless = true;
                    _graphics.ApplyChanges();
                }
                else
                {
                    //Default window size
                    _graphics.PreferredBackBufferWidth = 800;
                    _graphics.PreferredBackBufferHeight = 480;
                    Window.IsBorderless = false;
                    _graphics.ApplyChanges();
                }
            }

            _oldKeyboardState = _keyboardState;
            _oldMouseState = _mouseState;

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
