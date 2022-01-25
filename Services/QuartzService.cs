using ScrapingTaskScheduler.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

/*
 * It is used to do the differents functions contained in the Quartz.net complement 
 * (package installed using NuGet).
 * 
 * "Quartz.net" is an Open-source job scheduling system for .NET
 */

namespace ScrapingTaskScheduler.Services
{
    public class QuartzService 
    {
        private readonly IJobFactory JobFactory;
        
        // The principal object where is stored the scheduled tasks (each one is a thread)
        private IScheduler QuartzScheduler { get; set; }

        public List<ScheduledTask> ScheduledTasks { get; private set; }
        public JsonFileScheduledTaskService ScheduledTaskService;
        public QuartzService(
            IJobFactory jobFactory,
            JsonFileScheduledTaskService scheduledTaskService)
        {
            JobFactory = jobFactory;
            ScheduledTaskService = scheduledTaskService;
            QuartzScheduler = ConfigureQuartz();
        }

        /* Shutdown all the threads */
        ~QuartzService()
        {
            if(!QuartzScheduler.IsShutdown)
                QuartzScheduler.Shutdown();
        }

        /* Configuration. Here we define a specific factory ("QuartzSingletonService") */
        private IScheduler ConfigureQuartz()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            var scheduler = factory.GetScheduler().Result;
            scheduler.JobFactory = JobFactory;
            scheduler.Start().Wait();
            return scheduler;
        }

        /* Create a new Job */
        private IJobDetail NewJob(string taskName)
        {
            IJobDetail job = JobBuilder.Create<QuartzJob>()
                                .WithIdentity(taskName)
                                .Build();
            return job;
        }

        /* Create a new trigger for the job */
        private ITrigger NewTrigger(string triggerName, string cron_syntax_expression)
        {
            ITrigger trigger;
            try {
                trigger = TriggerBuilder.Create()
                                .WithIdentity(triggerName)
                                .WithCronSchedule(cron_syntax_expression)
                                .Build();
                return trigger;
            }
            catch (Exception) { // Bad crono_syntax_expression format
                return null;
            }
        }

        /* Launch a new job with a specific trigger (using a cron syntax expression) */
        public bool ExecuteScheduleJob(int index, string cronSyntax)
        {
            IJobDetail job = NewJob(index.ToString());
            ITrigger trigger = NewTrigger(index.ToString(),cronSyntax);

            if (trigger != null)
            {
                QuartzScheduler.ScheduleJob(job, trigger);
                return true;
            }
            return false;
        }

        /* Init: Lauch all the scheduled tasks */
        public void LaunchInitTasks()
        {
            ScheduledTasks = ScheduledTaskService.GetScheduledTasks();
            foreach (var scheduledTask in ScheduledTasks)
                this.ExecuteScheduleJob(scheduledTask.Id, scheduledTask.Cron_syntax);
        }
    }
}
