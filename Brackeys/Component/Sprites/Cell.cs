using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Brackeys.Component.Sprites
{
    public class Cell: Component
    {
        private Size InternalSize { get; set; }

        private MouseState CurrentMouse { get; set; }
        private MouseState PreviousMouse { get; set; }
        public bool IsMouseOver { get; private set; }

        public Color Color => Color.White;
        public Color HoverColor { get; set; } = Color.Gray;
        public string Text { get; set; }
        public bool Clicked { get; private set; }

        public Size Size
        {
            get
            {
                if (InternalSize == Size.Empty)
                {
                    if (AnimationManager.IsPlaying)
                        InternalSize = new Size(AnimationManager.AnimationRectangle.Width, AnimationManager.AnimationRectangle.Height);
                    else
                        InternalSize = new Size(Texture.Width, Texture.Height);
                }
                return InternalSize;
            }
            set
            {
                InternalSize = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                if (AnimationManager.IsPlaying) return AnimationManager.AnimationRectangle;

                return new Rectangle((int)Position.X, (int)Position.Y, Size.Width, Size.Height);
            }
        }
        public Texture2D Texture { get; set; }

        public event EventHandler OnClick;

        public Tower.Tower Tower { get; set; }
        public Microsoft.Xna.Framework.Point Coordinate { get; set; }
        public bool IsPath { get; set; }

        public Cell(int x, int y)
        {
            Texture = ContentManager.EnemyTexture;
            Coordinate = new Microsoft.Xna.Framework.Point(x, y);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color;
            if (IsMouseOver) color = HoverColor;

            spriteBatch.Draw(Texture, Rectangle, color);

            if (Tower != null) Tower.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            PreviousMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(CurrentMouse.X, CurrentMouse.Y, 1, 1);
            Rectangle previousMouseRectangle = new Rectangle(CurrentMouse.X, CurrentMouse.Y, 1, 1);

            IsMouseOver = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                IsMouseOver = true;

                if (CurrentMouse.LeftButton == ButtonState.Released && PreviousMouse.LeftButton == ButtonState.Pressed)
                {
                    OnClick?.Invoke(this, new EventArgs());
                }
            }

            if (Tower != null) Tower.Update(gameTime);
        }
    }
}
