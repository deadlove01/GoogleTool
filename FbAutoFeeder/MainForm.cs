using ComponentFactory.Krypton.Toolkit;
using FbAutoFeeder.Models;
using FbAutoFeeder.UserCotnrols;
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

namespace FbAutoFeeder
{
    public partial class MainForm : KryptonForm
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainForm));
        public MainForm()
        {
            InitializeComponent();

            XmlConfigurator.Configure();
         
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginFBControl loginCtrl = new LoginFBControl();
            loginCtrl.Dock = DockStyle.Fill;
            tabPage1.Controls.Add(loginCtrl);


            FbFeederControl feederCtrl = new FbFeederControl();
            feederCtrl.Dock = DockStyle.Fill;
            tabPage1.Controls.Add(feederCtrl);

        }
    }
}
