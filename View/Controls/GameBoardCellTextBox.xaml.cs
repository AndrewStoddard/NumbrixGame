using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using NumbrixGame.Annotations;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NumbrixGame.View
{
    public sealed partial class GameBoardCellTextBox : INotifyPropertyChanged
    {
        #region Types and Delegates

        public delegate void OnValueChange(GameBoardCellTextBox gameBoardCellTextBox);

        #endregion

        #region Data members

        public static readonly DependencyProperty NumbrixValueProperty =
            DependencyProperty.Register("NumbrixValue", typeof(object),
                typeof(GameBoardCellTextBox), null);

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
                if (this.Value != value)
                {
                    SetValue(NumbrixValueProperty, value);
                    this.Text = this.Value == null ? string.Empty : this.Value.ToString();
                }
            }
        }

        public string Text
        {
            get => this.text;
            set
            {
                this.text = value;
                this.Value = string.IsNullOrEmpty(this.text) ? (int?) null : int.Parse(this.text);
                this.OnPropertyChanged();
            }
        }

        public bool IsEnabled { get; set; }

        public int MaxValue { get; set; }

        #endregion

        #region Constructors

        public GameBoardCellTextBox()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        public event OnValueChange OnValueChanged;

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

                this.OnValueChanged?.Invoke(this);
            }
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}