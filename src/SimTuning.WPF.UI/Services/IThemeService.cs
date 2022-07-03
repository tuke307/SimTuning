// Copyright (c) 2021 tuke productions. All rights reserved.
using MaterialDesignThemes.Wpf;

namespace SimTuning.WPF.UI.Services
{
    public interface IThemeService
    {
        void UpdatePrimary(MaterialDesignColors.PrimaryColor primaryColor);

        void UpdateSecondary(MaterialDesignColors.SecondaryColor secondaryColor);

        void UpdateTheme(MaterialDesignThemes.Wpf.BaseTheme themeMode);
    }
}