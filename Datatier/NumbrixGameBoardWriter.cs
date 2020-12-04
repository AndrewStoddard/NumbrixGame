using System;
using Windows.Storage;
using Microsoft.Toolkit.Uwp.Helpers;
using NumbrixGame.Model;

namespace NumbrixGame.Datatier
{
    /// <summary>
    ///     Class NumbrixGameBoardWriter.
    /// </summary>
    public class NumbrixGameBoardWriter
    {
        #region Methods

        /// <summary>
        ///     Writes the gameboard.
        /// </summary>
        /// <param name="numbrixGameBoard">The numbrix game board.</param>
        /// <param name="fileName">Name of the file.</param>
        public static async void WriteGameboard(NumbrixGameBoard numbrixGameBoard, string fileName)
        {
            await FileIO.WriteTextAsync(
                await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.GenerateUniqueName), numbrixGameBoard.AsCSV());
        }

        /// <summary>
        ///     Files the exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool FileExists(string fileName)
        {
            return ApplicationData.Current.LocalFolder.FileExistsAsync(fileName).Result;
        }

        #endregion
    }
}