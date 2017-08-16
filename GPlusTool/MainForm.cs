using ComponentFactory.Krypton.Toolkit;
using GPlusTool.UserControls;
using GPlusTool.Utils;
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

namespace GPlusTool
{
    public partial class MainForm : KryptonForm
    {

        #region props

        #endregion


        #region logics
        public MainForm()
        {
            InitializeComponent();

            XmlConfigurator.Configure();
        }




        #endregion


        #region events

        const int LEADING_SPACE = 12;
        const int CLOSE_SPACE = 15;
        const int CLOSE_AREA = 35;
        private void mainTabControl_MouseHover(object sender, EventArgs e)
        {
            //using (var g = mainTabControl.SelectedTab.CreateGraphics())
            //{
            //    var bounds =  mainTabControl.SelectedTab.Bounds;
               
            //    var font = new Font("Arial", 12, FontStyle.Regular);
            //    g.DrawString("x", font,  Brushes.Black, bounds.Right - CLOSE_AREA, bounds.Top + 4);
            //    g.DrawString(mainTabControl.SelectedTab.Text, font, Brushes.Black, bounds.Left + LEADING_SPACE, bounds.Top + 4);
            //    //g.DrawFocusRectangle();
                
            //}
            //    Console.WriteLine("mouse hover!");
            

        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            //bool found = false;
            //for (int i = 0; i < this.Controls.Count; i++)
            //{
            //    if(this.Controls[i].Name == "GPLUS")
            //    {
            //        found = true;
            //        break;
            //    }
            //}

            //if(!found)
            //{
            //    GPlusControl ctrl = new GPlusControl();
            //    ctrl.Name = "GPLUS";
            //    ctrl.Dock = DockStyle.Fill;
            //    this.Controls.Add(ctrl);
            //}

            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void gplusMenuItem_Click(object sender, EventArgs e)
        {
            mainTabControl.AddTabPage(Constants.GMAIL_MANAGER_PAGE);
        }
        private void menuGplusTool_Click(object sender, EventArgs e)
        {
            mainTabControl.AddTabPage(Constants.GPLUS_TOOL_PAGE);
        }


        #endregion


    }
}
