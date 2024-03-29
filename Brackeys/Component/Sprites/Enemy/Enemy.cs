﻿using Brackeys.Models;
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
            VirtualHealth = Health; // wenn ein schuss nicht trifft, und v-Health <= 0 ist, wird der Gegner nicht mehr angegriffen
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

            if (GetTargePoints().Contains(new Point((int)Position.X + (Size.Width / 2), (int)Position.Y + (Size.Height / 2))))
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

        private Microsoft.Xna.Framework.Rectangle GetTargePoints()
        {
            int tolerance = Math.Max(1, (int)(Speed / 100));

            Vector2 center = new Vector2((TargetCell.Position.X + (TargetCell.Size.Width / 2)), TargetCell.Position.Y + (TargetCell.Size.Height / 2));

            Microsoft.Xna.Framework.Rectangle rect = new Microsoft.Xna.Framework.Rectangle((int)center.X - tolerance, (int)center.Y - tolerance, tolerance * 2, tolerance * 2);
            return rect;
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
                return damage / 1.5f;
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
                return damage / 1.5f;
            }

            if (Element == Elements.Wind && element == Elements.Fire)
            {
                return damage * 1.5f;
            }

            if (Element == Elements.Wind && element == Elements.Earth)
            {
                return damage / 1.5f;
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
