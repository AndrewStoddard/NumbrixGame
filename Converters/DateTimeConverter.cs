using System;
using Windows.UI.Xaml.Data;

namespace NumbrixGame.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        #region Methods

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var d = value as DateTimeOffset?;
            if (d == null)
            {
                return value;
            }

            return d.Value.DateTime.ToShortDateString() + " " + d.Value.DateTime.ToShortTimeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}