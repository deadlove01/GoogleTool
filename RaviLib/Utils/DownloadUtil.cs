using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RaviLib.Utils
{
    public class DownloadUtil
    {
        public static bool DownloadImage(string imageUrl, string destinationPath)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 53.0.2785.143 Safari / 537.36");
                client.DownloadFile(imageUrl, destinationPath);
                return true;
            }
            return false;
        }
    }
}
