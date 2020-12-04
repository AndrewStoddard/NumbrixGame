using System;
using System.ComponentModel;
using NumbrixGame.Model;

namespace NumbrixGame.ViewModel
{
    /// <summary>
    ///     Class NumbrixPlayerScoreViewModel.
    ///     Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class NumbrixPlayerScoreViewModel : INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public NumbrixPlayerScore Model { get; set; }

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username
        {
            get => this.Model.Username;
            set
            {
                this.Model.Username = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Username)));
            }
        }

        /// <summary>
        ///     Gets or sets the time taken.
        /// </summary>
        /// <value>The time taken.</value>
        public TimeSpan TimeTaken
        {
            get => this.Model.TimeTaken;
            set
            {
                this.Model.TimeTaken = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TimeTaken)));
            }
        }

        /// <summary>
        ///     Gets or sets the puzzle number.
        /// </summary>
        /// <value>The puzzle number.</value>
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

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixPlayerScoreViewModel" /> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="timeTaken">The time taken.</param>
        /// <param name="puzzleNumber">The puzzle number.</param>
        public NumbrixPlayerScoreViewModel(string username, TimeSpan timeTaken, int puzzleNumber)
        {
            this.Model = new NumbrixPlayerScore(username, timeTaken, puzzleNumber);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixPlayerScoreViewModel" /> class.
        /// </summary>
        /// <param name="score">The score.</param>
        public NumbrixPlayerScoreViewModel(NumbrixPlayerScore score)
        {
            this.Model = score;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}