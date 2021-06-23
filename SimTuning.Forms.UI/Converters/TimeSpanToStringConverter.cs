// project=SimTuning.Forms.UI, file=TimeSpanToStringConverter.cs, creation=2020:12:14
// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Forms.Converters;
using System;
using System.Globalization;

namespace SimTuning.Forms.UI.Converters
{
    public class TimeSpanToStringConverter : MvxFormsValueConverter<TimeSpan, string>
    {
        private const string DEFAULT_FORMAT = @"mm\:ss";

        protected override string Convert(TimeSpan value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is string format))
            {
                format = DEFAULT_FORMAT;
            }
            return value.ToString(format);
        }
    }
}