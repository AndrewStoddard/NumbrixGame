using System;
using System.Threading.Tasks;
using Windows.Storage;

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
            var dataFileContents = await FileIO.ReadTextAsync(puzzleFile);
            var dataFileLines = dataFileContents.Replace("\r", "").Split("\n");

            for (var i = 0; i < dataFileLines.Length - 1; i++)
            {
                var line = dataFileLines[i];
                if (i == 0)
                {
                    var settings = line.Split(DefaultDelimiter);

                    gameBoard.BoardWidth = string.IsNullOrEmpty(settings[0]) ? -1 : int.Parse(settings[0]);
                    gameBoard.BoardHeight = string.IsNullOrEmpty(settings[1]) ? -1 : int.Parse(settings[1]);
                    gameBoard.CreateBlankGameBoard();
                }
                else
                {
                    var cellInfo = line.Split(DefaultDelimiter);
                    var currentGameBoardCell = gameBoard.FindCell(int.Parse(cellInfo[0]), int.Parse(cellInfo[1]));
                    currentGameBoardCell.DefaultValue = bool.Parse(cellInfo[3]);
                    currentGameBoardCell.NumbrixValue = int.Parse(cellInfo[2]);
                }
            }

            return gameBoard;
        }

        #endregion
    }
}