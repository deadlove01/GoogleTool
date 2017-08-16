using FbAutoFeeder.Models;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FbAutoFeeder.Controllers
{
    public class RequestController : IDisposable
    {
        public EventHandler<string> NotifyLogLabelUI;
        private static readonly ILog logger = LogManager.GetLogger(typeof(RequestController));

        private CustomWeb web = null;
        private LoginFbInfo fbInfo;

        private string userAgent;
        public RequestController()
        {
            //UserAgentTool tool = new UserAgentTool();
            //string redirectUrl = string.Empty;
            //var agent = tool.RandomUserAgent("desktop", "windows", "browser", new string[] { "chrome", "firefox", "safari" });
            //Console.WriteLine("full: "+ agent);
            //CustomWeb web = new CustomWeb(agent.agent_string);
            //string result = web.SendRequest("https://www.facebook.com/help/", "GET", "www.facebook.com", null, ref redirectUrl, false, true);
            //Thread.Sleep(5000);
            //result = web.SendRequest("https://www.facebook.com/help/", "GET", "www.facebook.com", null, ref redirectUrl, false, false);
            //Thread.Sleep(5000);
            //NameValueCollection nvc = new NameValueCollection();
            ////trn_725750_1987@mailfree.host|xpadxjh5nt1987
            //nvc.Add("email", "minhtuan@bangtuoc.vn");
            //nvc.Add("pass", "bangtuoc@123");


            //redirectUrl = string.Empty;
            //result = web.SendRequest("https://www.facebook.com/login.php?login_attempt=1", "POST",
            //    "www.facebook.com", nvc, ref redirectUrl, true, false, "application/x-www-form-urlencoded");

            //Console.WriteLine("login result: "+ redirectUrl);
            //if(!redirectUrl.Contains("checkpoint"))
            //{
            //    result = web.SendRequest("https://www.facebook.com/", "GET", "www.facebook.com", null, ref redirectUrl, false, false);
            //}

            //Console.WriteLine("terst");
            web = new CustomWeb();
        }

        public RequestController(string userAgent)
        {
            this.userAgent = userAgent;
            web = new CustomWeb(userAgent);
        }
        Random rand = new Random();
        public bool Start(LoginFbInfo fbInfo)
        {
            this.fbInfo = fbInfo;
            try
            {
                
                string redirectUrl = string.Empty;
                string result = web.SendRequest("https://www.facebook.com/help/", "GET", "www.facebook.com", null, ref redirectUrl, false, true);
                Thread.Sleep(1000 * rand.Next(1, 5));

                result = web.SendRequest("https://www.facebook.com/help/", "GET", "www.facebook.com", null, ref redirectUrl, false, false);
                Thread.Sleep(1000 * rand.Next(1, 5));
                string loginResult = Login();
                if(!string.IsNullOrEmpty(loginResult) && !loginResult.Contains("checkpoint"))
                {
                    string refStr = string.Empty;
                    Thread.Sleep(1000 * rand.Next(1, 5));
                    result = web.SendRequest(loginResult, "GET",
                    "www.facebook.com", null, ref refStr, false, false);
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (NotifyLogLabelUI != null)
                {
                    NotifyLogLabelUI(this, "Error: "+ ex.Message);
                }
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
            return false;
        }


        

        public string GetFbCookie()
        {
            if (NotifyLogLabelUI != null)
            {
                NotifyLogLabelUI(this, "Get fb cookie...");
            }

            Random rand = new Random();
            Thread.Sleep(1000 * rand.Next(1, 3));
            string redirectUrl = string.Empty;
            string result = web.SendRequest("https://www.facebook.com/", "GET", "www.facebook.com", null, ref redirectUrl, false, false);
            
            string fbCookie = string.Empty;
            foreach (var item in web.CookieContainer.GetCookies(new Uri("https://www.facebook.com")))
            {
                Console.WriteLine(item);
                fbCookie += item + ";";
                //if (item.Domain.EndsWith(".facebook.com"))
                //{
                //fbCookie += item.Name + "=" + item.Value + "; ";
                //}
                //Console.WriteLine(item.Name + "|" + item.Value);
            }
            return fbCookie;
        }

        #region private methods

        private string Login()
        {
            string result = null;
            try
            {

                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("email", fbInfo.Email);
                nvc.Add("pass", fbInfo.Pass);

                string redirectUrl = string.Empty;
                result = web.SendRequest("https://www.facebook.com/login.php?login_attempt=1", "POST",
                    "www.facebook.com", nvc, ref redirectUrl, true, false, "application/x-www-form-urlencoded");

                result = redirectUrl;
                if (NotifyLogLabelUI != null)
                {
                    NotifyLogLabelUI(this, "Login result: " + result);
                }
                return result;
            }
            catch (Exception ex)
            {
                if (NotifyLogLabelUI != null)
                {
                    NotifyLogLabelUI(this, "Login Error: " + ex.Message);
                }
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
            return null;
        }

        #endregion
        public void Dispose()
        {
            if(web != null)
            {
                web.Dispose();
            }
        }
    }
}
