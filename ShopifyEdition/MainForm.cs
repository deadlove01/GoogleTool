using MetroFramework.Forms;
using ShopifyEdition.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopifyEdition
{
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AmzImporterControl amzImporter = new AmzImporterControl();
            amzImporter.Dock = DockStyle.Fill;
            metroTabPage1.Controls.Add(amzImporter);
            
        }
    }
}
