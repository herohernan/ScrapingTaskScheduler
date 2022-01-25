using System.Diagnostics;
using ScrapingTaskScheduler.Services;
using Quartz;

/*
 * It uses the Interface IJob to define the method that execute the threads' created 
 * by the Quartz package. Each thread is a scheduled task with a cron syntax expression.
 * The cron syntax expression is the trigger to launch the method: Execute
 */

namespace ScrapingTaskScheduler.Models
{
    [DisallowConcurrentExecution]
    internal class QuartzJob : IJob
    {
        public JsonFileScheduledTaskService ScheduledTaskService;
        public JsonFileScrapeService ScrapeService;
        public ScrapySharpService ScrapySharp;

        public QuartzJob(
            JsonFileScheduledTaskService scheduledTaskService,
            JsonFileScrapeService scrapeService,
            ScrapySharpService scrapySharp)
        {
            ScheduledTaskService = scheduledTaskService;
            ScrapeService = scrapeService;
            ScrapySharp = scrapySharp;
        }

        public Task Execute(IJobExecutionContext context)
        {
            // Debug
            int task = Convert.ToInt32(context.JobDetail.Key.Name.ToString());
            var message = $"The Job '{task}' is happening at '{DateTime.Now.ToString()}'";
            Debug.WriteLine(message);

            // Scrape 
            string url = ScheduledTaskService.GetUrl(task);
            if (url != null)
            {
                string? result = ScrapySharp.ScrapeTheHeader(url);
                if (result == null)
                {
                    result = ScrapySharp.ScrapeTheFirst_1000_characters(url);
                    ScrapeService.AddNewScrape(url, null, result);
                }
                else
                {
                    ScrapeService.AddNewScrape(url, result, null);
                }
            }

            // Finish the task
            return Task.CompletedTask;  
        }
    }
}