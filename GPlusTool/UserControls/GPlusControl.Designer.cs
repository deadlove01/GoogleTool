namespace GPlusTool.UserControls
{
    partial class GPlusControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kryptonSplitContainer1 = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonGroupBox2 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.dgvActions = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonGroupBox3 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.btnStop = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnRun = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonGroupBox1 = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.btnTextBrowse = new System.Windows.Forms.Button();
            this.btnImageBrowse = new System.Windows.Forms.Button();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblTotalGmails = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.headerGroupLog = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroup1 = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.tbLogs = new ComponentFactory.Krypton.Toolkit.KryptonRichTextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnLink = new System.Windows.Forms.Button();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnGroup = new System.Windows.Forms.Button();
            this.colSelect = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.Column2 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.Delay = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.MaxDelay = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            this.Times = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).BeginInit();
            this.kryptonSplitContainer1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).BeginInit();
            this.kryptonSplitContainer1.Panel2.SuspendLayout();
            this.kryptonSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).BeginInit();
            this.kryptonGroupBox2.Panel.SuspendLayout();
            this.kryptonGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).BeginInit();
            this.kryptonGroupBox3.Panel.SuspendLayout();
            this.kryptonGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).BeginInit();
            this.kryptonGroupBox1.Panel.SuspendLayout();
            this.kryptonGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupLog.Panel)).BeginInit();
            this.headerGroupLog.Panel.SuspendLayout();
            this.headerGroupLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonSplitContainer1
            // 
            this.kryptonSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            // 
            // kryptonSplitContainer1.Panel1
            // 
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kryptonGroupBox2);
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kryptonGroupBox3);
            this.kryptonSplitContainer1.Panel1.Controls.Add(this.kryptonGroupBox1);
            // 
            // kryptonSplitContainer1.Panel2
            // 
            this.kryptonSplitContainer1.Panel2.Controls.Add(this.headerGroupLog);
            this.kryptonSplitContainer1.Size = new System.Drawing.Size(1001, 680);
            this.kryptonSplitContainer1.SplitterDistance = 332;
            this.kryptonSplitContainer1.TabIndex = 0;
            // 
            // kryptonGroupBox2
            // 
            this.kryptonGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox2.Location = new System.Drawing.Point(4, 225);
            this.kryptonGroupBox2.Name = "kryptonGroupBox2";
            // 
            // kryptonGroupBox2.Panel
            // 
            this.kryptonGroupBox2.Panel.Controls.Add(this.dgvActions);
            this.kryptonGroupBox2.Size = new System.Drawing.Size(325, 328);
            this.kryptonGroupBox2.TabIndex = 0;
            this.kryptonGroupBox2.Values.Heading = "Gmail Action";
            // 
            // dgvActions
            // 
            this.dgvActions.AllowUserToAddRows = false;
            this.dgvActions.ColumnHeadersHeight = 28;
            this.dgvActions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.Column2,
            this.Delay,
            this.MaxDelay,
            this.Times});
            this.dgvActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActions.Location = new System.Drawing.Point(0, 0);
            this.dgvActions.Name = "dgvActions";
            this.dgvActions.RowHeadersVisible = false;
            this.dgvActions.RowTemplate.Height = 24;
            this.dgvActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActions.Size = new System.Drawing.Size(321, 300);
            this.dgvActions.TabIndex = 0;
            // 
            // kryptonGroupBox3
            // 
            this.kryptonGroupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox3.Location = new System.Drawing.Point(9, 559);
            this.kryptonGroupBox3.Name = "kryptonGroupBox3";
            // 
            // kryptonGroupBox3.Panel
            // 
            this.kryptonGroupBox3.Panel.Controls.Add(this.btnStop);
            this.kryptonGroupBox3.Panel.Controls.Add(this.btnRun);
            this.kryptonGroupBox3.Size = new System.Drawing.Size(325, 118);
            this.kryptonGroupBox3.TabIndex = 0;
            this.kryptonGroupBox3.Values.Heading = "Action";
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(169, 9);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(140, 60);
            this.btnStop.TabIndex = 1;
            this.btnStop.Values.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(16, 9);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(140, 60);
            this.btnRun.TabIndex = 1;
            this.btnRun.Values.Text = "Run";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // kryptonGroupBox1
            // 
            this.kryptonGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonGroupBox1.Location = new System.Drawing.Point(6, 3);
            this.kryptonGroupBox1.Name = "kryptonGroupBox1";
            // 
            // kryptonGroupBox1.Panel
            // 
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnGroup);
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnLink);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel5);
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnTextBrowse);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel4);
            this.kryptonGroupBox1.Panel.Controls.Add(this.btnImageBrowse);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel3);
            this.kryptonGroupBox1.Panel.Controls.Add(this.lblTotalGmails);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel2);
            this.kryptonGroupBox1.Panel.Controls.Add(this.kryptonLabel1);
            this.kryptonGroupBox1.Size = new System.Drawing.Size(321, 207);
            this.kryptonGroupBox1.TabIndex = 0;
            this.kryptonGroupBox1.Values.Heading = "Gmail Information";
            // 
            // btnTextBrowse
            // 
            this.btnTextBrowse.Location = new System.Drawing.Point(156, 88);
            this.btnTextBrowse.Name = "btnTextBrowse";
            this.btnTextBrowse.Size = new System.Drawing.Size(79, 24);
            this.btnTextBrowse.TabIndex = 1;
            this.btnTextBrowse.Text = "Browse";
            this.btnTextBrowse.UseVisualStyleBackColor = true;
            this.btnTextBrowse.Click += new System.EventHandler(this.btnTextBrowse_Click);
            // 
            // btnImageBrowse
            // 
            this.btnImageBrowse.Location = new System.Drawing.Point(156, 55);
            this.btnImageBrowse.Name = "btnImageBrowse";
            this.btnImageBrowse.Size = new System.Drawing.Size(79, 24);
            this.btnImageBrowse.TabIndex = 1;
            this.btnImageBrowse.Text = "Browse";
            this.btnImageBrowse.UseVisualStyleBackColor = true;
            this.btnImageBrowse.Click += new System.EventHandler(this.btnImageBrowse_Click);
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(35, 90);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(43, 24);
            this.kryptonLabel3.TabIndex = 0;
            this.kryptonLabel3.Values.Text = "Text:";
            // 
            // lblTotalGmails
            // 
            this.lblTotalGmails.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblTotalGmails.Location = new System.Drawing.Point(156, 22);
            this.lblTotalGmails.Name = "lblTotalGmails";
            this.lblTotalGmails.Size = new System.Drawing.Size(24, 24);
            this.lblTotalGmails.TabIndex = 0;
            this.lblTotalGmails.Values.Text = "...";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(35, 57);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(64, 24);
            this.kryptonLabel2.TabIndex = 0;
            this.kryptonLabel2.Values.Text = "Images:";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(35, 22);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(97, 24);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "Total gmails:";
            // 
            // headerGroupLog
            // 
            this.headerGroupLog.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroup1});
            this.headerGroupLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerGroupLog.HeaderStylePrimary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Secondary;
            this.headerGroupLog.HeaderVisibleSecondary = false;
            this.headerGroupLog.Location = new System.Drawing.Point(0, 0);
            this.headerGroupLog.Name = "headerGroupLog";
            // 
            // headerGroupLog.Panel
            // 
            this.headerGroupLog.Panel.Controls.Add(this.tbLogs);
            this.headerGroupLog.Size = new System.Drawing.Size(664, 680);
            this.headerGroupLog.TabIndex = 2;
            this.headerGroupLog.ValuesPrimary.Heading = "Log";
            // 
            // buttonSpecHeaderGroup1
            // 
            this.buttonSpecHeaderGroup1.Text = "Clear Log";
            this.buttonSpecHeaderGroup1.UniqueName = "EA8CE126A6A54F2F2EADB44D8D23A8E3";
            this.buttonSpecHeaderGroup1.Click += new System.EventHandler(this.buttonSpecHeaderGroup1_Click);
            // 
            // tbLogs
            // 
            this.tbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLogs.Location = new System.Drawing.Point(0, 0);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.ReadOnly = true;
            this.tbLogs.Size = new System.Drawing.Size(662, 647);
            this.tbLogs.TabIndex = 0;
            this.tbLogs.Text = "";
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(35, 120);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(42, 24);
            this.kryptonLabel4.TabIndex = 0;
            this.kryptonLabel4.Values.Text = "Link:";
            // 
            // btnLink
            // 
            this.btnLink.Location = new System.Drawing.Point(156, 118);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(79, 24);
            this.btnLink.TabIndex = 1;
            this.btnLink.Text = "Browse";
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(35, 150);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(58, 24);
            this.kryptonLabel5.TabIndex = 0;
            this.kryptonLabel5.Values.Text = "Group:";
            // 
            // btnGroup
            // 
            this.btnGroup.Location = new System.Drawing.Point(156, 148);
            this.btnGroup.Name = "btnGroup";
            this.btnGroup.Size = new System.Drawing.Size(79, 24);
            this.btnGroup.TabIndex = 1;
            this.btnGroup.Text = "Browse";
            this.btnGroup.UseVisualStyleBackColor = true;
            this.btnGroup.Click += new System.EventHandler(this.btnGroup_Click);
            // 
            // colSelect
            // 
            this.colSelect.DataPropertyName = "IsUse";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = false;
            this.colSelect.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSelect.FalseValue = null;
            this.colSelect.HeaderText = "Select";
            this.colSelect.IndeterminateValue = null;
            this.colSelect.Name = "colSelect";
            this.colSelect.TrueValue = null;
            this.colSelect.Width = 40;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "ActionName";
            this.Column2.HeaderText = "Action";
            this.Column2.Name = "Column2";
            this.Column2.Width = 100;
            // 
            // Delay
            // 
            this.Delay.DataPropertyName = "MinDelay";
            this.Delay.HeaderText = "Min Delay";
            this.Delay.Name = "Delay";
            this.Delay.Width = 50;
            // 
            // MaxDelay
            // 
            this.MaxDelay.DataPropertyName = "MaxDelay";
            this.MaxDelay.HeaderText = "Max Delay";
            this.MaxDelay.Name = "MaxDelay";
            this.MaxDelay.Width = 50;
            // 
            // Times
            // 
            this.Times.DataPropertyName = "Times";
            this.Times.HeaderText = "Times";
            this.Times.Name = "Times";
            this.Times.Width = 50;
            // 
            // GPlusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonSplitContainer1);
            this.Name = "GPlusControl";
            this.Size = new System.Drawing.Size(1001, 680);
            this.Load += new System.EventHandler(this.GPlusControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel1)).EndInit();
            this.kryptonSplitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1.Panel2)).EndInit();
            this.kryptonSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainer1)).EndInit();
            this.kryptonSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2.Panel)).EndInit();
            this.kryptonGroupBox2.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox2)).EndInit();
            this.kryptonGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3.Panel)).EndInit();
            this.kryptonGroupBox3.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox3)).EndInit();
            this.kryptonGroupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1.Panel)).EndInit();
            this.kryptonGroupBox1.Panel.ResumeLayout(false);
            this.kryptonGroupBox1.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonGroupBox1)).EndInit();
            this.kryptonGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupLog.Panel)).EndInit();
            this.headerGroupLog.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupLog)).EndInit();
            this.headerGroupLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroupLog;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvActions;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox kryptonGroupBox3;
        private ComponentFactory.Krypton.Toolkit.KryptonRichTextBox tbLogs;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnRun;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblTotalGmails;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnStop;
        private System.Windows.Forms.Button btnTextBrowse;
        private System.Windows.Forms.Button btnImageBrowse;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroup1;
        private System.Windows.Forms.Button btnGroup;
        private System.Windows.Forms.Button btnLink;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn colSelect;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn Column2;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn Delay;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn MaxDelay;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewNumericUpDownColumn Times;
    }
}
