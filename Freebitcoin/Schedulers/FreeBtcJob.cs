using Freebitcoin.Controller;
using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freebitcoin.Schedulers
{
    [DisallowConcurrentExecution]
    public class FreeBtcJob : IJob
    {
        private static readonly ILog logger =
          LogManager.GetLogger(typeof(FreeBtcJob));


        public const string JOBDATA_NAME = "CRAWL_DAILY_TOTAL_JOB";

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                logger.Info("start free btc job!");
                using (DriverController ctrl = new DriverController())
                {
                    ctrl.Start();
                    ctrl.GetFreeBtc();
                }
              
                logger.Info("end free btc job");
            }
            catch (Exception ex)
            {
                logger.ErrorFormat(context.JobDetail.Key + ": {0}, stacktrace: {1}", ex.Message, ex.StackTrace);
            }
        }
    }
}
