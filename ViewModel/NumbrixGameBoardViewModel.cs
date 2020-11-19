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
            var gameBoard = new NumbrixGameBoard();
            var dataFileLines = StartingPuzzles.puzzleOne.Replace("\r", "").Split("\n");

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

            this.NumbrixGameBoard = gameBoard;
        }

        #endregion
    }
}