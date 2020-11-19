using System.ComponentModel;
using System.Runtime.CompilerServices;
using NumbrixGame.Annotations;
using NumbrixGame.Model;

namespace NumbrixGame.ViewModel
{
    public class NumbrixGameBoardCellViewModel : INotifyPropertyChanged
    {
        #region Properties

        public int X
        {
            get => this.Model.X;
            set
            {
                this.Model.X = value;
                this.OnPropertyChanged();
            }
        }

        public int Y
        {
            get => this.Model.Y;
            set
            {
                this.Model.Y = value;
                this.OnPropertyChanged();
            }
        }

        public int? NumbrixValue
        {
            get => this.Model.NumbrixValue;
            set
            {
                this.Model.NumbrixValue = value;
                this.OnPropertyChanged();
            }
        }

        public bool DefaultValue
        {
            get => this.Model.DefaultValue;
            set
            {
                this.Model.DefaultValue = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsEnabled => !this.Model.DefaultValue;

        public NumbrixGameBoardCell Model { get; set; }

        #endregion

        #region Constructors

        public NumbrixGameBoardCellViewModel(int x, int y)
        {
            this.Model = new NumbrixGameBoardCell(x, y);
        }

        public NumbrixGameBoardCellViewModel(NumbrixGameBoardCell gameBoardCell)
        {
            this.Model = gameBoardCell;
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}