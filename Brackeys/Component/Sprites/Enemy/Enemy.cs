using Brackeys.Models;
using Brackeys.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Point = Microsoft.Xna.Framework.Point;

namespace Brackeys.Component.Sprites.Enemy
{
    public enum Elements
    {
        Earth,
        Fire,
        Wind,
        None
    }
    public abstract class Enemy : Entity
    {
        public Point LastCoordinate { get; set; }
        public Point Coordinate { get; set; }
        public Point TargetCoordinate { get; set; }
        protected Dictionary<string, Animation> Animations { get; set; }

        public int Reward { get; set; }

        public Elements Element { get; set; }

        private Cell TargetCell { get; set; }
        public float Progress { get; set; } = 0;
        public float VirtualHealth { get; set; }

        public Enemy(Elements element) { Element = element; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (Element)
            {
                case Elements.Earth:
                    Color = Microsoft.Xna.Framework.Color.Brown;
                    break;
                case Elements.Fire:
                    Color = Microsoft.Xna.Framework.Color.Red;
                    break;
                case Elements.Wind:
                    Color = Microsoft.Xna.Framework.Color.LightBlue;
                    break;
            }

            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            Progress += Speed;
            base.Update(gameTime);
        }

        private void Move()
        {
            GameState state = (GameState)CurrentState;
            Cell targetCell = null;

            if (Coordinate == TargetCoordinate)
            {
                state.Player.Health -= Damage;
                Reward = 0;
                IsRemoved = true;
                AudioManager.PlayEffect(ContentManager.HpLostSoundEffect);
                return;
            }

            if (Coordinate.X > 0)
            {
                if (state.Cells[Coordinate.X - 1, Coordinate.Y].IsPath && !IsLastCell(state.Cells[Coordinate.X - 1, Coordinate.Y]))
                {
                    targetCell = state.Cells[Coordinate.X - 1, Coordinate.Y];
                    Direction = new Vector2(-1, 0);
                }
            }
            if (Coordinate.X < state.Columns)
            {
                if (state.Cells[Coordinate.X + 1, Coordinate.Y].IsPath && !IsLastCell(state.Cells[Coordinate.X + 1, Coordinate.Y]))
                {
                    targetCell = state.Cells[Coordinate.X + 1, Coordinate.Y];
                    Direction = new Vector2(1, 0);
                }
            }

            if (Coordinate.Y > 0)
            {
                if (state.Cells[Coordinate.X, Coordinate.Y - 1].IsPath && !IsLastCell(state.Cells[Coordinate.X, Coordinate.Y - 1]))
                {
                    targetCell = state.Cells[Coordinate.X, Coordinate.Y - 1];
                    Direction = new Vector2(0, -1);
                }
            }
            if (Coordinate.Y < state.Rows)
            {
                if (state.Cells[Coordinate.X, Coordinate.Y + 1].IsPath && !IsLastCell(state.Cells[Coordinate.X, Coordinate.Y + 1]))
                {
                    targetCell = state.Cells[Coordinate.X, Coordinate.Y + 1];
                    Direction = new Vector2(0, 1);
                }
            }

            if (targetCell is null)
            {
                Direction = Vector2.Zero;
                return;
            }

            TargetCell = targetCell;

            if (GetTargePoints().Any(x => x == new Point((int)Position.X + Size.Width / 2, (int)Position.Y + Size.Height / 2)))
            {
                LastCoordinate = Coordinate;
                Coordinate = TargetCell.Coordinate;
                Position = TargetCell.Position;
                Direction = Vector2.Zero;
            }

        }

        private bool IsLastCell(Cell cell)
        {
            return cell.Coordinate.X == LastCoordinate.X && cell.Coordinate.Y == LastCoordinate.Y;
        }

        private List<Point> GetTargePoints()
        {
            GameState state = (GameState)CurrentState;
            List<Point> points = new List<Point>();

            int tolerance = Math.Max(1, (int)(Speed / 100));

            for (int x = -tolerance; x < tolerance + 1; x++)
            {
                for (int y = -tolerance; y < tolerance + 1; y++)
                {
                    points.Add(new Point(x + TargetCell.Coordinate.X * state.CellSize + TargetCell.Size.Width / 2, y + TargetCell.Coordinate.Y * state.CellSize + TargetCell.Size.Height / 2));
                }
            }

            return points;
        }

        public void TakeDamage(int damage, Elements element)
        {
            TakeDamage(CalculateDamage(damage, element));
        }

        public void TakeVirtualDamage(int damage, Elements element)
        {
            VirtualHealth -= CalculateDamage(damage, element);
        }

        public float CalculateDamage(int damage, Elements element)
        {
            if (element == Element)
                return damage;

            if (Element == Elements.Earth && element == Elements.Fire)
            {
                return damage / 2f;
            }

            if (Element == Elements.Earth && element == Elements.Wind)
            {
                return damage * 1.5f;
            }

            if (Element == Elements.Fire && element == Elements.Earth)
            {
                return damage * 1.5f;
            }

            if (Element == Elements.Fire && element == Elements.Wind)
            {
                return damage / 2f;
            }

            if (Element == Elements.Wind && element == Elements.Fire)
            {
                return damage * 1.5f;
            }

            if (Element == Elements.Wind && element == Elements.Earth)
            {
                return damage / 2f;
            }

            return damage;
        }

        public override void OnRemove()
        {
            ((GameState)CurrentState).Player.Money += Reward;
            base.OnRemove();
        }

        public abstract void LevelUp();
    }
}
