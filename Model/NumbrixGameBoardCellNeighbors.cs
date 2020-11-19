using System.Collections.Generic;
using System.Linq;

namespace NumbrixGame.Model
{
    public class NumbrixGameBoardCellNeighbors
    {
        #region Properties

        public NumbrixGameBoardCell NorthernGameBoardCellNeighbor { get; set; }
        public NumbrixGameBoardCell SouthernGameBoardCellNeighbor { get; set; }
        public NumbrixGameBoardCell WesternGameBoardCellNeighbor { get; set; }
        public NumbrixGameBoardCell EasternnGameBoardCellNeighbor { get; set; }

        public NumbrixGameBoardCell GameBoardCell { get; set; }
        public NumbrixGameBoard GameBoard { get; set; }

        #endregion

        #region Constructors

        public NumbrixGameBoardCellNeighbors(NumbrixGameBoard gameBoard,
            NumbrixGameBoardCell gameBoardCell)
        {
            this.GameBoard = gameBoard;
            this.GameBoardCell = gameBoardCell;
            this.setNeighbors(this.GameBoard.NumbrixGameBoardCells.ToList());
        }

        #endregion

        #region Methods

        public IList<NumbrixGameBoardCell> GetListOfNeighbors()
        {
            return new List<NumbrixGameBoardCell> {
                this.NorthernGameBoardCellNeighbor,
                this.SouthernGameBoardCellNeighbor,
                this.WesternGameBoardCellNeighbor,
                this.EasternnGameBoardCellNeighbor
            };
        }

        private void setNeighbors(List<NumbrixGameBoardCell> gameBoardCells)
        {
            if (this.GameBoardCell.X == 1)
            {
                this.WesternGameBoardCellNeighbor = null;
                this.EasternnGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell => gameBoardCell.X == this.GameBoardCell.X + 1);
            }
            else if (this.GameBoardCell.X == this.GameBoard.BoardWidth)
            {
                this.WesternGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell => gameBoardCell.X == this.GameBoardCell.X - 1);
                this.EasternnGameBoardCellNeighbor = null;
            }

            if (this.GameBoardCell.Y == 1)
            {
                this.NorthernGameBoardCellNeighbor = null;
                this.SouthernGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell => gameBoardCell.Y == this.GameBoardCell.Y + 1);
            }
            else if (this.GameBoardCell.Y == this.GameBoard.BoardHeight)
            {
                this.NorthernGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell => gameBoardCell.Y == this.GameBoardCell.Y - 1);
                this.SouthernGameBoardCellNeighbor = null;
            }
        }

        #endregion
    }
}