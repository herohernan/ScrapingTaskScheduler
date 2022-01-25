/* 
 * It is the C# representation of a "Request Body Format" (JSON) used in ScheduledTaskController,
 * The client send the JSON as part of the POST Api request
 */

namespace ScrapingTaskScheduler.Models
{
    public class ScheduleTask_RequestBody
    {
        public string UrlSelected { get; set; }
        public string CronSyntax { get; set; }
    }
}
