using Windows.UI.Xaml.Controls;
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
            this.numbrixScoreBoardViewModel = new NumbrixScoreBoardViewModel();
        }

        #endregion
    }
}