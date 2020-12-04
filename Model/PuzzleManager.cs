using System.Collections.Generic;
using System.Threading.Tasks;
using NumbrixGame.Datatier;

namespace NumbrixGame.Model
{
    /// <summary>
    ///     PuzzleManager class that manages puzzles
    /// </summary>
    public class PuzzleManager
    {
        #region Properties

        /// <summary>
        ///     Gets or sets all of the puzzles.
        /// </summary>
        /// <value>The puzzles.</value>
        public IList<NumbrixGameBoard> Puzzles { get; set; }

        /// <summary>
        ///     Gets or sets the current puzzle.
        /// </summary>
        /// <value>The current puzzle.</value>
        public NumbrixGameBoard CurrentPuzzle { get; set; }

        /// <summary>
        ///     Gets the next puzzle.
        /// </summary>
        /// <value>The next puzzle.</value>
        public NumbrixGameBoard NextPuzzle => this.CurrentPuzzle =
            this.Puzzles[
                this.Puzzles.IndexOf(this.CurrentPuzzle) == this.Puzzles.Count - 1
                    ? 0
                    : this.Puzzles.IndexOf(this.CurrentPuzzle) + 1];

        /// <summary>
        ///     Gets the previous puzzle.
        /// </summary>
        /// <value>The previous puzzle.</value>
        public NumbrixGameBoard PreviousPuzzle => this.CurrentPuzzle =
            this.Puzzles[
                this.Puzzles.IndexOf(this.CurrentPuzzle) == 0
                    ? this.Puzzles.Count - 1
                    : this.Puzzles.IndexOf(this.CurrentPuzzle) - 1];

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PuzzleManager" /> class.
        /// </summary>
        public PuzzleManager()
        {
            this.Puzzles = new List<NumbrixGameBoard>();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the starting puzzles.
        /// </summary>
        public async Task InitializeStartingPuzzles()
        {
            var prebuiltPuzzles = await NumbrixGameBoardReader.GetPrebuiltGames();
            foreach (var puzzle in prebuiltPuzzles)
            {
                this.Puzzles.Add(await NumbrixGameBoardReader.LoadPuzzle(puzzle));
            }
        }

        #endregion
    }
}