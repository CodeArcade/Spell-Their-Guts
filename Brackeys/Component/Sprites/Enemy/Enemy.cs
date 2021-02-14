using Brackeys.Models;
using Brackeys.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Point = Microsoft.Xna.Framework.Point;

namespace Brackeys.Component.Sprites.Enemy
{
    public class Enemy : Sprite
    {
        public Point LastCoordinate { get; set; }
        public Point Coordinate { get; set; }
        public Point TargetCoordinate { get; set; }

        public int Reward { get; set; }
        public int Damage { get; set; }

        private Cell TargetCell { get; set; }

        public Enemy()
        {
            Texture = ContentManager.EnemyTexture;
            Speed = 100;
            Damage = 1;
        }

        public override void Update(GameTime gameTime)
        {
            Move();
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        private void Move()
        {
            GameState state = (GameState)CurrentState;
            Cell targetCell = null;

            if (Coordinate == TargetCoordinate)
            {
                state.Player.Health -= Damage;
                IsRemoved = true;
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

    }
}
