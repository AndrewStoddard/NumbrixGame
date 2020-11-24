using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        private DispatcherTimer timer;
        private bool timerStarted;
        private int seconds = 0;
        private int minutes = 0;
        private int hours = 0;

        #endregion

        #region Constructors

        public NumbrixGameBoardPage()
        {
            this.InitializeComponent();
            this.numbrixGameBoardViewModel = new NumbrixGameBoardViewModel();
            this.numbrixGameBoardViewModel.OnValueChanged += this.checkSolution;
            this.gameBoardCellTextBoxes = new List<GameBoardCellTextBox>();

            this.createGameBoard();
            this.solutionCheckMessage.Visibility = Visibility.Collapsed;

            this.InitializeDispatcherTimerSetup();
            this.buttonStopTimer.IsEnabled = false;
            this.textBlockGamePaused.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Methods

        public void InitializeDispatcherTimerSetup()
        {
            this.timer = new DispatcherTimer();
            this.timer.Interval = new TimeSpan(0, 0, 1);
            this.timerStarted = false;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            this.TextBlockPuzzleTimer.Text = DateTime.Now.Second.ToString();

            // Forcing the CommandManager to raise the RequerySuggested event
            // CommandManager.InvalidateRequerySuggested();
        }

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

        private async void loadGameBoard(object sender, RoutedEventArgs e)
        {
            /*this.clearPuzzle();
            var file = await pickFile();
            if (file != null)
            {
                await this.numbrixGameBoardViewModel.LoadGameBoard(file);
                this.createGameBoard();
            }*/
            //TODO - Implement
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

        private void checkSolution()
        {
            var message = "Your solution is incorrect!";
            if (this.numbrixGameBoardViewModel.CheckSolution())
            {
                message = "Congratulations! You successfully solved the puzzle!";
            }

            this.solutionCheckMessage.Visibility = Visibility.Visible;
            this.solutionCheckMessage.Text = message;
        }

        private void clearBoard(object sender, RoutedEventArgs e)
        {
            this.numbrixGameBoardViewModel.ClearGameBoard();
        }

        private void NextPuzzle_OnClick(object sender, RoutedEventArgs e)
        {
            this.solutionCheckMessage.Visibility = Visibility.Collapsed;

            this.numbrixGameBoardViewModel.NextPuzzle();

            this.createGameBoard();
            this.resetTimer();
        }

        private void resetTimer()
        {
            this.timer.Stop();
            this.InitializeDispatcherTimerSetup();
            this.seconds = 0;
            this.minutes = 0;
            this.hours = 0;

            this.resetTimerButtonStates();
            this.TextBlockPuzzleTimer.Text = $"{this.hours:00} : {this.minutes:00} : {this.seconds:00}";
        }

        #endregion

        private void timerStart_OnClick(object sender, RoutedEventArgs e)
        {
            this.timer.Start();
            this.timer.Tick += Timer_Tick;

            this.buttonResetTimer.IsEnabled = false;
            this.buttonStartTimer.IsEnabled = false;
            this.buttonStopTimer.IsEnabled = true;

            this.parentGrid.Visibility = Visibility.Visible;
            this.textBlockGamePaused.Visibility = Visibility.Collapsed;
        }

        private void timerStop_OnClick(object sender, RoutedEventArgs e)
        {
            this.timer.Stop();
            this.InitializeDispatcherTimerSetup();
            this.buttonStartTimer.IsEnabled = true;
            this.buttonResetTimer.IsEnabled = true;
            this.buttonStopTimer.IsEnabled = false;

            this.parentGrid.Visibility = Visibility.Collapsed;
            this.textBlockGamePaused.Visibility = Visibility.Visible;
        }

        private void Timer_Tick(object sender, object e)
        {
            this.seconds++;
            if (seconds >= 60)
            {
                this.seconds = 0;
                this.minutes++;
            }

            if (minutes >= 60)
            {
                this.minutes = 0;
                this.hours++;
            }

            this.TextBlockPuzzleTimer.Text = $"{this.hours:00} : {this.minutes:00} : {this.seconds:00}";
        }

        private void timerReset_OnClick(object sender, RoutedEventArgs e)
        {
            this.resetTimer();
        }

        private void resetTimerButtonStates()
        {
            this.buttonStartTimer.IsEnabled = true;
            this.buttonStopTimer.IsEnabled = false;
            this.buttonResetTimer.IsEnabled = true;
        }
    }
}