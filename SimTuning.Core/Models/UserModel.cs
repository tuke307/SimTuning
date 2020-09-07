// project=SimTuning.Core, file=UserModel.cs, creation=2020:7:31 Copyright (c) 2020 tuke
// productions. All rights reserved.
namespace SimTuning.Core.Models
{
    /// <summary>
    /// UserModel.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [license valid].
        /// </summary>
        /// <value><c>true</c> if [license valid]; otherwise, <c>false</c>.</value>
        public bool LicenseValid { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public WooCommerceNET.WooCommerce.Legacy.Order? Order { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public WordPressPCL.Models.User? User { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [user valid].
        /// </summary>
        /// <value><c>true</c> if [user valid]; otherwise, <c>false</c>.</value>
        public bool UserValid { get; set; }
    }
}