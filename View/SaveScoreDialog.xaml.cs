using System;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NumbrixGame.View
{
    public sealed partial class SaveScoreDialog : ContentDialog
    {
        #region Properties

        public string Username { get; set; }
        public TimeSpan TimeTaken { get; set; }

        #endregion

        #region Constructors

        public SaveScoreDialog()
        {
            this.InitializeComponent();
        }

        #endregion
    }
}