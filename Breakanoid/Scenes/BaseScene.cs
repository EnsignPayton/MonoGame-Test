using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Breakanoid.Scenes
{
    public abstract class BaseScene : DrawableGameComponent
    {
        private readonly IList<IDrawable> _drawables;
        private readonly IList<IUpdateable> _updateables;

        protected BaseScene(MainGame game) : base(game)
        {
            Components = new GameComponentCollection();
            Components.ComponentAdded += Components_ComponentAdded;
            Components.ComponentRemoved += Components_ComponentRemoved;

            _drawables = new List<IDrawable>();
            _updateables = new List<IUpdateable>();
        }

        public new MainGame Game => (MainGame) base.Game;

        public GameComponentCollection Components { get; }

        private void Components_ComponentAdded(object sender, GameComponentCollectionEventArgs e)
        {
            if (e.GameComponent is IUpdateable updateable)
                _updateables.Add(updateable);
            if (e.GameComponent is IDrawable drawable)
                _drawables.Add(drawable);
        }

        private void Components_ComponentRemoved(object sender, GameComponentCollectionEventArgs e)
        {
            if (e.GameComponent is IUpdateable updateable)
                _updateables.Remove(updateable);
            if (e.GameComponent is IDrawable drawable)
                _drawables.Remove(drawable);
        }

        public override void Initialize()
        {
            foreach (var component in Components)
            {
                component.Initialize();
            }

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var updateable in _updateables)
            {
                updateable.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var drawable in _drawables)
            {
                drawable.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}
