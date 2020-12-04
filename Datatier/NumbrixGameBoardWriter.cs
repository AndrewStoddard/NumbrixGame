using System;
using Windows.Storage;
using Microsoft.Toolkit.Uwp.Helpers;
using NumbrixGame.Model;

namespace NumbrixGame.Datatier
{
    public class NumbrixGameBoardWriter
    {
        #region Methods

        public static async void WriteGameboard(NumbrixGameBoard numbrixGameBoard, string fileName)
        {
            await FileIO.WriteTextAsync(
                await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.GenerateUniqueName), numbrixGameBoard.AsCSV());
        }

        public static bool FileExists(string fileName)
        {
            return ApplicationData.Current.LocalFolder.FileExistsAsync(fileName).Result;
        }

        #endregion
    }
}