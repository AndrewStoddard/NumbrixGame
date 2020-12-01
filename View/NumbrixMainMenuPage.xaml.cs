using Windows.UI.Xaml.Controls;
using NumbrixGame.Datatier;
using NumbrixGame.PrebuiltGames;
using NumbrixGame.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NumbrixGame.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NumbrixMainMenuPage : Page
    {
        #region Data members

        private readonly NumbrixMainPageViewModel numbrixMainPageViewModel;

        #endregion

        #region Constructors

        public NumbrixMainMenuPage()
        {
            this.InitializeComponent();
            this.numbrixMainPageViewModel = new NumbrixMainPageViewModel();
            var prebuiltSuffix = "prebuilt_";
            for (var i = 1; i <= 8; i++)
            {
                var filename = prebuiltSuffix + i + ".csv";
                if (!NumbrixGameBoardWriter.FileExists(filename))
                {
                    NumbrixGameBoardWriter.WriteGameboard(
                        NumbrixGameBoardReader.LoadPuzzle(MainPuzzles.PuzzleList?[i - 1]), filename);
                }
            }
        }

        #endregion
    }
}