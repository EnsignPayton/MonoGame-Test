using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TestGame.Entities;
using TestGame.Input;

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TestGame : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        private Texture2D _flagTexture;
        private Song _song;

        public TestGame()
        {
            _graphics = new GraphicsDeviceManager(this);
        }

        public SpriteBatch SpriteBatch { get; private set; }
        public InputState InputState { get; private set; }

        protected override void Initialize()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            SpriteBatch = new SpriteBatch(GraphicsDevice);
            InputState = new InputState();

            Components.Add(new Player(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _flagTexture = Content.Load<Texture2D>("flag");
            _song = Content.Load<Song>("song");

            // Play and repeat
            MediaPlayer.Play(_song);
            MediaPlayer.MediaStateChanged += (s, e) => MediaPlayer.Play(_song);
        }

        protected override void Update(GameTime gameTime)
        {
            InputState.Update();

            // Exit on escape or back
            if (InputState.KeyDown(Keys.Escape) || InputState.GamePadButtonDown(0, Buttons.Back))
            {
                Exit();
            }

            // Change window mode only on key press, not hold
            if (InputState.KeyPressed(Keys.F11))
            {
                if (!Window.IsBorderless)
                {
                    // Set window size to resolution of monitor
                    _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

                    // Set position to top left corner, avoids offset by taskbars
                    Window.Position = new Point(0, 0);
                    Window.IsBorderless = true;
                    _graphics.ApplyChanges();
                }
                else
                {
                    // Default window size
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

            SpriteBatch.Begin();

            SpriteBatch.Draw(_flagTexture, GraphicsDevice.Viewport.Bounds, Color.White);

            base.Draw(gameTime);

            SpriteBatch.End();
        }
    }
}
