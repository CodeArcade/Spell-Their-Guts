
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
        public int UiWidthInCells  => 5;
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
            Cells[x, y] = new Cell(x, y) {
                Size = new Size(CellSize, CellSize),
                Position = new Vector2(x * CellSize, y * CellSize)
            };
            AddComponent(Cells[x, y], (int)Layers.Cells);
        }
    }
}