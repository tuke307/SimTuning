// Copyright (c) 2021 tuke productions. All rights reserved.
using System;
using System.Security;
using System.Threading.Tasks;

// using WooCommerceNET.WooCommerce.v3;
using WordPressPCL;
using WordPressPCL.Models;

namespace SimTuning.API
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
        public static async Task<WordPressPCL.Models.User> UserAccount(string email, SecureString password)
        {
            // Initialize
            var client = new WordPressClient(Constants.WPApiUrl);
            client.AuthMethod = AuthMethod.JWT;

            try
            {
                await client.RequestJWToken(email, SimTuning.Core.Converters.Converts.SecureStringToString(password)).ConfigureAwait(true);
                var user = await client.Users.GetCurrentUser().ConfigureAwait(true);
                return user;
            }
            catch (Exception exc)
            {
                System.Console.WriteLine("Fehler beim abrufen der Kunden-Bestellungen; " + exc.ToString());

                return null;
            }
        }
    }
}