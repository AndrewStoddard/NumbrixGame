namespace NumbrixGame.Model
{
    public class NumbrixGameBoardCell
    {
        #region Data members

        private const int TempHeight = 8;
        private const int TempWidth = 8;

        #endregion

        #region Properties

        public int X { get; set; }
        public int Y { get; set; }

        public int LinearCoordinate { get; set; }

        public int? NumbrixValue { get; set; }

        public bool DefaultValue { get; set; }

        #endregion

        #region Constructors

        public NumbrixGameBoardCell(int linearCoordinate)
        {
            this.LinearCoordinate = linearCoordinate;
            var xAndY = this.ConvertLinearToXY(this.LinearCoordinate);
            this.setXandY(xAndY.x, xAndY.y);
        }

        public NumbrixGameBoardCell(int x, int y)
        {
            this.setXandY(x, y);
            this.LinearCoordinate = this.ConvertXYToLinear(x, y);
        }

        #endregion

        #region Methods

        private void setXandY(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        private int ConvertXYToLinear(int x, int y)
        {
            return x + TempWidth * (y - 1);
        }

        private (int x, int y) ConvertLinearToXY(int linearCoordinate)
        {
            var x = linearCoordinate % TempWidth == 0 ? TempWidth : linearCoordinate % TempWidth;
            var y = linearCoordinate / TempHeight + 1;

            return (x, y);
        }

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