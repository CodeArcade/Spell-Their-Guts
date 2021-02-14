using Brackeys.Component.Sprites;
using Microsoft.Xna.Framework;
using System.Drawing;
using Brackeys.Models;
using System.Collections.Generic;
using System.Linq;
using Brackeys.Component.Sprites.Tower;

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
            AddShop();
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

                    Player.CurrentTowerInHand = null;
                }
            };

            AddComponent(Cells[x, y], (int)Layers.Cells);
        }
    
        private void AddShop()
        {
            
        }
    }
}