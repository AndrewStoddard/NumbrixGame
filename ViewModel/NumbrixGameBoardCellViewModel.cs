﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using NumbrixGame.Annotations;
using NumbrixGame.Model;

namespace NumbrixGame.ViewModel
{
    public class NumbrixGameBoardCellViewModel : INotifyPropertyChanged
    {
        #region Types and Delegates

        public delegate void NumbrixValueChanged();

        #endregion

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
                this.OnNumbrixValueChanged?.Invoke();
            }
        }

        public bool IsDefaultValue
        {
            get => this.Model.IsDefaultValue;
            set
            {
                this.Model.IsDefaultValue = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsEnabled => !this.Model.IsDefaultValue;

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
        public event NumbrixValueChanged OnNumbrixValueChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}