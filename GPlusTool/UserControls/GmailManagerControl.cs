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
using GPlusTool.Models;
using System.IO;
using GPlusTool.Properties;
using GPlusTool.Utils;
using GPlusTool.Forms;
using Equin.ApplicationFramework;

namespace GPlusTool.UserControls
{
    public partial class GmailManagerControl : UserControl
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(GmailManagerControl));
        private List<Gmail> gmailList;
        private BindingListView<Gmail> gmailBlv;

        public void AddGmailToList(Gmail gmail, Action<string> callback)
        {
            if(gmailList.Exists(p=>p.Email == gmail.Email))
            {
                callback(Constants.EXISTED_GMAIL_STR);
            }
            else
            {
                gmailList.Add(gmail);
                SaveData();

                this.Invoke(new Action(() => {
                    gmailBlv.Refresh();
                }));
                callback(null);
            }
            
          
        }


        public void UpdateGmailToList(Gmail gmail)
        {
            
            SaveData();

            this.Invoke(new Action(() => {
                gmailBlv.Refresh();
            }));

        }


        public GmailManagerControl()
        {
            InitializeComponent();

            gmailList = new List<Gmail>();
            gmailBlv = new BindingListView<Gmail>(gmailList);

            dgv.AutoGenerateColumns = false;
            dgv.DataSource = gmailBlv;

        }

        private void SaveData()
        {
            string filePath = Directory.GetCurrentDirectory() + Settings.Default.GmailPath;
            XmlUtil.SerializeObject<List<Gmail>>(gmailList, filePath);
        }

        private void GmailManagerControl_Load(object sender, EventArgs e)
        {
            string filePath = Directory.GetCurrentDirectory() + Settings.Default.GmailPath;
            if(File.Exists(filePath))
            {
                var gmails = XmlUtil.DeSerializeObject<List<Gmail>>(filePath);
                if(gmails != null && gmails.Count > 0)
                {
                    gmailList.AddRange(gmails);
                    gmailBlv.Refresh();
                }
            }
        }

        private void menuAdd_Click(object sender, EventArgs e)
        {
            GmailAddForm form = new GmailAddForm(this);
            form.ShowDialog();
        }

        private void menuEdit_Click(object sender, EventArgs e)
        {
            string email = string.Empty;
            for (int i = 0; i < dgv.SelectedRows.Count; i++)
            {
                email = dgv.SelectedRows[0].Cells["Email"].Value.ToString();                
            }

            var gmail = gmailList.Find(p => p.Email == email);
            if(gmail != null)
            {
                GmailEditForm form = new GmailEditForm(this, gmail);
                form.ShowDialog();
            }
          
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            if(dgv.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Do you really want to delete!", "Confimation", MessageBoxButtons.OKCancel);
                if(result == DialogResult.OK)
                {
                    List<string> emailList = new List<string>();
                    for (int i = 0; i < dgv.SelectedRows.Count; i++)
                    {
                        string email = dgv.SelectedRows[0].Cells["Email"].Value.ToString();
                        emailList.Add(email);
                    }
                    for (int i = 0; i < emailList.Count; i++)
                    {
                        var gmail = gmailList.Find(p => p.Email == emailList[i]);
                        if (gmail != null)
                        {
                            gmailList.Remove(gmail);
                        }
                    }
                    gmailBlv.Refresh();

                    SaveData();
                }


            }
            else
            {
                MessageBox.Show("Choose one row at least!");
                
            }
        }
    }
}
