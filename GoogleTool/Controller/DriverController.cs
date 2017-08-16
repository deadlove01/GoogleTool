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

namespace GoogleTool.Controller
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
            Login();
        }




        public bool Login()
        {
            try
            {
                // login url
                string loginUrl = "https://accounts.google.com/ServiceLogin";
                driver.Navigate().GoToUrl(loginUrl);

                Thread.Sleep(3000);
                string email = "deadlove011011@gmail.com";
                string pass = "lethibaongoc27";

                var emailInput = driver.FindElementById("identifierId");
                emailInput.Clear();
                emailInput.SendKeys(email);
                Thread.Sleep(1500);

                //identifierNext
                var divNext = driver.FindElementById("identifierNext");
                divNext.Click();
                Thread.Sleep(5000);

                var passInput = driver.FindElementByName("password");
                passInput.Clear();
                passInput.SendKeys(pass);
                Thread.Sleep(1500);

                divNext = driver.FindElementById("passwordNext");
                divNext.Click();
                Thread.Sleep(5000);

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
            driverService.HideCommandPromptWindow = true;
            driver = new ChromeDriver(driverService, options);
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
