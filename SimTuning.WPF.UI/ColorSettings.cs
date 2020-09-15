namespace SimTuning.WPF.UI
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;
    using System;

    /// <summary>
    /// ColorSettings.
    /// </summary>
    public static class ColorSettings
    {
        private const string userFile = "user";

        /// <summary>
        /// Gets or sets the primary.
        /// </summary>
        /// <value>The primary.</value>
        public static MaterialDesignColors.PrimaryColor Primary
        {
            get
            {
                return (MaterialDesignColors.PrimaryColor)Enum.Parse(typeof(MaterialDesignColors.PrimaryColor), AppSettings.GetValueOrDefault(nameof(Primary), nameof(MaterialDesignColors.PrimaryColor.Teal), userFile));
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(Primary), nameof(value), userFile);
            }
        }

        /// <summary>
        /// Gets or sets the secondary.
        /// </summary>
        /// <value>The secondary.</value>
        public static MaterialDesignColors.SecondaryColor Secondary
        {
            get
            {
                return (MaterialDesignColors.SecondaryColor)Enum.Parse(typeof(MaterialDesignColors.SecondaryColor), AppSettings.GetValueOrDefault(nameof(Secondary), nameof(MaterialDesignColors.SecondaryColor.Cyan), userFile));
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(Secondary), nameof(value), userFile);
            }
        }

        /// <summary>
        /// Gets or sets the theme. TODO:parsing dosnt work
        /// </summary>
        /// <value>The theme.</value>
        public static MaterialDesignThemes.Wpf.BaseTheme Theme
        {
            get
            {
                return MaterialDesignThemes.Wpf.BaseTheme.Light;//(MaterialDesignThemes.Wpf.BaseTheme)Enum.Parse(typeof(MaterialDesignThemes.Wpf.BaseTheme), AppSettings.GetValueOrDefault(nameof(Theme), nameof(MaterialDesignThemes.Wpf.BaseTheme.Light), userFile));
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(Theme), nameof(value), userFile);
            }
        }

        private static ISettings AppSettings => CrossSettings.Current;
    }
}