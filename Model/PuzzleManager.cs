using System.Collections.Generic;
using System.Linq;
using NumbrixGame.PrebuiltGames;

namespace NumbrixGame.Model
{
    public class PuzzleManager
    {
        #region Data members

        #endregion

        #region Properties

        public IList<NumbrixGameBoard> Puzzles { get; set; }

        public NumbrixGameBoard CurrentPuzzle { get; set; }

        public NumbrixGameBoard NextPuzzle => this.CurrentPuzzle = this
                                                                   .Puzzles.SkipWhile(x => x != this.CurrentPuzzle)
                                                                   .Skip(1).DefaultIfEmpty(this.Puzzles[0])
                                                                   .FirstOrDefault();

        #endregion

        #region Constructors

        public PuzzleManager()
        {
            this.Puzzles = new List<NumbrixGameBoard>();
            this.initializeStartingPuzzles();
        }

        #endregion

        #region Methods

        private void initializeStartingPuzzles()
        {
            this.Puzzles.Add(this.createBoard(StartingPuzzles.puzzleThree));
            this.Puzzles.Add(this.createBoard(StartingPuzzles.puzzleOne));
            this.Puzzles.Add(this.createBoard(StartingPuzzles.puzzleTwo));

            this.CurrentPuzzle = this.Puzzles[0];
        }

        private NumbrixGameBoard createBoard(string data)
        {
            var gameBoard = new NumbrixGameBoard();
            var dataFileLines = data.Replace("\r", "").Split("\n");

            for (var i = 0; i < dataFileLines.Length - 1; i++)
            {
                var line = dataFileLines[i];
                if (i == 0)
                {
                    var settings = line.Split(',');

                    gameBoard.BoardWidth = string.IsNullOrEmpty(settings[0]) ? -1 : int.Parse(settings[0]);
                    gameBoard.BoardHeight = string.IsNullOrEmpty(settings[1]) ? -1 : int.Parse(settings[1]);
                    gameBoard.CreateBlankGameBoard();
                }
                else
                {
                    var cellInfo = line.Split(',');
                    var currentGameBoardCell = gameBoard.FindCell(int.Parse(cellInfo[0]), int.Parse(cellInfo[1]));
                    currentGameBoardCell.DefaultValue = bool.Parse(cellInfo[3]);
                    currentGameBoardCell.NumbrixValue = int.Parse(cellInfo[2]);
                }
            }

            return gameBoard;
        }

        #endregion
    }
}