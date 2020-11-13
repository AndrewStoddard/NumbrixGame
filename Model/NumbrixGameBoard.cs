using System.Collections.Generic;

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

        #region Constructors

        public NumbrixGameBoard()
        {
            this.NumbrixGameBoardCells = new List<NumbrixGameBoardCell>();
        }

        #endregion

        public void AddCells(IList<NumbrixGameBoardCell> cellsToAdd)
        {
            foreach (var currentCell in cellsToAdd)
            {
                this.NumbrixGameBoardCells.Add(currentCell);
            }
        }
    }
}