using MvvmCross.Forms.Converters;
using System;
using System.Globalization;

namespace SimTuning.Forms.UI.Converters
{
    public class TimeSpanToDoubleValueConverter : MvxFormsValueConverter<TimeSpan, double>
    {
        protected override double Convert(TimeSpan value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.TotalSeconds;
        }

        protected override TimeSpan ConvertBack(double value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.FromSeconds(value);
        }
    }
}