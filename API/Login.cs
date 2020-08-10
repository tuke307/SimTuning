// project=API, file=Login.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace API
{
    using System;
    using System.Collections.Generic;
    using System.Security;
    using System.Threading.Tasks;
    using SimTuning.Core.Models;
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
        public static async Task<(UserModel, List<string>)> UserLoginAsync(string email = null, SecureString password = null)
        {
            // default
            UserModel userModel = new UserModel();
            List<string> messages = new List<string>();

            // admin start
            if (email == "admin123")
            {
                userModel.LicenseValid = true;
                userModel.UserValid = true;
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
            userModel.User = await WordPress.UserAccount(email, password).ConfigureAwait(true);
            if (userModel.User != null)
            {
                messages.Add("Erfolgreich eingeloggt");
                userModel.UserValid = true;
            }
            else
            {
                messages.Add("FEHLER beim einloggen");
                goto Finish;
            }

            userModel.Order = await WooCommerce.UserLicense(userModel.User.Id).ConfigureAwait(true);
            if (userModel.Order != null)
            {
                messages.Add("PRO Version");
                userModel.LicenseValid = true;
            }
            else
            {
                messages.Add("FREE Version");
            }

            // Speichern der daten
            SimTuning.Core.Business.Functions.SaveLoginCredentials(email, password);

        Finish:
            return (userModel, messages);
        }
    }
}