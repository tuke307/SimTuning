using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SimTuning.Forms.UI.Business
{
    public static class ApplicationChanges
    {
        /// <summary>
        /// Bools to base theme.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        /// <returns></returns>
        public static BaseTheme BoolToBaseTheme(bool value)
        {
            if (value)
            {
                return BaseTheme.Dark;
            }
            else
            {
                return BaseTheme.Light;
            }
        }

        /// <summary>
        /// Determines whether [is dark theme].
        /// </summary>
        /// <returns><c>true</c> if [is dark theme]; otherwise, <c>false</c>.</returns>
        public static bool IsDarkTheme()
        {
            if (ColorSettings.Theme == (int)BaseTheme.Dark)
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
            BaseTheme? baseTheme = null;

            if (ColorSettings.Theme == (int)BaseTheme.Inherit)
            {
                OSAppTheme currentTheme = Application.Current.RequestedTheme;

                switch (currentTheme)
                {
                    case OSAppTheme.Unspecified:
                        break;

                    case OSAppTheme.Light:
                        Application.Current.UserAppTheme = OSAppTheme.Light;
                        baseTheme = BaseTheme.Light;
                        break;

                    case OSAppTheme.Dark:
                        Application.Current.UserAppTheme = OSAppTheme.Dark;
                        baseTheme = BaseTheme.Dark;
                        break;
                }
            }
            else
            {
                baseTheme = (BaseTheme)ColorSettings.Theme;
            }

            var appChanges = new RessourceChanges();

            appChanges.Colors(
                primaryColor: (PrimaryColor)ColorSettings.Primary,
                secondaryColor: (SecondaryColor)ColorSettings.Secondary,
                baseTheme: baseTheme);
        }

        /// <summary>
        /// Sets the accent.
        /// </summary>
        /// <param name="acccent_color">Color of the acccent.</param>
        public static void SetAccent(SecondaryColor acccent_color)
        {
            ColorSettings.Secondary = (int)acccent_color;

            var appChanges = new RessourceChanges();

            appChanges.Colors(secondaryColor: (SecondaryColor)ColorSettings.Secondary);
        }

        /// <summary>
        /// Sets the base theme.
        /// </summary>
        /// <param name="base_color">if set to <c>true</c> [base color].</param>
        public static void SetBaseTheme(BaseTheme base_color)
        {
            ColorSettings.Theme = (int)base_color;

            var appChanges = new RessourceChanges();

            appChanges.Colors(baseTheme: (BaseTheme)ColorSettings.Theme);
        }

        /// <summary>
        /// Sets the primary.
        /// </summary>
        /// <param name="primary_color">Color of the primary.</param>
        public static void SetPrimary(PrimaryColor primary_color)
        {
            ColorSettings.Primary = (int)primary_color;

            var appChanges = new RessourceChanges();

            appChanges.Colors(primaryColor: (PrimaryColor)ColorSettings.Primary);
        }
    }
}