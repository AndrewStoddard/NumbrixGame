using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace NumbrixGame.Model
{
    public static class CsvReader
    {
        #region Data members

        private const char DefaultDelimiter = ',';

        #endregion

        #region Methods

        public static async Task<NumbrixGameBoard> LoadPuzzle(StorageFile puzzleFile)
        {
            var gameBoard = new NumbrixGameBoard();
            var buffer = await FileIO.ReadBufferAsync(puzzleFile);

            using (var dataReader = DataReader.FromBuffer(buffer))
            {
                var content = dataReader.ReadString(buffer.Length);
                var data = content.Split(Environment.NewLine);

                var row = 0;
                foreach (var line in data)
                {
                    if (row == 0)
                    {
                        var settings = line.Split(DefaultDelimiter);

                        var widthAsString = settings[0].Trim();
                        var heightAsString = settings[1].Trim();

                        gameBoard.BoardWidth = string.IsNullOrEmpty(widthAsString) ? -1 : int.Parse(widthAsString);
                        gameBoard.BoardHeight = string.IsNullOrEmpty(heightAsString) ? -1 : int.Parse(heightAsString);
                        gameBoard.CreateBlankGameBoard();
                    }
                    else
                    {
                        var cellRow = line.Split(DefaultDelimiter);
                        var currentGameBoardCell = gameBoard.FindCell(int.Parse(cellRow[0]), int.Parse(cellRow[1]));
                        currentGameBoardCell.DefaultValue = bool.Parse(cellRow[3]);
                        currentGameBoardCell.NumbrixValue = int.Parse(cellRow[2]);
                    }

                    row++;
                }
            }

            return gameBoard;
        }

        #endregion
    }
}