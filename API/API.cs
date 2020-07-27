using System;
using System.Collections.Generic;
using System.Security;
using System.Threading.Tasks;
using WordPressPCL.Models;

namespace API
{
    public static class API
    {
        /// <summary>
        /// Aufruf-Funktion zum einloggen des Nutzers
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>bool _userValid, bool _licenseValid, output messages for user</returns>
        public static async Task<Tuple<bool, bool, List<string>>> UserLoginAsync(string email = null, SecureString password = null)
        {
            //default
            bool userValid = false;
            bool licenseValid = false;
            List<string> messages = new List<string>();
            User user = null; //Account
            WooCommerceNET.WooCommerce.Legacy.Order order = null; //Lizenz

            //admin start
            if (email == "admin123")
            {
                licenseValid = true;
                userValid = true;
                messages.Add("ADMIN LOGIN");
                goto Finish;
            }

            //für automatischen start
            if (email == null && password == null)
                SimTuning.Core.Business.Functions.GetLoginCredentials(out email, out password);
            if (email == null || password == null)
                goto Finish;

            //wenn Email fehlt
            if (email == null && password != null)
            {
                messages.Add("FEHLER beim einloggen, Email nicht eingegeben");
                goto Finish;
            }

            //wenn passwort fehlt
            if (password == null && email != null)
            {
                messages.Add("FEHLER beim einloggen, Passwort nicht eingegeben");
                goto Finish;
            }

            user = await WordPress.UserAccount(email, password);
            if (user != null)
            {
                messages.Add("Erfolgreich eingeloggt");
                userValid = true;
            }
            else
            {
                messages.Add("FEHLER beim einloggen");
                goto Finish;
            }

            order = await WooCommerce.UserLicense(user.Id);
            if (order != null)
            {
                messages.Add("PRO Version");
                licenseValid = true;
            }
            else
            {
                messages.Add("FREE Version");
            }

            //Speichern der daten
            SimTuning.Core.Business.Functions.SaveLoginCredentials(email, password);

        Finish:
            return new Tuple<bool, bool, List<string>>(userValid, licenseValid, messages);
        }
    }
}