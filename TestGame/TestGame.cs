using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TestGame.Input;

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TestGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputState _inputState;

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
            _inputState = new InputState();

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

            _inputState.Update();

            if (_inputState.KeyDown(Keys.Escape))
            {
                Exit();
            }

            if (_inputState.KeyDown(Keys.A) || _inputState.KeyDown(Keys.Left))
            {
                _hulkPosition.X -= deltaTime * 32.0f;
            }

            if (_inputState.KeyDown(Keys.D) || _inputState.KeyDown(Keys.Right))
            {
                _hulkPosition.X += deltaTime * 32.0f;
            }

            if (_inputState.KeyDown(Keys.S) || _inputState.KeyDown(Keys.Down))
            {
                _hulkPosition.Y += deltaTime * 32.0f;
            }

            if (_inputState.KeyDown(Keys.W) || _inputState.KeyDown(Keys.Up))
            {
                _hulkPosition.Y -= deltaTime * 32.0f;
            }

            if (_inputState.KeyPressed(Keys.Space) ||
                _inputState.MousePressed(MouseButton.LeftButton) ||
                _inputState.MousePressed(MouseButton.RightButton))
            {
                _brotherInstance.Play();
            }

            // Change window mode only on key press, not hold
            if (_inputState.KeyPressed(Keys.F11))
            {
                if (!Window.IsBorderless)
                {
                    //Set window size to resolution of monitor
                    _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

                    //Set position to top left corner, avoids offset by taskbars
                    Window.Position = new Point(0, 0);
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
