using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Brackeys.Component.Sprites;

namespace Brackeys.Component.Controls
{
    enum TextBoxParts
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    class TextBox : Sprite
    {
        private int TextBoxPartSize => 16;
        private List<List<TextBoxParts>> TextBoxParts { get; set; }
        public string Text { get; set; }

        public TextBox(string text, SpriteFont font)
        {
            Texture = ContentManager.Textbox;
            Text = text;
            Vector2 stringSize = font.MeasureString(text);
            int xCount = Math.Max(3, (int)stringSize.X / TextBoxPartSize);
            int yCount = Math.Max(3, (int)stringSize.Y / TextBoxPartSize);

            TextBoxParts = new List<List<TextBoxParts>>();

            for (int i = 0; i < yCount; i++)
            {
                TextBoxParts.Add(new List<TextBoxParts>());

                for (int j = 0; j < xCount; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            TextBoxParts[i].Add(Controls.TextBoxParts.TopLeft);
                        } else if (j == xCount - 1)
                        {
                            TextBoxParts[i].Add(Controls.TextBoxParts.TopRight);
                        } else
                        {
                            TextBoxParts[i].Add(Controls.TextBoxParts.TopCenter);
                        }

                        continue;
                    }

                    if (i == yCount - 1)
                    {
                        if (j == 0)
                        {
                            TextBoxParts[i].Add(Controls.TextBoxParts.BottomLeft);
                        }
                        else if (j == xCount - 1)
                        {
                            TextBoxParts[i].Add(Controls.TextBoxParts.BottomRight);
                        }
                        else
                        {
                            TextBoxParts[i].Add(Controls.TextBoxParts.BottomCenter);
                        }

                        continue;
                    }

                    if (j == 0)
                    {
                        TextBoxParts[i].Add(Controls.TextBoxParts.MiddleLeft);
                    }
                    else if (j == xCount - 1)
                    {
                        TextBoxParts[i].Add(Controls.TextBoxParts.MiddleRight);
                    }
                    else
                    {
                        TextBoxParts[i].Add(Controls.TextBoxParts.MiddleCenter);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < TextBoxParts.Count; y++)
            {
                for (int x = 0; x < TextBoxParts[y].Count; x++)
                {
                    spriteBatch.Draw(Texture, new Vector2(Position.X + x * TextBoxPartSize, Position.Y + y * TextBoxPartSize), new Rectangle((int)TextBoxParts[y][x] * TextBoxPartSize,
                                               0,
                                               TextBoxPartSize,
                                               TextBoxPartSize), Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
                }
            }

            spriteBatch.DrawString(ContentManager.KenneyMini(20), Text, new Vector2(Position.X + 15, Position.Y + 15), Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
