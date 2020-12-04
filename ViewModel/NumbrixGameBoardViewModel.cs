using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using NumbrixGame.Datatier;
using NumbrixGame.Model;
using NumbrixGame.Properties;

namespace NumbrixGame.ViewModel
{
    /// <summary>
    ///     Class NumbrixGameBoardViewModel.
    ///     Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class NumbrixGameBoardViewModel : INotifyPropertyChanged
    {
        #region Types and Delegates

        /// <summary>
        ///     Delegate ValueChanged
        /// </summary>
        public delegate void ValueChanged();

        #endregion

        #region Data members

        /// <summary>
        ///     The timer
        /// </summary>
        private readonly DispatcherTimer timer;

        /// <summary>
        ///     The puzzle manager
        /// </summary>
        private readonly PuzzleManager puzzleManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public NumbrixGameBoard Model { get; set; }

        /// <summary>
        ///     Gets or sets the numbrix game board cells.
        /// </summary>
        /// <value>The numbrix game board cells.</value>
        public IList<NumbrixGameBoardCellViewModel> NumbrixGameBoardCells { get; set; }

        /// <summary>
        ///     Gets or sets the game board number.
        /// </summary>
        /// <value>The game board number.</value>
        public int GameBoardNumber
        {
            get => this.Model.GameBoardNumber;
            set => this.Model.GameBoardNumber = value;
        }

        /// <summary>
        ///     Gets or sets the width of the board.
        /// </summary>
        /// <value>The width of the board.</value>
        public int BoardWidth
        {
            get => this.Model.BoardWidth;
            set => this.Model.BoardWidth = value;
        }

        /// <summary>
        ///     Gets or sets the height of the board.
        /// </summary>
        /// <value>The height of the board.</value>
        public int BoardHeight
        {
            get => this.Model.BoardHeight;
            set => this.Model.BoardHeight = value;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is paused.
        /// </summary>
        /// <value><c>true</c> if this instance is paused; otherwise, <c>false</c>.</value>
        public bool IsPaused
        {
            get => this.Model.IsPaused;
            set
            {
                this.Model.IsPaused = value;
                this.OnPropertyChanged(nameof(this.IsPaused));
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is finished.
        /// </summary>
        /// <value><c>true</c> if this instance is finished; otherwise, <c>false</c>.</value>
        public bool IsFinished
        {
            get => this.Model.IsFinished;
            set
            {
                this.Model.IsFinished = value;
                this.OnPropertyChanged(nameof(this.IsFinished));
            }
        }

        /// <summary>
        ///     Gets or sets the time taken.
        /// </summary>
        /// <value>The time taken.</value>
        public TimeSpan TimeTaken
        {
            get => this.Model.TimeTaken;
            set
            {
                this.Model.TimeTaken = value;
                this.OnPropertyChanged(nameof(this.TimeTaken));
            }
        }

        /// <summary>
        ///     Gets the maximum board value.
        /// </summary>
        /// <value>The maximum board value.</value>
        public int MaxBoardValue => this.BoardHeight * this.BoardWidth;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixGameBoardViewModel" /> class.
        /// </summary>
        public NumbrixGameBoardViewModel()
        {
            this.timer = new DispatcherTimer {
                Interval = new TimeSpan(0, 0, 1)
            };
            this.timer.Tick += this.Timer_Tick;
            this.Model = new NumbrixGameBoard();
            this.puzzleManager = new PuzzleManager();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Timers the tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void Timer_Tick(object sender, object e)
        {
            this.TimeTaken = this.TimeTaken.Add(new TimeSpan(0, 0, 1));
        }

        /// <summary>
        ///     Occurs when [on value changed].
        /// </summary>
        public event ValueChanged OnValueChanged;

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Starts the time.
        /// </summary>
        public void StartTime()
        {
            this.IsPaused = false;
            this.timer.Start();
        }

        /// <summary>
        ///     Pauses the time.
        /// </summary>
        public void PauseTime()
        {
            this.IsPaused = true;
            this.timer.Stop();
        }

        /// <summary>
        ///     Resets the time.
        /// </summary>
        public void ResetTime()
        {
            this.TimeTaken = new TimeSpan(0, 0, 0);
            this.ClearGameBoard();
            this.IsPaused = true;
            this.timer.Stop();
        }

        /// <summary>
        ///     Creates the numbrix game board cells.
        /// </summary>
        /// <returns>IList&lt;NumbrixGameBoardCellViewModel&gt;.</returns>
        private IList<NumbrixGameBoardCellViewModel> createNumbrixGameBoardCells()
        {
            var result = new List<NumbrixGameBoardCellViewModel>();
            foreach (var gameBoardCell in this.Model.NumbrixGameBoardCells)
            {
                var cell = new NumbrixGameBoardCellViewModel(gameBoardCell);
                cell.OnNumbrixValueChanged += this.OnValueChange;
                result.Add(cell);
            }

            return result;
        }

        /// <summary>
        ///     Called when [value change].
        /// </summary>
        private void OnValueChange()
        {
            if (this.NumbrixGameBoardCells.All(cell => cell.NumbrixValue != null))
            {
                this.OnValueChanged?.Invoke();
            }
        }

        /// <summary>
        ///     Finds the cell.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>NumbrixGameBoardCellViewModel.</returns>
        public NumbrixGameBoardCellViewModel FindCell(int x, int y)
        {
            return this.NumbrixGameBoardCells.SingleOrDefault(cell => cell.X == x && cell.Y == y);
        }

        /// <summary>
        ///     Finds the cell.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>NumbrixGameBoardCellViewModel.</returns>
        public NumbrixGameBoardCellViewModel FindCell(int? value)
        {
            return this.NumbrixGameBoardCells.SingleOrDefault(cell => cell.NumbrixValue == value);
        }

        /// <summary>
        ///     Checks the solution.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CheckSolution()
        {
            return NumbrixSolver.CheckForSolved(this.Model);
        }

        /// <summary>
        ///     Updates the cell.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="value">The value.</param>
        public void UpdateCell(int x, int y, int? value)
        {
            this.Model.FindCell(x, y).NumbrixValue = value;
        }

        /// <summary>
        ///     Clears the game board.
        /// </summary>
        public void ClearGameBoard()
        {
            foreach (var gameBoardCell in this.NumbrixGameBoardCells)
            {
                if (!gameBoardCell.IsDefaultValue)
                {
                    gameBoardCell.NumbrixValue = null;
                }
            }
        }

        /// <summary>
        ///     Nexts the puzzle.
        /// </summary>
        public void NextPuzzle()
        {
            this.Model = this.puzzleManager.NextPuzzle;
            this.NumbrixGameBoardCells = this.createNumbrixGameBoardCells();
        }

        /// <summary>
        ///     Previouses the puzzle.
        /// </summary>
        public void PreviousPuzzle()
        {
            this.Model = this.puzzleManager.PreviousPuzzle;
            this.NumbrixGameBoardCells = this.createNumbrixGameBoardCells();
        }

        /// <summary>
        ///     Loads the game board.
        /// </summary>
        /// <param name="gameBoardFile">The game board file.</param>
        public async Task LoadGameBoard(StorageFile gameBoardFile)
        {
            this.Model = await NumbrixGameBoardReader.LoadPuzzle(gameBoardFile);
            await this.puzzleManager.initializeStartingPuzzles();
            this.puzzleManager.CurrentPuzzle =
                this.puzzleManager.Puzzles.SingleOrDefault(puzzle => puzzle.Equals(this.Model));
            this.NumbrixGameBoardCells = this.createNumbrixGameBoardCells();
        }

        #endregion
    }
}