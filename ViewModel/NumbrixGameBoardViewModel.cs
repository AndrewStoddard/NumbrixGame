using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using NumbrixGame.Model;
using NumbrixGame.View;

namespace NumbrixGame.ViewModel
{
    public class NumbrixGameBoardViewModel
    {
        #region Data members

        private IList<GameBoardCellTextBox> gameBoardTextCells;
        private readonly PuzzleManager puzzleManager;

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
            this.puzzleManager = new PuzzleManager();

            this.Model = this.loadStartingPuzzle();
            this.NumbrixGameBoardCells = this.createNumbrixGameBoardCells();
        }

        #endregion

        #region Methods

        public void NextPuzzle()
        {
            this.Model = this.puzzleManager.NextPuzzle;
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
            foreach (var gameBoardCell in this.NumbrixGameBoardCells)
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
            return this.puzzleManager.CurrentPuzzle;

            #endregion
        }
    }
}