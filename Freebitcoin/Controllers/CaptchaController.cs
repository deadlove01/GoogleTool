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
using Tesseract;

namespace Freebitcoin.Controller
{
    public class CaptchaController : IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CaptchaController));

        private TesseractEngine engine = null;
        public CaptchaController()
        {
            engine = new TesseractEngine(Directory.GetCurrentDirectory() + @"\\tessdata", "eng", EngineMode.Default);
        }

        public void Dispose()
        {
            engine.Dispose();
        }

        public bool SolveCatpcha(string imagePath, ref string result)
        {
            try
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        var text = page.GetText();
                        if(string.IsNullOrEmpty(text))
                        {
                            result = "";
                            return false;
                        }else
                        {
                            result = text.Replace("\n", " ");
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }

            return false;
        }

    }
}
