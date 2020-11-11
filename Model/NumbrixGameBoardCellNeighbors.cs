using System.Collections.Generic;
using System.Linq;

namespace NumbrixGame.Model
{
    public class NumbrixGameBoardCellNeighbors
    {
        #region Data members

        private const int TempHeight = 8;
        private const int TempWidth = 8;

        #endregion

        #region Properties

        public NumbrixGameBoardCell NorthernGameBoardCellNeighbor { get; set; }
        public NumbrixGameBoardCell SouthernGameBoardCellNeighbor { get; set; }
        public NumbrixGameBoardCell WesternGameBoardCellNeighbor { get; set; }
        public NumbrixGameBoardCell EasternnGameBoardCellNeighbor { get; set; }

        public NumbrixGameBoardCell GameBoardCell { get; set; }

        #endregion

        #region Constructors

        public NumbrixGameBoardCellNeighbors(List<NumbrixGameBoardCell> gameBoardCells,
            NumbrixGameBoardCell gameBoardCell)
        {
            this.GameBoardCell = gameBoardCell;
            this.setNeighbors(gameBoardCells);
        }

        public NumbrixGameBoardCellNeighbors(NumbrixGameBoardCell gameBoardCell)
        {
            this.GameBoardCell = gameBoardCell;
        }

        #endregion

        #region Methods

        private void setNeighbors(List<NumbrixGameBoardCell> gameBoardCells)
        {
            if (this.GameBoardCell.X == 1)
            {
                this.WesternGameBoardCellNeighbor = null;
                this.EasternnGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell => gameBoardCell.X == this.GameBoardCell.X + 1);
            }
            else if (this.GameBoardCell.X == TempWidth)
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
            else if (this.GameBoardCell.Y == TempHeight)
            {
                this.NorthernGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell => gameBoardCell.Y == this.GameBoardCell.Y - 1);
                this.SouthernGameBoardCellNeighbor = null;
            }
        }

        #endregion
    }
}