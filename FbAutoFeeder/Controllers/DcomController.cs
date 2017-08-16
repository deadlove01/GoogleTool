using DotRas;
using log4net;
using Newtonsoft.Json;
using RaviLib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FbAutoFeeder.Controllers
{
    public class DcomController : Singleton<DcomController>
    {
        private static readonly ILog logger =
              LogManager.GetLogger(typeof(DcomController));

        private RasDialer dialer;

        public string IP { get; set; }
        public string ProfileName { get; set; }

        public EventHandler<string> UpdateLogEvent;
        public DcomController()
        {
            ProfileName = string.Empty; ;

        }


        public string Connect(string profileName)
        {
            this.ProfileName = profileName;
            string ip = "IP IS UNKNOWN";
            if (UpdateLogEvent != null)
            {
                UpdateLogEvent(this, "prepare to get new ip...");
            }
            try
            {
                string pbkPath = Directory.GetCurrentDirectory() + "\\myphonebook.pbk";
                dialer = new RasDialer();
                dialer.EntryName = profileName;
                dialer.PhoneBookPath = pbkPath;

                RasPhoneBook pbk = new RasPhoneBook();
                RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

                pbk.Open(pbkPath);
                dialer.PhoneBookPath = pbkPath;

                dialer.Dial();

                using (WebClient web = new WebClient())
                {
                    string result = web.DownloadString("https://ipinfo.io/json");
                    logger.Info("ipinfo: " + result);

                    dynamic obj = JsonConvert.DeserializeObject<dynamic>(result);
                    ip = obj.ip;
                    IP = ip;
                    if(UpdateLogEvent != null)
                        UpdateLogEvent(this, "new ip: " + ip);
                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, stacktrace: {1}", ex.Message, ex.StackTrace);
            }

            return ip;
        }

        public void Reconnect()
        {
            try
            {
                Disconnect(ProfileName);
                Connect(ProfileName);
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, stacktrace: {1}", ex.Message, ex.StackTrace);
            }
        }


        public void Disconnect(string name)
        {
            try
            {
                foreach (var item in RasConnection.GetActiveConnections())
                {
                    if (item.EntryName == name)
                    {
                        item.HangUp();
                    }
                    logger.Info(item.EntryName + "|" + item.PhoneBookPath);
                }


                if (dialer != null)
                    dialer.Dispose();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, stacktrace: {1}", ex.Message, ex.StackTrace);
            }
        }

        public void CloseAll()
        {
            try
            {
                foreach (var item in RasConnection.GetActiveConnections())
                {
                    item.HangUp();
                }


                if (dialer != null)
                    dialer.Dispose();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, stacktrace: {1}", ex.Message, ex.StackTrace);
            }
        }



    }
}
