using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using NumbrixGame.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NumbrixGame.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NumbrixScoreboardPage : Page
    {
        #region Data members

        private NumbrixScoreBoardViewModel numbrixScoreBoardViewModel;

        #endregion

        #region Constructors

        public NumbrixScoreboardPage()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private void GoBack(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                this.numbrixScoreBoardViewModel = (NumbrixScoreBoardViewModel) e.Parameter;
            }
        }

        #endregion
    }
}