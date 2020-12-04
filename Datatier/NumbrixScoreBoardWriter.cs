using System;
using Windows.Storage;
using NumbrixGame.Model;

namespace NumbrixGame.Datatier
{
    public class NumbrixScoreBoardWriter
    {
        #region Methods

        public static async void WriteGameboard(NumbrixScoreBoard numbrixScoreBoard, string fileName)
        {
            await FileIO.WriteTextAsync(
                await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.GenerateUniqueName), numbrixScoreBoard.AsCSV());
        }

        #endregion
    }
}