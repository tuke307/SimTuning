// project=SimTuning.Forms.UI, file=NegateBooleanConverter.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using System;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.Behaviors
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