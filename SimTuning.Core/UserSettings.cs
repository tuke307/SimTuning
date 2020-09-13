namespace SimTuning.Core
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;
    using System.ComponentModel;

    /// <summary>
    /// UserSettings.
    /// </summary>
    public static class UserSettings
    {
        private static bool _LicenseValid;

        private static bool Valid;

        /// <summary>
        /// Gets or sets a value indicating whether [license valid].
        /// </summary>
        /// <value><c>true</c> if [license valid]; otherwise, <c>false</c>.</value>
        public static bool LicenseValid
        {
            get => _LicenseValid;
            set
            {
                _LicenseValid = value;
                OnStaticPropertyChanged("LicenseValid");
            }
        }

        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
        /// <value>The mail.</value>
        public static string Mail
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(Mail), null);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(Mail), value);
            }
        }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public static WooCommerceNET.WooCommerce.Legacy.Order? Order { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(Password), null);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(Password), value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value><c>true</c> if [remember me]; otherwise, <c>false</c>.</value>
        public static bool RememberMe
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(RememberMe), true);
            }

            set
            {
                AppSettings.AddOrUpdateValue(nameof(RememberMe), value);
            }
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public static WordPressPCL.Models.User? User { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [user valid].
        /// </summary>
        /// <value><c>true</c> if [user valid]; otherwise, <c>false</c>.</value>
        public static bool UserValid
        {
            get => Valid;
            set
            {
                Valid = value;
                OnStaticPropertyChanged("UserValid");
            }
        }

        private static ISettings AppSettings => CrossSettings.Current;

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private static void OnStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}