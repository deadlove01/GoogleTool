namespace FbAutoFeeder.UserCotnrols
{
    partial class LoginFBControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.kryptonGroup1 = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.tbAccListPath = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnBrowseAccPath = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.dgv = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.btnLogin = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbLogs = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsLblCurrentIP = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cookie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserAgent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).BeginInit();
            this.kryptonGroup1.Panel.SuspendLayout();
            this.kryptonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // kryptonGroup1
            // 
            this.kryptonGroup1.Location = new System.Drawing.Point(8, 14);
            this.kryptonGroup1.Name = "kryptonGroup1";
            // 
            // kryptonGroup1.Panel
            // 
            this.kryptonGroup1.Panel.Controls.Add(this.tbAccListPath);
            this.kryptonGroup1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroup1.Panel.Controls.Add(this.btnBrowseAccPath);
            this.kryptonGroup1.Size = new System.Drawing.Size(699, 247);
            this.kryptonGroup1.TabIndex = 5;
            // 
            // tbAccListPath
            // 
            this.tbAccListPath.Location = new System.Drawing.Point(20, 33);
            this.tbAccListPath.Name = "tbAccListPath";
            this.tbAccListPath.Size = new System.Drawing.Size(531, 191);
            this.tbAccListPath.TabIndex = 3;
            this.tbAccListPath.Text = "";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(20, 3);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(264, 24);
            this.kryptonLabel1.TabIndex = 2;
            this.kryptonLabel1.Values.Text = "Danh sách acc fb có dạng: email|pass";
            // 
            // btnBrowseAccPath
            // 
            this.btnBrowseAccPath.Location = new System.Drawing.Point(557, 28);
            this.btnBrowseAccPath.Name = "btnBrowseAccPath";
            this.btnBrowseAccPath.Size = new System.Drawing.Size(126, 70);
            this.btnBrowseAccPath.TabIndex = 1;
            this.btnBrowseAccPath.Values.Text = "Nhập DS Acc";
            this.btnBrowseAccPath.Click += new System.EventHandler(this.btnBrowseAccPath_Click);
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Location = new System.Drawing.Point(3, 254);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.dgv);
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnLogin);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(709, 385);
            this.kryptonGroupBox1.TabIndex = 6;
            this.kryptonGroupBox1.Values.Heading = "Danh Sách Acc";
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeight = 28;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Email,
            this.Pass,
            this.Cookie,
            this.UserAgent,
            this.CheckPoint});
            this.dgv.Location = new System.Drawing.Point(3, 3);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(552, 351);
            this.dgv.TabIndex = 0;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(561, 3);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(126, 70);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Values.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbLogs);
            this.groupBox1.Location = new System.Drawing.Point(731, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 496);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logs";
            // 
            // tbLogs
            // 
            this.tbLogs.Location = new System.Drawing.Point(16, 29);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.ReadOnly = true;
            this.tbLogs.Size = new System.Drawing.Size(352, 444);
            this.tbLogs.TabIndex = 0;
            this.tbLogs.Text = "...";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLblCurrentIP,
            this.toolStripLabel2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 642);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip2.Size = new System.Drawing.Size(1108, 25);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsLblCurrentIP
            // 
            this.tsLblCurrentIP.AutoSize = false;
            this.tsLblCurrentIP.ForeColor = System.Drawing.Color.Red;
            this.tsLblCurrentIP.Name = "tsLblCurrentIP";
            this.tsLblCurrentIP.Size = new System.Drawing.Size(150, 22);
            this.tsLblCurrentIP.Text = "...";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripLabel2.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel2.Text = "Current IP: ";
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            this.Email.Width = 150;
            // 
            // Pass
            // 
            this.Pass.DataPropertyName = "Pass";
            this.Pass.HeaderText = "Pass";
            this.Pass.Name = "Pass";
            this.Pass.ReadOnly = true;
            this.Pass.Visible = false;
            this.Pass.Width = 5;
            // 
            // Cookie
            // 
            this.Cookie.DataPropertyName = "Cookie";
            this.Cookie.HeaderText = "Cookie";
            this.Cookie.Name = "Cookie";
            this.Cookie.ReadOnly = true;
            // 
            // UserAgent
            // 
            this.UserAgent.DataPropertyName = "UserAgent";
            this.UserAgent.HeaderText = "UserAgent";
            this.UserAgent.Name = "UserAgent";
            this.UserAgent.ReadOnly = true;
            // 
            // CheckPoint
            // 
            this.CheckPoint.DataPropertyName = "CheckPoint";
            this.CheckPoint.HeaderText = "CheckPoint";
            this.CheckPoint.Name = "CheckPoint";
            this.CheckPoint.ReadOnly = true;
            // 
            // LoginFBControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.kryptonGroupBox1);
            this.Controls.Add(this.kryptonGroup1);
            this.Name = "LoginFBControl";
            this.Size = new System.Drawing.Size(1108, 667);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1.Panel)).EndInit();
            this.kryptonGroup1.Panel.ResumeLayout(false);
            this.kryptonGroup1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroup1)).EndInit();
            this.kryptonGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker bgWorker;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnBrowseAccPath;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnLogin;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgv;
        private System.Windows.Forms.GroupBox groupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox tbLogs;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox tbAccListPath;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel tsLblCurrentIP;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pass;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cookie;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserAgent;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckPoint;
    }
}
