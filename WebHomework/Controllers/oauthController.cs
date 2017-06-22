using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SDNUMobile.SDK;
using System.Threading;

namespace WebHomework.Controllers
{
    /// <summary>
    /// json类
    /// </summary>
    class JsonDeserializer : IJsonDeserializer
    {
        public static JsonDeserializer Instance = new JsonDeserializer();

        public T DeserializeJson<T>(String json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    public class oauthController : Controller
    {
        // GET: oauth

        #region 字段
        static String consumerKey = "a64120eb5ca9e262504858c22e2692cb";
        static String consumerSecret = "8f11aa58b085190e185c6d464a6b4280b085190e";
        String authorizeUrl = "";
        private static RequestToken _requestToken;

        OAuthClient client = new OAuthClient(JsonDeserializer.Instance, consumerKey, consumerSecret);

        String callbackUrl = "http://localhost:2024/oauth/callback";

        private static AccessToken _accessToken;
        private static string people;
        String verifier = "";
        #endregion

        #region 私有方法
        
        /// <summary>
        /// 取得RequestToken
        /// </summary>
        private void GetRequestToken()
        {
            client.RequestRequestTokenAsync(callbackUrl, result =>
            {
                RequestToken requestToken= result.Token as RequestToken;
                _requestToken = requestToken;
                // ViewBag.id = requestToken.TokenID;
                
                //ViewBag.url = authorizeUrl;

            });
        }
  
        /// <summary>
        /// 得到学分数据
        /// </summary>
        private void GetMessage()
        {
            client.RequestRestMethodAsync(new SDNUMobile.SDK.RestMethod.UniversalRestMethod.UniversalGet("GET", "score/get", "", "", "1", "1", "1", "1", "year", "term", "1", "1", "1", "1"), result =>
            {
                try
                {
                    people = result.Result.ToString();
                }
                catch (Exception ex)
                {

                }
            });
        }
        
        /// <summary>
        /// 得到accesstoken
        /// </summary>
        private void GetAccessToken()
        {
            String callback = callbackUrl + "#oauth_token=" + Request.QueryString["oauth_token"] + "&oauth_verifier=" + Request.QueryString["oauth_verifier"];
            verifier = client.GetVerifierFromCallbackUrl(callback);
            System.Threading.Thread.Sleep(1000);
            //while (_accessToken == null)
            //{
            try
            {
                client.RequestAccessTokenAsync(_requestToken, verifier, result =>
                {
                    AccessToken accessToken = result.Token as AccessToken;
                    _accessToken = accessToken;
                    System.Threading.Thread.Sleep(100);
                });
            }
            catch (Exception ex)
            {
                //
            }
            System.Threading.Thread.Sleep(1000);
        }
        #endregion

        #region 公有方法
        
        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult oauth()
        {
            //if (_requestToken == null)
            //{
            GetRequestToken();
            while (_requestToken==null)
            {              
                GetRequestToken();
                System.Threading.Thread.Sleep(100);
            }
            authorizeUrl = client.GetAuthorizeUrl(_requestToken);
            ViewBag.url = authorizeUrl;
            //}
            //else
            //{
            //    authorizeUrl = client.GetAuthorizeUrl(_requestToken);
            //    Response.Redirect(authorizeUrl, false);
            //}
            return View();
        }

        /// <summary>
        /// 获得callback地址
        /// </summary>
        /// <returns></returns>
        public ActionResult callback()
        {
            return View();
        }

        /// <summary>
        /// 数据页面
        /// </summary>
        /// <returns></returns>

        public ActionResult oauthview()
        {
            GetAccessToken();
            GetMessage();
            while (people == null)
            {
                System.Threading.Thread.Sleep(100);
                GetMessage();
            }
            ViewBag.pn = people;
            return View();
        }

        #endregion
    }
}