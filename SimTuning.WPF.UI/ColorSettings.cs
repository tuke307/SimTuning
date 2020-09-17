namespace SimTuning.WPF.UI
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;
    using System;

    /// <summary>
    /// ColorSettings.
    /// TODO: implementing getter.
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
                return MaterialDesignColors.PrimaryColor.Teal;//(MaterialDesignColors.PrimaryColor)AppSettings.GetValueOrDefault(nameof(Primary), (int)MaterialDesignColors.PrimaryColor.Teal, userFile);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(Primary), (int)value, userFile);
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
                return MaterialDesignColors.SecondaryColor.Cyan;//(MaterialDesignColors.SecondaryColor)AppSettings.GetValueOrDefault(nameof(Secondary), (int)MaterialDesignColors.SecondaryColor.Cyan, userFile);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(Secondary), (int)value, userFile);
            }
        }

        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        /// <value>The theme.</value>
        public static MaterialDesignThemes.Wpf.BaseTheme Theme
        {
            get
            {
                return MaterialDesignThemes.Wpf.BaseTheme.Light;//(MaterialDesignThemes.Wpf.BaseTheme)AppSettings.GetValueOrDefault(nameof(Theme), (int)MaterialDesignThemes.Wpf.BaseTheme.Light, userFile);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(Theme), (int)value, userFile);
            }
        }

        private static ISettings AppSettings => CrossSettings.Current;
    }
}