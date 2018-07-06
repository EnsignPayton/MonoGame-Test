using Microsoft.Xna.Framework;

namespace TestGame.Entities
{
    public abstract class Entity : DrawableGameComponent
    {
        protected Entity(TestGame game) : base(game)
        {
        }

        public new TestGame Game => (TestGame) base.Game;

        public Sprite Sprite { get; protected set; }

        public override void Draw(GameTime gameTime)
        {
            Sprite?.Draw(Game.SpriteBatch);

            base.Draw(gameTime);
        }
    }
}
