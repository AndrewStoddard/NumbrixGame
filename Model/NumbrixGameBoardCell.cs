namespace NumbrixGame.Model
{
    /// <summary>
    ///     Class NumbrixGameBoardCell.
    /// </summary>
    public class NumbrixGameBoardCell
    {
        #region Properties

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

        /// <summary>
        ///     Gets or sets the linear coordinate.
        /// </summary>
        /// <value>The linear coordinate.</value>
        public int LinearCoordinate { get; set; }

        /// <summary>
        ///     Gets or sets the numbrix value.
        /// </summary>
        /// <value>The numbrix value.</value>
        public int? NumbrixValue { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is default value.
        /// </summary>
        /// <value><c>true</c> if this instance is default value; otherwise, <c>false</c>.</value>
        public bool IsDefaultValue { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixGameBoardCell" /> class.
        /// </summary>
        /// <param name="linearCoordinate">The linear coordinate.</param>
        public NumbrixGameBoardCell(int linearCoordinate)
        {
            this.LinearCoordinate = linearCoordinate;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NumbrixGameBoardCell" /> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public NumbrixGameBoardCell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is NumbrixGameBoardCell cell)
            {
                return cell.X == this.X && cell.Y == this.Y;
            }

            return false;
        }

        protected bool Equals(NumbrixGameBoardCell other)
        {
            return this.X == other.X && this.Y == other.Y && this.LinearCoordinate == other.LinearCoordinate &&
                   this.NumbrixValue == other.NumbrixValue && this.IsDefaultValue == other.IsDefaultValue;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.X;
                hashCode = (hashCode * 397) ^ this.Y;
                hashCode = (hashCode * 397) ^ this.LinearCoordinate;
                hashCode = (hashCode * 397) ^ this.NumbrixValue.GetHashCode();
                hashCode = (hashCode * 397) ^ this.IsDefaultValue.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}