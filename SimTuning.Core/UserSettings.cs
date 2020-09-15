﻿namespace SimTuning.Core
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;
    using System.ComponentModel;

    /// <summary>
    /// UserSettings.
    /// </summary>
    public class UserSettings
    {
        // Singleton
        //public static UserSettings Current = new UserSettings();

        private static WooCommerceNET.WooCommerce.Legacy.Order _order;

        private static WordPressPCL.Models.User _user;

        /// <summary>
        /// Gets a value indicating whether [license valid].
        /// </summary>
        /// <value><c>true</c> if [license valid]; otherwise, <c>false</c>.</value>
        public static bool LicenseValid
        {
            get
            {
                if (Order != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        public static WooCommerceNET.WooCommerce.Legacy.Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OnPropertyChanged(nameof(LicenseValid));
            }
        }

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
        public static WordPressPCL.Models.User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(UserValid));
            }
        }

        /// <summary>
        /// Gets a value indicating whether [user valid].
        /// </summary>
        /// <value><c>true</c> if [user valid]; otherwise, <c>false</c>.</value>
        public static bool UserValid
        {
            get
            {
                if (User != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static ISettings AppSettings => CrossSettings.Current;

        /// <summary>
        /// Occurs when [static property changed].
        /// </summary>
        public static event PropertyChangedEventHandler PropertyChanged;

        private static void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}