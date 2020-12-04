using System;
using System.Threading.Tasks;
using Windows.Storage;
using NumbrixGame.Model;

namespace NumbrixGame.Datatier
{
    public class NumbrixScoreBoardReader
    {
        #region Methods

        public static async Task<NumbrixScoreBoard> LoadPuzzle(StorageFile scoreboardFile)
        {
            var dataFileContents = await FileIO.ReadTextAsync(scoreboardFile);
            return loadCsvStringScoreBoard(dataFileContents.Replace("\r", "").Split("\n"));
        }

        private static NumbrixScoreBoard loadCsvStringScoreBoard(string[] stringScoreBoard)
        {
            var scoreBoard = new NumbrixScoreBoard();

            for (var i = 0; i < stringScoreBoard.Length - 1; i++)
            {
                var line = stringScoreBoard[i];

                var scoreInfo = line.Split(',');
                var playerScore = new NumbrixPlayerScore(scoreInfo[DatatierConstants.PlayerNameLocation],
                    TimeSpan.Parse(scoreInfo[DatatierConstants.PlayerScoreGameBoardNumberLocation]),
                    int.Parse(scoreInfo[DatatierConstants.PlayerScoreGameBoardNumberLocation]));
                scoreBoard.AddScore(playerScore);
            }

            return scoreBoard;
        }

        #endregion
    }
}