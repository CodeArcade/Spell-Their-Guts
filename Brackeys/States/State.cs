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

        public List<Component.Component>[] Layers { get; set; }

        [Dependency]
        public ContentManager ContentManager { get; set; }

        [Dependency]
        public StateManager StateManager { get; set; }

        [Dependency]
        public JamGame JamGame { get; set; }

        [Dependency]
        public AudioManager AudioManager { get; set; }

        public bool HasLoaded { get; set; }

        #endregion

        #region Methods

        public void Load(params object[] parameter)
        {
            Layers = new List<Component.Component>[Enum.GetNames(typeof(Layers)).Length];
            for (int i = 0; i < Layers.Length; i++) Layers[i] = new List<Component.Component>();
            LoadComponents(); 
            OnLoad(parameter);
            HasLoaded = true;
        }

        protected virtual void LoadComponents() { }
        protected virtual void OnLoad(params object[] parameter) { }

        public virtual void AddComponent(Component.Component component, int layer)
        {
            component.CurrentState = this;
            Layers[layer].Add(component);
        }
        
        public virtual void AddComponent(Component.Component component, Layers layer)
        {
            component.CurrentState = this;
            Layers[(int)layer].Add(component);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Layers.All(l => l.Count == 0)) return;

            for (int layer = 0; layer < Layers.Length; layer++)
            {
                List<Component.Component> DrawOrder = Layers[layer].OrderByDescending(c => c.Position.Y).ToList();
                // draw projectiles over everything
                List<Component.Component> projectiles = DrawOrder.FindAll(x => x is Projectile);
                DrawOrder.RemoveAll(x => x is Projectile);
                DrawOrder.AddRange(projectiles);
                DrawOrder.Reverse();
                for (int i = DrawOrder.Count - 1; i >= 0; i--)
                {
                    if (DrawOrder[i].Visible)
                        DrawOrder[i].Draw(gameTime, spriteBatch);
                }
            }

        }

        public virtual void PostUpdate(GameTime gameTime)
        {
            if (Layers.All(l => l.Count == 0))
            {
                return;
            }
            foreach (List<Component.Component> components in Layers)
            {
                for (int i = components.Count - 1; i >= 0; i--)
                {
                    if (components[i].IsRemoved)
                    {
                        components[i].OnRemove();
                        components.RemoveAt(i);
                    }
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            AudioManager.Update();
            if (Layers.All(l => l.Count == 0))
            {
                return;
            }

            foreach (List<Component.Component> components in Layers)
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
            IEnumerable<Sprite> sprites = Layers[(int)States.Layers.PlayingArea].Where(x => x is Sprite).Select(x => x as Sprite).ToList();
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