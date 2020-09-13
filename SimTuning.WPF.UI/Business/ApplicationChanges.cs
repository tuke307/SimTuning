// project=SimTuning.WPF.UI, file=ApplicationChanges.cs, creation=2020:9:2 Copyright (c)
// 2020 tuke productions. All rights reserved.
using Data;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using WordPressPCL.Models;

namespace SimTuning.WPF.UI.Business
{
    /// <summary>
    /// ApplicationChanges.
    /// </summary>
    public static class ApplicationChanges
    {
        public static MaterialDesignThemes.Wpf.BaseTheme BoolToBaseTheme(bool value)
        {
            if (value)
                return MaterialDesignThemes.Wpf.BaseTheme.Dark;
            else
                return MaterialDesignThemes.Wpf.BaseTheme.Light;
        }

        public static bool IsDarkTheme()
        {
            if (ColorSettings.Theme == MaterialDesignThemes.Wpf.BaseTheme.Dark)
                return true;
            else
                return false;
        }

        public static void LoadColors()
        {
            var appChanges = new RessourceChanges();

            appChanges.Colors(
                primaryColor: ColorSettings.Primary.ToString(),
                secondaryColor: ColorSettings.Secondary.ToString(),
                baseTheme: ColorSettings.Theme.ToString());
        }

        public static void SetAccent(Swatch acccent_color)
        {
            ColorSettings.Secondary = (MaterialDesignColors.SecondaryColor)Enum.Parse(typeof(MaterialDesignColors.SecondaryColor), acccent_color.Name);

            var appChanges = new RessourceChanges();

            appChanges.Colors(secondaryColor: ColorSettings.Secondary.ToString());
        }

        public static void SetBaseTheme(bool base_color)
        {
            ColorSettings.Theme = ApplicationChanges.BoolToBaseTheme(base_color);

            var appChanges = new RessourceChanges();

            appChanges.Colors(baseTheme: ColorSettings.Theme.ToString());
        }

        public static void SetPrimary(Swatch primary_color)
        {
            ColorSettings.Primary = (MaterialDesignColors.PrimaryColor)Enum.Parse(typeof(MaterialDesignColors.PrimaryColor), primary_color.ToString(), true);

            var appChanges = new RessourceChanges();

            appChanges.Colors(primaryColor: ColorSettings.Primary.ToString());
        }
    }
}