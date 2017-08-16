namespace GPlusTool
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGplusTool = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManager = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGmailManager = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonBreadCrumbItem1 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem2 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem3 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem4 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem5 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.kryptonBreadCrumbItem6 = new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mainTabControl = new GPlusTool.UserControls.GPlusTabControl();
            this.menuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip3
            // 
            this.menuStrip3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.menuManager});
            this.menuStrip3.Location = new System.Drawing.Point(0, 0);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(1203, 28);
            this.menuStrip3.TabIndex = 2;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGplusTool});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(50, 24);
            this.toolStripMenuItem1.Text = "Tool";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // menuGplusTool
            // 
            this.menuGplusTool.Name = "menuGplusTool";
            this.menuGplusTool.Size = new System.Drawing.Size(181, 26);
            this.menuGplusTool.Text = "G+ tool";
            this.menuGplusTool.Click += new System.EventHandler(this.menuGplusTool_Click);
            // 
            // menuManager
            // 
            this.menuManager.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGmailManager});
            this.menuManager.Name = "menuManager";
            this.menuManager.Size = new System.Drawing.Size(80, 24);
            this.menuManager.Text = "Manager";
            // 
            // menuGmailManager
            // 
            this.menuGmailManager.Name = "menuGmailManager";
            this.menuGmailManager.Size = new System.Drawing.Size(186, 26);
            this.menuGmailManager.Text = "Gmail Manager";
            this.menuGmailManager.Click += new System.EventHandler(this.gplusMenuItem_Click);
            // 
            // kryptonBreadCrumbItem1
            // 
            this.kryptonBreadCrumbItem1.ShortText = "ListItem1";
            // 
            // kryptonBreadCrumbItem2
            // 
            this.kryptonBreadCrumbItem2.ShortText = "ListItem2";
            // 
            // kryptonBreadCrumbItem3
            // 
            this.kryptonBreadCrumbItem3.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem[] {
            this.kryptonBreadCrumbItem4,
            this.kryptonBreadCrumbItem5});
            this.kryptonBreadCrumbItem3.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem4
            // 
            this.kryptonBreadCrumbItem4.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem5
            // 
            this.kryptonBreadCrumbItem5.ShortText = "ListItem";
            // 
            // kryptonBreadCrumbItem6
            // 
            this.kryptonBreadCrumbItem6.ShortText = "ListItem";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1203, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mainTabControl
            // 
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.mainTabControl.ItemSize = new System.Drawing.Size(140, 25);
            this.mainTabControl.Location = new System.Drawing.Point(0, 52);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1203, 723);
            this.mainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.mainTabControl.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 775);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip3);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VGplus";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip3.ResumeLayout(false);
            this.menuStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem1;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem2;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem3;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem4;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem5;
        private ComponentFactory.Krypton.Toolkit.KryptonBreadCrumbItem kryptonBreadCrumbItem6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private UserControls.GPlusTabControl mainTabControl;
        private System.Windows.Forms.ToolStripMenuItem menuManager;
        private System.Windows.Forms.ToolStripMenuItem menuGmailManager;
        private System.Windows.Forms.ToolStripMenuItem menuGplusTool;
    }
}

