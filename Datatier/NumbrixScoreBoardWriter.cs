using System;
using Windows.Storage;
using NumbrixGame.Model;

namespace NumbrixGame.Datatier
{
    /// <summary>
    ///     Class NumbrixScoreBoardWriter.
    /// </summary>
    public class NumbrixScoreBoardWriter
    {
        #region Methods

        /// <summary>
        ///     Writes the score board.
        /// </summary>
        /// <param name="numbrixScoreBoard">The numbrix score board.</param>
        /// <param name="fileName">Name of the file.</param>
        public static async void WriteScoreBoard(NumbrixScoreBoard numbrixScoreBoard, string fileName)
        {
            await FileIO.WriteTextAsync(
                await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting), numbrixScoreBoard.AsCsv());
        }

        /// <summary>
        ///     Resets the scoreboard.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public static async void ResetScoreboard(string fileName)
        {
            await FileIO.WriteTextAsync(
                await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting), "");
        }

        #endregion
    }
}