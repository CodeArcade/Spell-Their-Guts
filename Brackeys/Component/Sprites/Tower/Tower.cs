using System;
using System.Collections.Generic;
using System.Text;
using Brackeys.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brackeys.Component.Sprites.Tower
{
    public class Tower : Sprite
    {
        public string Name { get; protected set; }
        public int Damage { get; set; }
        /// <summary>
        /// In cells
        /// </summary>
        public int Range { get; set; }
        public float FireSpeed { get; set; }
        public static int GlobalCost { get; set; }
        public int Cost => GlobalCost;

        public bool DrawRange { get; set; }
        public bool IsRoot { get; protected set; }

        public Rectangle RangeRectangle
        {
            get
            {
                GameState state = (GameState)CurrentState;
                return new Rectangle((int)Position.X - (Range * state.CellSize), (int)Position.Y - (Range * state.CellSize), Size.Height + (Range * state.CellSize * 2), Size.Width + (Range * state.CellSize * 2));
            }
        }

        public Sprite RangeSprite { get; private set; }

        private bool AddedRangeSprite { get; set; }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (DrawRange)
            {
                if (AddedRangeSprite)
                {
                    RangeSprite.Position = new Vector2(RangeRectangle.X, RangeRectangle.Y);
                    RangeSprite.Size = new System.Drawing.Size(RangeRectangle.Size.X, RangeRectangle.Size.Y);
                }
                else
                {
                    RangeSprite = new Sprite() { Texture = ContentManager.RangeTexture, Color = Color.White * 0.5f };
                    RangeSprite.Position = new Vector2(RangeRectangle.X, RangeRectangle.Y);
                    RangeSprite.Size = new System.Drawing.Size(RangeRectangle.Size.X, RangeRectangle.Size.Y);
                    CurrentState.AddComponent(RangeSprite, (int)Layers.UI);
                }

                AddedRangeSprite = true;
            }
            else
            {
                AddedRangeSprite = false;
                RangeSprite.IsRemoved = true;
            }
        }

        public override void OnRemove()
        {
            RangeSprite.IsRemoved = true;
        }

        public void Shoot()
        {

        }

        public virtual void OnPlace(Cell cell, Cell[,] cells)
        {
            Color = Color.White;
            DrawRange = false;
        }

    }
}
