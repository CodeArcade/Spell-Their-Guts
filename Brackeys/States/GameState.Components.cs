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

        public int Columns => 32;
        public int Rows => 18;
        public int UiWidthInCells => 5;
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
                Position = new Vector2(x * CellSize, y * CellSize),
                IsPath = paths.Any(pa => pa.Track.Any(po => po.X == x && po.Y == y))
            };

            Cells[x, y].OnClick += (sender, e) =>
            {
                if (Cells[x, y].IsPath) return;

                if (Cells[x, y].Tower != null)
                {
                    // TODO: Show info to tower
                }
                else
                {
                    if (Player.CurrentTowerInHand is null) return;

                    if (Player.Money < Player.CurrentTowerInHand.Cost) return;
                    Player.Money -= Player.CurrentTowerInHand.Cost;

                    Cells[x, y].Tower = (Tower)Player.CurrentTowerInHand.Copy();
                    Cells[x, y].Tower.Position = Cells[x, y].Position;
                    Cells[x, y].Tower.Size = Cells[x, y].Size;
                    Cells[x, y].Tower.OnPlace(Cells[x, y], Cells);

                    AddComponent(Cells[x, y].Tower, (int)Layers.PlayingArea);

                    Player.CurrentTowerInHand = null;
                }
            };

            AddComponent(Cells[x, y], (int)Layers.Cells);
        }

        private void AddUi()
        {
            #region Ui
            AddComponent(
                new Label(ContentManager.TestFont)
                {
                    Name = "HealthLabel",
                    Position = new Vector2((Columns - UiWidthInCells + 1) * CellSize, CellSize / 2),
                    FontColor = Color.Black,
                    Text = "test"
                },
                (int)Layers.UI);

            AddComponent(
            new Label(ContentManager.TestFont)
            {
                Name = "MoneyLabel",
                Position = new Vector2((Columns - UiWidthInCells + 1) * CellSize, CellSize),
                FontColor = Color.Black,
                Text = "test"
            },
            (int)Layers.UI);
            #endregion

            #region Shop

            AddShopEntry(new FireTower(), (Columns - UiWidthInCells + 1) * CellSize, CellSize * 2);
            AddShopEntry(new FireTower(), (Columns - UiWidthInCells + 3) * CellSize, CellSize * 2);

            AddShopEntry(new FireTower(), (Columns - UiWidthInCells + 1) * CellSize, CellSize * 4);
            AddShopEntry(new FireTower(), (Columns - UiWidthInCells + 3) * CellSize, CellSize * 4);

            AddShopEntry(new FireTower(), (Columns - UiWidthInCells + 1) * CellSize, CellSize * 6);
            AddShopEntry(new FireTower(), (Columns - UiWidthInCells + 3) * CellSize, CellSize * 6);
            #endregion
        }

        private void AddShopEntry(Tower tower, int x, int y)
        {
            Button button = new Button(tower.Texture, ContentManager.TestFont)
            {
                Position = new Vector2(x, y),
                Size = new Size(CellSize, CellSize)
            };

            button.OnClick += (sender, e) =>
            {
                Player.CurrentTowerInHand = (Tower)tower.Copy();
                Player.CurrentTowerInHand.Color = Color.White * 0.7f;
            };

            Label label = new Label(ContentManager.TestFont)
            {
                Position = new Vector2(x, y + CellSize),
                Text = tower.Cost.ToString()
            };

            AddComponent(button, (int)Layers.UI);
            AddComponent(label, (int)Layers.UI);
        }

    }

}