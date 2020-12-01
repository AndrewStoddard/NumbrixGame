using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumbrixGame.Model
{
    public class NumbrixGameBoard
    {
        #region Properties

        public IList<NumbrixGameBoardCell> NumbrixGameBoardCells { get; set; }

        public int BoardWidth { get; set; }

        public int BoardHeight { get; set; }
        public int GameBoardNumber { get; set; }
        public TimeSpan TimeTaken { get; set; }
        public bool IsPaused { get; set; }
        public bool IsFinished { get; set; }

        #endregion

        #region Constructors

        public NumbrixGameBoard(int boardWidth, int boardHeight)
        {
            this.BoardWidth = boardWidth;
            this.BoardHeight = boardHeight;
            this.TimeTaken = new TimeSpan(0, 0, 0);
            this.CreateBlankGameBoard();
        }

        public NumbrixGameBoard()
        {
            this.NumbrixGameBoardCells = new List<NumbrixGameBoardCell>();
        }

        #endregion

        #region Methods

        public string AsCSV()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(
                $"{this.GameBoardNumber},{this.BoardWidth},{this.BoardHeight},{this.TimeTaken.ToString()}");
            foreach (var gameBoardCell in this.NumbrixGameBoardCells)
            {
                stringBuilder.AppendLine(
                    $"{gameBoardCell.X},{gameBoardCell.Y},{gameBoardCell.NumbrixValue},{gameBoardCell.IsDefaultValue}");
            }

            return stringBuilder.ToString();
        }

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

        public IList<NumbrixGameBoardCell> FindNeighbors(NumbrixGameBoardCell gameboardCell)
        {
            return new NumbrixGameBoardCellNeighbors(this, gameboardCell).GetListOfNeighbors();
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

        public NumbrixGameBoardCell FindCell(int? numbrixValue)
        {
            return this.NumbrixGameBoardCells.Single(cell => cell.NumbrixValue == numbrixValue);
        }

        public NumbrixGameBoardCell FindCell(int x, int y)
        {
            return this.NumbrixGameBoardCells.Single(cell => cell.X == x && cell.Y == y);
        }

        public NumbrixGameBoardCell CreateCell(int x, int y, int? numbrixValue = null, bool isDefault = false)
        {
            var newCell = new NumbrixGameBoardCell(x, y) {
                NumbrixValue = numbrixValue, IsDefaultValue = isDefault, LinearCoordinate = this.ConvertXYToLinear(x, y)
            };
            return newCell;
        }

        public int ConvertXYToLinear(int x, int y)
        {
            var value = x + this.BoardWidth * (y - 1);
            return value;
        }

        public (int x, int y) ConvertLinearToXY(int linearCoordinate)
        {
            var x = linearCoordinate % this.BoardWidth == 0 ? this.BoardWidth : linearCoordinate % this.BoardWidth;
            var y = linearCoordinate / this.BoardHeight + 1;

            return (x, y);
        }

        #endregion
    }
}