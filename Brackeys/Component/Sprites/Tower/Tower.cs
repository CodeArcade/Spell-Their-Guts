using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Brackeys.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brackeys.Component.Sprites.Tower
{
    public abstract class Tower : Sprite
    {
        public string Name { get; protected set; }

        protected int BaseDamage { get; set; }
        protected int BaseRange { get; set; }
        protected float BaseAttackSpeed { get; set; }

        public int Damage
        {
            get
            {
                if (IsMain)
                    return BaseDamage + GetTowersInRange().Sum(x => x.Damage);

                return BaseDamage;
            }
        }
        /// <summary>
        /// In cells
        /// </summary>
        public int Range
        {
            get
            {
                if (IsMain)
                    return BaseRange + GetTowersInRange().Sum(x => x.Range);

                return BaseRange;
            }
        }
        /// <summary>
        /// In seconds
        /// </summary>
        public float AttackSpeed
        {
            get
            {
                if (IsMain)
                    return BaseAttackSpeed + GetTowersInRange().Sum(x => x.AttackSpeed);

                return BaseAttackSpeed;
            }
        }

        public static int GlobalCost { get; set; }
        public int Cost => GlobalCost;
        protected Cell Cell { get; set; }

        public bool DrawRange { get; set; }
        public bool IsMain { get; protected set; }

        protected Cell[,] Cells => ((GameState)CurrentState).Cells;

        public Rectangle RangeRectangle
        {
            get
            {
                GameState state = (GameState)CurrentState;
                return new Rectangle((int)Position.X - (BaseRange * state.CellSize), (int)Position.Y - (BaseRange * state.CellSize), Size.Height + (BaseRange * state.CellSize * 2), Size.Width + (BaseRange * state.CellSize * 2));
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

        public virtual void OnPlace(Cell cell)
        {
            Cell = cell;
            Color = Color.White;
            DrawRange = false;

            IsMain = !GetTowersInRange().Any(x => x.IsMain);
        }

        protected List<Tower> GetTowersInRange()
        {
            List<Tower> towers = new List<Tower>();

            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    Tower tower = Cells[x, y].Tower;

                    if (tower is null) continue;
                    if (tower == this) continue;
                    if (tower.GetType() != GetType()) continue;

                    if (RangeRectangle.Contains(tower.Position))
                        towers.Add(tower);
                }
            }

            return towers;
        }
    }
}
