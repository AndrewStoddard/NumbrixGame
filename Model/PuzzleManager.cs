using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using NumbrixGame.PrebuiltGames;

namespace NumbrixGame.Model
{
    public class PuzzleManager
    {
        private IList<NumbrixGameBoard> puzzles;

        public IList<NumbrixGameBoard> Puzzles
        {
            get { return this.puzzles; }
            set { this.puzzles = value;  }
        }

        public NumbrixGameBoard CurrentPuzzle { get; set; }

        public NumbrixGameBoard NextPuzzle => this.CurrentPuzzle = this.puzzles.SkipWhile(x => x != this.CurrentPuzzle).Skip(1).DefaultIfEmpty(this.puzzles[0])
                    .FirstOrDefault();

        public PuzzleManager()
        {
            this.puzzles = new List<NumbrixGameBoard>();
            this.initializeStartingPuzzles();
        }

        private void initializeStartingPuzzles()
        {
            Puzzles.Add(this.createBoard(StartingPuzzles.puzzleOne));
            Puzzles.Add(this.createBoard(StartingPuzzles.puzzleTwo));

            this.CurrentPuzzle = Puzzles[0];
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
    }
}
