using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Brackeys.Component.Sprites.Enemy;
using Brackeys.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brackeys.Component.Sprites.Tower
{
    public abstract class Tower : Sprite
    {
        public string Name { get; protected set; }
        public Elements Element { get; set; }

        public int Damage { get; set; }

        /// <summary>
        /// In cells
        /// </summary>
        public float Range { get; set; }

        /// <summary>
        /// In seconds
        /// </summary>
        public float AttackSpeed { get; set; }
        public float TimeSinceLastShot { get; set; }

        public static int GlobalCost { get; set; }
        public int Cost => GlobalCost;
        public Cell Cell { get; set; }

        public bool DrawRange { get; set; }
        public bool IsMain { get; protected set; }
        protected bool HasBeenPlaced { get; set; }

        protected Cell[,] Cells => ((GameState)CurrentState).Cells;
        public Enemy.Enemy TargetedEnemy { get; set; }

        protected float DistanceToTargetedEnemy => TargetedEnemy != null ? Vector2.Distance(Center, TargetedEnemy.Center) : float.NaN;
        protected Vector2 DirectionToTargetedEnemy
        {
            get
            {
                if (TargetedEnemy == null) return Vector2.Zero;
                Vector2 direction = TargetedEnemy.Center - Center;
                direction.Normalize();

                return direction;
            }

        }
        protected float AngleToTargetedEnemy
        {
            get
            {
                if (TargetedEnemy == null) return float.NaN;
                return (float)Math.Atan2(TargetedEnemy.Center.Y - Center.Y, TargetedEnemy.Center.X - Center.X);
            }
        }

        protected Vector2 DirectionToMainTower
        {
            get
            {
                if (IsMain) return Vector2.Zero;
                Vector2 direction = GetTowersInRange().FirstOrDefault(x => x.IsMain).Center - Center;
                direction.Normalize();

                return direction;
            }

        }

        public Rectangle RangeRectangle
        {
            get
            {
                GameState state = (GameState)CurrentState;
                return new Rectangle((int)Position.X - (int)(Range * state.CellSize), (int)Position.Y - (int)(Range * state.CellSize), Size.Height + (int)(Range * state.CellSize * 2), Size.Width + (int)(Range * state.CellSize * 2));
            }
        }

        public Sprite RangeSprite { get; private set; }

        private bool AddedRangeSprite { get; set; }

        public Tower(int baseDamage, int baseRange, int baseAttackSpeed, Elements element)
        {
            Damage = baseDamage;
            Range = baseRange;
            AttackSpeed = baseAttackSpeed;
            Damage = baseDamage;
            Range = baseRange;
            AttackSpeed = baseAttackSpeed;
            Element = element;
        }

        public override void Update(GameTime gameTime)
        {
            PlayParticles();

            base.Update(gameTime);
            TargetEnemy();

            TimeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Shoot();
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
                if (RangeSprite != null)
                    RangeSprite.IsRemoved = true;
            }
        }

        private void PlayParticles()
        {
            switch (Element)
            {
                case Elements.Earth:
                    if (IsMain)
                        ParticleManager.GenerateNewParticle(Color.White, Center, ContentManager.MainEarthTowerParticle, 5, 10, maxSize: new System.Drawing.Size(10, 10), new Vector2(0, 0));
                    else
                        ParticleManager.GenerateNewParticle(Color.White, Center, ContentManager.SupportEarthTowerParticle, 2, 10, maxSize: new System.Drawing.Size(6, 6), DirectionToMainTower, 0.01f);
                    break;
                case Elements.Fire:
                    if (IsMain)
                        ParticleManager.GenerateNewParticle(Color.White, Center, ContentManager.MainFireTowerParticle, 5, 10, maxSize: new System.Drawing.Size(10, 10), new Vector2(0, 0));
                    else
                        ParticleManager.GenerateNewParticle(Color.White, Center, ContentManager.SupportFireTowerParticle, 2, 10, maxSize: new System.Drawing.Size(6, 6), DirectionToMainTower, 0.01f);
                    break;
                case Elements.Wind:
                    if (IsMain)
                        ParticleManager.GenerateNewParticle(Color.White, Center, ContentManager.MainWindTowerParticle, 5, 10, maxSize: new System.Drawing.Size(10, 10), new Vector2(0, 0));
                    else
                        ParticleManager.GenerateNewParticle(Color.White, Center, ContentManager.SupportWindTowerParticle, 2, 10, maxSize: new System.Drawing.Size(6, 6), DirectionToMainTower, 0.01f);
                    break;
            }
        }

        public override void OnRemove()
        {

            if (!IsMain)
            {
                Tower mainTower = GetTowersInRange().FirstOrDefault(x => x.IsMain);

                if (mainTower != null && HasBeenPlaced)
                    RevokeBuff(mainTower);
            }

            if (RangeSprite != null)
                RangeSprite.IsRemoved = true;
        }

        public void Shoot()
        {
            if (TargetedEnemy == null) return;
            if (TimeSinceLastShot < AttackSpeed) return;
            if (TargetedEnemy.VirtualHealth <= 0) return;
            TimeSinceLastShot = 0;
            Projectile p = new Projectile(DirectionToTargetedEnemy, this, Element, ContentManager.EnemyTexture, new System.Drawing.Size(15, 15));

            switch (Element)
            {
                case Elements.Earth:
                    AudioManager.PlayEffect(ContentManager.EarthShootSoundEffect);
                    break;
                case Elements.Fire:
                    AudioManager.PlayEffect(ContentManager.FireShootSoundEffect);
                    break;
                case Elements.Wind:
                    AudioManager.PlayEffect(ContentManager.WindShootSoundEffect);
                    break;
                default:
                    break;
            }

            TargetedEnemy.TakeVirtualDamage(Damage, Element);
            CurrentState.AddComponent(p, (int)Layers.PlayingArea);
        }

        public virtual void OnPlace(Cell cell)
        {
            Cell = cell;
            Color = Color.White;
            DrawRange = false;

            StartAnimation();

            Tower mainTower = GetTowersInRange().FirstOrDefault(x => x.IsMain);

            IsMain = mainTower is null;
            if (mainTower != null)
                ApplyBuff(mainTower);

            HasBeenPlaced = true;
        }

        protected abstract void ApplyBuff(Tower tower);

        protected abstract void RevokeBuff(Tower tower);

        public List<Tower> GetTowersInRange()
        {
            List<Tower> towers = new List<Tower>();

            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    Tower tower = Cells[x, y].Tower;

                    if (tower is null) continue;
                    if (tower == this) continue;

                    if (RangeRectangle.Contains(tower.Position))
                        towers.Add(tower);
                }
            }

            return towers;
        }

        private void TargetEnemy()
        {
            if (!IsMain) return;
            if (CurrentState is GameState gs)
            {
                List<Enemy.Enemy> enemies = gs.Layers[(int)Layers.PlayingArea]
                    .Where(c => c is Enemy.Enemy).ToList()
                    .Where(enemy => RangeRectangle.Contains(((Enemy.Enemy)enemy).Center)).ToList()
                    .ConvertAll(new Converter<Component, Enemy.Enemy>(x => (Enemy.Enemy)x));

                if (enemies.Count == 0)
                {
                    TargetedEnemy = null;
                    return;
                }

                Enemy.Enemy enemy = enemies.First();
                foreach (Enemy.Enemy e in enemies)
                {
                    e.Color = Color.White;
                    if (enemy == e) continue;
                    if (e.Progress > enemy.Progress) enemy = e;
                }

                TargetedEnemy = enemy;
            }
        }

        public abstract void StartAnimation();
    }
}
