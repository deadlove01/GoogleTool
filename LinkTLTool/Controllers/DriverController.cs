using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkTLTool.Controller
{
    public class DriverController: IDisposable
    {
        public delegate void UpdateTextDelegate(string text);

        public event UpdateTextDelegate UpdateTextEvent;
        private static readonly ILog logger = LogManager.GetLogger(typeof(DriverController));

        private ChromeDriver driver = null;

        private static int Counter = 0;
        
        public DriverController()
        {
            Counter++;
        }

        public void Run()
        {
            if(driver == null)
                Init();

            driver.Navigate().GoToUrl("http://link.tl/14CIq");
            Thread.Sleep(5000);
            string pageTitle = "link.tl";
            logger.Info("Current page index: "+ pageTitle);
            int tryCount = 30;
            do
            {
                CloseAlert();
                HasSkipButton();
                SwithToDefault(pageTitle);
                bool isLeave = IsLeaveAlert();
                //if (!isLeave)
                //{
                //    Thread.Sleep(2000);
                //    tryCount--;
                //    SwithToDefault(pageTitle);
                //}
                //else
                //{
                    if (IsNavigatedToDes())
                    {
                        break;
                    }else
                {
                    Thread.Sleep(2000);
                    tryCount--;
                    SwithToDefault(pageTitle);
                }
                //}
                if (tryCount < 0)
                    break;
                  
                   
            } while (true);

            if (UpdateTextEvent != null)
                UpdateTextEvent(Counter.ToString());
        }

        private bool IsNavigatedToDes()
        {
            try
            {
                return SwitchToWindow(driver => driver.Title.ToLower().StartsWith("facebook"));
            }
            catch (Exception ex)
            {
                logger.Error("SwitchToWindow index error: " + ex.Message);
            }
            return false;
        }

        private void SwithToDefault(string defaultTitle)
        {
            try
            {
                SwitchToWindow(driver => driver.Title.ToLower().StartsWith(defaultTitle));
                Thread.Sleep(1500);
            }
            catch (Exception ex)
            {
                logger.Error("SwithToDefault title error: "+ex.Message);
            }
        
        }

        public void SwitchToWindow(int windowIndex)
        {
            try
            {

                driver.SwitchTo().Window(driver.WindowHandles[windowIndex]);
            }
            catch (Exception ex)
            {
                logger.Error("SwitchToWindow index error: "+ex.Message);
            }
        }
        public bool SwitchToWindow(Expression<Func<IWebDriver, bool>> predicateExp)
        {
            try
            {
                var predicate = predicateExp.Compile();
                bool found = false;
                foreach (var handle in driver.WindowHandles)
                {
                    driver.SwitchTo().Window(handle);
                    Thread.Sleep(500);
                    if (predicate(driver))
                    {
                        found = true;
                        break;
                    }
                }

                return found;
            }
            catch (Exception ex)
            {
                logger.Error("SwitchToWindow express error: "+ex.Message);
            }

            return false;
        }

        private void Skip()
        {
            try
            {
                // div class = skip_btn2
                int tryCount = 30;
                do
                {
                    try
                    {
                        var div = driver.FindElementByXPath("//div[@class='skip_btn2']");
                        if (div != null)
                        {
                            DivClick(driver, div, 3000);
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        Thread.Sleep(2000);
                        tryCount--;
                    }
                    if (tryCount < 0)
                        break;
                } while (true);
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
        }
        
        private bool HasSkipButton()
        {
            try
            {
                // div class = skip_btn2
                int tryCount = 30;
                do
                {
                    CloseReloadAlert();
                    try
                    {
                        var div = driver.FindElementByXPath("//div[@class='skip_btn2']");
                        if (div != null)
                        {
                            DivClick(driver, div, 3000);
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.Message);
                        Thread.Sleep(2000);
                        tryCount--;
                    }
                    if (tryCount < 0)
                        break;
                } while (true);
                
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private void CloseReloadAlert()
        {
            try
            {
                var leaveAlert = driver.SwitchTo().Alert();
                if (leaveAlert != null)
                {
                    leaveAlert.Accept();
                    Thread.Sleep(1500);
                }
               
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Close alert error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
        }

        private bool IsLeaveAlert()
        {
            try
            {
                // div class = skip_btn2
                int tryCount = 5;
                //do
                //{
                    try
                    {
                        var leaveAlert = driver.SwitchTo().Alert();
                        if(leaveAlert != null)
                        {
                            Console.WriteLine(leaveAlert.Text);
                            leaveAlert.Accept();
                            Thread.Sleep(4000);
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    return false;
                    }
                //    if (tryCount < 0)
                //        break;
                //} while (true);
                return true;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private bool CloseAlert()
        {
            try
            {
                // div class = skip_btn2
                int tryCount = 2;
                //do
                //{
                try
                {
                    var leaveAlert = driver.SwitchTo().Alert();
                    if (leaveAlert != null)
                    {
                        Console.WriteLine(leaveAlert.Text);
                        leaveAlert.Accept();
                        Thread.Sleep(4000);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                //    if (tryCount < 0)
                //        break;
                //} while (true);
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
            driver = new ChromeDriver(driverService, options);
        }


        public void Dispose()
        {
            if(driver != null)
            {
                driver.Quit();
            }
        }



        #region private methods


        private IWebElement GetElement(IWebDriver driver, By by, int sleep = 5)
        {
            if (sleep > 0)
            {
                //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(15));
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
    }
}
