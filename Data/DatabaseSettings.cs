// project=Data, file=Constants.cs, creation=2020:6:28 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace Data
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    /// <summary>
    /// Konstanten für die Datenbank-Abwicklung.
    /// </summary>
    public static class DatabaseSettings
    {
        /// <summary>
        /// The database name.
        /// </summary>
        public const string DatabaseName = "simtuning.db";

        /// <summary>
        /// Gets or sets the database path.
        /// </summary>
        /// <value>The database path.</value>
        public static string DatabasePath
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(DatabasePath), null);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(DatabasePath), value);
            }
        }

        private static ISettings AppSettings => CrossSettings.Current;
    }
}