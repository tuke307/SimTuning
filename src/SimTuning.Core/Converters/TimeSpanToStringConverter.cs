// Copyright (c) 2021 tuke productions. All rights reserved.
using MvvmCross.Converters;
using System;
using System.Globalization;

namespace SimTuning.Core.Converters
{
    public class TimeSpanToStringConverter : MvxValueConverter<TimeSpan, string>
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