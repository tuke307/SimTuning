// project=SimTuning.WPF.UI, file=ApplicationChanges.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.Business
{
    using MaterialDesignColors;
    using MaterialDesignThemes.Wpf;
    using System;

    /// <summary>
    /// ApplicationChanges.
    /// </summary>
    public static class ApplicationChanges
    {
        /// <summary>
        /// Loads the colors.
        /// </summary>
        public static void LoadColors()
        {
            SetBaseTheme((MaterialDesignThemes.Wpf.BaseTheme)ColorSettings.Theme);

            var appChanges = new RessourceChanges();

            appChanges.Colors(
                primaryColor: (MaterialDesignColors.PrimaryColor)ColorSettings.Primary,
                secondaryColor: (MaterialDesignColors.SecondaryColor)ColorSettings.Secondary
                /*baseTheme: (MaterialDesignThemes.Wpf.BaseTheme)ColorSettings.Theme*/);
        }

        /// <summary>
        /// Sets the accent.
        /// </summary>
        /// <param name="acccent_color">Color of the acccent.</param>
        public static void SetAccent(Swatch acccent_color)
        {
            ColorSettings.Secondary = (int)(MaterialDesignColors.SecondaryColor)Enum.Parse(typeof(MaterialDesignColors.SecondaryColor), acccent_color.Name);

            var appChanges = new RessourceChanges();

            appChanges.Colors(secondaryColor: (MaterialDesignColors.SecondaryColor)ColorSettings.Secondary);
        }

        /// <summary>
        /// Sets the base theme.
        /// </summary>
        /// <param name="base_color">if set to <c>true</c> [base color].</param>
        public static void SetBaseTheme(MaterialDesignThemes.Wpf.BaseTheme baseTheme)
        {
            ColorSettings.Theme = (int)baseTheme;

            if (baseTheme == (int)MaterialDesignThemes.Wpf.BaseTheme.Inherit)
            {
                baseTheme = Theme.GetSystemTheme().Value;
            }

            var paletteHelper = new PaletteHelper();

            //Retrieve the app's existing theme
            ITheme theme = paletteHelper.GetTheme();

            switch (baseTheme)
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

            //Change the app's current theme
            paletteHelper.SetTheme(theme);

            //var appChanges = new RessourceChanges();

            //appChanges.Colors(baseTheme: (MaterialDesignThemes.Wpf.BaseTheme)ColorSettings.Theme);
        }

        /// <summary>
        /// Sets the primary.
        /// </summary>
        /// <param name="primary_color">Color of the primary.</param>
        public static void SetPrimary(Swatch primary_color)
        {
            ColorSettings.Primary = (int)(MaterialDesignColors.PrimaryColor)Enum.Parse(typeof(MaterialDesignColors.PrimaryColor), primary_color.ToString(), true);

            var appChanges = new RessourceChanges();

            appChanges.Colors(primaryColor: (MaterialDesignColors.PrimaryColor)ColorSettings.Primary);
        }
    }
}