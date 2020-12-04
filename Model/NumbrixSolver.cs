using System;

namespace NumbrixGame.Model
{
    /// <summary>
    ///     Class NumbrixSolver.
    /// </summary>
    public static class NumbrixSolver
    {
        #region Methods

        /// <summary>
        ///     Checks for solved.
        /// </summary>
        /// <param name="numbrixGameBoard">The numbrix game board.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CheckForSolved(NumbrixGameBoard numbrixGameBoard)
        {
            try
            {
                var agent = new NumbrixSolverAgent(numbrixGameBoard);
                while (agent.CanMove())
                {
                    agent.Move();
                }

                return agent.IsFinished();
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Solves the game board.
        /// </summary>
        /// <param name="numbrixGameBoard">The numbrix game board.</param>
        /// <returns>NumbrixGameBoard.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static NumbrixGameBoard SolveGameBoard(NumbrixGameBoard numbrixGameBoard)
        {
            //TODO Implement
            throw new NotImplementedException();
        }

        #endregion
    }
}