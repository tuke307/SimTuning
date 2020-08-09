namespace API
{
    /// <summary>
    /// Konstanten für die API Verbindungen.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Wordpress Link.
        /// </summary>
        public static readonly string WPApiUrl = "https://www.tuke-productions.de/wp-json/";

        /// <summary>
        /// WooCommerce Link
        /// LEGACY REST API  = wc-api/v1,        wc-api/v2,          wc-api/v3
        /// NEW REST API     = wp-json/wc/v1,    wp-json/wc/v2,      wp-json/wc/v3
        /// </summary>
        public static readonly string WCApiUrl = "https://www.tuke-productions.de/wc-api/v3/";

        /// <summary>
        /// Der consumer key für Wordpress-WooCommerce.
        /// </summary>
        public static readonly string ConsumerKey = "ck_2a36ebc6b7bc47c9ca5ec75758000ea27b2846ca";

        /// <summary>
        /// Der consumer secret für Wordpress-WooCommerce.
        /// </summary>
        public static readonly string ConsumerSecret = "cs_eed168b7d7382fa7fb6e87f616b077ca0f8fe432";
    }
}