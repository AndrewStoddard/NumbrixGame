using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.Core.Preview;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NumbrixGame.Model;
using NumbrixGame.View.Controls;
using NumbrixGame.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NumbrixGame.View
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NumbrixGameBoardPage
    {
        #region Data members

        /// <summary>
        ///     The numbrix game board view model
        /// </summary>
        private readonly NumbrixGameBoardViewModel numbrixGameBoardViewModel;

        /// <summary>
        ///     The game board cell text boxes
        /// </summary>
        private List<GameBoardCellTextBox> gameBoardCellTextBoxes;

        /// <summary>
        ///     The numbrix score board view model
        /// </summary>
        private readonly NumbrixScoreBoardViewModel numbrixScoreBoardViewModel;

        /// <summary>
        ///     The sound manager
        /// </summary>
        private readonly SoundManager soundManager;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixGameBoardPage" /> class.
        /// </summary>
        public NumbrixGameBoardPage()
        {
            this.soundManager = new SoundManager();
            this.soundManager.Pause();
            this.numbrixScoreBoardViewModel = new NumbrixScoreBoardViewModel();
            this.numbrixGameBoardViewModel = new NumbrixGameBoardViewModel();
            this.numbrixGameBoardViewModel.OnValueChanged += this.checkSolution;
            this.gameBoardCellTextBoxes = new List<GameBoardCellTextBox>();
            this.InitializeComponent();
            this.textBlockGamePaused.Visibility = Visibility.Collapsed;
            SystemNavigationManagerPreview.GetForCurrentView().CloseRequested += this.OnCloseRequest;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Clears the puzzle.
        /// </summary>
        private void clearPuzzle()
        {
            this.gameBoardCellTextBoxes = new List<GameBoardCellTextBox>();

            foreach (var currentChild in this.parentGrid.Children)
            {
                this.parentGrid.Children.Remove(currentChild);
            }
        }

        /// <summary>
        ///     Creates the game board.
        /// </summary>
        private void createGameBoard()
        {
            this.clearPuzzle();
            var parentStackPanel = new StackPanel {
                Name = "StackPanelPuzzleBoard",
                BorderBrush = new SolidColorBrush(Colors.Blue),
                BorderThickness = new Thickness(2),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            this.parentGrid.Children.Add(parentStackPanel);

            for (var i = 1; i <= this.numbrixGameBoardViewModel.BoardHeight; i++)
            {
                var stackPanel = new StackPanel {
                    Orientation = Orientation.Horizontal,
                    Width = this.parentGrid.Width,
                    Spacing = 0,
                    BorderBrush = new SolidColorBrush(Colors.Blue),
                    BorderThickness = new Thickness(2),
                    Padding = new Thickness(0)
                };
                parentStackPanel.Children.Add(stackPanel);

                foreach (var gameBoardCell in this.numbrixGameBoardViewModel.NumbrixGameBoardCells)
                {
                    if (gameBoardCell.Y == i)
                    {
                        var cell = this.createCell(gameBoardCell.X, gameBoardCell.Y);
                        this.gameBoardCellTextBoxes.Add(cell);
                        stackPanel.Children.Add(cell);
                    }
                }
            }

            this.updatePuzzleNumber();
        }

        /// <summary>
        ///     Creates the cell.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>GameBoardCellTextBox.</returns>
        private GameBoardCellTextBox createCell(int x, int y)
        {
            var cellTextBoxControl = new GameBoardCellTextBox {
                MaxValue = this.numbrixGameBoardViewModel.MaxBoardValue,
                DataContext = this.numbrixGameBoardViewModel.FindCell(x, y),
                X = x,
                Y = y
            };
            cellTextBoxControl.KeyDown += this.OnArrowKeyDown;
            return cellTextBoxControl;
        }

        /// <summary>
        ///     Handles the <see cref="E:ArrowKeyDown" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs" /> instance containing the event data.</param>
        private void OnArrowKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (!(sender is GameBoardCellTextBox cellTextBox))
            {
                return;
            }

            switch (e.Key)
            {
                case VirtualKey.Right:
                    this.gameBoardCellTextBoxes
                        .SingleOrDefault(cell => cellTextBox.X + 1 == cell.X && cellTextBox.Y == cell.Y)
                        ?.Focus(FocusState.Keyboard);
                    break;
                case VirtualKey.Left:
                    this.gameBoardCellTextBoxes
                        .SingleOrDefault(cell => cellTextBox.X - 1 == cell.X && cellTextBox.Y == cell.Y)
                        ?.Focus(FocusState.Keyboard);
                    break;
                case VirtualKey.Up:
                    this.gameBoardCellTextBoxes
                        .SingleOrDefault(cell => cellTextBox.X == cell.X && cellTextBox.Y - 1 == cell.Y)
                        ?.Focus(FocusState.Keyboard);
                    break;
                case VirtualKey.Down:
                    this.gameBoardCellTextBoxes
                        .SingleOrDefault(cell => cellTextBox.X == cell.X && cellTextBox.Y + 1 == cell.Y)
                        ?.Focus(FocusState.Keyboard);
                    break;
            }
        }

        /// <summary>
        ///     Updates the puzzle number.
        /// </summary>
        private void updatePuzzleNumber()
        {
            this.textBlockPuzzleNumber.Text = "Puzzle Number: " + this.numbrixGameBoardViewModel.GameBoardNumber;
        }

        /// <summary>
        ///     Checks the solution.
        /// </summary>
        private async void checkSolution()
        {
            if (this.numbrixGameBoardViewModel.CheckSolution())
            {
                this.numbrixGameBoardViewModel.PauseTime();
                await this.createSaveScoreDialog();
            }
            else
            {
                this.incorrectTextBox.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        ///     Creates the save score dialog.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private async Task createSaveScoreDialog()
        {
            var saveScoreDialog = new SaveScoreDialog {TimeTaken = this.numbrixGameBoardViewModel.TimeTaken};
            var showDialog = await saveScoreDialog.ShowAsync();

            if (!string.IsNullOrEmpty(saveScoreDialog.Username))
            {
                this.numbrixScoreBoardViewModel.AddPlayerScore(new NumbrixPlayerScoreViewModel(saveScoreDialog.Username,
                    this.numbrixGameBoardViewModel.TimeTaken, this.numbrixGameBoardViewModel.GameBoardNumber));
                this.numbrixScoreBoardViewModel.SaveScores();
                Debug.WriteLine(saveScoreDialog.Username);
            }

            switch (showDialog)
            {
                case ContentDialogResult.Primary:
                    this.soundManager.Pause();
                    Frame.Navigate(typeof(NumbrixScoreboardPage), this.numbrixScoreBoardViewModel);
                    break;
                case ContentDialogResult.Secondary:
                    this.numbrixGameBoardViewModel.NextPuzzle();
                    this.createGameBoard();
                    break;
                case ContentDialogResult.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Clears the board.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void clearBoard(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.ClearGameBoard();
        }

        /// <summary>
        ///     Handles the <see cref="E:NextPuzzle" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnNextPuzzle(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.SaveGameState();
            this.numbrixGameBoardViewModel.NextPuzzle();
            this.createGameBoard();
        }

        /// <summary>
        ///     Handles the <see cref="E:PreviousPuzzle" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnPreviousPuzzle(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.SaveGameState();
            this.numbrixGameBoardViewModel.PreviousPuzzle();
            this.createGameBoard();
        }

        /// <summary>
        ///     Handles the <see cref="E:StopTimer" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnStopTimer(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.PauseTime();
            this.soundManager.Pause();
        }

        /// <summary>
        ///     Handles the <see cref="E:StartTimer" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnStartTimer(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.StartTime();
            this.soundManager.Play();
        }

        /// <summary>
        ///     Handles the <see cref="E:ResetTimer" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnResetTimer(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.ResetTime();
        }

        /// <summary>
        ///     Handles the <see cref="E:Scoreboard" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnScoreboard(object sender, RoutedEventArgs e)
        {
            this.soundManager.Pause();
            this.numbrixGameBoardViewModel.SaveGameState();
            Frame.Navigate(typeof(NumbrixScoreboardPage), this.numbrixScoreBoardViewModel);
        }

        /// <summary>
        ///     Loads the puzzle.
        /// </summary>
        /// <param name="gameFile">The game file.</param>
        private async void loadPuzzle(StorageFile gameFile)
        {
            await this.numbrixGameBoardViewModel.LoadGameBoard(gameFile);
            this.numbrixGameBoardViewModel.IsPaused = false;
            this.numbrixGameBoardViewModel.StartTime();
            this.createGameBoard();
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
            base.OnNavigatedTo(e);
            if (e.Parameter == null)
            {
                return;
            }

            var gameFile = (StorageFile) e.Parameter;
            this.loadPuzzle(gameFile);
            this.soundManager.Play();
        }

        /// <summary>
        ///     Handles the <see cref="E:ToggleMusic" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnToggleMusic(object sender, RoutedEventArgs e)
        {
            this.soundManager.MusicOn = !this.soundManager.MusicOn;
            if (this.soundManager.MusicOn)
            {
                this.toggleMusicButton.Content = "Music Off";
                this.soundManager.Pause();
            }
            else
            {
                this.toggleMusicButton.Content = "Music On";
                this.soundManager.Play();
            }
        }

        /// <summary>
        ///     Handles the <see cref="E:MainMenu" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void OnMainMenu(object sender, RoutedEventArgs e)
        {
            this.soundManager.Pause();
            this.numbrixGameBoardViewModel.SaveGameState();
            Frame.Navigate(typeof(NumbrixMainMenuPage));
        }

        /// <summary>
        ///     Handles the <see cref="E:CloseRequest" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SystemNavigationCloseRequestedPreviewEventArgs" /> instance containing the event data.</param>
        private async void OnCloseRequest(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            try
            {
                this.numbrixGameBoardViewModel.SaveGameState();
                e.Handled = true;
                await showSaveDialog();

                Application.Current.Exit();
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     Shows the save dialog.
        /// </summary>
        /// <returns>ContentDialogResult.</returns>
        private static async Task showSaveDialog()
        {
            var saveDialog = new ContentDialog {
                Title = "Saving Progress",
                Content = "Saving your current progress...",
                PrimaryButtonText = "Ok"
            };
            await saveDialog.ShowAsync();
        }

        /// <summary>
        ///     Saves the board.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void saveBoard(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.SaveGameState();
        }

        #endregion
    }
}