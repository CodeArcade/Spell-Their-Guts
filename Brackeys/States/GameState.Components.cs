using System;
using Brackeys.Component.Sprites;
using Microsoft.Xna.Framework;
using System.Drawing;

namespace Brackeys.States
{

    public partial class GameState : State
    {
        public Cell[,] Cells { get; set; }

        public int Columns => 32;
        public int Rows => 18;
        public int UiWidthInCells => 5;
        public int CellSize { get; private set; }

        protected override void LoadComponents()
        {
            BuildGrid(Columns, Rows);
        }

        private void BuildGrid(int columns, int rows)
        {
            CellSize = JamGame.GraphicsDevice.Viewport.Bounds.Width / Columns;
            Cells = new Cell[columns - UiWidthInCells, rows];

            for (int x = 0; x < columns - UiWidthInCells; x++)
                for (int y = 0; y < rows; y++)
                    AddCell(x, y);
        }

        private void AddCell(int x, int y)
        {
            Cells[x, y] = new Cell(x, y)
            {
                Size = new Size(CellSize, CellSize),
                Position = new Vector2(x * CellSize, y * CellSize)
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

                    // TODO: check if player money is enough to place tower

                    Cells[x, y].Tower = Player.CurrentTowerInHand.Copy();
                    Cells[x, y].Tower.Position = Cells[x, y].Position;
                    Cells[x, y].Tower.Size = Cells[x, y].Size;
                    Cells[x, y].Tower.OnPlace(Cells[x, y], Cells);

                    Player.CurrentTowerInHand = null;
                }
            };

            AddComponent(Cells[x, y], (int)Layers.Cells);
        }
    }
}