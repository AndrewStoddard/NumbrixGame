namespace NumbrixGame.Model
{

    public class NumbrixGameBoardCell
    {
        #region Properties

        public int X { get; set; }
        public int Y { get; set; }

        public int LinearCoordinate { get; set; }

        public int? NumbrixValue { get; set; }

        public bool IsDefaultValue { get; set; }

        #endregion

        #region Constructors

        public NumbrixGameBoardCell(int linearCoordinate)
        {
            this.LinearCoordinate = linearCoordinate;
        }

        public NumbrixGameBoardCell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            if (obj is NumbrixGameBoardCell cell)
            {
                return cell.X == this.X && cell.Y == this.Y;
            }

            return false;
        }

        #endregion
    }
}