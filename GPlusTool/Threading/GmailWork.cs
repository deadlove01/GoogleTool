
using GPlusTool.Controller;
using GPlusTool.Controllers;
using GPlusTool.Models;
using GPlusTool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPlusTool.Threading
{
    //public class GmailWork : WorkItem
    //{
    //    //public delegate void UpdateTextWork(string text);
    //    //public event UpdateTextWork UpdateTextEvent;
    //    private static Random random = new Random();
    //    private SingleGPlusAction action;
    //    private int index = 0;
    //    public GmailWork(SingleGPlusAction action) : base()
    //    {
    //        this.action = action;
    //    }

    //    public GmailWork(SingleGPlusAction action, int index) : base()
    //    {
    //        this.action = action;
    //        this.index = index;
    //    }

    //    public override void Perform()
    //    {
    //        Console.WriteLine("work item index: "+index);
    //        //if(currentGmail != null)
    //        //{
    //        //    DriverController ctrl = new DriverController();
    //        //    ctrl.UpdateTextEvent += NotifyText;
    //        //    bool startResult = ctrl.Start(currentGmail.Email, StringUtil.DecryptString(currentGmail.Password));
    //        //    if (startResult)
    //        //    {
    //        //        //ctrl.Run();
    //        //    }
    //        //    else
    //        //    {
    //        //        NotifyText("Starting Browser failed!");
    //        //    }


    //        //}

    //        if (action != null)
    //        {
    //            Console.WriteLine(action);
    //            GmailDataController.Instance.ExecuteAction(action);
    //        }

    //    }

    //    //private void NotifyText(string text)
    //    //{
    //    //    Console.WriteLine("call notifytext!");
    //    //    if(UpdateTextEvent != null)
    //    //    {
                
    //    //        UpdateTextEvent(text);
    //    //    }
    //    //}
    //}
}
