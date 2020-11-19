using System;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NumbrixGame.View
{
    public sealed partial class GameBoardCellTextBox
    {
        #region Types and Delegates

        public delegate void OnValueChange(GameBoardCellTextBox gameBoardCellTextBox);

        #endregion

        #region Data members

        public static readonly DependencyProperty NumbrixValueProperty =
            DependencyProperty.Register("NumbrixValue", typeof(int?),
                typeof(GameBoardCellTextBox), null);

        private bool isEnabled;
        private string text;

        #endregion

        #region Properties

        public int X { get; set; }
        public int Y { get; set; }

        public int? Value
        {
            get => (int?) GetValue(NumbrixValueProperty);
            set
            {
                SetValue(NumbrixValueProperty, value);

                this.text = value == null ? string.Empty : value.ToString();
            }
        }

        public string Text
        {
            get => this.text;
            set
            {
                this.text = value;
                this.textBox.Text = value;
            }
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set
            {
                this.isEnabled = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.isEnabled)));
            }
        }

        public int MaxValue { get; set; }

        #endregion

        #region Constructors

        public GameBoardCellTextBox()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        public event OnValueChange OnValueChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnFocused(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox ?? throw new NullReferenceException();
            textBox.SelectAll();
        }

        private void OnBeforeTextChanges(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.NewText))
            {
                args.Cancel = args.NewText.Any(c => !char.IsDigit(c)) ||
                              int.Parse(args.NewText) > this.MaxValue ||
                              args.NewText.StartsWith("0");
            }

            if (!args.Cancel)
            {
                this.Text = args.NewText;
                this.Value = string.IsNullOrEmpty(args.NewText) ? (int?) null : int.Parse(args.NewText);
                this.OnValueChanged?.Invoke(this);
            }
        }

        #endregion
    }
}