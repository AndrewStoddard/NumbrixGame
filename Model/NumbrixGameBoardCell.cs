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

        public int NumbrixValue { get; set; }

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

        #endregion
    }
}