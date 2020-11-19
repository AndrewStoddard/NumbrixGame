using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
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

        #endregion

        #region Constructors

        public NumbrixGameBoardPage()
        {
            this.InitializeComponent();
            this.numbrixGameBoardViewModel = new NumbrixGameBoardViewModel();

            this.createGameBoard();
            this.solutionCheckMessage.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Methods

        private void createGameBoard()
        {
            var children = this.parentGrid.Children;
            foreach (var currentChild in children)
            {
                this.parentGrid.Children.Remove(currentChild);
            }
            var parentStackPanel = new StackPanel();
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
                        var cell = this.createCell(gameBoardCell.X, gameBoardCell.Y, gameBoardCell.NumbrixValue,
                            gameBoardCell.DefaultValue);
                        stackPanel.Children.Add(cell);
                    }
                }
            }
        }

        private GameBoardCellTextBox createCell(int x, int y, int? value = null, bool isDefault = false)
        {
            var newTextBox = new GameBoardCellTextBox();
            newTextBox.MaxValue = this.numbrixGameBoardViewModel.BoardHeight *
                                  this.numbrixGameBoardViewModel.BoardWidth;
            newTextBox.X = x;
            newTextBox.Y = y;
            if (value != null)
            {
                newTextBox.Text = value.ToString();
            }

            newTextBox.IsEnabled = !isDefault;
            newTextBox.SetBinding(GameBoardCellTextBox.NumbrixValueProperty,
                new Binding {
                    Source = this.numbrixGameBoardViewModel.FindCell(x, y).NumbrixValue,
                    Mode = BindingMode.TwoWay
                });
            newTextBox.OnValueChanged += this.OnValueInTextBoxChange;

            return newTextBox;
        }

        private void OnValueInTextBoxChange(GameBoardCellTextBox gameBoardCellTextBox)
        {
            this.numbrixGameBoardViewModel.UpdateCell(gameBoardCellTextBox.X, gameBoardCellTextBox.Y,
                gameBoardCellTextBox.Value);
        }

        private async void loadGameBoard(object sender, RoutedEventArgs e)
        {
            var file = await pickFile();
            if (file != null)
            {
                await this.numbrixGameBoardViewModel.LoadGameBoard(file);
                this.createGameBoard();
            }
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
            var result = this.numbrixGameBoardViewModel.CheckSolution();
            this.displaySolutionResult(result);
            Debug.WriteLine(result);
        }

        private void displaySolutionResult(bool solved)
        {
            var message = "Your solution is incorrect!";
            if (solved)
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

        #endregion

        #region Constants

        #endregion

        private void NextPuzzle_OnClick(object sender, RoutedEventArgs e)
        {
            this.solutionCheckMessage.Visibility = Visibility.Collapsed;

            this.numbrixGameBoardViewModel.NextPuzzle();
            this.createGameBoard();
        }
    }
}