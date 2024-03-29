﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brackeys.Component.Controls
{
    public class Label : Component
    {
        public Color FontColor { get; set; }
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public float FontScale { get; set; } = 1;
        public Color DropShadow { get; set; }
        public int DropShadowOffset { get; set; } = 1;

        public Label(SpriteFont font)
        {
            Font = font;
            FontColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (DropShadow != null) spriteBatch.DrawString(Font, Text, new Vector2(Position.X + DropShadowOffset, Position.Y + DropShadowOffset), DropShadow, 0, Vector2.Zero, FontScale, SpriteEffects.None, 0);
            if (!string.IsNullOrEmpty(Text)) spriteBatch.DrawString(Font, Text, new Vector2(Position.X, Position.Y), FontColor, 0, Vector2.Zero, FontScale, SpriteEffects.None, 0);
        }

        public override void Update(GameTime gameTime) { }
    }
}