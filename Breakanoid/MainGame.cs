using System;
using System.Linq;
using Autofac;
using Breakanoid.Components;
using Breakanoid.Utilities;
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
        private readonly GraphicsDeviceManager _graphics;
        private readonly Random _random;
        private InputState _inputState;
        private Ball _ball;

        private readonly Color[] _colors = {
            new Color(255, 0, 0),
            new Color(0, 255, 0),
            new Color(0, 0, 255),
            new Color(0, 255, 255),
            new Color(255, 0, 255),
            new Color(255, 255, 0),
            new Color(255, 255, 255),
        };

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _random = new Random();
            Services.AddService(typeof(GraphicsDeviceManager), _graphics);
            Content.RootDirectory = "Content";
        }

        public IContainer Container { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 832;
            _graphics.PreferredBackBufferHeight = 634;
            _graphics.ApplyChanges();

            _inputState = Container.Resolve<InputState>();

            SpriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Components.Add(Container.Resolve<Brick>(brick =>
                    {
                        brick.Sprite.Position = new Vector2(i * brick.DefaultSize.X, j * brick.DefaultSize.Y);
                        brick.Sprite.Overlay = _colors[_random.Next(_colors.Length)];
                    }));
                }
            }

            Components.Add(Container.Resolve<Paddle>(paddle =>
            {
                paddle.Speed = 320.0f;
                paddle.Sprite.Overlay = Color.Gray;
                paddle.Sprite.Position = new Vector2(
                    GraphicsDevice.Viewport.Width / 2 - paddle.DefaultSize.X / 2,
                    GraphicsDevice.Viewport.Height - paddle.DefaultSize.Y * 2);
            }));

            _ball = Container.Resolve<Ball>(ball =>
            {
                double delta = Math.PI / 4 + _random.NextDouble() * Math.PI / 2;
                ball.Velocity = new Vector2((float) Math.Cos(delta) * 240.0f, (float) -Math.Sin(delta) * 240.0f);
                ball.Sprite.Overlay = Color.LightGray;
                ball.Sprite.Position = new Vector2(
                    GraphicsDevice.Viewport.Width / 2 - ball.DefaultSize.X / 2,
                    GraphicsDevice.Viewport.Height - ball.DefaultSize.Y * 4);
            });

            Components.Add(_ball);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            _inputState.Update();

            if (_inputState.KeyDown(Keys.Escape))
            {
                Exit();
            }

            var bricks = Components.OfType<Brick>().ToList();

            if (_inputState.KeyPressed(Keys.Space))
            {
                foreach (var brick in bricks)
                {
                    brick.Sprite.Overlay = _colors[_random.Next(_colors.Length)];
                }
            }

            // Collide with bricks
            var collidingObject = bricks.FirstOrDefault(x => x.Sprite.Destination.Intersects(_ball.Sprite.Destination));

            if (collidingObject != null)
            {
                _ball.CollideWith(collidingObject);
                Components.Remove(collidingObject);
            }

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
