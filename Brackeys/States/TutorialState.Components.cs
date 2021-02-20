using Brackeys.Component;
using Brackeys.Component.Controls;
using Brackeys.Component.Sprites.Tower;
using Brackeys.Component.Sprites.Tutorial;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Brackeys.States
{
    public partial class TutorialState : GameState
    {
        Vector2 TextBoxPosition { get; set; }

        protected override void LoadComponents()
        {
            TextBoxPosition = new Vector2(425, 250);
            base.LoadComponents();
            FillTutorialTexts();
            ShowNextText(TextBoxPosition);
            AddComponent(new Fairy()
            {
                Position = new Vector2(300, 300)
            }, States.Layers.PlayingArea);
        }

        protected override void AddShop()
        {
            AddShopEntry(new NormalTower(), (Columns - UiWidthInCells + 0.8f) * CellSize, CellSize * 3);
        }

        protected override void UpdateUI()
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

            GetLabel("NormalTowerLabel").Text = $"{NormalTower.GlobalCost}";
        }

        private void ShowNextText(Vector2 position)
        {
            Component.Component x = Layers[(int)States.Layers.UI].FirstOrDefault(c => c is TextBox);
            if (x != null)
            {
                x.IsRemoved = true;
            }

            if (TutorialTexts.Count == 0) return;

            TextBox box = new TextBox(TutorialTexts.Dequeue(), ContentManager.KenneyMini(20))
            {
                Position = position
            };

            AddComponent(box, States.Layers.UI);
        }

        
    }
}
