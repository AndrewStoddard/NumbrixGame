using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace NumbrixGame.Converters
{
    public class NegatedBooleanToVisibility : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var booleanValue = value as bool?;
            return booleanValue != null && !booleanValue.Value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}