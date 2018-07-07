using Microsoft.Xna.Framework;

namespace Breakanoid.Components
{
    public abstract class BaseComponent : DrawableGameComponent
    {
        private Sprite _sprite;

        protected BaseComponent(MainGame game) : base(game)
        {
        }

        public new MainGame Game => (MainGame) base.Game;

        public Point DefaultSize { get; set; } = new Point(64, 32);

        public Sprite Sprite
        {
            get => _sprite ?? (_sprite = new Sprite());
            set => _sprite = value;
        }

        public override void Draw(GameTime gameTime)
        {
            Sprite?.Draw(Game.SpriteBatch);

            base.Draw(gameTime);
        }
    }
}
