using RaviLib.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YahooCrawler.Models
{
    public class Crawler
    {
        public string Cookie { get; set; }
        public string Crumb { get; set; }
        public Crawler()
        {

        }

        public Crawler(string cookie, string crumb)
        {
            this.Cookie = cookie;
            this.Crumb = crumb;
        }
      

        public string GetHistoricalData(string symbol, DateTime start, DateTime end)
        {
            string data = string.Empty;

            // make sure use .net framework 4.6.2
            long startSecondUnix = (new DateTimeOffset(start)).ToUnixTimeSeconds();
            long endSecondUnix = (new DateTimeOffset(end)).ToUnixTimeSeconds();
            using (var client = new WebClient())
            {
                string url = $"https://query1.finance.yahoo.com/v7/finance/download/C?period1=" +
                    $"{startSecondUnix}&period2={endSecondUnix}&interval=1d&events=history&crumb={Crumb}";
                client.Headers.Add(HttpRequestHeader.Cookie, Cookie);
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                client.Headers.Add(HttpRequestHeader.Host, "query1.finance.yahoo.com");
                data = client.DownloadString(url);
            }

            return data;
        }

        public string GetStockSplitData(string symbol, DateTime start, DateTime end)
        {
            string data = string.Empty;
            // make sure use .net framework 4.6.2
            long startSecondUnix = (new DateTimeOffset(start)).ToUnixTimeSeconds();
            long endSecondUnix = (new DateTimeOffset(end)).ToUnixTimeSeconds();

            using (var client = new WebClient())
            {
                string url = $"https://query1.finance.yahoo.com/v7/finance/download/C?period1=" +
                     $"{startSecondUnix}&period2={endSecondUnix}&interval=1d&events=split&crumb={Crumb}";
                client.Headers.Add(HttpRequestHeader.Cookie, Cookie);
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                client.Headers.Add(HttpRequestHeader.Host, "query1.finance.yahoo.com");
                data = client.DownloadString(url);
            }

            return data;
        }

        public string GetDividendData(string symbol, DateTime start, DateTime end)
        {
            string data = string.Empty;
            // make sure use .net framework 4.6.2
            long startSecondUnix = (new DateTimeOffset(start)).ToUnixTimeSeconds();
            long endSecondUnix = (new DateTimeOffset(end)).ToUnixTimeSeconds();

            using (var client = new WebClient())
            {
                string url = $"https://query1.finance.yahoo.com/v7/finance/download/C?period1=" +
                    $"{startSecondUnix}&period2={endSecondUnix}&interval=1d&events=div&crumb={Crumb}";
                client.Headers.Add(HttpRequestHeader.Cookie, Cookie);
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                client.Headers.Add(HttpRequestHeader.Host, "query1.finance.yahoo.com");
                data = client.DownloadString(url);
            }

            return data;
        }
    }
}
