using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GPlusTool.Forms;
using GPlusTool.Models;
using log4net;
using GPlusTool.Controller;
using GPlusTool.Properties;
using System.IO;
using GPlusTool.Utils;
//using BlackHen.Threading;
using GPlusTool.Threading;
using Equin.ApplicationFramework;
using GPlusTool.Controllers;

namespace GPlusTool.UserControls
{
    public partial class GPlusControl : UserControl
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(GPlusControl));

        private List<GPlusAction> actionList;
        private BindingListView<GPlusAction> actionBlv;
        private bool isRun = false;


       // private WorkQueue work = null;
        private List<Gmail> gmailList;
        private string imagePath = string.Empty;
        private string textPath = string.Empty;
        private string linkPath = string.Empty;
        private string groupPath = string.Empty;
        
        System.Threading.ManualResetEvent _busy = new System.Threading.ManualResetEvent(false);
        public GPlusControl()
        {
            InitializeComponent();

            actionList = new List<GPlusAction>();
            actionBlv = new BindingListView<GPlusAction>(actionList);

            dgvActions.AutoGenerateColumns = false;
            dgvActions.DataSource = actionBlv;

            btnRun.Text = Constants.RUN_BTN_STR;

            gmailList = new List<Gmail>();

            //// configure thread
            //work = new WorkQueue();
            //work.ConcurrentLimit = 1;
            //((WorkThreadPool)work.WorkerPool).MinThreads = 1;
            //((WorkThreadPool)work.WorkerPool).MaxThreads = 1;


            //work.AllWorkCompleted += new EventHandler(work_AllWorkCompleted);
            //work.WorkerException += new ResourceExceptionEventHandler(work_WorkerException);
            //work.ChangedWorkItemState += new ChangedWorkItemStateEventHandler(work_ChangedWorkItemState);

        }

     

        #region multi threading


        //private void work_AllWorkCompleted(object sender, EventArgs e)
        //{
        //    Console.WriteLine("thread completed!");
        //}

        //private void work_ChangedWorkItemState(object sender, ChangedWorkItemStateEventArgs e)
        //{
            
        //    Console.WriteLine("thread state changed!");
        //    Console.WriteLine(work.Count);
        //}

        //private void work_WorkerException(object sender, ResourceExceptionEventArgs e)
        //{
        //    Console.WriteLine("thread exception!");
        //}

        #endregion



        private void btnAddGmail_Click(object sender, EventArgs e)
        {
           // GmailAddForm gmailAddForm = new GmailAddForm(this);
            //gmailAddForm.ShowDialog();
        }

        private void GPlusControl_Load(object sender, EventArgs e)
        {

            actionList.Add(new GPlusAction
            {
                ActionName = "GPlus +1",
                Action = GPLUS_ACTIONS.PLUS_1_FEEDS,
                MinDelay = 20,
                MaxDelay = 30,
                Times = Constants.DEFAULT_ACTION_TIMES
            });
            actionList.Add(new GPlusAction
            {
                ActionName = "Join Communities",
                Action = GPLUS_ACTIONS.JOIN_COMMUNITY,
                MinDelay = 20,
                MaxDelay = 30,
                Times = Constants.DEFAULT_ACTION_TIMES
            });
            actionList.Add(new GPlusAction
            {
                ActionName = "Post image on home",
                Action = GPLUS_ACTIONS.POST_IMAGE_HOME,
                MinDelay = 20,
                MaxDelay = 30,
                Times = Constants.DEFAULT_ACTION_TIMES
            });
            actionList.Add(new GPlusAction
            {
                ActionName = "Post image on community comment",
                Action = GPLUS_ACTIONS.POST_IMAGE_COMMUNITY,
                MinDelay = 20,
                MaxDelay = 30,
                Times = Constants.DEFAULT_ACTION_TIMES
            });

            actionList.Add(new GPlusAction
            {
                ActionName = "Post comment",
                Action = GPLUS_ACTIONS.POST_COMMENT_COMMUNITY,
                MinDelay = 20,
                MaxDelay = 30,
                Times = Constants.DEFAULT_ACTION_TIMES
            });

            actionList.Add(new GPlusAction
            {
                ActionName = "Post link list on community list",
                Action = GPLUS_ACTIONS.POST_LINK_LIST_COMMUNITY_LIST,
                MinDelay = 20,
                MaxDelay = 30,
                Times = Constants.DEFAULT_ACTION_TIMES
            });
            actionList.Add(new GPlusAction
            {
                ActionName = "Combo post link",
                Action = GPLUS_ACTIONS.COMBO_POST_LINK,
                MinDelay = 20,
                MaxDelay = 30,
                Times = Constants.DEFAULT_ACTION_TIMES
            });
            actionList.Add(new GPlusAction
            {
                ActionName = "Combo post image",
                Action = GPLUS_ACTIONS.COMBO_POST_IMAGE,
                MinDelay = 20,
                MaxDelay = 30,
                Times = Constants.DEFAULT_ACTION_TIMES
            });

            actionBlv.Refresh();
       
            

            string filePath = Directory.GetCurrentDirectory() + Settings.Default.GmailPath;
            if (File.Exists(filePath))
            {
                var gmails = XmlUtil.DeSerializeObject<List<Gmail>>(filePath);
                if (gmails != null && gmails.Count > 0)
                {
                    lblTotalGmails.Text = gmails.Count.ToString();
                    gmailList.AddRange(gmails);
                }else
                {
                    lblTotalGmails.Text = "0".ToString();
                }
            }
            else
            {
                lblTotalGmails.Text = "0".ToString();
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(imagePath))
            {
                MessageBox.Show("Cannot found image path!");
                return;
            }

            if (string.IsNullOrEmpty(textPath))
            {
                MessageBox.Show("Cannot found excel file to random content comment!");
                return;
            }

            if (string.IsNullOrEmpty(linkPath))
            {
                MessageBox.Show("Cannot found link path!");
                return;
            }

            if (string.IsNullOrEmpty(groupPath))
            {
                MessageBox.Show("Cannot found group path!");
                return;
            }

            isRun = !isRun;
            if(isRun)
            {
              
                btnRun.Text = Constants.STOP_BTN_STR;
                _busy.Set(); // unblock worker

                if(!bgWorker.IsBusy) // first start
                {
                    var selectedActions = actionList.FindAll(p => p.IsUse);
                    if (selectedActions == null || selectedActions.Count == 0)
                    {
                        MessageBox.Show("Select at least one action!");
                        return;
                    }

                    // init work item

                    var items = actionList.FindAll(p => p.IsUse == true);

                    GmailDataController.Instance.Init(items, gmailList, imagePath, textPath, linkPath, groupPath);
                    GmailDataController.Instance.UpdateTextEvent += UpdateText;

                    bgWorker.RunWorkerAsync();
                }
                   
                btnStop.Enabled = true;
                btnRun.Text = Constants.PAUSE_BTN_STR;
            }
            else
            {
               
                _busy.Reset();  // pause
                btnRun.Text = Constants.RESUME_BTN_STR;
            }
          
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            btnRun.Enabled = false;
            // Set CancellationPending property to true
            bgWorker.CancelAsync();
            // Unblock worker so it can see that
            _busy.Set();
            UpdateText("Wait to stop...");
         
        }


        public void UpdateText(string text)
        {
            DateTime now = DateTime.Now;
            string timeStr = now.ToString("dd/MM/yyyy HH:mm:ss");
            string fmt = "{0}: {1}\n";
            if(tbLogs.InvokeRequired)
            {
                tbLogs.Invoke(new Action(() =>
                {
                    tbLogs.Text = string.Format(fmt, timeStr, text) + tbLogs.Text;
                }));
            }else
            {
                lock(this)
                {
                    tbLogs.Text = string.Format(fmt, timeStr, text) + tbLogs.Text;
                }
            
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                BackgroundWorker worker = sender as BackgroundWorker;
                do
                {
                    _busy.WaitOne();
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        StopWorker();
                        break;
                    }
                    var action = GmailDataController.Instance.GetSingleAction();
                    if (action == null)
                        break;
                    else
                    {
                        GmailDataController.Instance.ExecuteAction(action);
                    }

                } while (true);
            }
            catch (Exception ex)
            {
                UpdateText(string.Format("Error: {0}, Details: {1}", ex.Message, ex.StackTrace));
                
                logger.ErrorFormat("Error: {0}, Details: {1}", ex.Message, ex.StackTrace);
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnRun.Enabled = true;

        }


        private void StopWorker()
        {
            logger.Info("Stop background worker!");
            UpdateText("Stop actions!");
       
            GmailDataController.Instance.CloseDriverController();

            this.Invoke(new Action(()=> {
                btnRun.Enabled = true;
                btnRun.Text = Constants.RUN_BTN_STR;
            }));
          
            GmailDataController.Instance.UpdateTextEvent -= UpdateText;
            isRun = false;
          
        }

        private void btnImageBrowse_Click(object sender, EventArgs e)
        {
            var folderDlg = new FolderBrowserDialog();
            var result = folderDlg.ShowDialog();
            if(result == DialogResult.OK)
            {
                imagePath = folderDlg.SelectedPath;
            }
        }

        private void btnTextBrowse_Click(object sender, EventArgs e)
        {
            var fileDlg = new OpenFileDialog();
            fileDlg.Filter = "Excel file| *.xlsx";
           
            var result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textPath = fileDlg.FileName;
            }
        }

        private void buttonSpecHeaderGroup1_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(()=> {
                tbLogs.Text = "";
            }));
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            var fileDlg = new OpenFileDialog();
            fileDlg.Filter = "Text file| *.txt";

            var result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                groupPath = fileDlg.FileName;
            }
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            var fileDlg = new OpenFileDialog();
            fileDlg.Filter = "Text file| *.txt";

            var result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                linkPath = fileDlg.FileName;
            }
        }
    }
}
