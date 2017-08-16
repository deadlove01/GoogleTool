using LinkTLTool.Controller;
using log4net;
using Quartz;
using Quartz.Impl;
using RaviLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkTLTool.Schedules
{
    public class DailySchedulerController : Singleton<DailySchedulerController>, IDisposable
    {
        private static readonly ILog logger =
            LogManager.GetLogger(typeof(DailySchedulerController));

        private IScheduler scheduler;
        public const string GROUP_UPLOAD = "PERIOD_UPLOADER";

        public const string CRAWL_DAILY_JOB_KEY = "CRAWL_DAILY_JOB_";
        public const string CRAWL_DAILY_TRIGGER = "CRAWL_DAILY_TRIGGER_";

        public const string CRAWL_PERIOD_JOB_KEY = "CRAWL_PERIOD_JOB_KEY_";
        public const string CRAWL_PERIOD_TRIGGER = "CRAWL_PERIOD_TRIGGER_";

        public IScheduler Scheduler
        {
            get { return scheduler; }
        }

        public DailySchedulerController()
        {

        }

        public void Start( string cronExpress)
        {
            logger.Info("Scheduler started...");
            if (scheduler == null || scheduler.IsShutdown)
            {
                scheduler = StdSchedulerFactory.GetDefaultScheduler();
            }

            // analyze

            JobKey periodKey = new JobKey(CRAWL_PERIOD_JOB_KEY, GROUP_UPLOAD);
            IJobDetail crawlPeriodJob = JobBuilder.Create<ClickJob>().WithIdentity(periodKey).Build();

            ITrigger triggerJob = TriggerBuilder.Create().StartNow().WithIdentity(CRAWL_PERIOD_TRIGGER, GROUP_UPLOAD)
                //.WithSimpleSchedule(x => x.WithIntervalInMinutes(everyMinutes).RepeatForever())
                .WithCronSchedule(cronExpress)
                //.WithSimpleSchedule()
                .Build();

            //scheduler.ListenerManager.AddJobListener(new CrawlPeriodTotalJobListener(), KeyMatcher<JobKey>
            //    .KeyEquals(periodKey));

            //scheduler.ListenerManager.AddJobListener(listener, GroupMatcher<JobKey>.GroupEquals(GROUP_UPLOAD));
            scheduler.ScheduleJob(crawlPeriodJob, triggerJob);

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
