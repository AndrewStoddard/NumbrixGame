using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage.Pickers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        private readonly IList<TextBox> cells;
        private readonly NumbrixGameBoardViewModel numbrixGameBoardViewModel;

        #endregion

        #region Constructors

        public NumbrixGameBoardPage()
        {
            this.InitializeComponent();
            this.numbrixGameBoardViewModel = new NumbrixGameBoardViewModel();
            this.cells = this.createGridGameBoard(this.TempWidth, this.TempHeight);
        }

        #endregion

        #region Methods

        private IList<TextBox> createGridGameBoard(int width, int height)
        {
            var cells = new List<TextBox>();
            for (var i = 1; i <= height; i++)
            {
                for (var j = 1; j <= width; j++)
                {
                    var cell = this.createCell();

                    cell.Text = (j + width * (i - 1)).ToString();
                    cell.Tag = this.numbrixGameBoardViewModel.CreateCell(j, i, j + width * (i - 1));

                    cells.Add(cell);
                }
            }

            return cells;
        }

        private void createGameBoard(int width, int height)
        {
            var parentStackPanel = new StackPanel();
            parentStackPanel.BorderBrush = new SolidColorBrush(Colors.Blue);
            parentStackPanel.BorderThickness = new Thickness(2);

            this.parentGrid.Children.Add(parentStackPanel);

            for (var i = 1; i <= height; i++)
            {
                var stackPanel = new StackPanel();

                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Width = this.parentGrid.Width;
                stackPanel.Spacing = 0;
                stackPanel.BorderBrush = new SolidColorBrush(Colors.Blue);
                stackPanel.BorderThickness = new Thickness(2);
                stackPanel.Padding = new Thickness(0);
                parentStackPanel.Children.Add(stackPanel);

                for (var j = 1; j <= width; j++)
                {
                    var cell = this.createCell();

                    cell.Text = (j + width * (i - 1)).ToString();

                    stackPanel.Children.Add(cell);
                }
            }
        }

        private TextBox createCell()
        {
            var newTextBox = new TextBox();
            newTextBox.Width = 50;
            newTextBox.BorderBrush = new SolidColorBrush(Colors.Transparent);
            newTextBox.BorderThickness = new Thickness(0);
            newTextBox.Height = 50;
            newTextBox.FontSize = 24;
            newTextBox.TextAlignment = TextAlignment.Center;
            newTextBox.BeforeTextChanging += this.NewCell_BeforeTextChanging;
            newTextBox.GotFocus += this.NewTextBoxOnGotFocus;
            newTextBox.TextChanged += this.NewTextBox_TextChanged;
            return newTextBox;
        }

        private void NewTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox ?? throw new NullReferenceException();

            var cell = textbox.Tag as NumbrixGameBoardCell ?? throw new NullReferenceException();
            cell.NumbrixValue = int.Parse(textbox.Text);
        }

        private void NewTextBoxOnGotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox ?? throw new NullReferenceException();

            textBox.SelectAll();
        }

        private void NewCell_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.NewText))
            {
                args.Cancel = args.NewText.Any(c => !char.IsDigit(c)) ||
                              int.Parse(args.NewText) > this.TempHeight * this.TempWidth ||
                              args.NewText.StartsWith("0");
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.Desktop
            };

            filePicker.FileTypeFilter.Add(".csv");

            var puzzleFile = await filePicker.PickSingleFileAsync();

            var reader = new CsvReader();
            reader.LoadPuzzle(puzzleFile);
        }

        #endregion

        #region Constants

        private readonly int TempWidth = 9;
        private readonly int TempHeight = 9;

        #endregion
    }
}