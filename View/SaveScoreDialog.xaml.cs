using System;
using System.ComponentModel;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NumbrixGame.View
{
    public sealed partial class SaveScoreDialog : INotifyPropertyChanged
    {
        #region Data members

        private string username;

        #endregion

        #region Properties

        public string Username
        {
            get => this.username;
            set
            {
                this.username = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.username)));
            }
        }

        public TimeSpan TimeTaken { get; set; }

        #endregion

        #region Constructors

        public SaveScoreDialog()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}