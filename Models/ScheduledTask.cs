using System.Text.Json;
using System.Text.Json.Serialization;

/* 
 * It is the C# representation of the JSON file called "scheduledTasks.json",
 * the JSON file is stored in the path "<project>/wwwroot/data/scheduledTasks.json"
 */

namespace ScrapingTaskScheduler.Models
{
    public class ScheduledTask
    {
        public int Id { get; set; }

        [JsonPropertyName("url_source")]
        public string Url { get; set; } 

        public string Cron_syntax { get; set; }

        public ScheduledTask()
        {
        }
        public ScheduledTask(int newId, string newUrl, string newCron_syntax)
        {
            Id = newId;
            Url = newUrl;
            Cron_syntax = newCron_syntax;
        }

        /* Serialize = Convert the Object to a JSON string */
        public override string ToString() => JsonSerializer.Serialize<ScheduledTask>(this);
    }
}
