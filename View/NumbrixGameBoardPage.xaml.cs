using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        private IList<GameBoardCellTextBox> cells;
        private readonly NumbrixGameBoardViewModel numbrixGameBoardViewModel;

        #endregion

        #region Constructors

        public NumbrixGameBoardPage()
        {
            this.InitializeComponent();
            this.numbrixGameBoardViewModel = new NumbrixGameBoardViewModel();
            this.createGameBoard();
        }

        #endregion

        #region Methods

        private IList<GameBoardCellTextBox> createGridGameBoard()
        {
            var cells = new List<GameBoardCellTextBox>();

            return cells;
        }

        private void createGameBoard()
        {
            var parentStackPanel = new StackPanel();
            parentStackPanel.BorderBrush = new SolidColorBrush(Colors.Blue);
            parentStackPanel.BorderThickness = new Thickness(2);
            parentStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            parentStackPanel.VerticalAlignment = VerticalAlignment.Center;

            this.parentGrid.Children.Add(parentStackPanel);

            for (var i = 1; i <= this.numbrixGameBoardViewModel.NumbrixGameBoard.BoardHeight; i++)
            {
                var stackPanel = new StackPanel();

                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Width = this.parentGrid.Width;
                stackPanel.Spacing = 0;
                stackPanel.BorderBrush = new SolidColorBrush(Colors.Blue);
                stackPanel.BorderThickness = new Thickness(2);
                stackPanel.Padding = new Thickness(0);
                parentStackPanel.Children.Add(stackPanel);

                foreach (var gameBoardCell in this.numbrixGameBoardViewModel.NumbrixGameBoard.NumbrixGameBoardCells)
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
            newTextBox.MaxValue = this.numbrixGameBoardViewModel.NumbrixGameBoard.BoardHeight *
                                  this.numbrixGameBoardViewModel.NumbrixGameBoard.BoardWidth;
            newTextBox.X = x;
            newTextBox.Y = y;
            if (value != null)
            {
                newTextBox.Text = value.ToString();
            }

            newTextBox.IsEnabled = !isDefault;
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
            Debug.WriteLine(result);
        }

        #endregion

        #region Constants

        private readonly int TempWidth = 9;
        private readonly int TempHeight = 9;

        #endregion
    }
}