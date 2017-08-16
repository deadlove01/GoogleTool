using LinkTLTool.Controllers;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkTLTool
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
        }
        static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        public bool IsRunning(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //  HotspotShieldController.Instance.Start();
            HotspotShieldController.Instance.ClickConnectNewVersion();
            return;
            var path = ProgramFilesx86() + "\\Hotspot Shield\\bin\\hsscp.exe";
            Console.WriteLine(path);
            //Process.Start(path);
            do
            {
                var processList = Process.GetProcessesByName("hsscp");
                if(processList != null && processList.Length> 0)
                {
                    break;
                }
                else
                {
                    Process.Start(path);
                    Thread.Sleep(10000);
                }
            } while (true);
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HotspotShieldController.Instance.DisconnectNewVersion();
            //Process[] processes = Process.GetProcesses();
            //bool found = false;
            //foreach (Process process in processes)
            //{
            //    Console.WriteLine(process.Id);
            //    if (process.ProcessName.ToLower() == "hsscp")
            //    {
            //        try
            //        {
            //            process.Kill();
            //            found = true;
            //            break;
            //        }
            //        catch (Exception ex)
            //        {
            //            //handle any exception here
            //            Console.WriteLine(ex.Message);
            //        }
            //    }
            //}

            //if(!found)
            //    Console.WriteLine("ko tim thay process hsscp.exe");
        }
    
    }
}
