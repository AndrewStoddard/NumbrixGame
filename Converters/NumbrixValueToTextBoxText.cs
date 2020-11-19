using System;
using Windows.UI.Xaml.Data;

namespace NumbrixGame.Converters
{
    public class NumbrixValueToTextBoxText : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? string.Empty : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return string.IsNullOrEmpty(value.ToString()) ? (int?) null : int.Parse(value.ToString());
        }

        #endregion
    }
}