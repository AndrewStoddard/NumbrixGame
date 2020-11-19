﻿using System.Collections.Generic;
using System.Linq;

namespace NumbrixGame.Model
{
    public class NumbrixSolverAgent
    {
        #region Data members

        private NumbrixGameBoardCell agentLocation;
        private readonly NumbrixGameBoard agentGameBoard;

        #endregion

        #region Constructors

        public NumbrixSolverAgent(NumbrixGameBoard numbrixGameBoard)
        {
            this.agentGameBoard = numbrixGameBoard;
            this.agentLocation = numbrixGameBoard.FindCell(1);
        }

        #endregion

        #region Methods

        public void Move()
        {
            var nextMove = this.findNextMove();
            this.agentLocation = nextMove;
        }

        public bool CanMove()
        {
            var nextMove = this.findNextMove();
            return nextMove != null && !this.IsFinished();
        }

        public bool IsFinished()
        {
            return this.agentLocation.NumbrixValue == this.agentGameBoard.BoardHeight * this.agentGameBoard.BoardWidth;
        }

        private IList<NumbrixGameBoardCell> findAgentNeighbors()
        {
            return new NumbrixGameBoardCellNeighbors(this.agentGameBoard, this.agentLocation).GetListOfNeighbors();
        }

        private NumbrixGameBoardCell findNextMove()
        {
            return this.findAgentNeighbors().SingleOrDefault(
                cell => cell != null && cell.NumbrixValue == this.agentLocation.NumbrixValue + 1);
        }

        private NumbrixGameBoardCell findPreviousMove()
        {
            return this.findAgentNeighbors().SingleOrDefault(
                cell => cell != null && cell.NumbrixValue == this.agentLocation.NumbrixValue - 1);
        }

        private List<NumbrixGameBoardCell> findPossibleNewMovements()
        {
            return this.findAgentNeighbors().Where(cell => cell != null && cell.NumbrixValue == (int?) null).ToList();
        }

        #endregion
    }
}