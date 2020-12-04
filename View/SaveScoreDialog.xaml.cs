using System;
using System.ComponentModel;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NumbrixGame.View
{
    /// <summary>
    ///     Class SaveScoreDialog. This class cannot be inherited.
    ///     Implements the <see cref="Windows.UI.Xaml.Controls.ContentDialog" />
    ///     Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    ///     Implements the <see cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    ///     Implements the <see cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class SaveScoreDialog : INotifyPropertyChanged
    {
        #region Data members

        /// <summary>
        ///     The username
        /// </summary>
        private string username;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username
        {
            get => this.username;
            set
            {
                this.username = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.username)));
            }
        }

        /// <summary>
        ///     Gets or sets the time taken.
        /// </summary>
        /// <value>The time taken.</value>
        public TimeSpan TimeTaken { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SaveScoreDialog" /> class.
        /// </summary>
        public SaveScoreDialog()
        {
            this.InitializeComponent();
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