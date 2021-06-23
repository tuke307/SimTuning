// project=SimTuning.Forms.UI, file=TimeSpanToDoubleValueConverter.cs, creation=2020:12:14
// Copyright (c) 2021 tuke productions. All rights reserved.
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