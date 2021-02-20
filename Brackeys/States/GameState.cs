﻿using Brackeys.Component.Controls;
using Brackeys.Component.Sprites.Tower;
using Brackeys.Models;
using Brackeys.Models.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace Brackeys.States
{
    public partial class GameState : State
    {
        public static string Name = "Game";
        protected Level Level { get; set; }
        protected bool LevelStarted { get; set; } = false;

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

            if (Player.Health <= 0)
            {
                OnPlayerDeath();
                return;
            }

            if(Level.Stages.Count == 0)
            {
                OnWin();
                return;
            }

            Level.Update(gameTime, this);
            base.Update(gameTime);
            Player.Update(gameTime);
            UpdateUI();
            GC.Collect();
        }

        private void OnPlayerDeath()
        {
            StateManager.ChangeTo<GameOverState>(GameOverState.Name, false, Level.CurrentStage);
        }

        private void OnWin()
        {
            StateManager.ChangeTo<GameOverState>(GameOverState.Name, true, Level.CurrentStage);
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

        protected virtual void UpdateUI()
        {
            GetLabel("StageLabel").Text = $"Level: {Level.CurrentStage}";
            GetLabel("HealthLabel").Text = $"Health: {Player.Health}";
            GetLabel("MoneyLabel").Text = $"Money: {Player.Money}";

            GetLabel("TowerNameLabel").Visible = Player.SelectedTower != null;
            GetLabel("TowerElementLabel").Visible = Player.SelectedTower != null;
            GetLabel("TowerDamageLabel").Visible = Player.SelectedTower != null;
            GetLabel("TowerRangeLabel").Visible = Player.SelectedTower != null;
            GetLabel("TowerSpeedLabel").Visible = Player.SelectedTower != null;
            GetButton("SellTowerButton").Visible = Player.SelectedTower != null && Player.CurrentTowerInHand is null && (!Player.SelectedTower.IsMain || Player.SelectedTower.GetTowersInRange().Count == 1);

            GetLabel("TowerNameLabel").Text = $"{Player.SelectedTower?.Name}";
            GetLabel("TowerElementLabel").Text = $"{Player.SelectedTower?.Element.ToString()}";
            GetLabel("TowerDamageLabel").Text = $"Damage: {Player.SelectedTower?.Damage}";
            GetLabel("TowerRangeLabel").Text = $"Range: {Player.SelectedTower?.Range}";
            GetLabel("TowerSpeedLabel").Text = $"Speed: {Player.SelectedTower?.AttackSpeed}";

            GetLabel("FireTowerLabel").Text = $"{FireTower.GlobalCost}";
            GetLabel("EarthTowerLabel").Text = $"{EarthTower.GlobalCost}";
            GetLabel("WindTowerLabel").Text = $"{WindTower.GlobalCost}";
        }

        protected Label GetLabel(string name) => (Label)Layers[(int)States.Layers.UI].First(x => x is Label label && label.Name == name);
        protected Button GetButton(string name) => (Button)Layers[(int)States.Layers.UI].First(x => x is Button button && button.Name == name);
    }
}