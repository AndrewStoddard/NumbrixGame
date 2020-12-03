using System;
using System.Threading.Tasks;
using Windows.Storage;
using NumbrixGame.Model;

namespace NumbrixGame.Datatier
{
    public static class NumbrixGameBoardReader
    {
        #region Data members

        private const char DefaultDelimiter = ',';

        #endregion

        #region Methods

        public static async Task<NumbrixGameBoard> LoadPuzzle(StorageFile puzzleFile)
        {
            var dataFileContents = await FileIO.ReadTextAsync(puzzleFile);
            return loadCsvStringGameBoard(dataFileContents.Replace("\r", "").Split("\n"));
        }

        public static NumbrixGameBoard LoadPuzzle(string gameBoardString)
        {
            return loadCsvStringGameBoard(gameBoardString.Replace("\r", "").Split("\n"));
        }

        private static NumbrixGameBoard loadCsvStringGameBoard(string[] stringGameBoard)
        {
            var gameBoard = new NumbrixGameBoard();

            for (var i = 0; i < stringGameBoard.Length - 1; i++)
            {
                var line = stringGameBoard[i];
                if (i == 0)
                {
                    var settings = line.Split(DefaultDelimiter);
                    gameBoard.GameBoardNumber =
                        string.IsNullOrEmpty(settings[DatatierConstants.GameBoardNumberSettingLocation])
                            ? -1
                            : int.Parse(settings[DatatierConstants.GameBoardNumberSettingLocation]);
                    gameBoard.BoardWidth = string.IsNullOrEmpty(settings[DatatierConstants.WidthSettingLoaction])
                        ? -1
                        : int.Parse(settings[DatatierConstants.WidthSettingLoaction]);
                    gameBoard.BoardHeight = string.IsNullOrEmpty(settings[DatatierConstants.HeightSettingLocation])
                        ? -1
                        : int.Parse(settings[DatatierConstants.HeightSettingLocation]);
                    gameBoard.TimeTaken = string.IsNullOrEmpty(settings[DatatierConstants.TimeSettingLocation])
                        ? TimeSpan.Zero
                        : TimeSpan.Parse(settings[DatatierConstants.TimeSettingLocation]);
                    gameBoard.CreateBlankGameBoard();
                }
                else
                {
                    var cellInfo = line.Split(',');
                    var currentGameBoardCell = gameBoard.FindCell(int.Parse(cellInfo[DatatierConstants.XLocation]),
                        int.Parse(cellInfo[DatatierConstants.YLocation]));
                    currentGameBoardCell.IsDefaultValue =
                        bool.Parse(cellInfo[DatatierConstants.IsDefaultValueLocation]);
                    currentGameBoardCell.NumbrixValue =
                        string.IsNullOrEmpty(cellInfo[DatatierConstants.NumbrixValueLocation])
                            ? (int?) null
                            : int.Parse(cellInfo[DatatierConstants.NumbrixValueLocation]);
                }
            }

            return gameBoard;
        }

        #endregion
    }
}