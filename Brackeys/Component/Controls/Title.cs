using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brackeys.Component.Controls
{
    class Title : Component
    {

        public Texture2D Texture => ContentManager.Title;
        public Rectangle Rectangle { get; set; }

        private Vector2 InternalPosition { get; set; }
        new public Vector2 Position
        {
            get => InternalPosition;
            set
            {
                InternalPosition = value;
                Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width / 2, Texture.Height / 2);
                AnimationManager.Position = new Vector2(value.X - (Texture.Width * AnimationManager.Scale / 3), value.Y - (Texture.Height / 2));
            }
        }

        public Title()
        {
            AnimationManager.Scale = 3f;
            AnimationManager.Play(new Models.Animation(Texture, 1));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            AnimationManager.Draw(spriteBatch, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
