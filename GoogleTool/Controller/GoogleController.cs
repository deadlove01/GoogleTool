using Awesomium.Core;
using Awesomium.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Plus.v1;
using Google.Apis.Util.Store;
using GoogleTool.Model;
using GoogleTool.Properties;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace GoogleTool.Controller
{
    public class GoogleController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(GoogleController));

        private string authCode = string.Empty;
        private string token = string.Empty;
        private AuthModel authData;

        string[] scopes = new string[] {
                       "https://www.googleapis.com/auth/plus.login",
        "https://www.googleapis.com/auth/userinfo.email",
        "https://www.googleapis.com/auth/plus.me",
        "https://www.googleapis.com/auth/plus.circles.read",
        "https://www.googleapis.com/auth/plus.me",
        "https://www.googleapis.com/auth/youtubepartner",
        "https://www.googleapis.com/auth/youtube",
        "https://www.googleapis.com/auth/youtube.force-ssl"
            };
        #region public methods
        public void GetAuthCode(WebControl webCtrl)
        {
            webCtrl.LoadingFrameComplete += WebCtrl_LoadingFrameComplete;
            //    string[] scopes = new string[] {PlusService.Scope.PlusLogin,
            //         PlusService.Scope.UserinfoEmail,
            //         PlusService.Scope.UserinfoProfile};
            //    // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
            //    UserCredential credential =
            //            GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            //            {
            //                ClientId = Settings.Default.CLIENT_ID,
            //                ClientSecret = Settings.Default.CLIENT_SECRET
            //            },
            //                scopes,
            //                Environment.UserName,
            //                CancellationToken.None,
            //            new FileDataStore("MyGPlus.Auth.Store")
            //                ).Result;
        
            //https://www.googleapis.com/auth/plus.login 
            //https://www.googleapis.com/auth/userinfo.email 
            //https://www.googleapis.com/auth/plus.me 
            //https://www.googleapis.com/auth/plus.circles.read 
            //https://www.googleapis.com/auth/plus.me 
            //https://www.googleapis.com/auth/youtubepartner 
            //https://www.googleapis.com/auth/youtube 
            //https://www.googleapis.com/auth/youtube.force-ssl

            string scopesStr = string.Join("+", scopes);
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("client_id",  Settings.Default.CLIENT_WEB_ID);
            nvc.Add("response_type", "code");
            nvc.Add("scope", scopesStr);
            nvc.Add("redirect_uri", "https://developers.google.com/apis-explorer/oauthWindow.html");
            nvc.Add("login_hint", "");

            string paramStr = ConvertNVCToString(nvc);
            string authUrl = "https://accounts.google.com/o/oauth2/auth?" + paramStr;
            Console.WriteLine(authUrl);
            webCtrl.Source = new Uri(authUrl);
            Console.WriteLine("xong");
        }


        public void GetToken()
        {
            using (WebClient client = new WebClient())
            {
                //     POST https://accounts.google.com/o/oauth2/token HTTP/1.1
                //        User - Agent: google - api - dotnet - client / 1.14.0.0(gzip)
                //        Content - Type: application / x - www - form - urlencoded
                //        Host: accounts.google.com
                //        Content - Length: 731
                //        Connection: Keep - Alive

                string scopesStr = string.Join("+", scopes);
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("client_id", Settings.Default.CLIENT_WEB_ID);
                nvc.Add("client_secret", Settings.Default.CLIENT_WEB_SECRET);
                nvc.Add("code", authCode);
                nvc.Add("grant_type", "authorization_code");
                nvc.Add("redirect_uri", "https://developers.google.com/apis-explorer/oauthWindow.html");
                nvc.Add("scope", scopesStr);


                client.Headers.Add(HttpRequestHeader.Host, "accounts.google.com");
                client.Headers.Add(HttpRequestHeader.KeepAlive, "true");
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                client.Headers.Add(HttpRequestHeader.UserAgent, "google-api-dotnet-client/1.14.0.0 (gzip)");
                string result = client.UploadString("https://accounts.google.com/o/oauth2/token", "POST", ConvertNVCToString(nvc));
                authData = JsonConvert.DeserializeObject<AuthModel>(result);
                Console.WriteLine(result);

                Thread.Sleep(3000);
                GPlusController.Instance.GetMyInfo(authData.AccessToken);
            }
        }


        public void GetAllPosts(WebControl webCtrl)
        {
            // me: https://plus.google.com/116332687540507489784
            string url = "https://plus.google.com/communities/103386775708787324155";
            webCtrl.Source = new Uri(url);
            
            Thread.Sleep(1500);
            //while(webCtrl.IsDocumentReady)
            //{
            //    Application.DoEvents();
            //    Thread.Sleep(500);
            //}

            // var javascript = @"(function (){ 
            //       //write JavaScript code here.
            //      var tags = document.getElementsByTagName('c-wiz');  
            //      var element = document.evaluate( './/div[@role='button']' ,tags, null, XPathResult.ANY_TYPE, null ).singleNodeValue;
            //        if (element != null) {
            //          console.log(element);
            //        }

            //    })()";
            //string result  = webCtrl.ExecuteJavascriptWithResult("document.getElementsByName('button.submit')[0].click();");
            //Console.WriteLine(result);
        }

        public void ExecuteScript(WebControl webCtrl, string js)
        {
            //dynamic document = (JSObject)webCtrl.ExecuteJavascriptWithResult("document");

            //using (document)
            //{
            //    try
            //    {
            //        dynamic divList =  document.querySelectorAll("div[data-itemid^=\'update\']>div");
            //       /// Console.WriteLine(divList.length.ToString());
            //        divList[0].onclick();
            //    }
            //    catch (Exception)
            //    {

            //    }

            //}

            string result = webCtrl.ExecuteJavascriptWithResult(js.Trim());
            Console.WriteLine(result);
        }

        public string JsFireEvent(WebControl webCtrl, string getElementQuery, string eventName)
        {
           return  webCtrl.ExecuteJavascriptWithResult(@"
                        function fireEvent(element,event) {
                            var evt = document.createEvent('HTMLEvents');
                            evt.initEvent(event, true, true ); // event type,bubbling,cancelable
                            element.dispatchEvent(evt);                                 
                        }
                        " + String.Format("fireEvent({0}, '{1}');", getElementQuery, eventName));
        }


        private bool test = false;
        private void WebCtrl_LoadingFrameComplete(object sender, Awesomium.Core.FrameEventArgs e)
        {
            WebControl webCtrl = sender as WebControl;
            if (webCtrl.Source.AbsoluteUri.StartsWith("https://developers.google.com/apis-explorer/oauthWindow.html?code="))
            {
                var parsedQuery = HttpUtility.ParseQueryString(webCtrl.Source.Query);
                authCode = parsedQuery["code"];
                Console.WriteLine("authcode: " + authCode);
                Console.WriteLine(ConvertNVCToString(parsedQuery));
                webCtrl.Update();
                Thread.Sleep(1000);
                GetToken();
            }else if(webCtrl.Source.AbsoluteUri.StartsWith("https://plus.google.com/communities/103386775708787324155"))
            {
                if (!test)
                {
                    int totalPost = GetTotalPosts(webCtrl);
                    webCtrl.Update();
                    Console.WriteLine("totalpost: " + totalPost);
                    if (totalPost > 0)
                    {
                        ClickAllPost(webCtrl, totalPost);
                    }
                    string html = webCtrl.HTML;

                    test = true;
                }

            }
            Console.WriteLine(webCtrl.Source.AbsoluteUri);
        }

        private int GetTotalPosts(WebControl webCtrl)
        {
            var javascript = @"document.querySelectorAll('div[data-itemid^=\'update\']>div').length";
            string result = webCtrl.ExecuteJavascriptWithResult(javascript.Trim());
            int num = 0;
            int.TryParse(result, out num);
            return num;
        }



        private void ClickAllPost(WebControl webCtrl, int size)
        {
            while(!webCtrl.IsDocumentReady)
            {
                Application.DoEvents();
                Thread.Sleep(300);
            }
            var javascript = @"document.querySelectorAll('div[data-itemid^=\'update\']>div')[{0}].click()";
            for (int i = 0; i < size; i++)
            {
                string newJs = string.Format(javascript, i);
                    webCtrl.Invoke(new Action(()=> {
                    webCtrl.ExecuteJavascript(newJs);
                   // webCtrl.Update();
                }));
                break;
                // webCtrl.ExecuteJavascriptWithResult(newJs);
                
                Thread.Sleep(1500);
            }
        }

        #endregion

        #region private methods
        private string ConvertNVCToString(NameValueCollection nvc)
        {
            if (nvc == null)
                return "";
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            foreach (var item in nvc)
            {
                sb.AppendFormat("{0}={1}&", item.ToString(), nvc.Get(item.ToString()));
            }


            string result = sb.ToString();
            if (result == null || result == "")
                return "";
            result = result.Remove(result.LastIndexOf('&'), 1);
            return result;
        }
        #endregion  
    }
}
