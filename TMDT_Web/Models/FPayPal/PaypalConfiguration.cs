using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMDT_Web.Models.FPayPal
{
    public class PaypalConfiguration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;
        static PaypalConfiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }
        public static Dictionary<string, string> GetConfig()
        {
            /*return new Dictionary<string, string>()
            {
                {"clientId","AYi-O9HCF7nObjhzJ5vLYZsc1g53cE-xJBtPcztJRV0K-74IJEW65qiiEuy8rwQFTPS56--bvQ-Bz7U8"},
                {"clientSecret", "EPqrgfyH205W0n3yO4SgfyWtl6GgLY3aI0D2I0YZ6jMuEemBKJU6NUusYxCce7-CLHavzGAJYjl0jxpP"}
            };*/
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext()
        {
            var aPIContext = new APIContext(GetAccessToken());
            aPIContext.Config = GetConfig();
            return aPIContext;
        }
    }
}