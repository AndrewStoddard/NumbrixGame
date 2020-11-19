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
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleOne));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleTwo));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleThree));

            this.CurrentPuzzle = this.Puzzles[0];
        }

        #endregion
    }
}