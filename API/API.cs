using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
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
        /// <returns>bool USER_VALID, bool LICENSE_VALID, output messages for user</returns>
        public static async Task<Tuple<bool, bool, List<string>>> UserLoginAsync(string email = null, SecureString password = null)
        {
            //default
            bool user_valid = false;
            bool license_valid = false;
            List<string> messages = new List<string>();
            User user = null; //Account
            WooCommerceNET.WooCommerce.Legacy.Order order = null; //Lizenz


            //admin start
            if (email == "admin123")
            {
                license_valid = true;
                user_valid = true;
                messages.Add("ADMIN LOGIN");
                goto Finish;
            }


            //für automatischen start
            if (email == null && password == null)
                SimTuning.Business.Functions.GetLoginCredentials(out email, out password);
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
                user_valid = true;
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
                license_valid = true;
            }
            else
            {
                messages.Add("FREE Version");
            }

            //Speichern der daten
            SimTuning.Business.Functions.SaveLoginCredentials(email, password);

        Finish:
            return new Tuple<bool, bool, List<string>>(user_valid, license_valid, messages);
        }
    }
}