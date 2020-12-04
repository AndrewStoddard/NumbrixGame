using System;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace NumbrixGame.Converters
{
    /// <summary>
    ///     Class StorageFileToListViewConverter.
    ///     Implements the <see cref="Windows.UI.Xaml.Data.IValueConverter" />
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class StorageFileToListViewConverter : IValueConverter
    {
        #region Methods

        /// <summary>
        ///     Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var file = value as StorageFile ?? throw new NullReferenceException();
            return file.DisplayName;
        }

        /// <summary>
        ///     Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var filename = value as string ?? throw new NullReferenceException();
            return ApplicationData.Current.LocalFolder.GetFileAsync(filename);
        }

        #endregion
    }
}