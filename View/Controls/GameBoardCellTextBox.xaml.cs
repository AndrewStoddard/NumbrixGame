using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using NumbrixGame.Properties;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace NumbrixGame.View.Controls
{
    /// <summary>
    ///     Class GameBoardCellTextBox. This class cannot be inherited.
    ///     Implements the <see cref="Windows.UI.Xaml.Controls.UserControl" />
    ///     Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    ///     Implements the <see cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    ///     Implements the <see cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class GameBoardCellTextBox : INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        public int MaxValue { get; set; }

        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public int X { get; set; }

        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public int Y { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameBoardCellTextBox" /> class.
        /// </summary>
        public GameBoardCellTextBox()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Handles the <see cref="E:Focused" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        /// <exception cref="NullReferenceException"></exception>
        private void OnFocused(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox ?? throw new NullReferenceException();
            textBox.SelectAll();
        }

        /// <summary>
        ///     Handles the <see cref="E:BeforeTextChanges" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="TextBoxBeforeTextChangingEventArgs" /> instance containing the event data.</param>
        private void OnBeforeTextChanges(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.NewText))
            {
                args.Cancel = args.NewText.Any(c => !char.IsDigit(c)) ||
                              int.Parse(args.NewText) > this.MaxValue ||
                              args.NewText.StartsWith("0");
            }
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}