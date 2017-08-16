using log4net;
using log4net.Config;
using RaviLib.Utils;
using SunfrogCrawler.Models;
using SunfrogCrawler.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SunfrogCrawler
{
    public partial class MainForm : Form
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainForm));

        private string email = string.Empty;
        private string pass = string.Empty;
        private CrawlerInfo crawlerInfo = null;

       
        public MainForm()
        {
            InitializeComponent();

            XmlConfigurator.Configure();

            LoadSetting();

            tbEmail.Text = Settings.Default.EMAIL;
            tbPass.Text = Settings.Default.PASS;
            numDelay.Value = Settings.Default.DELAY;
            numLines.Value = Settings.Default.SIZE;
        }

        private void SaveSetting()
        {
            string path = Directory.GetCurrentDirectory() + "\\crawler.xml";
            if (crawlerInfo == null)
                crawlerInfo = new CrawlerInfo();
            XmlUtil.SerializeObject<CrawlerInfo>(crawlerInfo, path);

        }

        private void LoadSetting()
        {
            string path = Directory.GetCurrentDirectory() + "\\crawler.xml";
            if(File.Exists(path))
            {
                crawlerInfo = XmlUtil.DeSerializeObject<CrawlerInfo>(path);
            }else
            {
                crawlerInfo = new CrawlerInfo();
            }
        }

        private void UpdateText(string text)
        {
            if(tbLogs.InvokeRequired)
            {
                if (text == "clr")
                {
                    tbLogs.Invoke(new Action(() => {
                        tbLogs.Text = "";
                    }));
                }else
                {
                    tbLogs.Invoke(new Action(() => {
                        tbLogs.Text = text + "\r\n" + tbLogs.Text;
                    }));
                }
               
            }else
            {
                if (text == "clr")
                {
                    tbLogs.Text = "";
                }
                else
                {
                    tbLogs.Text = text + "\r\n" + tbLogs.Text;
                }
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CustomWeb web = new CustomWeb();
                bool loginResult = Login(email, pass, web);
                if(loginResult)
                {
                    int pageSize = 100;
                    int nextIndex = crawlerInfo.CurrentIndex;
                    int counter = 1;
                    string rootPath = Directory.GetCurrentDirectory() + "\\Links\\";
                    if (!Directory.Exists(rootPath))
                    {
                        Directory.CreateDirectory(rootPath);
                    }
                    List<Sunfrog> totalList = new List<Sunfrog>();
                    do
                    {
                        List<Sunfrog> dataList = new List<Sunfrog>();
                        bool hasNext = GetNextLinks(web, ref dataList, nextIndex);
                        if(dataList.Count > 0)
                        {
                            totalList.AddRange(dataList);
                        }
                        DateTime now = DateTime.Now;
                        UpdateText(now.ToString("dd/MM/yyyy HH:mm:ss") +" : current file size: " + totalList.Count);
                        if (hasNext)
                        {
                            nextIndex += pageSize;
                            crawlerInfo.CurrentIndex = nextIndex;
                            //SaveSetting();
                            if(totalList.Count >= Settings.Default.SIZE)
                            {
                                CsvUtil.WriteObjectsToCSV(totalList, rootPath, $"links_{counter++}.csv");
                                totalList.Clear();
                                SaveSetting();
                                UpdateText("clr");
                            }
                            Thread.Sleep(1000 * Settings.Default.DELAY);
                           
                        }
                        else
                        {
                            if (totalList.Count > 0)
                            {
                                // string desPath = rootPath + $"links_{startIndex}.txt";
                                CsvUtil.WriteObjectsToCSV(totalList, rootPath, $"links_{counter++}.csv");
                                totalList.Clear();
                                SaveSetting();
                            }
                            break;
                        }
                    } while (true);
                }else
                {
                    MessageBox.Show("Login failed!");
                }
            }
            catch (Exception ex)
            {
                logger.ErrorFormat($"Error: {ex.Message}, Stack Track: {ex.StackTrace}");
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdateText("Xong");
            btnCrawl.Enabled = true;
            MessageBox.Show("Xong");
        }

        private void btnCrawl_Click(object sender, EventArgs e)
        {
            email = tbEmail.Text.Trim();
            if(string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email cannot be empty!");
                return;
            }

            pass = tbPass.Text.Trim();
            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Password cannot be empty!");
                return;
            }

            Settings.Default.EMAIL = email;
            Settings.Default.PASS = pass;
            Settings.Default.SIZE = (int)numLines.Value;
            Settings.Default.DELAY = (int)numDelay.Value;
            Settings.Default.Save();
            btnCrawl.Enabled = false;
            bgWorker.RunWorkerAsync();
        }


        public bool GetNextLinks(CustomWeb web, ref List<Sunfrog> dataList, int startIndex=1)
        {
            try
            {

                //string rootPath = Directory.GetCurrentDirectory() + "\\Links\\";
                //if(!Directory.Exists(rootPath))
                //{
                //    Directory.CreateDirectory(rootPath);
                //}
                string url = $"https://manager.sunfrogshirts.com/my-art.cfm?start={startIndex}&sortby=1&sortDir=ASC&searchfield=";
                string result = web.SendRequest(url, "GET", null);
                if(!string.IsNullOrEmpty(result))
                {
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(result);

                    var divParent = doc.DocumentNode.SelectSingleNode("//div[@id='sortable']");
                    if(divParent != null)
                    {
                       // var divs = divParent.SelectNodes(".//div[@class='col-xs-6 col-sm-4 col-md-3']");
                      //  if(divs != null && divs.Count > 0)
                        {
                            dataList = new List<Sunfrog>();
                            var divs = divParent.SelectNodes(".//a[starts-with(@href, 'my-art-edit.cfm?GroupID=')]");
                            foreach (var item in divs)
                            {
                                Sunfrog data = new Sunfrog();
                                //my-art-edit.cfm?GroupID=134983299
                                //var ahref = item.SelectSingleNode(".//a[starts-with(@href, 'my-art-edit.cfm?GroupID=')]");
                               // if(ahref != null)
                                {
                                    string groupLink = item.GetAttributeValue("href", string.Empty);
                                    if(!string.IsNullOrEmpty(groupLink))
                                    {
                                        data.EditLink = "https://manager.sunfrogshirts.com/" + groupLink;
                                    }
                                    var imgTag = item.SelectSingleNode(".//img");
                                    if(imgTag != null)
                                    {
                                        data.ImageLink = imgTag.GetAttributeValue("src", string.Empty);
                                    }
                                }

                                if(!string.IsNullOrEmpty(data.EditLink))
                                {
                                    dataList.Add(data);
                                }
                            }

                            //if(dataList.Count > 0)
                            //{
                            //   // string desPath = rootPath + $"links_{startIndex}.txt";
                            //    CsvUtil.WriteObjectsToCSV(dataList, rootPath, $"links_{startIndex}.csv");
                                
                            //}
                            return true;
                        }
                    }
             
                }
                
            }
            catch (Exception ex)
            {
                logger.ErrorFormat($"Error: {ex.Message}, Stack Track: {ex.StackTrace}");
            }

            return false;
        }
        public bool Login(string sfAcc, string sfPass, CustomWeb web)
        {
            string url = "https://manager.sunfrogshirts.com/Login.cfm";
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("username", sfAcc);
            nvc.Add("password", sfPass);

            string result = web.SendRequest(url, "POST", nvc, true, "application/x-www-form-urlencoded");
            if (!string.IsNullOrEmpty(result))
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(result);
                try
                {
                    string aff = doc.DocumentNode.SelectSingleNode("//a[@id='showAffiliateID']")
                        .SelectSingleNode(".//strong[@class='clearfix']").InnerText.Trim().Replace("?", "");
                 
                }
                catch
                {

                }

            }
            return IsLoggedIn(result);
        }

        public bool IsLoggedIn(string html)
        {
            try
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);
                var node = doc.DocumentNode.SelectSingleNode("//input[@id='exampleInputEmail1']");
                if (null == node)
                    return true;

            }
            catch
            {

            }

            return false;
        }

    }
}
