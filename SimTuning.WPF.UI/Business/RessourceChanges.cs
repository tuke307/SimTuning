// project=SimTuning.WPF.UI, file=RessourceChanges.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SimTuning.WPF.UI.Business
{
    /// <summary>
    /// RessourceChanges.
    /// </summary>
    public class RessourceChanges
    {
        /// <summary>
        /// Colorses the specified primary color.
        /// </summary>
        /// <param name="primaryColor">Color of the primary.</param>
        /// <param name="secondaryColor">Color of the secondary.</param>
        /// <param name="baseTheme">The base theme.</param>
        public void Colors(MaterialDesignColors.PrimaryColor? primaryColor = null, MaterialDesignColors.SecondaryColor? secondaryColor = null, MaterialDesignThemes.Wpf.BaseTheme? baseTheme = null)
        {
            int position = 0;
            Uri changes = null;

            if (primaryColor != null)
            {
                position = 0;
                changes = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + primaryColor + ".xaml");
                Application.Current.Resources.MergedDictionaries.RemoveAt(position);
                Application.Current.Resources.MergedDictionaries.Insert(position, new ResourceDictionary() { Source = changes });
            }

            if (secondaryColor != null)
            {
                position = 1;
                changes = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor." + secondaryColor + ".xaml");
                Application.Current.Resources.MergedDictionaries.RemoveAt(position);
                Application.Current.Resources.MergedDictionaries.Insert(position, new ResourceDictionary() { Source = changes });
            }

            if (baseTheme != null)
            {
                position = 2;
                changes = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme." + baseTheme + ".xaml");
                Application.Current.Resources.MergedDictionaries.RemoveAt(position);
                Application.Current.Resources.MergedDictionaries.Insert(position, new ResourceDictionary() { Source = changes });
            }
        }
    }
}