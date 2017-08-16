using LinkTLTool.Controller;
using LinkTLTool.Controllers;
using LinkTLTool.Schedules;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkTLTool
{
    public partial class MainForm : Form
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(MainForm));
        private string express = string.Empty;
        public MainForm()
        {
            InitializeComponent();

            XmlConfigurator.Configure();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DailySchedulerController.Instance.Start(express);
                //for (int i = 0; i < 15; i++)
                //{
                //    if(string.IsNullOrEmpty(DcomController.Instance.ProfileName))
                //    {
                //        DcomController.Instance.Connect("tester");
                //    }else
                //    {
                //        DcomController.Instance.Reconnect();
                //    }

                //    logger.Info("last ip: " + DcomController.Instance.IP);

                //    DriverController ctrl = new DriverController();
                //    ctrl.Run();
                //    ctrl.Dispose();
                //    Thread.Sleep(5000);
                //}
                //logger.Info("xong");
               
            }
            catch (Exception ex)
            {
                logger.Error($"Error: {ex.Message}, Details: {ex.StackTrace}");
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //btnRun.Enabled = true;
            //MessageBox.Show("Xong");
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            express = tbExpress.Text.Trim();
            if(string.IsNullOrEmpty(express))
            {
                MessageBox.Show("Schedule expression is required!");
                return;
            }
            btnRun.Enabled = false;
            bgWorker.RunWorkerAsync();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            DailySchedulerController.Instance.Dispose();
            btnRun.Enabled = true;
        }

        public void UpdateText(string text)
        {
            //{
            //    if (lblTimes.InvokeRequired)
            //    {
            //        lblTimes.Invoke(new Action(() =>
            //        {
            //            lblTimes.Text = text;
            //        }));
            //    }
            //    else
            //    {
            //        lock (this)
            //        {
            //            lblTimes.Text = text;
            //        }

            //    }
            //}
        }
    }
}
