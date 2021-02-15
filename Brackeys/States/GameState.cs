
using Brackeys.Component.Controls;
using Brackeys.Component.Sprites.Tower;
using Brackeys.Models;
using Brackeys.Models.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Brackeys.States
{
    public partial class GameState : State
    {
        public static string Name = "Game";
        private Level Level { get; set; }
        private bool LevelStarted { get; set; } = false;

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
            StartGame();

            Level.Update(gameTime, this);
            base.Update(gameTime);
            Player.Update(gameTime);
            UpdateUI();
        }

        private void StartGame()
        {
            if (LevelStarted) return;

            if (Layers[(int)States.Layers.PlayingArea].Any(x => x is Tower))
            {
                LevelStarted = true;
                Level.Start();
            }
        }

        private void UpdateUI()
        {
            GetLabel("StageLabel").Text = $"Level: {Level.CurrentStage}";
            GetLabel("HealthLabel").Text = $"Health: {Player.Health}";
            GetLabel("MoneyLabel").Text = $"Money: {Player.Money}";
        }

        private Label GetLabel(string name) => (Label)Layers[(int)States.Layers.UI].First(x => x is Label && ((Label)x).Name == name);

    }
}