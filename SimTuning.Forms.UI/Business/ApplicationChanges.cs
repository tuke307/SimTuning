using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.Business
{
    public static class ApplicationChanges
    {
        /// <summary>
        /// Loads the colors.
        /// TODO: improve color
        /// </summary>
        public static void LoadColors()
        {
            Themes.BaseTheme? baseTheme = null;

            if (ColorSettings.Theme == (int)Themes.BaseTheme.Inherit)
            {
                OSAppTheme currentTheme = Application.Current.RequestedTheme;

                switch (currentTheme)
                {
                    case OSAppTheme.Unspecified:
                        break;

                    case OSAppTheme.Light:
                        Application.Current.UserAppTheme = OSAppTheme.Light;
                        baseTheme = Themes.BaseTheme.Light;
                        break;

                    case OSAppTheme.Dark:
                        Application.Current.UserAppTheme = OSAppTheme.Dark;
                        baseTheme = Themes.BaseTheme.Dark;
                        break;
                }
            }
            else
            {
                baseTheme = (Themes.BaseTheme)ColorSettings.Theme;
            }

            var appChanges = new RessourceChanges();

            appChanges.Colors(
                primaryColor: (Themes.PrimaryColor)ColorSettings.Primary,
                secondaryColor: (Themes.SecondaryColor)ColorSettings.Secondary,
                baseTheme: baseTheme);
        }

        /// <summary>
        /// Sets the accent.
        /// </summary>
        /// <param name="acccent_color">Color of the acccent.</param>
        public static void SetAccent(Themes.SecondaryColor acccent_color)
        {
            ColorSettings.Secondary = (int)acccent_color;

            var appChanges = new RessourceChanges();

            appChanges.Colors(secondaryColor: (Themes.SecondaryColor)ColorSettings.Secondary);
        }

        /// <summary>
        /// Sets the base theme.
        /// TODO: improve color
        /// </summary>
        /// <param name="base_color">if set to <c>true</c> [base color].</param>
        public static void SetBaseTheme(Themes.BaseTheme base_color)
        {
            ColorSettings.Theme = (int)base_color;

            var appChanges = new RessourceChanges();

            appChanges.Colors(baseTheme: (Themes.BaseTheme)ColorSettings.Theme);
        }

        /// <summary>
        /// Sets the primary.
        /// </summary>
        /// <param name="primary_color">Color of the primary.</param>
        public static void SetPrimary(Themes.PrimaryColor primary_color)
        {
            ColorSettings.Primary = (int)primary_color;

            var appChanges = new RessourceChanges();

            appChanges.Colors(primaryColor: (Themes.PrimaryColor)ColorSettings.Primary);
        }
    }
}