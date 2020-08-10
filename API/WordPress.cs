// project=API, file=WordPress.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
using System.Security;
using System.Threading.Tasks;

//using WooCommerceNET.WooCommerce.v3;
using WordPressPCL;
using WordPressPCL.Models;

namespace API
{
    /// <summary>
    /// Wordpress-Klasse.
    /// </summary>
    public static class WordPress
    {
        /// <summary>
        /// Meldet Nutzer in Wordpress(Datenbank) an und holt Nutzerdaten.
        /// </summary>
        /// <param name="email">Die E-Mail des Nutzers.</param>
        /// <param name="password">Das Passwort des Nutzers.</param>
        /// <returns>Nutzerdaten.</returns>
        public static async Task<User> UserAccount(string email, SecureString password)
        {
            // Initialize
            var client = new WordPressClient(Constants.WPApiUrl);
            client.AuthMethod = AuthMethod.JWT;

            try
            {
                await client.RequestJWToken(email, SimTuning.Core.Business.Converts.SecureStringToString(password)).ConfigureAwait(true);
                var user = await client.Users.GetCurrentUser().ConfigureAwait(true);
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}