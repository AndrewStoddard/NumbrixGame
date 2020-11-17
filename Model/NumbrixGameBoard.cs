using System.Collections.Generic;
using System.Linq;

namespace NumbrixGame.Model
{
    public class NumbrixGameBoard
    {
        #region Properties

        public IList<NumbrixGameBoardCell> NumbrixGameBoardCells { get; set; }

        public int BoardWidth { get; set; }

        public int BoardHeight { get; set; }

        public IList<int> numbers { get; set; }

        #endregion

        #region Methods

        public void CreateBlankGameBoard()
        {
            this.NumbrixGameBoardCells = new List<NumbrixGameBoardCell>();
            for (var x = 1; x <= this.BoardWidth; x++)
            {
                for (var y = 1; y <= this.BoardHeight; y++)
                {
                    this.NumbrixGameBoardCells.Add(this.CreateCell(x, y));
                }
            }
        }

        public void AddCells(IList<NumbrixGameBoardCell> cellsToAdd)
        {
            foreach (var currentCell in cellsToAdd)
            {
                this.NumbrixGameBoardCells.Add(currentCell);
            }
        }

        public void AddCell(NumbrixGameBoardCell cellToAdd)
        {
            this.NumbrixGameBoardCells.Add(cellToAdd);
        }

        public NumbrixGameBoardCell FindCell(NumbrixGameBoardCell gameBoardCell)
        {
            return this.NumbrixGameBoardCells.Single(cell => cell.Equals(gameBoardCell));
        }

        public NumbrixGameBoardCell FindCell(int x, int y)
        {
            return this.NumbrixGameBoardCells.Single(cell => cell.X == x && cell.Y == y);
        }

        public NumbrixGameBoardCell CreateCell(int x, int y, int? numbrixValue = null, bool isDefault = false)
        {
            var newCell = new NumbrixGameBoardCell(x, y) {NumbrixValue = numbrixValue, DefaultValue = isDefault};
            return newCell;
        }

        #endregion
    }
}