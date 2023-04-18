using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PayPal.Api;


namespace freshMart
{
    public class PaypalConfiguration
    {

        public readonly static string clientId;
        public readonly static string clientSecret;
        static PaypalConfiguration()
        {
            var config = getconfig();
            clientId = "AcqpgR4TPrUFFayopxZWg9KOGWdfDu-Mri-QhtJN9-_GEkonnHYpwwlfaAhuLbyQQY1kNN-Uqztdl2eH";
            clientSecret = "EAQ1UFh94_CYUMJ2y5QNOKgAupORyMG9jTnbS02Y-6RadZy0C-0kEA4tH88DSOF7GjdgBkrsKIrrp9so";
        }

        private static Dictionary<string, string> getconfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, getconfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            APIContext apicontext = new APIContext(GetAccessToken());
            apicontext.Config = getconfig();
            return apicontext;
        }
    }
}