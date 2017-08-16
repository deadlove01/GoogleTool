using FbAutoFeeder.Models;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FbAutoFeeder.Controllers
{
    public class DriverController : IDisposable
    {
        public EventHandler<string> NotifyLogLabelUI;
        private static readonly ILog logger = LogManager.GetLogger(typeof(DriverController));

        private ChromeDriver driver = null;
        private LoginFbInfo fbInfo;

        private string userAgent;
        public DriverController()
        {

        }

        public DriverController(string userAgent)
        {
            this.userAgent = userAgent;
        }

        public bool Test()
        {
            try
            {
                if (driver == null)
                {
                    Init();
                }

                string url = "https://www.facebook.com/help";
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(3000);
                Console.WriteLine("fdsfsd");
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
            return false;
        }

        public bool Start(LoginFbInfo fbInfo)
        {
            this.fbInfo = fbInfo;
            try
            {
                if(driver == null)
                {
                    Init();
                }


                return CheckLogin();

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
            driver.Navigate().GoToUrl("https://facebook.com");
            Thread.Sleep(2000);
            string fbCookie = string.Empty;
            foreach (var item in driver.Manage().Cookies.AllCookies)
            {
                if (item.Domain.EndsWith(".facebook.com"))
                {
                    fbCookie += item.Name + "=" + item.Value + "; ";
                }
                Console.WriteLine(item.Name + "|" + item.Value);
            }
            return fbCookie;
        }

        #region private methods

        private bool CheckLogin()
        {
            try
            {
                if (NotifyLogLabelUI != null)
                {
                    NotifyLogLabelUI(this, "Check login fb...");
                }
                Thread.Sleep(2000);
                driver.Navigate().GoToUrl("https://facebook.com");
                Thread.Sleep(2000);

                var emailInput = GetElement(driver, By.Name("email"));
                if(emailInput!= null)
                {
                    emailInput.Clear();
                    emailInput.SendKeys(fbInfo.Email);
                    Thread.Sleep(1500);
                }

                try
                {
                    //var nextBtn = GetElement(driver, By.Name("next"));
                    var nextBtn = driver.FindElementByName("next");
                    if (nextBtn != null)
                    {
                        DivClick(driver, nextBtn, 2000);
                    }
                }
                catch 
                {

                }
                

                var passInput = driver.FindElementByName("pass");
                if (passInput != null)
                {
                    passInput.Clear();
                    passInput.SendKeys(fbInfo.Pass);
                    Thread.Sleep(1500);
                }

                var loginBtn = driver.FindElementByName("login");
                if(loginBtn != null)
                {
                    DivClick(driver, loginBtn, 2000);
                }

                if (driver.Url.Contains("checkpoint"))
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                if (NotifyLogLabelUI != null)
                {
                    NotifyLogLabelUI(this, "Login Error: " + ex.Message);
                }
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
            return false;
        }
        private void Init()
        {
            if (NotifyLogLabelUI != null)
            {
                NotifyLogLabelUI(this, "Init web driver...");
            }
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArguments("excludeSwitches", "enable-automation");
            options.AddExtension(Directory.GetCurrentDirectory() + "\\Swap My Cookies.crx");
            if(!string.IsNullOrEmpty(userAgent))
            {
                options.AddArguments("--user-agent="+userAgent);
                //options.AddArguments("--user-agent=Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3_2 like Mac OS X; en-u");
                Console.WriteLine("user agent: "+userAgent);
            }
            //  options.AddArgument("--lang=en-au");
            var driverService = ChromeDriverService.CreateDefaultService();
            //driverService.HideCommandPromptWindow = true;
            driver = new ChromeDriver(driverService, options);
            //IsStarted = true;
        }

        private IWebElement GetElement(IWebDriver driver, By by, int sleep = 5)
        {
            if (sleep > 0)
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(sleep));
                //return wait.Until(drv => drv.FindElement(by));
                return wait.Until(ExpectedConditions.ElementExists(by));
            }
            return driver.FindElement(by);

        }

        private IWebElement GetElementWaitForClickable(IWebDriver driver, By by, int sleep = 5)
        {
            if (sleep > 0)
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(sleep));
                //return wait.Until(drv => drv.FindElement(by));
                return wait.Until(ExpectedConditions.ElementToBeClickable(by));
            }
            return driver.FindElement(by);

        }

        private void DivClick(IWebDriver driver, IWebElement div, int sleep = 0)
        {
            try
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(div);
                actions.Click();
                actions.Build().Perform();

                if (sleep > 0)
                    Thread.Sleep(sleep);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }

        }


        #endregion
        public void Dispose()
        {
            if(driver != null)
            {
                driver.Quit();
            }
        }
    }
}
