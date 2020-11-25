using System;
using System.ComponentModel;
using NumbrixGame.Model;

namespace NumbrixGame.ViewModel
{
    public class NumbrixPlayerScoreViewModel : INotifyPropertyChanged
    {
        #region Properties

        public NumbrixPlayerScore Model { get; set; }

        public string Username
        {
            get => this.Model.Username;
            set
            {
                this.Model.Username = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Username)));
            }
        }

        public TimeSpan TimeTaken
        {
            get => this.Model.TimeTaken;
            set
            {
                this.Model.TimeTaken = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TimeTaken)));
            }
        }

        public int PuzzleNumber
        {
            get => this.Model.PuzzleNumber;
            set
            {
                this.Model.PuzzleNumber = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.PuzzleNumber)));
            }
        }

        #endregion

        #region Constructors

        public NumbrixPlayerScoreViewModel(string username, TimeSpan timeTaken, int puzzleNumber)
        {
            this.Model = new NumbrixPlayerScore(username, timeTaken, puzzleNumber);
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}