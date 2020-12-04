using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.Core.Preview;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NumbrixGame.Model;
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

        private readonly NumbrixGameBoardViewModel numbrixGameBoardViewModel;
        private List<GameBoardCellTextBox> gameBoardCellTextBoxes;
        private readonly NumbrixScoreBoardViewModel numbrixScoreBoardViewModel;
        private readonly SoundManager soundManager;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="NumbrixGameBoardPage" /> class.</summary>
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

        private void clearPuzzle()
        {
            this.gameBoardCellTextBoxes = new List<GameBoardCellTextBox>();

            foreach (var currentChild in this.parentGrid.Children)
            {
                this.parentGrid.Children.Remove(currentChild);
            }
        }

        private void createGameBoard()
        {
            this.clearPuzzle();
            var parentStackPanel = new StackPanel();
            parentStackPanel.Name = "StackPanelPuzzleBoard";
            parentStackPanel.BorderBrush = new SolidColorBrush(Colors.Blue);
            parentStackPanel.BorderThickness = new Thickness(2);
            parentStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            parentStackPanel.VerticalAlignment = VerticalAlignment.Center;

            this.parentGrid.Children.Add(parentStackPanel);

            for (var i = 1; i <= this.numbrixGameBoardViewModel.BoardHeight; i++)
            {
                var stackPanel = new StackPanel();

                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Width = this.parentGrid.Width;
                stackPanel.Spacing = 0;
                stackPanel.BorderBrush = new SolidColorBrush(Colors.Blue);
                stackPanel.BorderThickness = new Thickness(2);
                stackPanel.Padding = new Thickness(0);
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

        private GameBoardCellTextBox createCell(int x, int y)
        {
            var cellTextBoxControl = new GameBoardCellTextBox();
            cellTextBoxControl.MaxValue = this.numbrixGameBoardViewModel.MaxBoardValue;
            cellTextBoxControl.DataContext = this.numbrixGameBoardViewModel.FindCell(x, y);
            cellTextBoxControl.X = x;
            cellTextBoxControl.Y = y;
            cellTextBoxControl.KeyDown += this.OnArrowKeyDown;
            return cellTextBoxControl;
        }

        private void OnArrowKeyDown(object sender, KeyRoutedEventArgs e)
        {
            var cellTextBox = sender as GameBoardCellTextBox;
            if (cellTextBox == null)
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

        private void updatePuzzleNumber()
        {
            this.textBlockPuzzleNumber.Text = "Puzzle Number: " + this.numbrixGameBoardViewModel.GameBoardNumber;
        }

        private static async Task<StorageFile> pickFile()
        {
            var filePicker = new FileOpenPicker {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            filePicker.FileTypeFilter.Add(".csv");

            var file = await filePicker.PickSingleFileAsync();
            return file;
        }

        private void checkSolution(object sender, RoutedEventArgs e)
        {
            this.checkSolution();
        }

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

        private async Task createSaveScoreDialog()
        {
            var saveScoreDialog = new SaveScoreDialog {TimeTaken = this.numbrixGameBoardViewModel.TimeTaken};
            var showDialog = await saveScoreDialog.ShowAsync();

            if (!string.IsNullOrEmpty(saveScoreDialog.Username))
            {
                this.numbrixScoreBoardViewModel.AddPlayerScore(new NumbrixPlayerScoreViewModel(saveScoreDialog.Username,
                    this.numbrixGameBoardViewModel.TimeTaken, this.numbrixGameBoardViewModel.GameBoardNumber));
                Debug.WriteLine(saveScoreDialog.Username);
            }

            switch (showDialog)
            {
                case ContentDialogResult.Primary:
                    Frame.Navigate(typeof(NumbrixScoreboardPage), this.numbrixScoreBoardViewModel);
                    break;
                case ContentDialogResult.Secondary:
                    this.numbrixGameBoardViewModel.NextPuzzle();
                    this.createGameBoard();
                    break;
            }
        }

        private void clearBoard(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.ClearGameBoard();
        }

        private void OnNextPuzzle(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.NextPuzzle();
            this.createGameBoard();
        }

        private void OnPreviousPuzzle(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.PreviousPuzzle();
            this.createGameBoard();
        }

        private void OnStopTimer(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.PauseTime();
            this.soundManager.Pause();
        }

        private void OnStartTimer(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.StartTime();
            this.soundManager.Play();
        }

        private void OnResetTimer(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.ResetTime();
        }

        private void OnScoreboard(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NumbrixScoreboardPage), this.numbrixScoreBoardViewModel);
        }

        private async void loadPuzzle(StorageFile gameFile)
        {
            await this.numbrixGameBoardViewModel.LoadGameBoard(gameFile);
            this.numbrixGameBoardViewModel.IsPaused = true;
            this.createGameBoard();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter == null)
            {
                return;
            }

            var gameFile = (StorageFile) e.Parameter;
            this.loadPuzzle(gameFile);
        }

        private void OnToggleMusic(object sender, RoutedEventArgs e)
        {
            this.soundManager.MusicOn = !this.soundManager.MusicOn;
            if (this.soundManager.MusicOn)
            {
                this.ToggleMusicButton.Content = "Music Off";
                this.soundManager.Pause();
            }
            else
            {
                this.ToggleMusicButton.Content = "Music On";
                this.soundManager.Play();
            }
        }

        private void createSaveReminderDialog()
        {
        }

        private void OnMainMenu(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NumbrixMainMenuPage));
        }

        private void OnSavedPuzzles(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void OnCloseRequest(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            e.Handled = true;
            var result = await this.showSaveDialog();
            Application.Current.Exit();
        }

        private async Task<ContentDialogResult> showSaveDialog()
        {
            var saveDialog = new ContentDialog {
                Title = "Saving Progress",
                Content = "Saving your current progress...",
                PrimaryButtonText = "Ok"
            };
            var result = await saveDialog.ShowAsync();
            return result;
        }

        #endregion
    }
}