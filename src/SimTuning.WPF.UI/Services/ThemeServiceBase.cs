// Copyright (c) 2021 tuke productions. All rights reserved.
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;

namespace SimTuning.WPF.UI.Services
{
    public class ThemeServiceBase : IThemeService
    {
        public void UpdatePrimary(PrimaryColor primaryColor)
        {
            int position = 0;
            Uri changes = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + primaryColor + ".xaml");
            Application.Current.Resources.MergedDictionaries.RemoveAt(position);
            Application.Current.Resources.MergedDictionaries.Insert(position, new ResourceDictionary() { Source = changes });

            ColorSettings.Primary = (int)primaryColor;
        }

        public void UpdateSecondary(SecondaryColor secondaryColor)
        {
            int position = 1;
            Uri changes = new Uri($"pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor." + secondaryColor + ".xaml");
            Application.Current.Resources.MergedDictionaries.RemoveAt(position);
            Application.Current.Resources.MergedDictionaries.Insert(position, new ResourceDictionary() { Source = changes });

            ColorSettings.Secondary = (int)secondaryColor;
        }

        public virtual void UpdateTheme(BaseTheme themeMode)
        {
            // if (ColorSettings.Theme == (int)themeMode) { return; }

            this.SetColors(themeMode);

            ColorSettings.Theme = (int)themeMode;
        }

        private void SetColors(BaseTheme themeMode)
        {
            if (themeMode == (int)MaterialDesignThemes.Wpf.BaseTheme.Inherit)
            {
                themeMode = Theme.GetSystemTheme().Value;
            }

            var paletteHelper = new PaletteHelper();

            // Retrieve the app's existing theme
            ITheme theme = paletteHelper.GetTheme();

            switch (themeMode)
            {
                case BaseTheme.Inherit:
                    break;

                case BaseTheme.Light:
                    theme.SetBaseTheme(Theme.Light);
                    break;

                case BaseTheme.Dark:
                    theme.SetBaseTheme(Theme.Dark);
                    break;

                default:
                    break;
            }

            // Change the app's current theme
            paletteHelper.SetTheme(theme);
        }
    }
}