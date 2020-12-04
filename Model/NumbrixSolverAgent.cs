using System.Collections.Generic;
using System.Linq;

namespace NumbrixGame.Model
{
    /// <summary>
    ///     Class NumbrixSolverAgent.
    /// </summary>
    public class NumbrixSolverAgent
    {
        #region Data members

        /// <summary>
        ///     The agent location
        /// </summary>
        private NumbrixGameBoardCell agentLocation;

        /// <summary>
        ///     The agent game board
        /// </summary>
        private readonly NumbrixGameBoard agentGameBoard;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixSolverAgent" /> class.
        /// </summary>
        /// <param name="numbrixGameBoard">The numbrix game board.</param>
        public NumbrixSolverAgent(NumbrixGameBoard numbrixGameBoard)
        {
            this.agentGameBoard = numbrixGameBoard;
            this.agentLocation = numbrixGameBoard.FindCell(1);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Moves this instance.
        /// </summary>
        public void Move()
        {
            var nextMove = this.findNextMove();
            this.agentLocation = nextMove;
        }

        /// <summary>
        ///     Determines whether this instance can move.
        /// </summary>
        /// <returns><c>true</c> if this instance can move; otherwise, <c>false</c>.</returns>
        public bool CanMove()
        {
            var nextMove = this.findNextMove();
            return nextMove != null && !this.IsFinished();
        }

        /// <summary>
        ///     Determines whether this instance is finished.
        /// </summary>
        /// <returns><c>true</c> if this instance is finished; otherwise, <c>false</c>.</returns>
        public bool IsFinished()
        {
            return this.agentLocation.NumbrixValue == this.agentGameBoard.BoardHeight * this.agentGameBoard.BoardWidth;
        }

        /// <summary>
        ///     Finds the agent neighbors.
        /// </summary>
        /// <returns>IList&lt;NumbrixGameBoardCell&gt;.</returns>
        private IEnumerable<NumbrixGameBoardCell> findAgentNeighbors()
        {
            return new NumbrixGameBoardCellNeighbors(this.agentGameBoard, this.agentLocation).GetListOfNeighbors();
        }

        /// <summary>
        ///     Finds the next move.
        /// </summary>
        /// <returns>NumbrixGameBoardCell.</returns>
        private NumbrixGameBoardCell findNextMove()
        {
            return this.findAgentNeighbors().SingleOrDefault(
                cell => cell != null && cell.NumbrixValue == this.agentLocation.NumbrixValue + 1);
        }

        /// <summary>
        ///     Finds the previous move.
        /// </summary>
        /// <returns>NumbrixGameBoardCell.</returns>
        private NumbrixGameBoardCell findPreviousMove()
        {
            return this.findAgentNeighbors().SingleOrDefault(
                cell => cell != null && cell.NumbrixValue == this.agentLocation.NumbrixValue - 1);
        }

        /// <summary>
        ///     Finds the possible new movements.
        /// </summary>
        /// <returns>List&lt;NumbrixGameBoardCell&gt;.</returns>
        private List<NumbrixGameBoardCell> findPossibleNewMovements()
        {
            return this.findAgentNeighbors().Where(cell => cell != null && cell.NumbrixValue == (int?) null).ToList();
        }

        #endregion
    }
}