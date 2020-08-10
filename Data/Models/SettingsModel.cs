// project=Data, file=SettingsModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// EinstellungenModel.
    /// TODO: in user.settings umbauen, also nicht in DB.
    /// </summary>
    /// <seealso cref="Data.Models.BaseEntityModel" />
    public class SettingsModel : BaseEntityModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [dark mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [dark mode]; otherwise, <c>false</c>.
        /// </value>
        [Required]
        public bool DarkMode { get; set; }

        /// <summary>
        /// Gets or sets the color of the primary.
        /// </summary>
        /// <value>
        /// The color of the primary.
        /// </value>
        [Required]
        public string PrimaryColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the secondary.
        /// </summary>
        /// <value>
        /// The color of the secondary.
        /// </value>
        [Required]
        public string SecondaryColor { get; set; }

        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
        /// <value>
        /// The mail.
        /// </value>
        public string Mail { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }
}