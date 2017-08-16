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
    public partial class GmailEditForm : KryptonForm
    {

        private GmailManagerControl parentCtrl;
        private string _email = string.Empty;
        private string _pass = string.Empty;
        private string note = string.Empty;
        private string _recoveryEmail = string.Empty;

        private Gmail editGmail;
        public GmailEditForm()
        {
            InitializeComponent();
        }

        public GmailEditForm(GmailManagerControl parentCtrl, Gmail editGmail)
        {
            InitializeComponent();
            this.parentCtrl = parentCtrl;

            this.editGmail = editGmail;
        }

    
        private void GmailAddForm_Load(object sender, EventArgs e)
        {
            if(editGmail != null)
            {
                tbEmail.Text = editGmail.Email;
                tbPass.Text = StringUtil.DecryptString(editGmail.Password);
                tbNote.Text = editGmail.Note;
                tbRecoveryEmail.Text = editGmail.RecoveryEmail;
            }else
            {
                editGmail = new Gmail
                {
                    IsUse = true,
                    LastUsedTime = DateTime.MinValue
                };
            }
          
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

            editGmail.Email = email;
            editGmail.Password = StringUtil.EncryptString(pass);
            editGmail.RecoveryEmail = recoveryEmail;
            editGmail.Note = tbNote.Text.Trim();

            btnSave.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
          
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                
                parentCtrl.UpdateGmailToList(editGmail);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnSave.Enabled = true;
            MessageBox.Show("Done!");
        }
    }
}
