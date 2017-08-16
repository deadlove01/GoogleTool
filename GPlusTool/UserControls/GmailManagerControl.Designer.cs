namespace GPlusTool.UserControls
{
    partial class GmailManagerControl
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Email = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.RecoveryEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsUse = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn();
            this.LastUsedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAdd,
            this.menuEdit,
            this.menuDelete});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(692, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuAdd
            // 
            this.menuAdd.Image = global::GPlusTool.Properties.Resources.add;
            this.menuAdd.Name = "menuAdd";
            this.menuAdd.Size = new System.Drawing.Size(69, 24);
            this.menuAdd.Text = "Add";
            this.menuAdd.Click += new System.EventHandler(this.menuAdd_Click);
            // 
            // menuEdit
            // 
            this.menuEdit.Image = global::GPlusTool.Properties.Resources.edit;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(67, 24);
            this.menuEdit.Text = "Edit";
            this.menuEdit.Click += new System.EventHandler(this.menuEdit_Click);
            // 
            // menuDelete
            // 
            this.menuDelete.Image = global::GPlusTool.Properties.Resources.delete;
            this.menuDelete.Name = "menuDelete";
            this.menuDelete.Size = new System.Drawing.Size(85, 24);
            this.menuDelete.Text = "Delete";
            this.menuDelete.Click += new System.EventHandler(this.menuDelete_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeight = 28;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Email,
            this.RecoveryEmail,
            this.IsUse,
            this.LastUsedTime,
            this.Note});
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 28);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 24;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(692, 417);
            this.dgv.TabIndex = 2;
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            this.Email.Width = 200;
            // 
            // RecoveryEmail
            // 
            this.RecoveryEmail.DataPropertyName = "RecoveryEmail";
            this.RecoveryEmail.HeaderText = "RecoveryEmail";
            this.RecoveryEmail.Name = "RecoveryEmail";
            this.RecoveryEmail.ReadOnly = true;
            // 
            // IsUse
            // 
            this.IsUse.DataPropertyName = "IsUse";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = false;
            this.IsUse.DefaultCellStyle = dataGridViewCellStyle1;
            this.IsUse.FalseValue = null;
            this.IsUse.HeaderText = "Is Use";
            this.IsUse.IndeterminateValue = null;
            this.IsUse.Name = "IsUse";
            this.IsUse.ReadOnly = true;
            this.IsUse.TrueValue = null;
            // 
            // LastUsedTime
            // 
            this.LastUsedTime.DataPropertyName = "LastUsedTime";
            this.LastUsedTime.HeaderText = "Last Used Time";
            this.LastUsedTime.Name = "LastUsedTime";
            this.LastUsedTime.ReadOnly = true;
            // 
            // Note
            // 
            this.Note.DataPropertyName = "Note";
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            // 
            // GmailManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.menuStrip1);
            this.Name = "GmailManagerControl";
            this.Size = new System.Drawing.Size(692, 445);
            this.Load += new System.EventHandler(this.GmailManagerControl_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuAdd;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.ToolStripMenuItem menuDelete;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgv;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecoveryEmail;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewCheckBoxColumn IsUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastUsedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
    }
}
