using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TestGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private KeyboardState _keyboardState;
        private SpriteBatch _spriteBatch;
        private Texture2D _flagTexture;
        private Texture2D _hulkTexture;
        private Vector2 _hulkPosition = Vector2.Zero;

        public TestGame()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _flagTexture = Content.Load<Texture2D>("flag");
            _hulkTexture = Content.Load<Texture2D>("hulk");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            _keyboardState = Keyboard.GetState();

            if (_keyboardState.IsKeyDown(Keys.A) || _keyboardState.IsKeyDown(Keys.Left))
            {
                _hulkPosition.X -= deltaTime * 32.0f;
            }

            if (_keyboardState.IsKeyDown(Keys.D) || _keyboardState.IsKeyDown(Keys.Right))
            {
                _hulkPosition.X += deltaTime * 32.0f;
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
