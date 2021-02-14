
using Brackeys.Models;
using Brackeys.Models.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Brackeys.States
{
    public partial class GameState : State
    {
        public static string Name = "Game";
        private Level Level { get; set; }

        public GameState()
        {
            Level = new Level1();
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
            Level.Update(gameTime, this);
            base.Update(gameTime);
            Player.Update(gameTime);
        }

    }
}