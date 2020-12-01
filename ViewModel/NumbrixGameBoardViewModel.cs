using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
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
            this.puzzleManager = new PuzzleManager();

            this.Model = this.loadStartingPuzzle();
            this.NumbrixGameBoardCells = this.createNumbrixGameBoardCells();
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        public event ValueChanged OnValueChanged;

        public void NextPuzzle()
        {
            this.Model = this.puzzleManager.NextPuzzle;
            this.NumbrixGameBoardCells = this.createNumbrixGameBoardCells();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods

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

        public async Task LoadGameBoard(StorageFile gameBoardFile)
        {
            this.Model = await NumbrixGameBoardReader.LoadPuzzle(gameBoardFile);
        }

        private NumbrixGameBoard loadStartingPuzzle()
        {
            return this.puzzleManager.CurrentPuzzle;

            #endregion
        }
    }
}