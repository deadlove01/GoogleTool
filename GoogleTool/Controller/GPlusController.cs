using GoogleTool.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTool.Controller
{
    public class GPlusController: Singleton<GPlusController>
    {
        public GPlusController()
        {

        }
        public void GetMyInfo(string accessToken)
        {
            using (WebClient client = new WebClient())
            {
        //                GET https://www.googleapis.com/plus/v1/people/me HTTP/1.1
                //User - Agent: plus24h google-api - dotnet - client / 1.14.0.0(gzip)
                //Authorization: Bearer access_token
                //        Host: www.googleapis.com
                //Accept - Encoding: gzip, deflate
                client.Headers.Add(HttpRequestHeader.Host, "www.googleapis.com");
                client.Headers.Add(HttpRequestHeader.UserAgent, "google-api-dotnet-client/1.14.0.0 (gzip)");
                client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + accessToken);
                string result = client.DownloadString("https://www.googleapis.com/plus/v1/people/me");

                Console.WriteLine("my info: "+result);
            }
        }

        

        public void Comment()
        {

        }

        public void Plus1()
        {

        }

        public void Share()
        {

        }
    }
}
