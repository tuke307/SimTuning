// project=SimTuning.Forms.WPFCore, file=CombiningConverter.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using System;
using System.Windows.Data;

namespace SimTuning.Forms.WPFCore.Business
{
    public class CombiningConverter : IValueConverter
    {
        public IValueConverter Converter1 { get; set; }
        public IValueConverter Converter2 { get; set; }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object convertedValue = Converter1.Convert(value, targetType, parameter, culture);
            return Converter2.Convert(convertedValue, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion IValueConverter Members
    }
}