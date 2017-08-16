#region Copyright (c) 2004 Richard Schneider (Black Hen Limited) 
/*
   Copyright (c) 2004 Richard Schneider (Black Hen Limited) 
   All rights are reserved.

   Permission to use, copy, modify, and distribute this software 
   for any purpose and without any fee is hereby granted, 
   provided this notice is included in its entirety in the 
   documentation and in the source files.
  
   This software and any related documentation is provided "as is" 
   without any warranty of any kind, either express or implied, 
   including, without limitation, the implied warranties of 
   merchantibility or fitness for a particular purpose. The entire 
   risk arising out of use or performance of the software remains 
   with you. 
   
   In no event shall Richard Schneider, Black Hen Limited, or their agents 
   be liable for any cost, loss, or damage incurred due to the 
   use, malfunction, or misuse of the software or the inaccuracy 
   of the documentation associated with the software. 
*/
#endregion

using BlackHen.Threading;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Sample
{
	/// <summary>
	///  A WorkQueue sample.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
      private System.Windows.Forms.TextBox maxThreads;
      private System.Windows.Forms.TextBox concurrentLimit;
      private System.Windows.Forms.TextBox minThreads;
      private System.Windows.Forms.TextBox newWork;
      private System.Windows.Forms.ProgressBar progressBar;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Label queuedCount;
      private System.Windows.Forms.Label scheduledCount;
      private System.Windows.Forms.Label runningCount;
      private System.Windows.Forms.Label completedCount;
      private System.Windows.Forms.Label failingCount;

      private WorkQueue work;
      private System.Windows.Forms.PictureBox fsm;
      private System.Windows.Forms.ContextMenu fsmMenu;
      private System.Windows.Forms.MenuItem menuItem3;
      private System.Windows.Forms.MenuItem reset;
      private System.Windows.Forms.MenuItem pause;
      private System.Windows.Forms.MenuItem resume;
      private System.Windows.Forms.MenuItem menuItem1;
      private System.Windows.Forms.Button produceWork;
      private int[] stats;
      private TimeSpan refreshInterval;
      private DateTime nextRefreshTime;

      public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

         nextRefreshTime = DateTime.Now;
         refreshInterval = TimeSpan.FromSeconds(0.20);

         stats = new int[6];

         work = new WorkQueue();
         work.ConcurrentLimit = 100;
         work.AllWorkCompleted += new EventHandler(work_AllWorkCompleted);
         work.WorkerException += new ResourceExceptionEventHandler(work_WorkerException);
         work.ChangedWorkItemState += new ChangedWorkItemStateEventHandler(work_ChangedWorkItemState);

         minThreads.Text = ((WorkThreadPool) work.WorkerPool).MinThreads.ToString();
         maxThreads.Text = ((WorkThreadPool) work.WorkerPool).MaxThreads.ToString();
         concurrentLimit.Text = work.ConcurrentLimit.ToString();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fsm = new System.Windows.Forms.PictureBox();
            this.fsmMenu = new System.Windows.Forms.ContextMenu();
            this.reset = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.pause = new System.Windows.Forms.MenuItem();
            this.resume = new System.Windows.Forms.MenuItem();
            this.failingCount = new System.Windows.Forms.Label();
            this.completedCount = new System.Windows.Forms.Label();
            this.runningCount = new System.Windows.Forms.Label();
            this.scheduledCount = new System.Windows.Forms.Label();
            this.queuedCount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maxThreads = new System.Windows.Forms.TextBox();
            this.concurrentLimit = new System.Windows.Forms.TextBox();
            this.minThreads = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.newWork = new System.Windows.Forms.TextBox();
            this.produceWork = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fsm)).BeginInit();
            this.fsm.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fsm
            // 
            this.fsm.BackColor = System.Drawing.Color.White;
            this.fsm.ContextMenu = this.fsmMenu;
            this.fsm.Controls.Add(this.failingCount);
            this.fsm.Controls.Add(this.completedCount);
            this.fsm.Controls.Add(this.runningCount);
            this.fsm.Controls.Add(this.scheduledCount);
            this.fsm.Controls.Add(this.queuedCount);
            this.fsm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsm.Image = ((System.Drawing.Image)(resources.GetObject("fsm.Image")));
            this.fsm.Location = new System.Drawing.Point(0, 0);
            this.fsm.Name = "fsm";
            this.fsm.Size = new System.Drawing.Size(596, 167);
            this.fsm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.fsm.TabIndex = 0;
            this.fsm.TabStop = false;
            // 
            // fsmMenu
            // 
            this.fsmMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.reset,
            this.menuItem1,
            this.pause,
            this.resume});
            // 
            // reset
            // 
            this.reset.Index = 0;
            this.reset.Text = "Reset Counts";
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.Text = "-";
            // 
            // pause
            // 
            this.pause.Index = 2;
            this.pause.Text = "Pause";
            this.pause.Click += new System.EventHandler(this.pause_Click);
            // 
            // resume
            // 
            this.resume.Enabled = false;
            this.resume.Index = 3;
            this.resume.Text = "Resume";
            this.resume.Click += new System.EventHandler(this.resume_Click);
            // 
            // failingCount
            // 
            this.failingCount.BackColor = System.Drawing.Color.Transparent;
            this.failingCount.Location = new System.Drawing.Point(401, 117);
            this.failingCount.Name = "failingCount";
            this.failingCount.Size = new System.Drawing.Size(86, 21);
            this.failingCount.TabIndex = 5;
            this.failingCount.Text = "0";
            this.failingCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // completedCount
            // 
            this.completedCount.BackColor = System.Drawing.Color.Transparent;
            this.completedCount.Location = new System.Drawing.Point(485, 44);
            this.completedCount.Name = "completedCount";
            this.completedCount.Size = new System.Drawing.Size(86, 22);
            this.completedCount.TabIndex = 4;
            this.completedCount.Text = "0";
            this.completedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // runningCount
            // 
            this.runningCount.BackColor = System.Drawing.Color.Transparent;
            this.runningCount.Location = new System.Drawing.Point(313, 44);
            this.runningCount.Name = "runningCount";
            this.runningCount.Size = new System.Drawing.Size(87, 22);
            this.runningCount.TabIndex = 3;
            this.runningCount.Text = "0";
            this.runningCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // scheduledCount
            // 
            this.scheduledCount.BackColor = System.Drawing.Color.Transparent;
            this.scheduledCount.Location = new System.Drawing.Point(170, 44);
            this.scheduledCount.Name = "scheduledCount";
            this.scheduledCount.Size = new System.Drawing.Size(87, 22);
            this.scheduledCount.TabIndex = 2;
            this.scheduledCount.Text = "0";
            this.scheduledCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // queuedCount
            // 
            this.queuedCount.BackColor = System.Drawing.Color.Transparent;
            this.queuedCount.Location = new System.Drawing.Point(169, 117);
            this.queuedCount.Name = "queuedCount";
            this.queuedCount.Size = new System.Drawing.Size(91, 21);
            this.queuedCount.TabIndex = 1;
            this.queuedCount.Text = "0";
            this.queuedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.maxThreads);
            this.groupBox1.Controls.Add(this.concurrentLimit);
            this.groupBox1.Controls.Add(this.minThreads);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(10, 188);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 129);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Worker management";
            // 
            // maxThreads
            // 
            this.maxThreads.Location = new System.Drawing.Point(192, 55);
            this.maxThreads.Name = "maxThreads";
            this.maxThreads.Size = new System.Drawing.Size(77, 22);
            this.maxThreads.TabIndex = 5;
            this.maxThreads.Text = "textBox3";
            this.maxThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maxThreads.Leave += new System.EventHandler(this.maxThreads_Leave);
            // 
            // concurrentLimit
            // 
            this.concurrentLimit.Location = new System.Drawing.Point(192, 83);
            this.concurrentLimit.Name = "concurrentLimit";
            this.concurrentLimit.Size = new System.Drawing.Size(77, 22);
            this.concurrentLimit.TabIndex = 1;
            this.concurrentLimit.Text = "textBox2";
            this.concurrentLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.concurrentLimit.Leave += new System.EventHandler(this.concurrentLimit_Leave);
            // 
            // minThreads
            // 
            this.minThreads.Location = new System.Drawing.Point(192, 25);
            this.minThreads.Name = "minThreads";
            this.minThreads.Size = new System.Drawing.Size(77, 22);
            this.minThreads.TabIndex = 4;
            this.minThreads.Text = "textBox1";
            this.minThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(29, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 19);
            this.label3.TabIndex = 11;
            this.label3.Text = "Concurrency limit";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(29, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "Max threads";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(29, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "Min threads";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.newWork);
            this.groupBox2.Controls.Add(this.produceWork);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox2.Location = new System.Drawing.Point(332, 188);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 129);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Work production";
            // 
            // newWork
            // 
            this.newWork.Location = new System.Drawing.Point(182, 28);
            this.newWork.Name = "newWork";
            this.newWork.Size = new System.Drawing.Size(77, 22);
            this.newWork.TabIndex = 2;
            this.newWork.Text = "500";
            this.newWork.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // produceWork
            // 
            this.produceWork.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.produceWork.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.produceWork.Location = new System.Drawing.Point(48, 74);
            this.produceWork.Name = "produceWork";
            this.produceWork.Size = new System.Drawing.Size(173, 26);
            this.produceWork.TabIndex = 3;
            this.produceWork.Text = "Produce work items";
            this.produceWork.Click += new System.EventHandler(this.produceWork_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(19, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Items to produce";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(332, 327);
            this.progressBar.Maximum = 0;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(279, 18);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 12;
            this.progressBar.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.fsm);
            this.panel1.Location = new System.Drawing.Point(12, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 167);
            this.panel1.TabIndex = 13;
            // 
            // menuItem3
            // 
            this.menuItem3.Index = -1;
            this.menuItem3.Text = "-";
            // 
            // Form1
            // 
            this.AcceptButton = this.produceWork;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(695, 303);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Threading Sample";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            ((System.ComponentModel.ISupportInitialize)(this.fsm)).EndInit();
            this.fsm.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

      }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
         Application.EnableVisualStyles();
         Application.DoEvents();

			Application.Run(new Form1());
		}

      /// <summary>
      ///   Generate some work.
      /// </summary>
      private void produceWork_Click(object sender, System.EventArgs e)
      {
         lock (work)
         {
            Cursor = Cursors.AppStarting;
            int n = Int32.Parse(newWork.Text);

            progressBar.Maximum += n;
            progressBar.Visible = true;

            for (int i = 0; i < n; ++i)
               work.Add(new SampleWorkItem());
         }
      }

      /// <summary>
      ///   All work is completed.  Update the GUI.
      /// </summary>
      private void work_AllWorkCompleted(object sender, EventArgs e)
      {
         if (this.InvokeRequired)
         {
            this.Invoke(new EventHandler(work_AllWorkCompleted), new object[] {sender, e});
         }
         else
         {
            RefreshCounts();

            progressBar.Maximum = 0;
            progressBar.Value = 0;
            progressBar.Visible = false;

            stats = new int[6];

            Cursor = Cursors.Arrow;
         }
      }

      /// <summary>
      ///   Change the number of threads to use.
      /// </summary>
      private void maxThreads_Leave(object sender, System.EventArgs e)
      {
         ((WorkThreadPool) work.WorkerPool).MaxThreads = Int32.Parse(maxThreads.Text);
      }

      /// <summary>
      ///   Change the number of concurrent work items that can ran.
      /// </summary>
      private void concurrentLimit_Leave(object sender, System.EventArgs e)
      {
         work.ConcurrentLimit = Int32.Parse(concurrentLimit.Text);
      }

      /// <summary>
      ///   Record where the state of the work item.
      /// </summary>
      private void work_ChangedWorkItemState(object sender, ChangedWorkItemStateEventArgs e)
      {
         lock (this)
         {
            stats[(int) e.PreviousState] -= 1;
            stats[(int) e.WorkItem.State] += 1;
         }

         if (!pause.Enabled || DateTime.Now > nextRefreshTime)
         {
            RefreshCounts();
            nextRefreshTime = DateTime.Now + refreshInterval;
         }
      }

      /// <summary>
      ///   Update the work item state counts.
      /// </summary>
      private void RefreshCounts()
      {
         if (this.InvokeRequired)
         {
            MethodInvoker mi = new MethodInvoker(RefreshCounts);
            this.BeginInvoke(mi);
         }
         else
         {
            lock (this)
            {
               completedCount.Text = stats[(int) WorkItemState.Completed].ToString("N0");
               failingCount.Text = stats[(int) WorkItemState.Failing].ToString("N0");
               queuedCount.Text = stats[(int) WorkItemState.Queued].ToString("N0");
               runningCount.Text = stats[(int) WorkItemState.Running].ToString("N0");
               scheduledCount.Text = stats[(int) WorkItemState.Scheduled].ToString("N0");

               progressBar.Value = stats[(int) WorkItemState.Completed];
            }
         }
      }

      /// <summary>
      ///   Clear the work item state counts.
      /// </summary>
      private void reset_Click(object sender, System.EventArgs e)
      {
         lock (stats)
         {
            for (int i = 0; i < stats.Length; ++i)
               stats[i] = 0;
         }
         RefreshCounts();
      }

      /// <summary>
      ///   Pause the work queue.
      /// </summary>
      private void pause_Click(object sender, System.EventArgs e)
      {
         work.Pause();
         pause.Enabled = false;
         resume.Enabled = true;
         Refresh();
      }

      /// <summary>
      ///   Resume the work queue.
      /// </summary>
      private void resume_Click(object sender, System.EventArgs e)
      {
         work.Resume();
         pause.Enabled = true;
         resume.Enabled = false;
         Refresh();
      }

      private void work_WorkerException(object sender, ResourceExceptionEventArgs e)
      {
         Application.OnThreadException(e.Exception);
      }
   }
}
