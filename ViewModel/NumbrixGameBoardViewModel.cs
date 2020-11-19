using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using NumbrixGame.Annotations;
using NumbrixGame.Model;
using NumbrixGame.PrebuiltGames;
using NumbrixGame.View;

namespace NumbrixGame.ViewModel
{
    public class NumbrixGameBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        private IList<GameBoardCellTextBox> gameBoardTextCells;
        private PuzzleManager puzzleManager;

        #endregion

        #region Properties

        public NumbrixGameBoard NumbrixGameBoard { get; set; }

        public IList<GameBoardCellTextBox> GameBoardTextCells
        {
            get => this.gameBoardTextCells;
            set
            {
                this.gameBoardTextCells = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.gameBoardTextCells)));
            }
        }

        #endregion

        #region Constructors

        public NumbrixGameBoardViewModel()
        {
            this.NumbrixGameBoard = new NumbrixGameBoard();
            this.puzzleManager = new PuzzleManager();
            this.loadStartingPuzzle();
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        public bool CheckSolution()
        {
            return NumbrixSolver.CheckForSolved(this.NumbrixGameBoard);
        }

        public void UpdateCell(int x, int y, int? value)
        {
            this.NumbrixGameBoard.FindCell(x, y).NumbrixValue = value;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadGameBoard(StorageFile gameBoardFile)
        {
            this.NumbrixGameBoard = await CsvReader.LoadPuzzle(gameBoardFile);
        }

        private void loadStartingPuzzle()
        {
            this.NumbrixGameBoard = this.puzzleManager.CurrentPuzzle;
        }

        public void NextPuzzle()
        {
            this.NumbrixGameBoard = this.puzzleManager.NextPuzzle;
        }

        #endregion
    }
}