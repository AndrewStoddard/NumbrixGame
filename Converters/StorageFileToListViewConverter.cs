using System;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace NumbrixGame.Converters
{
    public class StorageFileToListViewConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var file = value as StorageFile ?? throw new NullReferenceException();
            return file.DisplayName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var filename = value as string ?? throw new NullReferenceException();
            return ApplicationData.Current.LocalFolder.GetFileAsync(filename);
        }

        #endregion
    }
}