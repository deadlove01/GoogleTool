using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using FbAutoFeeder.Models;
using Equin.ApplicationFramework;
using ComponentFactory.Krypton.Toolkit;
using System.IO;
using FbAutoFeeder.Utils;
using FbAutoFeeder.Controllers;
using FbAutoFeeder.Properties;
using System.Threading;
using System.Collections.Specialized;

namespace FbAutoFeeder.UserCotnrols
{
    public partial class FbFeederControl : UserControl
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(FbFeederControl));

        private List<LoginFbInfo> fbInfoList;
        private BindingListView<LoginFbInfo> blvFbInfos;
        public FbFeederControl()
        {
            InitializeComponent();

            fbInfoList = new List<LoginFbInfo>();
            blvFbInfos = new BindingListView<LoginFbInfo>(fbInfoList);

            dgv.AutoGenerateColumns = false;
            dgv.DataSource = blvFbInfos;
        }

        private void btnBrowseAccPath_Click(object sender, EventArgs e)
        {
            //var openFileDlg = new OpenFileDialog();
            //openFileDlg.Filter = "Text | *.txt";
            //var result = openFileDlg.ShowDialog();
            //if(result == DialogResult.OK)
            //{
            //    tbAccListPath.Text = openFileDlg.FileName;
            //    var lines = File.ReadAllLines(openFileDlg.FileName);
            //    for (int i = 0; i < lines.Length; i++)
            //    {
            //        var fbInfo = FbUtil.ConvertToFbInfo(lines[i]);
            //        if(fbInfo != null)
            //        {
            //            fbInfoList.Add(fbInfo);
            //        }
            //    }

            //    blvFbInfos.Refresh();
            //}

            var lines = tbAccListPath.Text.Trim().Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                var fbInfo = FbUtil.ConvertToFbInfo(lines[i]);
                if (fbInfo != null)
                {
                    fbInfoList.Add(fbInfo);
                }
            }
            blvFbInfos.Refresh();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string cookiePath = Directory.GetCurrentDirectory() + Settings.Default.COOKIE_PATH;
                if(!Directory.Exists(cookiePath))
                {
                    Directory.CreateDirectory(cookiePath);
                }

                DcomController.Instance.UpdateLogEvent += UpdateLog;
                DcomController.Instance.CloseAll();
                UserAgentTool userAgentTool = new UserAgentTool();
                for (int i = 0; i < fbInfoList.Count; i++)
                {
                    //if (string.IsNullOrEmpty(DcomController.Instance.ProfileName))
                    //{
                    //    DcomController.Instance.Connect("tester");
                    //}
                    //else
                    //{
                    //    DcomController.Instance.Reconnect();
                    //}
                    //if (!string.IsNullOrEmpty(DcomController.Instance.IP))
                    //{
                    //    this.Invoke(new Action(() =>
                    //    {
                    //        tsLblCurrentIP.Text = DcomController.Instance.IP;
                    //    }));
                    //}

                    var userAgent = userAgentTool.RandomUserAgent("desktop", "windows", "browser",
                        new string[] { "firefox", "safari" });

                    //using (RequestController ctrl = new RequestController(userAgent.agent_string))
                    //{
                    //    ctrl.NotifyLogLabelUI += UpdateLog;
                    //    bool startResult = ctrl.Start(fbInfoList[i]);
                    //    if (startResult)
                    //    {
                    //        UpdateLog(this, "Login successful!");
                    //        string cookie = ctrl.GetFbCookie();
                    //        if (!string.IsNullOrEmpty(cookie))
                    //        {
                    //            logger.Info("cookie: \n" + cookie);
                    //            fbInfoList[i].Cookie = cookie;
                    //            fbInfoList[i].UserAgent = userAgent.agent_string;
                    //            fbInfoList[i].CheckPoint = false;

                    //            File.AppendAllLines(cookiePath + "cookies.txt", new string[] { fbInfoList[i].ToString() });
                    //            UpdateLog(this, "Write cookie to file successful!");
                    //        }else
                    //        {                         
                    //            UpdateLog(this, "Get Cookie failed!");
                    //        }

                    //        this.Invoke(new Action(() =>
                    //        {
                    //            blvFbInfos.Refresh();
                    //        }));
                    //    }else
                    //    {
                    //        fbInfoList[i].CheckPoint = true;
                    //        UpdateLog(this, "Login failed!");
                    //    }
                    //    Thread.Sleep(3000);
                    //}




                    using (DriverController ctrl = new DriverController(userAgent.agent_string))
                    {
                        ctrl.NotifyLogLabelUI += UpdateLog;
                        ctrl.Test();
                        //bool result = ctrl.Start(fbInfoList[i]);
                        //if (result)
                        //{
                        //    string cookie = ctrl.GetFbCookie();
                        //    if (!string.IsNullOrEmpty(cookie))
                        //    {
                        //        logger.Info("cookie: \n" + cookie);

                        //        fbInfoList[i].Cookie = cookie;

                        //        this.Invoke(new Action(() =>
                        //        {
                        //            blvFbInfos.Refresh();
                        //        }));
                        //        File.AppendAllLines(cookiePath + "cookies.txt", new string[] { fbInfoList[i].ToString() });
                        //        UpdateLog(this, "Write cookie to file successful!");
                        //    }
                        //}
                    }
                }
                
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnLogin.Enabled = true;
            MessageBox.Show("Done");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (fbInfoList.Count == 0)
            {
                MessageBox.Show("Chưa có thông tin đăng nhập fb!");
                return;
            }
            btnLogin.Enabled = false;
            bgWorker.RunWorkerAsync();


            //UserAgentTool tool = new UserAgentTool();
            //string redirectUrl = string.Empty;
            //var agent = tool.RandomUserAgent("desktop", "windows", "browser", new string[] { "chrome", "firefox", "safari" });
            //Console.WriteLine("full: "+ agent);
            //CustomWeb web = new CustomWeb(agent.agent_string);
            //string result = web.SendRequest("https://www.facebook.com/help/", "GET", "www.facebook.com", null, ref redirectUrl, false, true);
            //Thread.Sleep(5000);
            //result = web.SendRequest("https://www.facebook.com/help/", "GET", "www.facebook.com", null, ref redirectUrl, false, false);
            //Thread.Sleep(5000);
            //NameValueCollection nvc = new NameValueCollection();
            ////trn_725750_1987@mailfree.host|xpadxjh5nt1987
            //nvc.Add("email", "minhtuan@bangtuoc.vn");
            //nvc.Add("pass", "bangtuoc@123");


            //redirectUrl = string.Empty;
            //result = web.SendRequest("https://www.facebook.com/login.php?login_attempt=1", "POST",
            //    "www.facebook.com", nvc, ref redirectUrl, true, false, "application/x-www-form-urlencoded");

            //Console.WriteLine("login result: "+ redirectUrl);
            //if(!redirectUrl.Contains("checkpoint"))
            //{
            //    result = web.SendRequest("https://www.facebook.com/", "GET", "www.facebook.com", null, ref redirectUrl, false, false);
            //}

            //Console.WriteLine("terst");
        }


        private void UpdateLog(object sender, string text)
        {
            if (tbLogs.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    if (tbLogs.Text.Length > 1000000)
                        tbLogs.Text = "";
                    tbLogs.Text = text + "\n" + tbLogs.Text;
                }));
            }
            else
            {
                if (tbLogs.Text.Length > 1000000)
                    tbLogs.Text = "";
                tbLogs.Text = text + "\n" + tbLogs.Text;
            }
        }
    }
}
