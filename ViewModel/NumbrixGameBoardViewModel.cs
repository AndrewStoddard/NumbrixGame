using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Storage;
using NumbrixGame.Annotations;
using NumbrixGame.Model;

namespace NumbrixGame.ViewModel
{
    public class NumbrixGameBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        #endregion

        #region Properties

        public NumbrixGameBoard NumbrixGameBoard { get; set; }

        public List<NumbrixGameBoardCell> gameBoardCells { get; set; }

        #endregion

        #region Constructors

        public NumbrixGameBoardViewModel()
        {
            this.gameBoardCells = new List<NumbrixGameBoardCell>();
            this.NumbrixGameBoard = new NumbrixGameBoard();
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        public NumbrixGameBoardCell CreateCell(int x, int y, int numbrixValue = -1, bool isDefault = false)
        {
            var newCell = new NumbrixGameBoardCell(x, y) {NumbrixValue = numbrixValue, DefaultValue = isDefault};
            this.gameBoardCells.Add(newCell);
            return newCell;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void LoadGameBoard(StorageFile gameBoardFile)
        {
            this.NumbrixGameBoard = await CsvReader.LoadPuzzle(gameBoardFile);
        }

        #endregion
    }
}