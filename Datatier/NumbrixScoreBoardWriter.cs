using System;
using Windows.Storage;
using NumbrixGame.Model;

namespace NumbrixGame.Datatier
{
    public class NumbrixScoreBoardWriter
    {
        #region Methods

        public static async void WriteScoreBoard(NumbrixScoreBoard numbrixScoreBoard, string fileName)
        {
            await FileIO.WriteTextAsync(
                await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting), numbrixScoreBoard.AsCSV());
        }

        public static async void ResetScoreboard(string fileName)
        {
            await FileIO.WriteTextAsync(
                await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName,
                    CreationCollisionOption.ReplaceExisting), "");
        }

        #endregion
    }
}