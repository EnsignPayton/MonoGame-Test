﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakanoid.Components
{
    public class Ball : BaseComponent
    {
        public Ball(MainGame game) : base(game)
        {
            DefaultSize = new Point(32);
        }

        public Vector2 Velocity { get; set; }
        public Rectangle? Walls { get; set; }

        protected override void LoadContent()
        {
            Sprite.Texture = Game.Content.Load<Texture2D>("ball");
            Sprite.SizeToTexture();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

            Sprite.Position +=  Velocity * deltaTime;

            base.Update(gameTime);
        }

        public void CollideWith(BaseComponent component)
        {
            var normal = Physics.GetNormal(Sprite.Destination, component.Sprite.Destination);

            Velocity += -2.0f * (Vector2.Dot(Velocity, normal) * normal);
        }

        public void CollideWithWalls(Rectangle walls)
        {
            var bounds = Sprite.Destination;

            var normal = Physics.GetWallNormal(Sprite.Destination, walls);
            if (normal == Vector2.Zero) return;

            Velocity += -2.0f * (Vector2.Dot(Velocity, normal) * normal);

            // Jump to avoid being stuck in wall
            if (normal == Vector2.UnitX)
                Sprite.Position = new Vector2(Sprite.Position.X + (walls.Left - bounds.Left), Sprite.Position.Y);
            if (normal == -Vector2.UnitX)
                Sprite.Position = new Vector2(Sprite.Position.X - (bounds.Right - walls.Right), Sprite.Position.Y);
            if (normal == Vector2.UnitY)
                Sprite.Position = new Vector2(Sprite.Position.X, Sprite.Position.Y + (walls.Top - bounds.Top));
        }

        public void CollideWithPaddle(Paddle paddle, float speedup = 0.0f)
        {
            var normal = (Sprite.Destination.Center - paddle.Sprite.Destination.Center).ToVector2();
            normal.Normalize();

            Velocity = (Velocity.Length() + speedup) * normal;
        }
    }
}
