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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Freebitcoin.Controller
{
    public class DriverController: IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(DriverController));

        private FirefoxDriver driver = null;
        
        public DriverController()
        {

        }

        public void Start()
        {
            if(driver == null)
                Init();

            driver.Navigate().GoToUrl("https://freebitco.in/?op=home");
            Thread.Sleep(5000);

            var signInBtn = driver.FindElementByXPath("//li[@class='login_menu_button']/a");
            if(signInBtn != null)
            {
                signInBtn.Click();
                Thread.Sleep(2000);
            }
            CheckLogin();
        }

        
        private bool CheckLogin()
        {
            try
            {
                string email = "deadlove011011@gmail.com";
                string pass = "19001560";

                var emailInput = driver.FindElementById("login_form_btc_address");
                emailInput.Clear();
                emailInput.SendKeys(email);
                Thread.Sleep(1000);

                var passInput = driver.FindElementById("login_form_password");
                passInput.Clear();
                passInput.SendKeys(pass);
                Thread.Sleep(1000);

                var btnLogin = driver.FindElementById("login_button");
                btnLogin.Click();
                Thread.Sleep(5000);

                return true;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }



        private bool IsCaptchaCorrect()
        {
            var errorTag = driver.FindElementById("free_play_error");
            if(errorTag != null)
            {
                string errorText = errorTag.Text.Trim().ToLower();
                if(errorText.Equals("incorrect captcha entered"))
                {
                    return false;
                }
            }

            return true;
        }

        private bool FillCaptchaText(string capText)
        {
            var captInput = driver.FindElementById("adcopy_response");
            captInput.Clear();
            captInput.SendKeys(capText);
            Thread.Sleep(5000);

            if(!IsCaptchaCorrect())
            {
                return false;
            }

            //submit captcha text
            var btnRoll = driver.FindElementById("free_play_form_button");
            btnRoll.Click();
            Thread.Sleep(5000);

            return true;
        }
        public bool GetFreeBtc()
        {
            try
            {
                string url = "https://freebitco.in/?op=home";
                driver.Navigate().GoToUrl(url);
                Thread.Sleep(3000);

                CheckLogin();

                // solve capcha
                var selectOptions = driver.FindElementById("free_play_captcha_types");
                if(selectOptions != null)
                {
                    SelectElement selector = new SelectElement(selectOptions);
                    selector.SelectByText("Solve Media");
                    Thread.Sleep(1500);

                    //IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                    //js.ExecuteScript("window.scrollBy(0,10000);", driver);
                    //double val = .1;
                    //string script = string.Format("window.scrollTo(0, document.body.scrollHeight/%s);", val);
                    //js.ExecuteScript(script, driver);
                    // driver.ExecuteScript("window.scrollTo(0, document.body.scrollHeight/0.1)");
                    // Thread.Sleep(1500);
                }


                Random rand = new Random();
                CaptchaController captchaCtrl = new CaptchaController();
                string desPath = Directory.GetCurrentDirectory() + "\\captcha.png";  
                var captchaTag = driver.FindElementById("adcopy-puzzle-image");
                do
                {
                    int randSec = rand.Next(1, 7);
                    TakeElementScreenShort(captchaTag, desPath);
                    string result = string.Empty;
                    captchaCtrl.SolveCatpcha(desPath, ref result);
                    if(!string.IsNullOrEmpty(result))
                    {
                        logger.Info("captcha text: " + result);
                        bool submitResult = FillCaptchaText(result);
                        if(submitResult)
                            break;
                        else
                        {
                            Thread.Sleep(randSec * 1000);
                        }
                    }else
                    {
                        Thread.Sleep(randSec * 1000);
                    }
                } while (true);

                return true;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }


       public void Test(string xpath)
        {
            try
            {
                var captchaTag = driver.FindElementByXPath(xpath);
                string desPath = Directory.GetCurrentDirectory() + "\\captcha.png";
                TakeElementScreenShort(captchaTag, desPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
       
        }

        public Bitmap TakeElementScreenShort(IWebElement element, string desPath)
        {
            //Screenshot sc = ((ITakesScreenshot)driver).GetScreenshot();
            //var img = Image.FromStream(new MemoryStream(sc.AsByteArray)) as Bitmap;
            //img.Save(Directory.GetCurrentDirectory()+"\\temp.png");
            //Size size = element.Size;
            ////size.Width = 300;\


            //Bitmap bmp = new Bitmap(size.Width, size.Height);
            //using (Graphics gph = Graphics.FromImage(bmp))
            //{
            //    gph.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), new Rectangle(element.Location, size)
            //        , GraphicsUnit.Pixel);
            //}

            ////var eImg = img.Clone(new Rectangle(element.Location, size), img.PixelFormat);
            //bmp.Save(desPath);
            //return bmp;


            string tempPath = Directory.GetCurrentDirectory() + "\\temp.png";
            Screenshot screenshot = ((ITakesScreenshot)this.driver).GetScreenshot();
            screenshot.SaveAsFile(tempPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            Image img = Bitmap.FromFile(tempPath);
            Rectangle rect = new Rectangle();
            //760|480|380|160
            int x = 760;
            int y = 480;
            int w = 380;
            int h = 160;
            if (element != null)
            {
                // Get the Width and Height of the WebElement using
                int width = element.Size.Width;
                int height = element.Size.Height;
                width = w;
                height = h;
                // Get the Location of WebElement in a Point.
                // This will provide X & Y co-ordinates of the WebElement
                Point p = element.Location;
                p = new Point(x, y);
                // Create a rectangle using Width, Height and element location
                rect = new Rectangle(p.X, p.Y, width, height);
            }

            Console.WriteLine(rect.X + "|"+rect.Y+"|"+rect.Width+"|"+rect.Height);
            // croping the image based on rect.
            Bitmap bmpImage = new Bitmap(w, h);
            using (Graphics gph = Graphics.FromImage(bmpImage))
            {
                gph.DrawImage(img, new Rectangle(0, 0, bmpImage.Width, bmpImage.Height), 
                    rect
                    , GraphicsUnit.Pixel);
            }

            //var eImg = img.Clone(new Rectangle(element.Location, size), img.PixelFormat);
            bmpImage.Save(desPath);
            //var cropedImag = bmpImage.Clone(rect, bmpImage.PixelFormat);

            return bmpImage;
        }
        private void Init()
        {
            //ChromeOptions options = new ChromeOptions();
            //options.AddExcludedArguments("excludeSwitches", "enable-automation");

            //var driverService = ChromeDriverService.CreateDefaultService();
            ////driverService.HideCommandPromptWindow = true;
            //driver = new ChromeDriver(driverService, options);
            driver = new FirefoxDriver();

            driver.Manage().Window.Maximize();
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
