using Brackeys.Component.Sprites;
using Microsoft.Xna.Framework;
using System.Drawing;
using Brackeys.Models;
using System.Collections.Generic;
using System.Linq;
using Brackeys.Component.Sprites.Tower;
using Brackeys.Component.Controls;
using Color = Microsoft.Xna.Framework.Color;

namespace Brackeys.States
{
    public partial class GameState : State
    {
        public Player Player { get; set; }
        public Cell[,] Cells { get; set; }

        public int Columns => 24;
        public int Rows => 16;
        public int UiWidthInCells => 3;
        public int CellSize { get; private set; }

        protected override void LoadComponents()
        {
            Player = new Player();

            AddGrid(Columns, Rows);
            AddUi();
        }

        private void AddGrid(int columns, int rows)
        {
            CellSize = JamGame.GraphicsDevice.Viewport.Bounds.Width / Columns;
            Cells = new Cell[columns - UiWidthInCells, rows];

            for (int x = 0; x < columns - UiWidthInCells; x++)
                for (int y = 0; y < rows; y++)
                    AddCell(x, y, Level.Paths);
        }

        private void AddCell(int x, int y, List<Path> paths)
        {
            Cells[x, y] = new Cell(x, y)
            {
                Size = new Size(CellSize, CellSize),
                Position = new Vector2(x * CellSize - CellSize, y * CellSize - CellSize),
                IsPath = paths.Any(pa => pa.Track.Any(po => po.X == x && po.Y == y))
            };

            Cells[x, y].OnClick += (sender, e) =>
            {
                foreach (Tower tower in Layers[(int)States.Layers.PlayingArea].Where(x => x is Tower))
                {
                    tower.DrawRange = false;
                    Player.SelectedTower = null;
                }

                if (Cells[x, y].IsPath) return;

                if (Cells[x, y].Tower != null)
                {
                    Player.SelectedTower = Cells[x, y].Tower.Copy<Tower>();
                    Cells[x, y].Tower.DrawRange = true;
                }
                else
                {
                    if (Player.CurrentTowerInHand is null) return;

                    if (Player.Money < Player.CurrentTowerInHand.Cost) return;
                    Player.Money -= Player.CurrentTowerInHand.Cost;

                    if (Player.CurrentTowerInHand is FireTower)
                        Cells[x, y].Tower = new FireTower();
                    else if (Player.CurrentTowerInHand is EarthTower)
                        Cells[x, y].Tower = new EarthTower();
                    else if (Player.CurrentTowerInHand is WindTower)
                        Cells[x, y].Tower = new WindTower();
                    else
                        Cells[x, y].Tower = new NormalTower();

                    Cells[x, y].Tower.CurrentState = this;
                    Cells[x, y].Tower.Position = Cells[x, y].Position;
                    Cells[x, y].Tower.Size = Cells[x, y].Size;
                    Cells[x, y].Tower.OnPlace(Cells[x, y]);

                    AudioManager.PlayEffect(ContentManager.SelectSoundEffect);

                    if (Player.CurrentTowerInHand is FireTower)
                        FireTower.GlobalCost += 10;
                    else if (Player.CurrentTowerInHand is EarthTower)
                        EarthTower.GlobalCost += 10;
                    else
                        WindTower.GlobalCost += 10;

                    base.AddComponent(Cells[x, y].Tower, (int)States.Layers.PlayingArea);

                    Player.Unselect();
                }
            };

            base.AddComponent(Cells[x, y], (int)States.Layers.Cells);
        }

        private void AddUi()
        {
            AddPlayerInfoText();
            AddShop();
            AddTowerInfo();
        }

        protected void AddPlayerInfoText()
        {
            base.AddComponent(
             new Label(ContentManager.TestFont)
             {
                 Name = "StageLabel",
                 Position = new Vector2((Columns - UiWidthInCells) * CellSize, CellSize / 2),
                 FontColor = Color.Black,
                 Text = "test"
             },
             (int)States.Layers.UI);

            base.AddComponent(
                new Label(ContentManager.TestFont)
                {
                    Name = "HealthLabel",
                    Position = new Vector2((Columns - UiWidthInCells) * CellSize, CellSize),
                    FontColor = Color.Black,
                    Text = "test"
                },
                (int)States.Layers.UI);

            base.AddComponent(
            new Label(ContentManager.TestFont)
            {
                Name = "MoneyLabel",
                Position = new Vector2((Columns - UiWidthInCells) * CellSize, CellSize * 1.5f),
                FontColor = Color.Black,
                Text = "test"
            },
            (int)States.Layers.UI);
        }

        protected virtual void AddShop()
        {
            AddShopEntry(new FireTower(), (Columns - UiWidthInCells + 0.8f) * CellSize, CellSize * 3);
            AddShopEntry(new EarthTower(), (Columns - UiWidthInCells + 0.8f) * CellSize, CellSize * 5);
            AddShopEntry(new WindTower(), (Columns - UiWidthInCells + 0.8f) * CellSize, CellSize * 7);
        }

        protected void AddTowerInfo()
        {
            base.AddComponent(
                new Label(ContentManager.TestFont)
                {
                    Name = "TowerNameLabel",
                    Position = new Vector2((Columns - UiWidthInCells) * CellSize, (CellSize * Rows) - (7.5f * CellSize)),
                    FontColor = Color.Black,
                    Text = ""
                },
                (int)States.Layers.UI);

            base.AddComponent(
                new Label(ContentManager.TestFont)
                {
                    Name = "TowerElementLabel",
                    Position = new Vector2((Columns - UiWidthInCells) * CellSize, (CellSize * Rows) - (7f * CellSize)),
                    FontColor = Color.Black,
                    Text = ""
                },
                (int)States.Layers.UI);

            base.AddComponent(
                new Label(ContentManager.TestFont)
                {
                    Name = "TowerDamageLabel",
                    Position = new Vector2((Columns - UiWidthInCells) * CellSize, (CellSize * Rows) - (6.2f * CellSize)),
                    FontColor = Color.Black,
                    Text = ""
                },
                (int)States.Layers.UI);

            base.AddComponent(
                new Label(ContentManager.TestFont)
                {
                    Name = "TowerRangeLabel",
                    Position = new Vector2((Columns - UiWidthInCells) * CellSize, (CellSize * Rows) - (5.8f * CellSize)),
                    FontColor = Color.Black,
                    Text = ""
                },
                (int)States.Layers.UI);

            base.AddComponent(
                new Label(ContentManager.TestFont)
                {
                    Name = "TowerSpeedLabel",
                    Position = new Vector2((Columns - UiWidthInCells) * CellSize, (CellSize * Rows) - (5.4f * CellSize)),
                    FontColor = Color.Black,
                    Text = ""
                },
                (int)States.Layers.UI);

            Button button = new Button(ContentManager.RangeTexture, ContentManager.TestFont)
            {
                Position = new Vector2((Columns - UiWidthInCells) * CellSize - (CellSize / 2), (CellSize * Rows) - (4f * CellSize)),
                FontColor = Color.Black,
                Text = "Sell",
                Size = new Size(CellSize * 3, CellSize),
                Name = "SellTowerButton"
            };
            button.OnClick += (sender, e) =>
            {
                if (Player.SelectedTower != null && !Player.SelectedTower.IsRemoved)
                {
                    Player.Money += (int)(Player.SelectedTower.Cost * 0.5);

                    Cells[Player.SelectedTower.Cell.Coordinate.X, Player.SelectedTower.Cell.Coordinate.Y].Tower.OnRemove();
                    Layers[(int)States.Layers.PlayingArea].Remove(Cells[Player.SelectedTower.Cell.Coordinate.X, Player.SelectedTower.Cell.Coordinate.Y].Tower);
                    Cells[Player.SelectedTower.Cell.Coordinate.X, Player.SelectedTower.Cell.Coordinate.Y].Tower = null;
                    Player.SelectedTower = null;

                    Player.Unselect();
                }
            };

            base.AddComponent(button, (int)States.Layers.UI);
        }

        protected void AddShopEntry(Tower tower, float x, float y)
        {
            Button button = new Button(ContentManager.SmallWhiteButton, ContentManager.TestFont)
            {
                Position = new Vector2(x, y),
                Size = new Size(CellSize, CellSize),
            };

            button.AnimationManager.Scale = 3f;
            button.AnimationManager.Parent = button;

            if (tower is FireTower)
                button.AnimationManager.Play(new Animation(ContentManager.FireTowerTexture, 4) { FrameSpeed = 0.1f });
            else if (tower is EarthTower)
                button.AnimationManager.Play(new Animation(ContentManager.EarthTowerTexture, 4) { FrameSpeed = 0.1f });
            else if (tower is WindTower)
                button.AnimationManager.Play(new Animation(ContentManager.WindTowerTexture, 4) { FrameSpeed = 0.1f });
            else
                button.AnimationManager.Play(new Animation(ContentManager.NormalTowerTexture, 4) { FrameSpeed = 0.1f });

            button.OnClick += (sender, e) =>
            {
                if (Player.CurrentTowerInHand != null)
                {
                    Player.Unselect();
                }
                foreach (Tower tower in Layers[(int)States.Layers.PlayingArea].Where(x => x is Tower))
                {
                    tower.DrawRange = false;
                    Player.SelectedTower = null;
                }

                if (tower is FireTower)
                    Player.CurrentTowerInHand = new FireTower();
                else if (tower is EarthTower)
                    Player.CurrentTowerInHand = new EarthTower();
                else if (tower is WindTower)
                    Player.CurrentTowerInHand = new WindTower();
                else
                    Player.CurrentTowerInHand = new NormalTower();

                Player.CurrentTowerInHand.StartAnimation();
                Player.CurrentTowerInHand.Size = new Size(CellSize, CellSize);
                Player.CurrentTowerInHand.Color = Color.White * 0.7f;
                Player.CurrentTowerInHand.DrawRange = true;
                Player.CurrentTowerInHand.CurrentState = this;
                Player.SelectedTower = Player.CurrentTowerInHand;
            };

            Label label = new Label(ContentManager.TestFont)
            {
                Position = new Vector2(x, y + CellSize),
                Text = tower.Cost.ToString(),
                Name = $"{tower.GetType().Name}Label"
            };

            base.AddComponent(button, (int)States.Layers.UI);
            base.AddComponent(label, (int)States.Layers.UI);
        }
    }
}