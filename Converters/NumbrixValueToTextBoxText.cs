using System;
using Windows.UI.Xaml.Data;

namespace NumbrixGame.Converters
{
    public class NumbrixValueToTextBoxText : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (int?) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (int?) value;
        }

        #endregion
    }
}