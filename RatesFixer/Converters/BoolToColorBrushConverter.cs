using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RatesFixer.Converters
{
    [ValueConversion(typeof(bool), typeof(SolidColorBrush))]
    class BoolToColorBrushConverter : IValueConverter
    {
        #region Implementation of IValueConverter
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush color;
            var colorIfTrue = Colors.Transparent;
            var colorIfFalse = Colors.Pink;
            double opacity = 1;

            if ((bool)value)
            {
                color = new SolidColorBrush(colorIfTrue);
                color.Opacity = opacity;
            }
            else
            {
                color = new SolidColorBrush(colorIfFalse);
                color.Opacity = opacity;
            }
            return color;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
