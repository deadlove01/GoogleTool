using log4net;
using RaviLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkTLTool.Controllers
{
    public class HotspotShieldController : Singleton<HotspotShieldController>, IDisposable
    {
        private static readonly ILog logger =
            LogManager.GetLogger(typeof(HotspotShieldController));

        static AutoItX3Lib.AutoItX3 au3;
        private const string APP_TITLE = "Hotspot Shield";
        public HotspotShieldController()
        {
                
        }


        public bool ClickConnect()
        {
            try
            {
                //Start();
                if (au3 == null)
                    au3 = new AutoItX3Lib.AutoItX3();

                au3.WinActivate(APP_TITLE);
                Thread.Sleep(1000);

                int xx= au3.WinGetPosX(APP_TITLE);
                int yy = au3.WinGetPosY(APP_TITLE);
                int w = au3.WinGetPosWidth(APP_TITLE);
                int h = au3.WinGetPosHeight(APP_TITLE);
                //au3.MouseClick("left", xx + w/2, yy + h/2, 5, 5);
                au3.MouseClick("left", xx + w -55, yy + 80, 1, 5);
                Thread.Sleep(5000);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool ClickConnectNewVersion()
        {
            try
            {
                //Start();
                if (au3 == null)
                    au3 = new AutoItX3Lib.AutoItX3();

                au3.WinActivate(APP_TITLE);
                Thread.Sleep(1000);

                int xx = au3.WinGetPosX(APP_TITLE);
                int yy = au3.WinGetPosY(APP_TITLE);
                int w = au3.WinGetPosWidth(APP_TITLE);
                int h = au3.WinGetPosHeight(APP_TITLE);
                au3.MouseClick("left", xx + w/2, yy + h/2, 1, 5);
                //au3.MouseClick("left", xx + w - 55, yy + 80, 1, 5);
                Thread.Sleep(7000);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public bool DisconnectNewVersion()
        {
            try
            {
                //Start();

                if (au3 == null)
                    au3 = new AutoItX3Lib.AutoItX3();

                au3.WinActivate(APP_TITLE);
                Thread.Sleep(1000);

                int xx = au3.WinGetPosX(APP_TITLE);
                int yy = au3.WinGetPosY(APP_TITLE);
                int w = au3.WinGetPosWidth(APP_TITLE);
                int h = au3.WinGetPosHeight(APP_TITLE);
                //au3.MouseClick("left", xx + w/2, yy + h/2, 5, 5);
                au3.MouseClick("left", xx + w / 2, yy + 120, 1, 5);
                Thread.Sleep(5000);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }


        private bool Start()
        {
            try
            {
                if(au3 == null)
                    au3 = new AutoItX3Lib.AutoItX3();

                if (au3.WinExists(APP_TITLE) == 0)
                {
                    var path = ProgramFilesx86() + "\\Hotspot Shield\\bin\\hsscp.exe";
                    logger.Info("path: " + path);
                    au3.Run(path, "", au3.SW_SHOW);
                    Thread.Sleep(8000);
                }
              
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }


        private string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
        public void Dispose()
        {
        }
    }
}
