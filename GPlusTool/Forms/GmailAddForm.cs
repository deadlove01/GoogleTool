using ComponentFactory.Krypton.Toolkit;
using GPlusTool.Controller;
using GPlusTool.Models;
using GPlusTool.Properties;
using GPlusTool.UserControls;
using GPlusTool.Utils;
using RaviLib.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPlusTool.Forms
{
    public partial class GmailAddForm : KryptonForm
    {

        private GmailManagerControl parentCtrl;
        private string _email = string.Empty;
        private string _pass = string.Empty;
        private string _recoveryEmail = string.Empty;
        private string note = string.Empty;
        
        public GmailAddForm()
        {
            InitializeComponent();
        }

        public GmailAddForm(GmailManagerControl parentCtrl)
        {
            InitializeComponent();
            this.parentCtrl = parentCtrl;
        }

    
        private void GmailAddForm_Load(object sender, EventArgs e)
        {
            //tbEmail.Text = Settings.Default.GMAIL;
            //tbPass.Text = Settings.Default.PASSWORD;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string email = tbEmail.Text.Trim();
            if (email.Length == 0)
            {
                MessageBox.Show("Email cannot be empty!");
                return;
            }
            string pass = tbPass.Text.Trim();
            if(pass.Length == 0)
            {
                MessageBox.Show("Password cannot be empty!");
                return;
            }

            string recoveryEmail = tbRecoveryEmail.Text.Trim();
            if (recoveryEmail.Length == 0)
            {
                MessageBox.Show("Recovery email cannot be empty!");
                return;
            }

            _email = email;
            _pass = pass;
            _recoveryEmail = recoveryEmail;
            note = tbNote.Text.Trim();

            btnSave.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
          
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Gmail gmail = new Gmail
                {
                    Email = _email,
                    Password = StringUtil.EncryptString(_pass),
                    Note = note,
                    IsUse = true,
                    LastUsedTime = DateTime.MinValue,
                    RecoveryEmail = _recoveryEmail
                };

                parentCtrl.AddGmailToList(gmail, result=> {
                    if(!string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show(result);
                    }
                    else
                    {

                        MessageBox.Show("Done!");
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
         


            // DriverController ctrl = new DriverController();
            //// ctrl.UpdateTextEvent += parentCtrl.UpdateText;
            // bool loginResult = ctrl.Start(_email, _pass);
            // if (loginResult)
            // {
            //     //Settings.Default.GMAIL = _email;
            //     //Settings.Default.PASSWORD = _pass;
            //     //Settings.Default.Save();
            //     Gmail gmail = new Gmail
            //     {
            //         Email = _email,
            //         Password = _pass,
            //         Note = note,
            //         IsUse = true,                   
            //     };
            //     parentCtrl.AddGmailToList(gmail);
            //     MessageBox.Show("Saved");
            // }
            // else
            // {
            //     MessageBox.Show("Save failed!");
            // }

            // ctrl.Dispose();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSave.Enabled = true;
        }
    }
}
