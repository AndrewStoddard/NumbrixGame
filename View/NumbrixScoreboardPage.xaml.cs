using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.UI.Controls;
using NumbrixGame.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NumbrixGame.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NumbrixScoreboardPage
    {
        #region Data members

        /// <summary>
        ///     The numbrix score board view model
        /// </summary>
        private NumbrixScoreBoardViewModel numbrixScoreBoardViewModel;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixScoreboardPage" /> class.
        /// </summary>
        public NumbrixScoreboardPage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Handles the <see cref="E:MainMenu" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnMainMenu(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NumbrixMainMenuPage));
        }

        /// <summary>
        ///     Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">
        ///     Event data that can be examined by overriding code. The event data is representative of the pending
        ///     navigation that will load the current Page. Usually the most relevant property to examine is Parameter.
        /// </param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this.numbrixScoreBoardViewModel = (NumbrixScoreBoardViewModel) e.Parameter;
            }
        }

        /// <summary>
        ///     Clears the score board.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void clearScoreBoard(object sender, RoutedEventArgs e)
        {
            this.numbrixScoreBoardViewModel.ResetScores();
        }

        /// <summary>
        ///     Handles the <see cref="E:Sort" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataGridColumnEventArgs" /> instance containing the event data.</param>
        private void OnSort(object sender, DataGridColumnEventArgs e)
        {
            this.numbrixScoreBoardViewModel.OnSortByColumn(this.scoreBoard.Columns, e);
        }

        #endregion
    }
}