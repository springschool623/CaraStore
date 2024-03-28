using CaraLuggage.Models;
using Facebook;
using GoogleAuthentication.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CaraLuggage.Controllers.Singleton
{
    public class SocialAuthService
    {
        private static SocialAuthService _instance;

        //lockObject để đảm bảo rằng chỉ có 1 luồng duy nhất
        private static readonly object lockObject = new object();

        private FacebookClient fbClient;
        private string fbClientId = "25155442967434663";
        private string fbClientSecret = "dfb968e202308c4ca44f70d70ef48321";

        private string googleClientId = "1032401930561-u8ubttlmgn8vc4pb6f69s9qn6s7rhnqv.apps.googleusercontent.com";
        private string googleClientSecret = "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";
        private string googleRedirectUrl = "https://localhost:44357/dang-nhap-google";

        public SocialAuthService()
        {
            fbClient = new FacebookClient();
        }

        public static SocialAuthService Instance
        {
            get
            {
                lock (lockObject)
                {
                    if(_instance == null)
                    {
                        _instance = new SocialAuthService();
                    }
                    return _instance;
                }
            }
        }

        public string GetFacebookLoginUrl(string fbRedirectUrl)
        {
            var loginUrl = fbClient.GetLoginUrl(new
            {
                client_id = fbClientId,
                client_secret = fbClientSecret,
                redirect_uri = fbRedirectUrl,
                respone_type = "code",
                scope = "public_profile,email",
            });
            return loginUrl.ToString();
        }

        public async Task<(string name, string email)> GetFacebookLoginDetails(string code, string fbRedirectUrl)
        {
            dynamic tokenResult = await fbClient.GetTaskAsync("oauth/access_token", new
            {
                client_id = fbClientId,
                client_secret = fbClientSecret,
                redirect_uri = fbRedirectUrl,
                code = code
            });

            string accessToken = tokenResult.access_token;

            dynamic me = await fbClient.GetTaskAsync("me?fields=name,email", new { access_token = accessToken });

            string name = me["name"];
            string email = me["email"];


            return (name, email);
        }

        public string GetGoogleAuthUrl(string redirectUrl)
        {
            return GoogleAuth.GetAuthUrl(googleClientId, redirectUrl);
        }

        public async Task<UserProfile> GetUserProfileFromGoogle(string code)
        {
            var token = await GoogleAuth.GetAuthAccessToken(code, googleClientId, googleClientSecret, googleRedirectUrl);
            var userProfile = await GoogleAuth.GetProfileResponseAsync(token.AccessToken.ToString());

            var userProfileObject = new UserProfile();

            JObject userProfileJSon = JObject.Parse(userProfile);

            // Truy cập thông tin từ đối tượng JSON
            var name = (string)userProfileJSon["name"];
            var email = (string)userProfileJSon["email"];

            userProfileObject.Name = name;
            userProfileObject.Email = email;

            return userProfileObject;
        }
    }
}