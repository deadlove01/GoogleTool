using GoogleTool.Controller;
using log4net;
using log4net.Config;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
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
    public partial class WebMainForm : Form
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(WebMainForm));
        public WebMainForm()
        {
            InitializeComponent();

            XmlConfigurator.Configure();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ChromeOptions options = new ChromeOptions();
            //options.AddExcludedArguments("excludeSwitches", "enable-automation");
            ////options.setExperimentalOption("excludeSwitches", Arrays.("enable-automation"));

            //var driverService = ChromeDriverService.CreateDefaultService();
            //driverService.HideCommandPromptWindow = true;


            //ChromeDriver driver = new ChromeDriver(driverService, options);
            //driver.Navigate().GoToUrl("https://plus.google.com");

            button1.Enabled = false;
            bgWorker.RunWorkerAsync();

        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DriverController ctrl = new DriverController();
                ctrl.Start();
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Xong");
            button1.Enabled = true;
        }
    }
}
