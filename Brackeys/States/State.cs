using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Unity;
using System.Collections.Generic;
using Brackeys.Manager;
using System.Linq;
using Brackeys.Component.Sprites;
using System;


namespace Brackeys.States
{

    public enum Layers
    {
        Background,
        Cells,
        PlayingArea,
        UI
    }

    public class State
    {
        #region Fields

        public List<Component.Component>[] Layer { get; set; }

        [Dependency]
        public ContentManager ContentManager { get; set; }

        [Dependency]
        public StateManager StateManager { get; set; }

        [Dependency]
        public JamGame JamGame { get; set; }

        [Dependency]
        public AudioManager AudioManager { get; set; }

        public bool HasLoaded { get;  set; }

        #endregion

        #region Methods

        public void Load()
        {
            Layer = new List<Component.Component>[Enum.GetNames(typeof(Layers)).Length];
            for (int i = 0; i < Layer.Length; i++) Layer[i] = new List<Component.Component>();
            LoadComponents(); OnLoad(); 
            HasLoaded = true; 
        }

        protected virtual void LoadComponents() { }
        protected virtual void OnLoad() { }

        public virtual void AddComponent(Component.Component component, int layer)
        {
            component.CurrentState = this;
            Layer[layer].Add(component);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Layer.All(l => l.Count == 0))
            {
                return;
            }
            // draw components from top to bottom
            foreach (List<Component.Component> components in Layer)
            {
                List<Component.Component> DrawOrder = components.OrderByDescending(c => c.Position.Y).ToList();
                for (int i = DrawOrder.Count - 1; i >= 0; i--)
                {
                    DrawOrder[i].Draw(gameTime, spriteBatch);
                }
            }
        }

        public virtual void PostUpdate(GameTime gameTime)
        {
            if (Layer.All(l => l.Count == 0))
            {
                return;
            }
            foreach (List<Component.Component> components in Layer)
            {
                for (int i = components.Count - 1; i >= 0; i--)
                {
                    if (components[i].IsRemoved) components.RemoveAt(i);
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            AudioManager.Update();
            if (Layer.All(l => l.Count == 0))
            {
                return;
            }

            foreach (List<Component.Component> components in Layer)
            {
                for (int i = components.Count - 1; i >= 0; i--)
                {
                    components[i].Update(gameTime);
                }
            }

            CollisionCheck(gameTime);
        }

        private void CollisionCheck(GameTime gameTime)
        {
            IEnumerable<Sprite> sprites = Layer[(int)Layers.PlayingArea].Where(x => x is Sprite).Select(x => x as Sprite).ToList();
            foreach (Sprite sprite in sprites)
            {
                foreach (Sprite sprite2 in sprites)
                {
                    if (!sprite.Collide || !sprite2.Collide) continue;
                    if (sprite == sprite2) continue;
                    if (sprite.Rectangle.Intersects(sprite2.Rectangle))
                    {
                        sprite.OnCollision(sprite2, gameTime);
                        sprite2.OnCollision(sprite, gameTime);
                    }
                }
            }
        }

        #endregion
    }
}