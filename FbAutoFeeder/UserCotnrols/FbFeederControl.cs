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

            dgvAccs.AutoGenerateColumns = false;
            dgvAccs.DataSource = blvFbInfos;
        }

        private void btnBrowseAccPath_Click(object sender, EventArgs e)
        {

        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }


        private void UpdateLog(object sender, string text)
        {
          
        }
    }
}
