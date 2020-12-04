using System.ComponentModel;
using System.Runtime.CompilerServices;
using NumbrixGame.Model;
using NumbrixGame.Properties;

namespace NumbrixGame.ViewModel
{
    /// <summary>
    ///     Class NumbrixGameBoardCellViewModel.
    ///     Implements the <see cref="System.ComponentModel.INotifyPropertyChanged" />
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class NumbrixGameBoardCellViewModel : INotifyPropertyChanged
    {
        #region Types and Delegates

        /// <summary>
        ///     Delegate NumbrixValueChanged
        /// </summary>
        public delegate void NumbrixValueChanged();

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        /// <value>The x.</value>
        public int X
        {
            get => this.Model.X;
            set
            {
                this.Model.X = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        /// <value>The y.</value>
        public int Y
        {
            get => this.Model.Y;
            set
            {
                this.Model.Y = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the numbrix value.
        /// </summary>
        /// <value>The numbrix value.</value>
        public int? NumbrixValue
        {
            get => this.Model.NumbrixValue;
            set
            {
                this.Model.NumbrixValue = value;
                this.OnPropertyChanged();
                this.OnNumbrixValueChanged?.Invoke();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is default value.
        /// </summary>
        /// <value><c>true</c> if this instance is default value; otherwise, <c>false</c>.</value>
        public bool IsDefaultValue
        {
            get => this.Model.IsDefaultValue;
            set
            {
                this.Model.IsDefaultValue = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</value>
        public bool IsEnabled => !this.Model.IsDefaultValue;

        /// <summary>
        ///     Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public NumbrixGameBoardCell Model { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixGameBoardCellViewModel" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public NumbrixGameBoardCellViewModel(int x, int y)
        {
            this.Model = new NumbrixGameBoardCell(x, y);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixGameBoardCellViewModel" /> class.
        /// </summary>
        /// <param name="gameBoardCell">The game board cell.</param>
        public NumbrixGameBoardCellViewModel(NumbrixGameBoardCell gameBoardCell)
        {
            this.Model = gameBoardCell;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        /// <returns></returns>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Occurs when [on numbrix value changed].
        /// </summary>
        public event NumbrixValueChanged OnNumbrixValueChanged;

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}