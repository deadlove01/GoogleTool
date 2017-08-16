using LinkTLTool.Controller;
using LinkTLTool.Controllers;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkTLTool.Schedules
{
    [DisallowConcurrentExecution]
    public class ClickJob : IJob
    {
        private static readonly ILog logger =
          LogManager.GetLogger(typeof(ClickJob));


        public const string JOBDATA_NAME = "CRAWL_OFFER_HISTORY";

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                logger.Info("start click job...");
                //JobDataMap jobData = context.JobDetail.JobDataMap;
                //int botIndex = jobData.GetIntValue("BOT_INDEX");

                //if (string.IsNullOrEmpty(DcomController.Instance.ProfileName))
                //{
                //    DcomController.Instance.Connect("tester");
                //}
                //else
                //{
                //    DcomController.Instance.Reconnect();
                //}

                //logger.Info("last ip: " + DcomController.Instance.IP);

                HotspotShieldController.Instance.ClickConnect();

                using (var ctrl = new DriverController())
                {
                    ctrl.Run();
                    Thread.Sleep(5000);
                }
                // HotspotShieldController.Instance.DisconnectNewVersion();
                HotspotShieldController.Instance.ClickConnect();
                logger.Info("end click job");
            }
            catch (Exception ex)
            {
                logger.ErrorFormat(context.JobDetail.Key + ": {0}, stacktrace: {1}", ex.Message, ex.StackTrace);
            }
        }

      
    }
}
