using System;
using System.Collections.Generic;
using System.Text;
using Brackeys.Component.Sprites.Tower;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Brackeys.Models
{
    public class Player : Component.Component
    {
        public Tower CurrentTowerInHand { get; set; }
        public int Money { get; set; }
        public int Health { get; set; }

        public Player()
        {
            Money = 5;
            Health = 100;           
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            CancelPlace();
            if (CurrentTowerInHand != null)
            {
                Point p = Mouse.GetState().Position;
                CurrentTowerInHand.Position = new Vector2(p.X - CurrentTowerInHand.Size.Width / 2, p.Y - CurrentTowerInHand.Size.Height / 2);
            }
        }

        public void CancelPlace()
        {
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                CurrentTowerInHand = null;
            }
        }
    }
}
