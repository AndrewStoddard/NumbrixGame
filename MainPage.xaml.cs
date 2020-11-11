using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NumbrixGame
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Constants

        private const int DefaultWidth = 9;
        private const int DefaultHeight = 9;

        #endregion

        #region Constructors

        public MainPage()
        {
            this.InitializeComponent();
            this.createGameBoard(DefaultWidth, DefaultHeight);
        }

        #endregion

        #region Methods

        private void createGameBoard(int width, int height)
        {
            var parentStackPanel = new StackPanel();
            this.parentGrid.VerticalAlignment = VerticalAlignment.Center;
            this.parentGrid.HorizontalAlignment = HorizontalAlignment.Center;

            this.parentGrid.Children.Add(parentStackPanel);
            var counter = 1;
            for (var i = 1; i <= height; i++)
            {
                var stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                stackPanel.Width = this.parentGrid.Width;
                parentStackPanel.Children.Add(stackPanel);

                for (var j = 1; j <= width; j++)
                {
                    var cell = this.createCell();

                    cell.Text = counter.ToString();

                    stackPanel.Children.Add(cell);
                    counter++;
                }
            }
        }

        private TextBox createCell()
        {
            var newTextBox = new TextBox();
            newTextBox.MaxLength = 2;
            newTextBox.Width = 50;
            newTextBox.Height = 50;
            newTextBox.FontSize = 24;
            newTextBox.TextAlignment = TextAlignment.Center;
            newTextBox.BeforeTextChanging += NewCell_BeforeTextChanging;
            return newTextBox;
        }

        private void NewCell_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        #endregion
    }
}