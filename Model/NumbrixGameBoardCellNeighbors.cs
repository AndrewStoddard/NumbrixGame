using System.Collections.Generic;
using System.Linq;

namespace NumbrixGame.Model
{
    /// <summary>
    ///     Class NumbrixGameBoardCellNeighbors.
    /// </summary>
    public class NumbrixGameBoardCellNeighbors
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the northern game board cell neighbor.
        /// </summary>
        /// <value>The northern game board cell neighbor.</value>
        public NumbrixGameBoardCell NorthernGameBoardCellNeighbor { get; set; }

        /// <summary>
        ///     Gets or sets the southern game board cell neighbor.
        /// </summary>
        /// <value>The southern game board cell neighbor.</value>
        public NumbrixGameBoardCell SouthernGameBoardCellNeighbor { get; set; }

        /// <summary>
        ///     Gets or sets the western game board cell neighbor.
        /// </summary>
        /// <value>The western game board cell neighbor.</value>
        public NumbrixGameBoardCell WesternGameBoardCellNeighbor { get; set; }

        /// <summary>
        ///     Gets or sets the easternn game board cell neighbor.
        /// </summary>
        /// <value>The easternn game board cell neighbor.</value>
        public NumbrixGameBoardCell EasternnGameBoardCellNeighbor { get; set; }

        /// <summary>
        ///     Gets or sets the game board cell.
        /// </summary>
        /// <value>The game board cell.</value>
        public NumbrixGameBoardCell GameBoardCell { get; set; }

        /// <summary>
        ///     Gets or sets the game board.
        /// </summary>
        /// <value>The game board.</value>
        public NumbrixGameBoard GameBoard { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixGameBoardCellNeighbors" /> class.
        /// </summary>
        /// <param name="gameBoard">The game board.</param>
        /// <param name="gameBoardCell">The game board cell.</param>
        public NumbrixGameBoardCellNeighbors(NumbrixGameBoard gameBoard,
            NumbrixGameBoardCell gameBoardCell)
        {
            this.GameBoard = gameBoard;
            this.GameBoardCell = gameBoardCell;
            this.setNeighbors(this.GameBoard.NumbrixGameBoardCells.ToList());
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the list of neighbors.
        /// </summary>
        /// <returns>IList&lt;NumbrixGameBoardCell&gt;.</returns>
        public IList<NumbrixGameBoardCell> GetListOfNeighbors()
        {
            return new List<NumbrixGameBoardCell> {
                this.NorthernGameBoardCellNeighbor,
                this.SouthernGameBoardCellNeighbor,
                this.WesternGameBoardCellNeighbor,
                this.EasternnGameBoardCellNeighbor
            };
        }

        /// <summary>
        ///     Sets the neighbors.
        /// </summary>
        /// <param name="gameBoardCells">The game board cells.</param>
        private void setNeighbors(IReadOnlyCollection<NumbrixGameBoardCell> gameBoardCells)
        {
            if (this.GameBoardCell.X == 1)
            {
                this.WesternGameBoardCellNeighbor = null;
                this.EasternnGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell =>
                        gameBoardCell.X == this.GameBoardCell.X + 1 && gameBoardCell.Y == this.GameBoardCell.Y);
            }
            else if (this.GameBoardCell.X == this.GameBoard.BoardWidth)
            {
                this.WesternGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell =>
                        gameBoardCell.X == this.GameBoardCell.X - 1 && gameBoardCell.Y == this.GameBoardCell.Y);
                this.EasternnGameBoardCellNeighbor = null;
            }
            else
            {
                this.WesternGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell =>
                        gameBoardCell.X == this.GameBoardCell.X - 1 && gameBoardCell.Y == this.GameBoardCell.Y);
                this.EasternnGameBoardCellNeighbor = gameBoardCells.First(gameBoardCell =>
                    gameBoardCell.X == this.GameBoardCell.X + 1 && gameBoardCell.Y == this.GameBoardCell.Y);
            }

            if (this.GameBoardCell.Y == 1)
            {
                this.NorthernGameBoardCellNeighbor = null;
                this.SouthernGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell =>
                        gameBoardCell.Y == this.GameBoardCell.Y + 1 && gameBoardCell.X == this.GameBoardCell.X);
            }
            else if (this.GameBoardCell.Y == this.GameBoard.BoardHeight)
            {
                this.NorthernGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell =>
                        gameBoardCell.Y == this.GameBoardCell.Y - 1 && gameBoardCell.X == this.GameBoardCell.X);
                this.SouthernGameBoardCellNeighbor = null;
            }
            else
            {
                this.NorthernGameBoardCellNeighbor =
                    gameBoardCells.First(gameBoardCell =>
                        gameBoardCell.Y == this.GameBoardCell.Y - 1 && gameBoardCell.X == this.GameBoardCell.X);
                this.SouthernGameBoardCellNeighbor = gameBoardCells.First(gameBoardCell =>
                    gameBoardCell.Y == this.GameBoardCell.Y + 1 && gameBoardCell.X == this.GameBoardCell.X);
            }
        }

        #endregion
    }
}