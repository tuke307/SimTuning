// project=SimTuning.WPF.UI, file=NegateBooleanConverter.cs, creation=2020:9:2 Copyright
// (c) 2020 tuke productions. All rights reserved.
using System;
using System.Windows.Data;

namespace SimTuning.WPF.UI.Business
{
    public class NegateBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}