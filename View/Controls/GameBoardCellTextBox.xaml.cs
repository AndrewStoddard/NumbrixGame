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
        #region Properties

        public int MaxValue { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        #endregion

        #region Constructors

        public GameBoardCellTextBox()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

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
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}