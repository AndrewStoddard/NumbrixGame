using System.Collections.Generic;
using System.Linq;
using NumbrixGame.Datatier;
using NumbrixGame.PrebuiltGames;

namespace NumbrixGame.Model
{

    /// <summary>
    ///   PuzzleManager class that manages puzzles
    /// </summary>
    public class PuzzleManager
    {
        #region Properties

        /// <summary>Gets or sets all of the puzzles.</summary>
        /// <value>The puzzles.</value>
        public IList<NumbrixGameBoard> Puzzles { get; set; }

        /// <summary>Gets or sets the current puzzle.</summary>
        /// <value>The current puzzle.</value>
        public NumbrixGameBoard CurrentPuzzle { get; set; }

        /// <summary>Gets the next puzzle.</summary>
        /// <value>The next puzzle.</value>
        public NumbrixGameBoard NextPuzzle => this.CurrentPuzzle = this
                                                                   .Puzzles.SkipWhile(x => x != this.CurrentPuzzle)
                                                                   .Skip(1).DefaultIfEmpty(this.Puzzles[0])
                                                                   .FirstOrDefault();

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="PuzzleManager" /> class.</summary>
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
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleK));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleL));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleM));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleN));
            this.Puzzles.Add(NumbrixGameBoardReader.LoadPuzzle(StartingPuzzles.puzzleO));

            this.CurrentPuzzle = this.Puzzles[0];
        }

        #endregion
    }
}