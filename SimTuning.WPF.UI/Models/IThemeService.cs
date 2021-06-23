// project=SimTuning.WPF.UI, file=IThemeService.cs, creation=2020:12:14 Copyright (c) 2021
// tuke productions. All rights reserved.
using MaterialDesignThemes.Wpf;

namespace SimTuning.WPF.UI.Models
{
    public interface IThemeService
    {
        void UpdatePrimary(MaterialDesignColors.PrimaryColor primaryColor);

        void UpdateSecondary(MaterialDesignColors.SecondaryColor secondaryColor);

        void UpdateTheme(MaterialDesignThemes.Wpf.BaseTheme themeMode);
    }
}