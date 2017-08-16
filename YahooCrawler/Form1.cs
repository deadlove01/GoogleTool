using log4net;
using log4net.Config;
using RaviLib.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YahooCrawler.Controller;
using YahooCrawler.Models;
using YahooFinance.NET;

namespace YahooCrawler
{
    public partial class Form1 : Form
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Form1));

        private string cookie; // go to url https://finance.yahoo.com/quote/AFI.AX/history?p=AFI.AX, then get cookie

        /*
         * 
         * 
         * */
        private string crumb;

        private string symbol;
        //
        public Form1()
        {
            InitializeComponent();

            XmlConfigurator.Configure();
        }
        //https://finance.yahoo.com/quote/C/history?period1=1467907602&period2=1499443602&interval=1d&filter=history&frequency=1d&ignore=.csv
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string rootPath = Directory.GetCurrentDirectory() + "\\Data\\";
                if (!Directory.Exists(rootPath))
                    Directory.CreateDirectory(rootPath);

                DriverController ctrl = new DriverController();
                ctrl.Start();
                ctrl.GetCookieAndCrumb(symbol, ref cookie, ref crumb);

                Crawler crawler = new Crawler(cookie, crumb);
                GetAllData(crawler, symbol, 0, 5);
                Thread.Sleep(5000);
                GetAllData(crawler, symbol, 1, 0);
                Thread.Sleep(5000);
                GetAllData(crawler, symbol, 5, 0);
                Thread.Sleep(5000);

                ctrl.Dispose();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
        }

        private void GetAllData(Crawler crawler, string symbol, int year, int days)
        {
            string rootPath = Directory.GetCurrentDirectory() + "\\Data\\";
            DateTime now = DateTime.Now;
            DateTime start = DateTimeUtil.GetCustomDate(now.AddYears(year * -1).AddDays(days * -1));
            DateTime end = DateTimeUtil.GetCustomDate(now, 23, 59, 59);

            // 5D
            string history = crawler.GetHistoricalData(symbol, start, end);
            Thread.Sleep(2000);
            string dividend = crawler.GetDividendData(symbol, start, end);
            Thread.Sleep(2000);
            string split = crawler.GetStockSplitData(symbol, start, end);
            Thread.Sleep(2000);

            // save file
            string historyName = $"{symbol}_HISTORY.csv";
            string dividendName = $"{symbol}_DIVIDEND.csv";
            string splitName = $"{symbol}_SPLIT.csv";

            if(year != 0)
            {
                historyName = $"{symbol}_{year}Y_HISTORY.csv";
                dividendName = $"{symbol}_{year}Y_DIVIDEND.csv";
                splitName = $"{symbol}_{year}Y_SPLIT.csv";
            }
            else
            {
                historyName = $"{symbol}_{days}D_HISTORY.csv";
                dividendName = $"{symbol}_{days}D_DIVIDEND.csv";
                splitName = $"{symbol}_{days}D_SPLIT.csv";
            }

            File.WriteAllLines(rootPath + historyName, history.Split('\n'));
            File.WriteAllLines(rootPath + dividendName, dividend.Split('\n'));
            File.WriteAllLines(rootPath + splitName, split.Split('\n'));
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnRun.Enabled = true;
            MessageBox.Show("Done!");
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            symbol = tbSymbol.Text.Trim().ToUpper();
            if(symbol.Length == 0)
            {
                MessageBox.Show("Symbol cannot be empty!");
                return;
            }

            btnRun.Enabled = false;
            bgWorker.RunWorkerAsync();
        }
    }
}
