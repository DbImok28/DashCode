using System;
using System.Windows.Data;

namespace DashCode.Infrastructure.Converter
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBoolConverter : BaseConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(bool))
                return !(bool)value;
            else throw new InvalidOperationException("The target must be a boolean");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType == typeof(bool))
                return !(bool)value;
            else throw new InvalidOperationException("The target must be a boolean");
        }
    }
}
