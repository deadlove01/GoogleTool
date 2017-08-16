using GPlusTool.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPlusTool.UserControls
{

    public delegate bool PreRemoveTab(int indx);
    public class GPlusTabControl: TabControl
    {

        private List<string> tabNames;
        public GPlusTabControl()
            : base()
        {
            PreRemoveTabPage = null;
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.SizeMode = TabSizeMode.Fixed;
            this.ItemSize = new Size(140, 25);
            this.TabPages.Clear();
            tabNames = new List<string>();
        }

        public void AddTabPage(string name)
        {
            string tabName = name.Replace(" ", "").Replace("+", "");
            if (!tabNames.Contains(name))
            {
                TabPage tab = new TabPage(name);
                
                if (name == Constants.GMAIL_MANAGER_PAGE)
                {
                    GmailManagerControl gmailCtrl = new GmailManagerControl();
                    gmailCtrl.Dock = DockStyle.Fill;
                    tab.Controls.Add(gmailCtrl);
                   

                }else if( name == Constants.GPLUS_TOOL_PAGE)
                {
                    GPlusControl ctrl = new GPlusControl();
                    ctrl.Dock = DockStyle.Fill;
                    tab.Controls.Add(ctrl);
                }
                tab.Name = tabName;
                this.TabPages.Add(tab);
                tabNames.Add(tab.Text);
                this.SelectedTab = tab;
            }
            else
            {
                var tabPage = this.TabPages[tabName];
                if(tabPage != null)
                {
                    this.SelectedTab = tabPage;
                }
            }
        }

        public PreRemoveTab PreRemoveTabPage;

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Rectangle r = GetTabRect(e.Index);
            Image img = Properties.Resources.close_mini;
            Rectangle rectImage = new Rectangle(r.Right - 4 - img.Width, r.Top + (r.Height - img.Width) / 2,
                img.Width, img.Height);


            r.Size = new Size(r.Width + 30, 38);
            e.Graphics.DrawImage(img, rectImage);

            Brush b = new SolidBrush(Color.Red);
            e.Graphics.DrawRectangle(new Pen(b), rectImage);

            b = new SolidBrush(Color.Black);
            string titel = this.TabPages[e.Index].Text;
            Font f = this.Font;
            e.Graphics.DrawString(titel + "    ", f, b, new PointF(r.X, r.Y));
         
            ////r.Offset(5, 5);
            ////r.Width = closeW;
            ////r.Height = closeH;

            //Pen p = new Pen(b);
            //e.Graphics.DrawLine(p, r.X + r.Width + closeW, r.Y, r.X + closeW, r.Y + closeH);
            //e.Graphics.DrawLine(p, r.X + closeW, r.Y, r.X + r.Width + closeW, r.Y + closeH);
        }
        
        protected override void OnMouseHover(EventArgs e)
        {
            //Console.WriteLine("hover");
          
            //Rectangle r = GetTabRect(SelectedIndex);
            //Image img = Properties.Resources.close_mini;
            //Rectangle rectImage = new Rectangle(r.Right - 4 - img.Width, r.Top + (r.Height - img.Width) / 2,
            //    img.Width, img.Height);

            //using (var g = this.SelectedTab.CreateGraphics())
            //{
            //    g.DrawImage(img, rectImage);

            //    Brush b = new SolidBrush(Color.Red);
            //    g.DrawRectangle(new Pen(b), rectImage);
            //}
            //r.Size = new Size(r.Width + 30, 38);


            //Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);
            //for (int i = 0; i < this.TabCount; i++)
            //{
            //    if (this.GetTabRect(i).IntersectsWith(mouseRect))
            //    {
            //        this.SelectedIndex = i;
            //        break;
            //    }
            //}
        }

        protected override void OnMouseLeave(EventArgs e)
        {

            //Console.WriteLine("selected index: "+this.SelectedIndex+"|tab index: "+this.TabIndex);
            //Console.WriteLine("mouse leave");
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            Point p = e.Location;
            for (int i = 0; i < TabCount; i++)
            {
                Rectangle r = GetTabRect(i);
                Image img = Properties.Resources.close_mini;
                Rectangle rectImage = new Rectangle(r.Right - 4 - img.Width, r.Top + (r.Height - img.Width) / 2,
                    img.Width, img.Height);
                //r.Offset(2, 2);
                //r.Width = 5;
                //r.Height = 5;
                if (rectImage.Contains(p))
                {
                    CloseTab(i);
                }
            }
        }

        private void CloseTab(int i)
        {
            if (PreRemoveTabPage != null)
            {
                bool closeIt = PreRemoveTabPage(i);
                if (!closeIt)
                    return;
            }
            tabNames.Remove(TabPages[i].Text);
            TabPages.Remove(TabPages[i]);
        }
    }
}
