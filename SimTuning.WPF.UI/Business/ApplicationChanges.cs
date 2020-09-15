// project=SimTuning.WPF.UI, file=ApplicationChanges.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Business
{
    using MaterialDesignColors;
    using System;

    /// <summary>
    /// ApplicationChanges.
    /// </summary>
    public static class ApplicationChanges
    {
        /// <summary>
        /// Bools to base theme.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static MaterialDesignThemes.Wpf.BaseTheme BoolToBaseTheme(bool value)
        {
            if (value)
            {
                return MaterialDesignThemes.Wpf.BaseTheme.Dark;
            }
            else
            {
                return MaterialDesignThemes.Wpf.BaseTheme.Light;
            }
        }

        /// <summary>
        /// Determines whether [is dark theme].
        /// </summary>
        /// <returns><c>true</c> if [is dark theme]; otherwise, <c>false</c>.</returns>
        public static bool IsDarkTheme()
        {
            if (ColorSettings.Theme == MaterialDesignThemes.Wpf.BaseTheme.Dark)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Loads the colors.
        /// </summary>
        public static void LoadColors()
        {
            var appChanges = new RessourceChanges();

            appChanges.Colors(
                primaryColor: ColorSettings.Primary.ToString(),
                secondaryColor: ColorSettings.Secondary.ToString(),
                baseTheme: ColorSettings.Theme.ToString());
        }

        /// <summary>
        /// Sets the accent.
        /// </summary>
        /// <param name="acccent_color">Color of the acccent.</param>
        public static void SetAccent(Swatch acccent_color)
        {
            ColorSettings.Secondary = (MaterialDesignColors.SecondaryColor)Enum.Parse(typeof(MaterialDesignColors.SecondaryColor), acccent_color.Name);

            var appChanges = new RessourceChanges();

            appChanges.Colors(secondaryColor: ColorSettings.Secondary.ToString());
        }

        /// <summary>
        /// Sets the base theme.
        /// </summary>
        /// <param name="base_color">if set to <c>true</c> [base color].</param>
        public static void SetBaseTheme(bool base_color)
        {
            ColorSettings.Theme = ApplicationChanges.BoolToBaseTheme(base_color);

            var appChanges = new RessourceChanges();

            appChanges.Colors(baseTheme: ColorSettings.Theme.ToString());
        }

        /// <summary>
        /// Sets the primary.
        /// </summary>
        /// <param name="primary_color">Color of the primary.</param>
        public static void SetPrimary(Swatch primary_color)
        {
            ColorSettings.Primary = (MaterialDesignColors.PrimaryColor)Enum.Parse(typeof(MaterialDesignColors.PrimaryColor), primary_color.ToString(), true);

            var appChanges = new RessourceChanges();

            appChanges.Colors(primaryColor: ColorSettings.Primary.ToString());
        }
    }
}