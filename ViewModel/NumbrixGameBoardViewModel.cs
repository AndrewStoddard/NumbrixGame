using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using NumbrixGame.Annotations;
using NumbrixGame.Datatier;
using NumbrixGame.Model;
using NumbrixGame.View;

namespace NumbrixGame.ViewModel
{
    public class NumbrixGameBoardViewModel : INotifyPropertyChanged
    {
        #region Types and Delegates

        public delegate void ValueChanged();

        #endregion

        #region Data members

        private IList<GameBoardCellTextBox> gameBoardTextCells;
        private readonly DispatcherTimer timer;
        private readonly PuzzleManager puzzleManager;

        #endregion

        #region Properties

        public NumbrixGameBoard Model { get; set; }

        public IList<NumbrixGameBoardCellViewModel> NumbrixGameBoardCells { get; set; }

        public int GameBoardNumber
        {
            get => this.Model.GameBoardNumber;
            set => this.Model.GameBoardNumber = value;
        }

        public int BoardWidth
        {
            get => this.Model.BoardWidth;
            set => this.Model.BoardWidth = value;
        }

        public int BoardHeight
        {
            get => this.Model.BoardHeight;
            set => this.Model.BoardHeight = value;
        }

        public bool IsPaused
        {
            get => this.Model.IsPaused;
            set
            {
                this.Model.IsPaused = value;
                this.OnPropertyChanged(nameof(this.IsPaused));
            }
        }

        public bool IsFinished
        {
            get => this.Model.IsFinished;
            set
            {
                this.Model.IsFinished = value;
                this.OnPropertyChanged(nameof(this.IsFinished));
            }
        }

        public TimeSpan TimeTaken
        {
            get => this.Model.TimeTaken;
            set
            {
                this.Model.TimeTaken = value;
                this.OnPropertyChanged(nameof(this.TimeTaken));
            }
        }

        public int MaxBoardValue => this.BoardHeight * this.BoardWidth;

        #endregion

        #region Constructors

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void Timer_Tick(object sender, object e)
        {
            this.TimeTaken = this.TimeTaken.Add(new TimeSpan(0, 0, 1));
        }

        public event ValueChanged OnValueChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartTime()
        {
            this.IsPaused = false;
            this.timer.Start();
        }

        public void PauseTime()
        {
            this.IsPaused = true;
            this.timer.Stop();
        }

        public void ResetTime()
        {
            this.TimeTaken = new TimeSpan(0, 0, 0);
            this.ClearGameBoard();
            this.IsPaused = true;
            this.timer.Stop();
        }

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

        private void OnValueChange()
        {
            if (this.NumbrixGameBoardCells.All(cell => cell.NumbrixValue != null))
            {
                this.OnValueChanged?.Invoke();
            }
        }

        public NumbrixGameBoardCellViewModel FindCell(int x, int y)
        {
            return this.NumbrixGameBoardCells.SingleOrDefault(cell => cell.X == x && cell.Y == y);
        }

        public NumbrixGameBoardCellViewModel FindCell(int? value)
        {
            return this.NumbrixGameBoardCells.SingleOrDefault(cell => cell.NumbrixValue == value);
        }

        public bool CheckSolution()
        {
            return NumbrixSolver.CheckForSolved(this.Model);
        }

        public void UpdateCell(int x, int y, int? value)
        {
            this.Model.FindCell(x, y).NumbrixValue = value;
        }

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

        public void NextPuzzle()
        {
            this.Model = this.puzzleManager.NextPuzzle;
            this.NumbrixGameBoardCells = this.createNumbrixGameBoardCells();
        }

        public void PreviousPuzzle()
        {
            this.Model = this.puzzleManager.PreviousPuzzle;
            this.NumbrixGameBoardCells = this.createNumbrixGameBoardCells();
        }

        public async Task LoadGameBoard(StorageFile gameBoardFile)
        {
            this.Model = await NumbrixGameBoardReader.LoadPuzzle(gameBoardFile);
            this.puzzleManager.CurrentPuzzle =
                this.puzzleManager.Puzzles.SingleOrDefault(puzzle => puzzle.Equals(this.Model));
            this.NumbrixGameBoardCells = this.createNumbrixGameBoardCells();
        }

        #endregion
    }
}