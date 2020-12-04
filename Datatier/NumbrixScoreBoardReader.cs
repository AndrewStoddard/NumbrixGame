using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using NumbrixGame.Model;

namespace NumbrixGame.Datatier
{
    /// <summary>
    ///     Class NumbrixScoreBoardReader.
    /// </summary>
    public class NumbrixScoreBoardReader
    {
        #region Methods

        /// <summary>
        ///     Loads the score board.
        /// </summary>
        /// <param name="scoreboardFile">The scoreboard file.</param>
        /// <returns>NumbrixScoreBoard.</returns>
        public static async Task<NumbrixScoreBoard> LoadScoreBoard(StorageFile scoreboardFile)
        {
            var dataFileContents = await FileIO.ReadTextAsync(scoreboardFile);
            return loadCsvStringScoreBoard(dataFileContents.Replace("\r", "").Split("\n"));
        }

        /// <summary>
        ///     Loads the CSV string score board.
        /// </summary>
        /// <param name="stringScoreBoard">The string score board.</param>
        /// <returns>NumbrixScoreBoard.</returns>
        private static NumbrixScoreBoard loadCsvStringScoreBoard(IReadOnlyList<string> stringScoreBoard)
        {
            var scoreBoard = new NumbrixScoreBoard();

            for (var i = 0; i < stringScoreBoard.Count - 1; i++)
            {
                var line = stringScoreBoard[i];

                var scoreInfo = line.Split(',');
                var playerScore = new NumbrixPlayerScore(scoreInfo[DatatierConstants.PlayerNameLocation],
                    TimeSpan.Parse(scoreInfo[DatatierConstants.PlayerScoreTimeTakenLocation]),
                    int.Parse(scoreInfo[DatatierConstants.PlayerScoreGameBoardNumberLocation]));
                scoreBoard.AddScore(playerScore);
            }

            return scoreBoard;
        }

        #endregion
    }
}