using System;

namespace NumbrixGame.Model
{
    public class NumbrixCell
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

        public NumbrixCell(int linearCoordinate)
        {
            this.LinearCoordinate = linearCoordinate;
        }

        public NumbrixCell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        #endregion

        #region Methods

        private int ConvertXYToLinear(int x, int y)
        {
            var linearCoordinate = 0;
            throw new NotImplementedException();
        }

        private (int x, int y) ConvertLinearToXY(int linearCoordinate)
        {
            var x = -1;
            var y = -1;
            for (var i = 1; i <= TempWidth; i++)
            {
                if (linearCoordinate % TempWidth + 1 == i)
                {
                    x = TempWidth - i;
                }
            }

            for (var i = 0; i < TempHeight; i++)
            {
                if (linearCoordinate / TempHeight + 1 == i)
                {
                    y = TempHeight - i;
                }
            }

            return (x, y);
        }

        #endregion
    }
}