using System.Collections.Generic;
using System.Linq;
using NumbrixGame.Datatier;
using NumbrixGame.PrebuiltGames;

namespace NumbrixGame.Model
{
    public class PuzzleManager
    {
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
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleA));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleB));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleC));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.PuzzleD));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleE));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleF));

            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleG));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleH));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleI));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleJ));

            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleX));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleY));

            // 3x3 Test Puzzle
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleZ));
            this.CurrentPuzzle = this.Puzzles[0];
        }

        #endregion
    }
}