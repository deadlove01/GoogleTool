using Freebitcoin.Models;
using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freebitcoin.Schedulers
{
    public class SchedulerController : Singleton<SchedulerController>, IDisposable
    {
        private static readonly ILog logger =
            LogManager.GetLogger(typeof(SchedulerController));

        private IScheduler scheduler;
        public const string GROUP_UPLOAD = "UPLOADER";
        public const string CRAWL_MANAGE_JOB_KEY = "CRAWL_MANAGE_JOB_";
        public const string CRAWL_MANAGE_TRIGGER = "CRAWL_MANAGE_TRIGGER_";

        public const string CRAWL_ANALYZE_JOB_KEY = "CRAWL_ANALYZE_JOB_";
        public const string CRAWL_ANALYZE_TRIGGER = "CRAWL_ANALYZE_TRIGGER_";

        public const string DAILY_CRAWL_ANALYZE_JOB_KEY = "DAILYCRAWL_ANALYZE_JOB_";
        public const string DAILY_CRAWL_ANALYZE_TRIGGER = "DAILY_CRAWL_ANALYZE_TRIGGER_";
        public const string CRAWL_MANAGE_LISTENER = "CRAWL_MANAGE_LISTENER_";
        public const string CRAWL_ANALYZE_LISTENER = "CRAWL_ANALYZE_LISTENER_";

        public IScheduler Scheduler
        {
            get { return scheduler; }
        }

        public SchedulerController()
        {

        }

        public void Start()
        {
            logger.Info("Scheduler started...");
            if (scheduler == null || scheduler.IsShutdown)
            {
                scheduler = StdSchedulerFactory.GetDefaultScheduler();
            }

            // analyze
            JobKey analyzeKey = new JobKey(CRAWL_ANALYZE_JOB_KEY, GROUP_UPLOAD);
            IJobDetail crawlAnalyzeJob = JobBuilder.Create<FreeBtcJob>().WithIdentity(analyzeKey).Build();
      
            //crawlAnalyzeJob.JobDataMap.Add("DELAY", maxDelay);

            ITrigger triggerJob = null;
            //if (runOnce)
            //{
            //    triggerJob = TriggerBuilder.Create().StartNow().WithIdentity(CRAWL_ANALYZE_JOB_KEY, GROUP_UPLOAD)
            //    .WithSimpleSchedule()
            //    .Build();

            //}
            //else
            //{
            string cronExpress = "";
            triggerJob = TriggerBuilder.Create().StartNow().WithIdentity(CRAWL_ANALYZE_JOB_KEY, GROUP_UPLOAD)
                //.WithCronSchedule(cronExpress)
                    .WithSimpleSchedule()
                    .Build();

            //}

            scheduler.ScheduleJob(crawlAnalyzeJob, triggerJob);
            scheduler.Start();
        }

        public void DeleteJob(JobKey jobKey)
        {
            if (scheduler != null && !scheduler.IsShutdown)
            {
                scheduler.DeleteJob(jobKey);
            }
        }

        public void Dispose()
        {
            if (scheduler != null)
            {
                scheduler.Shutdown();
                scheduler = null;
                logger.Info("Scheduler has been shutdown.");
            }
        }
    }
}
