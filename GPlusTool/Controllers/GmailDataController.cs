using GPlusTool.Controller;
using GPlusTool.Models;
using GPlusTool.Utils;
using log4net;
using RaviLib.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPlusTool.Controllers
{
    public sealed  class GmailDataController
    {
        #region create thread safe instance
        private static readonly Lazy<GmailDataController> lazy =
        new Lazy<GmailDataController>(() => new GmailDataController());

        public static GmailDataController Instance { get { return lazy.Value; } }

        private GmailDataController()
        {
        }

        #endregion


        public delegate void UpdateTextWork(string text);
        public event UpdateTextWork UpdateTextEvent;


        private static readonly ILog logger = LogManager.GetLogger(typeof(GmailDataController));
        
        private List<GmailUser> gmailUserList;
        private int actionIndex = 0;

        private DriverController driverCtrl;
        public string CurrentEmail { get; set; }
        private List<string> imageList;
        private List<ExcelModel> contentList;
        private List<string> groupList;
        private List<string> linkList;

        private int currentLinkIndex = 0;
        private int currentGroupIndex = 0;
        public GmailUser CurrentGmailUser
        {
            get
            {
                if (gmailUserList != null && gmailUserList.Count > 0)
                    return gmailUserList[0];
                else
                    return null;
            }
        }
       
        public void Init(List<GPlusAction> actions, List<Gmail> gmailList, string imagePath, string textPath,
            string linkPath, string groupPath)
        {
            imageList = new List<string>();
            contentList = new List<ExcelModel>();
            linkList = new List<string>();
            groupList = new List<string>();

            this.gmailUserList = new List<GmailUser>();
            for (int i = 0; i < gmailList.Count; i++)
            {
                this.gmailUserList.Add(new GmailUser (
                     gmailList[i], actions
                ));
            }

            driverCtrl = new DriverController();
            driverCtrl.UpdateTextEvent += NotifyText;


            var temp = Directory.GetFiles(imagePath, "*.png", SearchOption.AllDirectories);
            var temp2 = Directory.GetFiles(imagePath, "*.jpg", SearchOption.AllDirectories);
            if (temp != null && temp.Length > 0)
            {
                imageList.AddRange(temp);
            }

            if (temp2 != null && temp2.Length > 0)
            {
                imageList.AddRange(temp2);
            }

            //Excel
            contentList = ExcelUtil.GetModelFromExcel<ExcelModel>(textPath, 2, 1);

            // link 
            var lines = File.ReadAllLines(linkPath);
            if(lines != null && lines.Length > 0)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    linkList.Add(lines[i].Trim());
                }
            }

            // group
            lines = File.ReadAllLines(groupPath);
            if (lines != null && lines.Length > 0)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    groupList.Add(lines[i].Trim());
                }
            }
        }

        public void CloseDriverController()
        {
            if (driverCtrl != null)
                driverCtrl.Dispose();
        }

        private GPlusAction GetNextAction(List<GPlusAction> actionList, bool isRandom)
        {
            GPlusAction action = null;
            if (actionIndex >= actionList.Count)
                actionIndex = 0;
            if (isRandom)
            {
                if (actionList.Count > 1)
                {
                    action = actionList[actionIndex++];
                }
                else if (actionList.Count == 1)
                {
                    
                    action = actionList[actionIndex];
                }
            }
            else
            {
                if (actionList.Count > 0)
                {
                    action = actionList[actionIndex];

                }
            }
            return action;
        }

        public SingleGPlusAction GetSingleAction(bool isRandom=true)
        {
            if (gmailUserList.Count == 0)
                return null;


            var actionList = Instance.CurrentGmailUser.Actions;
            var action = GetNextAction(actionList, isRandom);
            if (action != null)
            {
                if(action.Times > 0)
                {
                    action.Times--;
                    return new SingleGPlusAction
                    {
                        Action = action.Action,
                        MaxDelay = action.MaxDelay,
                        MinDelay = action.MinDelay,
                        Remains = action.Times,
                        User = Instance.CurrentGmailUser.Gmail
                    };
                }else
                {
                    while(true)
                    {
                       if(action.Times <=0)
                        {
                            actionList.Remove(action);
                            if (actionList.Count == 0)
                            {
                                gmailUserList.RemoveAt(0);
                                if (gmailUserList.Count > 0)
                                    actionList = Instance.CurrentGmailUser.Actions;
                                else
                                    return null;
                            }
                        }else
                        {
                            action.Times--;
                            return new SingleGPlusAction
                            {
                                Action = action.Action,
                                MaxDelay = action.MaxDelay,
                                MinDelay = action.MinDelay,
                                Remains = action.Times,
                                User = Instance.CurrentGmailUser.Gmail
                            };
                        }
                       action = GetNextAction(actionList, isRandom);
                       
                    }
                   

                }                
            }
            return null;
        }

        public void ExecuteAction(SingleGPlusAction action)
        {
            if(Instance.CurrentEmail != action.User.Email)
            {
                Instance.CurrentEmail = action.User.Email;
                driverCtrl.Dispose();
            }
            if (!driverCtrl.IsStarted)
            {
               
                driverCtrl.Start(action.User, StringUtil.DecryptString(action.User.Password));
              
            }

            if(driverCtrl.IsStarted)
            {
                driverCtrl.ExecuteAction(action);
            }
           
        }

        private void NotifyText(string text)
        {
            Console.WriteLine("call notifytext!");
            if (UpdateTextEvent != null)
            {

                UpdateTextEvent(text);
            }
        }


        public string GetRandomContent()
        {
            Random rand = new Random();
            if(contentList != null && contentList.Count > 0)
            {
                return contentList[rand.Next(0, contentList.Count)].Content;
            }
            return "Like!";
        }

        public string GetSequenceLink()
        {
            Random rand = new Random();
            if (linkList != null && linkList.Count > 0)
            {
                string result = linkList[currentLinkIndex++];
                if (currentLinkIndex >= linkList.Count)
                    currentLinkIndex = 0;
                return result;
            }
            return "";
        }

        public string GetSequenceGroup()
        {
            Random rand = new Random();
            if (groupList != null && groupList.Count > 0)
            {
                string result = groupList[currentGroupIndex++];
                if (currentGroupIndex >= groupList.Count)
                    currentGroupIndex = 0;
                return result;
            }
            return "";
        }

        public string GetRandomImage()
        {
            Random rand = new Random();
            if (imageList != null && imageList.Count > 0)
            {
                return imageList[rand.Next(0, imageList.Count)];
            }

            return null;
        }

    }
}
