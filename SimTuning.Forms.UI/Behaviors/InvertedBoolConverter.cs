// project=SimTuning.Forms.UI, file=InvertedBoolConverter.cs, creation=2020:8:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimTuning.Forms.UI.Behaviors
{
    public class InvertedBoolConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}