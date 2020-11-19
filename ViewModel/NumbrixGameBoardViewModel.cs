using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using NumbrixGame.Model;
using NumbrixGame.PrebuiltGames;
using NumbrixGame.View;

namespace NumbrixGame.ViewModel
{
    public class NumbrixGameBoardViewModel
    {
        #region Data members

        private IList<GameBoardCellTextBox> gameBoardTextCells;

        #endregion

        #region Properties

        public NumbrixGameBoard Model { get; set; }

        public IList<NumbrixGameBoardCellViewModel> NumbrixGameBoardCells { get; set; }

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

        #endregion

        #region Constructors

        public NumbrixGameBoardViewModel()
        {
            this.Model = this.loadStartingPuzzle();
            this.NumbrixGameBoardCells = this.createNumbrixGameBoardCells();
        }

        #endregion

        #region Methods

        private IList<NumbrixGameBoardCellViewModel> createNumbrixGameBoardCells()
        {
            var result = new List<NumbrixGameBoardCellViewModel>();
            foreach (var gameBoardCell in this.Model.NumbrixGameBoardCells)
            {
                result.Add(new NumbrixGameBoardCellViewModel(gameBoardCell));
            }

            return result;
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
            foreach (var gameBoardCell in this.Model.NumbrixGameBoardCells)
            {
                if (!gameBoardCell.DefaultValue)
                {
                    gameBoardCell.NumbrixValue = null;
                }
            }
        }

        public async Task LoadGameBoard(StorageFile gameBoardFile)
        {
            this.Model = await CsvReader.LoadPuzzle(gameBoardFile);
        }

        private NumbrixGameBoard loadStartingPuzzle()
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

            return gameBoard;
        }

        #endregion
    }
}