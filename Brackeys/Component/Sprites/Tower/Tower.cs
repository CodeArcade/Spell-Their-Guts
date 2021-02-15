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

        public int Damage { get; set; }
        /// <summary>
        /// In cells
        /// </summary>
        public int Range { get; set; }
        /// <summary>
        /// In seconds
        /// </summary>
        public float AttackSpeed { get; set; }

        public static int GlobalCost { get; set; }
        public int Cost => GlobalCost;
        protected Cell Cell { get; set; }

        public bool DrawRange { get; set; }
        public bool IsMain { get; protected set; }

        protected Cell[,] Cells => ((GameState)CurrentState).Cells;
        public List<Tower> BuffedTowers = new List<Tower>();

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

        public Tower(int baseDamage, int baseRange, int baseAttackSpeed)
        {
            BaseDamage = baseDamage;
            BaseRange = baseRange;
            BaseAttackSpeed = baseAttackSpeed;
            Damage = baseDamage;
            Range = baseRange;
            AttackSpeed = baseAttackSpeed;
        }

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

            if (!IsMain)
            {
                foreach (Tower t in GetTowersInRange())
                { 
                    RevokeBuff(t);
                }
            }

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

            List<Tower> towersInRange = GetTowersInRange();

            IsMain = !towersInRange.Any(x => x.IsMain);
            foreach (Tower t in towersInRange)
            {
                ApplyBuff(t);
            }
        }

        protected abstract void ApplyBuff(Tower tower);

        protected abstract void RevokeBuff(Tower tower);

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
