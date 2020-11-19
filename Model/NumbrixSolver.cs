using System;

namespace NumbrixGame.Model
{
    public static class NumbrixSolver
    {
        #region Methods

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

        public static NumbrixGameBoard SolveGameBoard(NumbrixGameBoard numbrixGameBoard)
        {
            //TODO Implement
            throw new NotImplementedException();
        }

        #endregion
    }
}