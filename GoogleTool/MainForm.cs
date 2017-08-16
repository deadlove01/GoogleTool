using Awesomium.Core;
using Awesomium.Windows.Forms;
using GoogleTool.Controller;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleTool
{
    public partial class MainForm : Form
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(MainForm));
        
        public MainForm()
        {
            InitializeComponent();


            XmlConfigurator.Configure();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //webCtrl.Source = new Uri("https://plus.google.com/");
            //webCtrl.Source = new Uri("https://www.google.com");
            //Task t = new Task(() =>
            //{

            //    WebCore.Initialize(new WebConfig(), true);
            //    WebView browser = WebCore.CreateWebView(1024, 768, WebViewType.Offscreen);
            //    browser.DocumentReady += Browser_DocumentReady;
            //    browser.Source = new Uri("https://www.google.ru/");

            //    webCtrl = new WebControl();
            //    tableLayoutPanel1.Controls.Add(browser);
            //    WebCore.Run();
            //});
            //t.Start();
            GoogleController ctrl = new GoogleController();
            ctrl.GetAuthCode(webCtrl);
            Console.WriteLine("done");

        }

        private void Browser_DocumentReady(object sender, DocumentReadyEventArgs e)
        {
            Console.WriteLine("test");
        }

        private void btnPost_Click(object sender, EventArgs e)
        {

            GoogleController ctrl = new GoogleController();
            webCtrl.Update();
            if(tbScript.Text.Trim().Length == 0)
            {
                webCtrl.Source = new Uri("https://plus.google.com/communities/103386775708787324155");
            }else
            {
                JSValue cookie = webCtrl.ExecuteJavascriptWithResult("document.cookie");
           
                //var javascript = @"document.querySelectorAll('div[data-itemid^=\'update\']>div')[1].click()";
                ctrl.ExecuteScript(webCtrl, tbScript.Text.Trim());
                //string result = ctrl.JsFireEvent(webCtrl, javascript, "click");
            }
          
            //ctrl.GetAllPosts(webCtrl);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("xong");
        }
    }
}
