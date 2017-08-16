using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YahooCrawler.Controller
{
    public class DriverController: IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DriverController));

        private ChromeDriver driver = null;

        public DriverController()
        {

        }

        public void Start()
        {
            if(driver == null)
                Init();

            driver.Navigate().GoToUrl("https://plus.google.com");
            Thread.Sleep(5000);
            //Login();
        }




        public bool Login()
        {
            try
            {
                // login url
                string loginUrl = "https://finance.yahoo.com/quote/C/history?p=C";
                driver.Navigate().GoToUrl(loginUrl);

                Thread.Sleep(3000);
                var cookies = driver.Manage().Cookies;
                foreach (var cc in cookies.AllCookies)
                {
                    Console.WriteLine(cc.Domain +"|"+cc.Name +"|"+cc.Value+"|"+cc.Expiry);
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }


        private void Init()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArguments("excludeSwitches", "enable-automation");

            var driverService = ChromeDriverService.CreateDefaultService();
            //driverService.HideCommandPromptWindow = true;
            driver = new ChromeDriver(driverService, options);
        }

        public string GeCookie(string symbol)
        {
            string url = $"https://finance.yahoo.com/quote/{symbol}/history?p={symbol}";
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(5000);
            string fbCookie = string.Empty;
            foreach (var item in driver.Manage().Cookies.AllCookies)
            {
                if (item.Domain.EndsWith(".yahoo.com"))
                {
                    fbCookie += item.Name + "=" + item.Value + "; ";
                }
                Console.WriteLine(item.Name + "|" + item.Value);
            }
            return fbCookie;
        }


        public void GetCookieAndCrumb(string symbol, ref string cookie, ref string crumb)
        {
            string url = $"https://finance.yahoo.com/quote/{symbol}/history?p={symbol}";
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(5000);
            cookie = string.Empty;
            foreach (var item in driver.Manage().Cookies.AllCookies)
            {
                if (item.Domain.EndsWith(".yahoo.com"))
                {
                    cookie += item.Name + "=" + item.Value + "; ";
                }
                Console.WriteLine(item.Name + "|" + item.Value);
            }

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(driver.PageSource);

            var atag = doc.DocumentNode.SelectSingleNode("//a[starts-with(@href, 'https://query1.finance.yahoo.com/v7/finance/download/')]");
            if (atag != null)
            {
                Uri uri = new Uri(atag.GetAttributeValue("href", string.Empty).Replace("amp;", ""));


                var queryDictionary = System.Web.HttpUtility.ParseQueryString(uri.Query);
                Console.WriteLine(queryDictionary.Get("crumb"));
                crumb = queryDictionary.Get("crumb");
            }
        }
        public string GetCrumb(string symbol)
        {
            string url = $"https://finance.yahoo.com/quote/{symbol}/history?p={symbol}";
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(5000);
            string crumb = string.Empty;

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(driver.PageSource);

            var atag = doc.DocumentNode.SelectSingleNode("//a[starts-with(@href, 'https://query1.finance.yahoo.com/v7/finance/download/')]");
            if(atag != null)
            {

            }

            return crumb;
        }


        public void Dispose()
        {
            if(driver != null)
            {
                driver.Quit();
            }
        }
    }
}
