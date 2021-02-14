
using Brackeys.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brackeys.States
{
    public partial class GameState : State
    {
        public static string Name = "Game";
        public Player Player { get; set; }


        public GameState()
        {
            Player = new Player();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (Player.CurrentTowerInHand != null)
            {
                Player.CurrentTowerInHand.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Player.Update(gameTime);
        }

    }
}