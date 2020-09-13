namespace Data
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    /// <summary>
    /// UnitSettings.
    /// </summary>
    public static class UnitSettings
    {
        /// <summary>
        /// Gets or sets the rounding accuracy.
        /// </summary>
        /// <value>The rounding accuracy.</value>
        public static int RoundingAccuracy
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(RoundingAccuracy), 2);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(RoundingAccuracy), value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [round on unit change].
        /// </summary>
        /// <value><c>true</c> if [round on unit change]; otherwise, <c>false</c>.</value>
        public static bool RoundOnUnitChange
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(RoundOnUnitChange), true);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(RoundOnUnitChange), value);
            }
        }

        private static ISettings AppSettings => CrossSettings.Current;
    }
}