using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using NumbrixGame.Model;
using NumbrixGame.PrebuiltGames;

namespace NumbrixGame.Datatier
{
    /// <summary>
    ///     Class NumbrixGameBoardReader.
    /// </summary>
    public static class NumbrixGameBoardReader
    {
        #region Methods

        /// <summary>
        ///     Loads the puzzle.
        /// </summary>
        /// <param name="puzzleFile">The puzzle file.</param>
        /// <returns>NumbrixGameBoard.</returns>
        public static async Task<NumbrixGameBoard> LoadPuzzle(StorageFile puzzleFile)
        {
            var dataFileContents = await FileIO.ReadTextAsync(puzzleFile);
            return loadCsvStringGameBoard(dataFileContents.Replace("\r", "").Split("\n"));
        }

        /// <summary>
        ///     Loads the puzzle.
        /// </summary>
        /// <param name="gameBoardString">The game board string.</param>
        /// <returns>NumbrixGameBoard.</returns>
        public static NumbrixGameBoard LoadPuzzle(string gameBoardString)
        {
            return loadCsvStringGameBoard(gameBoardString.Replace("\r", "").Split("\n"));
        }

        /// <summary>
        ///     Loads the CSV string game board.
        /// </summary>
        /// <param name="stringGameBoard">The string game board.</param>
        /// <returns>NumbrixGameBoard.</returns>
        private static NumbrixGameBoard loadCsvStringGameBoard(IReadOnlyList<string> stringGameBoard)
        {
            var gameBoard = new NumbrixGameBoard();

            for (var i = 0; i < stringGameBoard.Count - 1; i++)
            {
                var line = stringGameBoard[i];
                if (i == 0)
                {
                    var settings = line.Split(DatatierConstants.DefaultDelimiter);
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
                    var cellInfo = line.Split(DatatierConstants.DefaultDelimiter);
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

        /// <summary>
        ///     Gets the saved games.
        /// </summary>
        /// <returns>List&lt;StorageFile&gt;.</returns>
        public static async Task<List<StorageFile>> GetSavedGames()
        {
            var files = await ApplicationData.Current.LocalFolder.GetFilesAsync();
            return files.Where(file => file.Name.StartsWith("save_")).ToList();
        }

        /// <summary>
        ///     Gets the prebuilt games.
        /// </summary>
        /// <returns>List&lt;StorageFile&gt;.</returns>
        public static async Task<List<StorageFile>> GetPrebuiltGames()
        {
            const string prebuiltSuffix = "puzzle_";
            for (var i = 1; i <= MainPuzzles.PuzzleList.Count; i++)
            {
                var filename = prebuiltSuffix + i + ".csv";
                if (!NumbrixGameBoardWriter.FileExists(filename))
                {
                    NumbrixGameBoardWriter.WriteGameboard(
                        LoadPuzzle(MainPuzzles.PuzzleList?[i - 1]), filename);
                }
            }

            var files = await ApplicationData.Current.LocalFolder.GetFilesAsync();
            return files.Where(file => file.Name.StartsWith("puzzle_")).ToList();
        }

        #endregion
    }
}