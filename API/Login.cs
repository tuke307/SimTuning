// project=API, file=Login.cs, creation=2020:6:28 Copyright (c) 2020 tuke productions. All
// rights reserved.
namespace API
{
    using SimTuning.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Security;
    using System.Threading.Tasks;
    using WordPressPCL.Models;

    /// <summary>
    /// Einlogg-Klasse.
    /// </summary>
    public static class Login
    {
        /// <summary>
        /// Aufruf-Funktion zum einloggen des Nutzers.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>UserModel userModel, output messages for user.</returns>
        public static async Task<(WordPressPCL.Models.User, WooCommerceNET.WooCommerce.Legacy.Order, List<string>)> UserLoginAsync(string email = null, SecureString password = null)
        {
            // default
            WooCommerceNET.WooCommerce.Legacy.Order order = null;
            WordPressPCL.Models.User user = null;

            List<string> messages = new List<string>();

            // admin start
            if (email == "admin123")
            {
                messages.Add("ADMIN LOGIN");
                goto Finish;
            }

            // für automatischen start, wenn keine Übergabeparameter angegeben wurden
            if (email == null && password == null)
            {
                SimTuning.Core.Business.Functions.GetLoginCredentials(out email, out password);

                // wenn keine Anmeldedaten hinterlegt wurden
                if (email == null || password == null)
                {
                    // es sollen keine nachrichten angezeigt werden
                    messages = null;
                    goto Finish;
                }
            }

            // wenn Email fehlt
            if (email == null && password != null)
            {
                messages.Add("FEHLER beim einloggen, Email nicht eingegeben");
                goto Finish;
            }

            // wenn passwort fehlt
            if (password == null && email != null)
            {
                messages.Add("FEHLER beim einloggen, Passwort nicht eingegeben");
                goto Finish;
            }

            // User-Daten von Wordpress holen
            user = await WordPress.UserAccount(email, password).ConfigureAwait(true);
            if (user != null)
            {
                messages.Add("Erfolgreich eingeloggt");
            }
            else
            {
                messages.Add("FEHLER beim einloggen");
                goto Finish;
            }

            order = await WooCommerce.UserLicense(user.Id).ConfigureAwait(true);
            if (order != null)
            {
                messages.Add("PRO Version");
            }
            else
            {
                messages.Add("FREE Version");
            }

            // Speichern der daten
            SimTuning.Core.Business.Functions.SaveLoginCredentials(email, password);

        Finish:
            return (user, order, messages);
        }
    }
}