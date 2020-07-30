using System.Security;
using System.Threading.Tasks;

//using WooCommerceNET.WooCommerce.v3;
using WordPressPCL;
using WordPressPCL.Models;

namespace API
{
    public static class WordPress
    {
        /// <summary>
        /// Meldet Nutzer in Wordpress(Datenbank) an und holt Nutzerdaten.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Nutzerdaten.</returns>
        public static async Task<User> UserAccount(string email, SecureString password)
        {
            // Initialize
            var client = new WordPressClient(Constants.WPApiUrl);
            client.AuthMethod = AuthMethod.JWT;

            try
            {
                await client.RequestJWToken(email, SimTuning.Core.Business.Converts.SecureStringToString(password));
                var user = await client.Users.GetCurrentUser();
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}