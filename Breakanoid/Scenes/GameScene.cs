using System;
using System.Linq;
using Breakanoid.Components;
using Breakanoid.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakanoid.Scenes
{
    public class GameScene : BaseScene
    {
        private const int Columns = 13;
        private const int Rows = 8;

        private static readonly Color[] BrickColors =
        {
            // 0,255
            new Color(255, 0, 0),
            new Color(0, 255, 0),
            new Color(0, 0, 255),
            new Color(0, 255, 255),
            new Color(255, 0, 255),
            new Color(255, 255, 0),
            new Color(255, 255, 255),
            // 0,127,255
            new Color(0, 127, 255),
            new Color(0, 255, 127),
            new Color(127, 0, 255),
            new Color(127, 255, 0),
            new Color(255, 0, 127),
            new Color(255, 127, 0),
            // 0,63,255
            new Color(0, 63, 255),
            new Color(0, 255, 63),
            new Color(63, 0, 255),
            new Color(63, 255, 0),
            new Color(255, 0, 63),
            new Color(255, 63, 0),
            // 63,255
            new Color(255, 63, 63),
            new Color(63, 255, 63),
            new Color(63, 63, 255),
            new Color(63, 255, 255),
            new Color(255, 63, 255),
            new Color(255, 255, 63),
        };

        private readonly InputState _inputState;
        private readonly Random _random;

        private Rectangle _bounds;
        private Paddle _paddle;
        private Ball _ball;

        public GameScene(MainGame game, InputState inputState, Random random) : base(game)
        {
            _inputState = inputState;
            _random = random;
            _bounds = Game.GraphicsDevice.Viewport.Bounds;
        }

        public Rectangle Bounds
        {
            get => _bounds;
            set
            {
                _bounds = value;

                if (_paddle != null)
                {
                    _paddle.Walls = Bounds;
                }
            }
        }

        public Point Size => new Point(832, 634);

        public void CenterInScreen()
        {
            Bounds = new Rectangle(new Point(
                (GraphicsDevice.Viewport.Width - Size.X) / 2,
                (GraphicsDevice.Viewport.Height - Size.Y) / 2), Size);
        }

        public override void Initialize()
        {
            for (int i = 0; i < Columns; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    Components.Add(Game.Container.Resolve<Brick>(brick =>
                    {
                        brick.Sprite.Position = new Vector2(i * brick.DefaultSize.X + Bounds.Left, j * brick.DefaultSize.Y + Bounds.Top);
                        brick.Sprite.Overlay = BrickColors[_random.Next(BrickColors.Length)];
                    }));
                }
            }

            _paddle = Game.Container.Resolve<Paddle>(paddle =>
            {
                paddle.Walls = Bounds;
                paddle.Speed = 320.0f;
                paddle.Sprite.Overlay = Color.Gray;
                paddle.Sprite.Position = new Vector2(
                    Bounds.Width / 2 - paddle.DefaultSize.X / 2,
                    Bounds.Height - paddle.DefaultSize.Y * 2);
            });

            Components.Add(_paddle);

            _ball = Game.Container.Resolve<Ball>(ball =>
            {
                double delta = Math.PI / 4 + _random.NextDouble() * Math.PI / 2;
                ball.Velocity = new Vector2((float)Math.Cos(delta), (float)-Math.Sin(delta)) * 240.0f;
                ball.Sprite.Overlay = Color.LightGray;
                ball.Sprite.Position = new Vector2(
                    Bounds.Width / 2 - ball.DefaultSize.X / 2,
                    Bounds.Height - ball.DefaultSize.Y * 4);
            });

            Components.Add(_ball);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            var bricks = Components.OfType<Brick>().ToList();

            // Recolor bricks on space
            if (_inputState.KeyPressed(Keys.Space))
            {
                foreach (var brick in bricks)
                {
                    brick.Sprite.Overlay = BrickColors[_random.Next(BrickColors.Length)];
                }
            }

            // Collide ball with bricks
            var collidingObject = bricks.FirstOrDefault(x => x.Sprite.Destination.Intersects(_ball.Sprite.Destination));

            if (collidingObject != null)
            {
                _ball.CollideWith(collidingObject);
                Components.Remove(collidingObject);
            }

            if (_ball.Sprite.Destination.Intersects(_paddle.Sprite.Destination))
            {
                _ball.CollideWithPaddle(_paddle, 0.5f);
                _paddle.Speed += 0.5f;
            }

            _ball.CollideWithWalls(Bounds);

            base.Update(gameTime);
        }
    }
}
