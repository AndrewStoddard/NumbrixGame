using System;
using Windows.UI.Xaml.Data;

namespace NumbrixGame.Converters
{
    public class TimeSpanToTextConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? string.Empty : value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return TimeSpan.Parse((string) value);
        }

        #endregion
    }
}