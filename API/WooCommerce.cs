// project=API, file=WooCommerce.cs, creation=2020:6:28 Copyright (c) 2020 tuke
// productions. All rights reserved.
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.Legacy;

namespace API
{
    /// <summary>
    /// WooCommerce-Klasse.
    /// </summary>
    public static class WooCommerce
    {
        /// <summary>
        /// Holt die Bestellung von SimTuning.
        /// </summary>
        /// <param name="userId">Nutzer bei der, die Bestellung geholt werden soll.</param>
        /// <returns>Bestellung von SimTuning.</returns>
        public static async Task<WooCommerceNET.WooCommerce.Legacy.Order> UserLicense(int userId)
        {
            List<WooCommerceNET.WooCommerce.Legacy.Order> orders = null;
            WooCommerceNET.WooCommerce.Legacy.Order order = null;

            RestAPI rest = new RestAPI(Constants.WCApiUrl, Constants.ConsumerKey, Constants.ConsumerSecret, requestFilter: RequestFilter);
            WCObject wc_object = new WCObject(rest);
            try
            {
                var parms = new Dictionary<string, string>();
                parms.Add("status", "completed, processing");
                orders = await wc_object.GetCustomerOrders(userId, parms).ConfigureAwait(true);

                // SimTuning Produkt Id = 312
                order = orders.Find(i => i.line_items.Find(l => l.product_id == 312) != null);

                return order;
            }
            catch (Exception exc)
            {
                System.Console.WriteLine("Fehler beim abrufen der Kunden-Bestellungen; " + exc.ToString());
                return null;
            }
        }

        /// <summary>
        /// Anfrage für http-Pakete, ohne werden diese evtl. nicht durchgelassen.
        /// </summary>
        /// <param name="request">HTTP Anfrage.</param>
        private static void RequestFilter(HttpWebRequest request)
        {
            request.UserAgent = "WooCommerce.NET";
        }
    }
}