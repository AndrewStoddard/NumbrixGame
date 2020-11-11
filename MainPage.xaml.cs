using System.Linq;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NumbrixGame
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Constructors

        public MainPage()
        {
            this.InitializeComponent();
            this.createGameBoard(TempWidth, TempHeight);
        }

        #endregion

        #region Methods

        private void createGameBoard(int width, int height)
        {
            var parentStackPanel = new StackPanel();
            parentStackPanel.BorderBrush = new SolidColorBrush(Colors.Blue);
            parentStackPanel.BorderThickness = new Thickness(2);
            this.parentGrid.VerticalAlignment = VerticalAlignment.Center;
            this.parentGrid.HorizontalAlignment = HorizontalAlignment.Center;

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

        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (e.Key == VirtualKey.Left)
            {
            }
            else if (e.Key == VirtualKey.Right)
            {
            }
        }

        private TextBox createCell()
        {
            var newTextBox = new TextBox();
            newTextBox.Width = 50;
            newTextBox.Height = 50;
            newTextBox.FontSize = 24;
            newTextBox.TextAlignment = TextAlignment.Center;
            newTextBox.BeforeTextChanging += this.NewCell_BeforeTextChanging;
            newTextBox.GotFocus += this.NewTextBoxOnGotFocus;
            newTextBox.SelectionHighlightColor = new SolidColorBrush(Colors.Transparent);
            newTextBox.KeyDown += this.OnKeyDown;
            return newTextBox;
        }

        private void NewTextBoxOnGotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;

            textBox.SelectAll();
        }

        private void NewCell_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.NewText))
            {
                args.Cancel = args.NewText.Any(c => !char.IsDigit(c)) ||
                              int.Parse(args.NewText) > TempHeight * TempWidth || args.NewText.StartsWith("0");
            }
        }

        #endregion

        #region Constants

        private const int TempWidth = 9;
        private const int TempHeight = 9;

        #endregion
    }
}