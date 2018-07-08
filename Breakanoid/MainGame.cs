using Autofac;
using Breakanoid.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakanoid
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        private const int DefaultWidth = 832;
        private const int DefaultHeight = 634;

        private readonly GraphicsDeviceManager _graphics;
        private InputState _inputState;
        private GameScene _gameScene;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Services.AddService(typeof(GraphicsDeviceManager), _graphics);
            Content.RootDirectory = "Content";
        }

        public IContainer Container { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = DefaultWidth;
            _graphics.PreferredBackBufferHeight = DefaultHeight;
            _graphics.ApplyChanges();

            _inputState = Container.Resolve<InputState>();

            SpriteBatch = new SpriteBatch(GraphicsDevice);

            _gameScene = Container.Resolve<GameScene>();
            _gameScene.CenterInScreen();

            Components.Add(_gameScene);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            _inputState.Update();

            if (_inputState.KeyDown(Keys.Escape))
            {
                Exit();
            }

            // Not working yet
            //if (_inputState.KeyPressed(Keys.F11))
            //{
            //    if (Window.IsBorderless)
            //    {
            //        _graphics.PreferredBackBufferWidth = DefaultWidth;
            //        _graphics.PreferredBackBufferHeight = DefaultHeight;
            //    }
            //    else
            //    {
            //        // Set window size to resolution of monitor
            //        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            //        // Set position to top left corner, avoids offset by taskbars
            //        Window.Position = Point.Zero;
            //    }

            //    Window.IsBorderless = !Window.IsBorderless;
            //    _graphics.ApplyChanges();
            //    _gameScene.CenterInScreen();
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();

            base.Draw(gameTime);

            SpriteBatch.End();
        }
    }
}
