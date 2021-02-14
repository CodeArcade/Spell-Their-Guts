using Brackeys.States;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Point = Microsoft.Xna.Framework.Point;

namespace Brackeys.Component.Sprites.Enemy
{
    public class Enemy : Sprite
    {
        public Point LastCoordinate { get; set; }
        public Point Coordinate { get; set; }
        public int Reward { get; set; }

        private Cell TargetCell { get; set; }

        public Enemy()
        {
            Texture = ContentManager.EnemyTexture;
            Speed = 100;
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

            if (new Point((int)Position.X + Size.Width / 2, (int)Position.Y + Size.Height / 2) ==
                new Point(targetCell.Coordinate.X * state.CellSize + targetCell.Size.Width / 2, targetCell.Coordinate.Y * state.CellSize + targetCell.Size.Height / 2))
            {
                LastCoordinate = Coordinate;
                Coordinate = TargetCell.Coordinate;
                TargetCell = null;
                Direction = Vector2.Zero;
            }

        }

        private bool IsLastCell(Cell cell)
        {
            return cell.Coordinate.X == LastCoordinate.X && cell.Coordinate.Y == LastCoordinate.Y;
        }

    }
}
