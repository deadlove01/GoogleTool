namespace Freebitcoin
{
    partial class FreeBitcoinForm
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
            this.btnGet = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnStop = new System.Windows.Forms.Button();
            this.tbXpath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbX = new System.Windows.Forms.TextBox();
            this.tbY = new System.Windows.Forms.TextBox();
            this.tbW = new System.Windows.Forms.TextBox();
            this.tbH = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(62, 44);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(116, 76);
            this.btnGet.TabIndex = 0;
            this.btnGet.Text = "Get Free Bitcoin";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(199, 44);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(122, 76);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tbXpath
            // 
            this.tbXpath.Location = new System.Drawing.Point(138, 179);
            this.tbXpath.Name = "tbXpath";
            this.tbXpath.Size = new System.Drawing.Size(211, 22);
            this.tbXpath.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbX
            // 
            this.tbX.Location = new System.Drawing.Point(51, 263);
            this.tbX.Name = "tbX";
            this.tbX.Size = new System.Drawing.Size(73, 22);
            this.tbX.TabIndex = 5;
            this.tbX.Text = "760";
            // 
            // tbY
            // 
            this.tbY.Location = new System.Drawing.Point(148, 263);
            this.tbY.Name = "tbY";
            this.tbY.Size = new System.Drawing.Size(73, 22);
            this.tbY.TabIndex = 5;
            this.tbY.Text = "480";
            // 
            // tbW
            // 
            this.tbW.Location = new System.Drawing.Point(248, 263);
            this.tbW.Name = "tbW";
            this.tbW.Size = new System.Drawing.Size(73, 22);
            this.tbW.TabIndex = 5;
            this.tbW.Text = "380";
            // 
            // tbH
            // 
            this.tbH.Location = new System.Drawing.Point(345, 263);
            this.tbH.Name = "tbH";
            this.tbH.Size = new System.Drawing.Size(73, 22);
            this.tbH.TabIndex = 5;
            this.tbH.Text = "160";
            // 
            // FreeBitcoinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 515);
            this.Controls.Add(this.tbH);
            this.Controls.Add(this.tbW);
            this.Controls.Add(this.tbY);
            this.Controls.Add(this.tbX);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbXpath);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnGet);
            this.Name = "FreeBitcoinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FreeBitcoinForm";
            this.Load += new System.EventHandler(this.FreeBitcoinForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGet;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox tbXpath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbX;
        private System.Windows.Forms.TextBox tbY;
        private System.Windows.Forms.TextBox tbW;
        private System.Windows.Forms.TextBox tbH;
    }
}