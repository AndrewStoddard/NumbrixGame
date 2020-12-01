using System;
using Windows.Storage;
using NumbrixGame.Model;

namespace NumbrixGame.Datatier
{
    public class NumbrixGameBoardWriter
    {
        #region Methods

        public static async void WriteGameboard(NumbrixGameBoard numbrixGameBoard, string fileName)
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var saveFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(saveFile, numbrixGameBoard.AsCSV());
        }

        #endregion
    }
}