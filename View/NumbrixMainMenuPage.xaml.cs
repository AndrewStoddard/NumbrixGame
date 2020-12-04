using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using NumbrixGame.Properties;
using NumbrixGame.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NumbrixGame.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NumbrixMainMenuPage : INotifyPropertyChanged
    {
        #region Data members

        /// <summary>
        ///     The numbrix main page view model
        /// </summary>
        private NumbrixMainPageViewModel numbrixMainPageViewModel;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixMainMenuPage" /> class.
        /// </summary>
        public NumbrixMainMenuPage()
        {
            this.createViewmodel();
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Creates the viewmodel.
        /// </summary>
        private async void createViewmodel()
        {
            this.numbrixMainPageViewModel = await NumbrixMainPageViewModel.BuildViewModelAsync();
            this.OnPropertyChanged(nameof(this.numbrixMainPageViewModel));
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Handles the OnClick event of the ButtonBase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.numbrixMainPageViewModel.SelectedGameFile != null)
            {
                Frame.Navigate(typeof(NumbrixGameBoardPage), this.numbrixMainPageViewModel.SelectedGameFile);
            }
        }

        #endregion
    }
}