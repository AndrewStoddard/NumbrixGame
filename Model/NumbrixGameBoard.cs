using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumbrixGame.Model
{
    /// <summary>
    ///     The Numbrix Game Board class
    /// </summary>
    public class NumbrixGameBoard
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the numbrix game board cells.
        /// </summary>
        /// <value>The numbrix game board cells.</value>
        public IList<NumbrixGameBoardCell> NumbrixGameBoardCells { get; set; }

        /// <summary>
        ///     Gets or sets the width of the board.
        /// </summary>
        /// <value>The width of the board.</value>
        public int BoardWidth { get; set; }

        /// <summary>
        ///     Gets or sets the height of the board.
        /// </summary>
        /// <value>The height of the board.</value>
        public int BoardHeight { get; set; }

        /// <summary>
        ///     Gets or sets the game board number.
        /// </summary>
        /// <value>The game board number.</value>
        public int GameBoardNumber { get; set; }

        /// <summary>
        ///     Gets or sets the time taken.
        /// </summary>
        /// <value>The time taken.</value>
        public TimeSpan TimeTaken { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is paused.
        /// </summary>
        /// <value><c>true</c> if this instance is paused; otherwise, <c>false</c>.</value>
        public bool IsPaused { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is finished.
        /// </summary>
        /// <value><c>true</c> if this instance is finished; otherwise, <c>false</c>.</value>
        public bool IsFinished { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixGameBoard" /> class.
        /// </summary>
        /// <param name="boardWidth">Width of the board.</param>
        /// <param name="boardHeight">Height of the board.</param>
        public NumbrixGameBoard(int boardWidth, int boardHeight)
        {
            this.BoardWidth = boardWidth;
            this.IsFinished = false;
            this.IsPaused = false;
            this.BoardHeight = boardHeight;
            this.TimeTaken = new TimeSpan(0, 0, 0);
            this.CreateBlankGameBoard();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixGameBoard" /> class.
        /// </summary>
        public NumbrixGameBoard()
        {
            this.NumbrixGameBoardCells = new List<NumbrixGameBoardCell>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Builds puzzle in CSV format.
        /// </summary>
        /// <returns>puzzle in CSV format</returns>
        public string AsCSV()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(
                $"{this.GameBoardNumber},{this.BoardWidth},{this.BoardHeight},{this.TimeTaken.ToString()}");
            foreach (var gameBoardCell in this.NumbrixGameBoardCells)
            {
                stringBuilder.AppendLine(
                    $"{gameBoardCell.X},{gameBoardCell.Y},{gameBoardCell.NumbrixValue},{gameBoardCell.IsDefaultValue}");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Creates the blank game board.
        /// </summary>
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

        /// <summary>
        ///     Finds the cell with the matching numbrix value.
        /// </summary>
        /// <param name="numbrixValue">The numbrix value.</param>
        /// <returns>NumbrixGameBoardCell matching that value</returns>
        public NumbrixGameBoardCell FindCell(int? numbrixValue)
        {
            return this.NumbrixGameBoardCells.Single(cell => cell.NumbrixValue == numbrixValue);
        }

        /// <summary>
        ///     Finds the cell of the given coordinates.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>NumbrixGameBoardCell matching that location</returns>
        public NumbrixGameBoardCell FindCell(int x, int y)
        {
            return this.NumbrixGameBoardCells.Single(cell => cell.X == x && cell.Y == y);
        }

        /// <summary>
        ///     Creates the cell.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="numbrixValue">The numbrix value.</param>
        /// <param name="isDefault">if set to <c>true</c> [is default].</param>
        /// <returns>Newly created Numbrix Game Board Cell</returns>
        public NumbrixGameBoardCell CreateCell(int x, int y, int? numbrixValue = null, bool isDefault = false)
        {
            var newCell = new NumbrixGameBoardCell(x, y) {
                NumbrixValue = numbrixValue, IsDefaultValue = isDefault, LinearCoordinate = this.ConvertXYToLinear(x, y)
            };
            return newCell;
        }

        /// <summary>
        ///     Converts the xy to linear.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>linear location based off of the x and y location (ex: (1,1) = 1, (1,2) = 2)</returns>
        public int ConvertXYToLinear(int x, int y)
        {
            var value = x + this.BoardWidth * (y - 1);
            return value;
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            return obj is NumbrixGameBoard gameboard && gameboard.GameBoardNumber == this.GameBoardNumber;
        }

        protected bool Equals(NumbrixGameBoard other)
        {
            return Equals(this.NumbrixGameBoardCells, other.NumbrixGameBoardCells) &&
                   this.BoardWidth == other.BoardWidth && this.BoardHeight == other.BoardHeight &&
                   this.GameBoardNumber == other.GameBoardNumber && this.TimeTaken.Equals(other.TimeTaken) &&
                   this.IsPaused == other.IsPaused && this.IsFinished == other.IsFinished;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.NumbrixGameBoardCells != null ? this.NumbrixGameBoardCells.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ this.BoardWidth;
                hashCode = (hashCode * 397) ^ this.BoardHeight;
                hashCode = (hashCode * 397) ^ this.GameBoardNumber;
                hashCode = (hashCode * 397) ^ this.TimeTaken.GetHashCode();
                hashCode = (hashCode * 397) ^ this.IsPaused.GetHashCode();
                hashCode = (hashCode * 397) ^ this.IsFinished.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}