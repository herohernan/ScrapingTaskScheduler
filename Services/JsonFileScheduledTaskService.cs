using System.Text.Json;
using ScrapingTaskScheduler.Models;

/* 
 * It is a service to pick file up (JSON file called "ScheduledTask.json"), turn into a C# list (array), 
 * add new information to it and get a specific value from the list.
 * 
 * The JSON file is stored in the path "<project>/wwwroot/data/ScheduledTask.json"
 */

namespace ScrapingTaskScheduler.Services
{
    public class JsonFileScheduledTaskService
    {
        string JSON_FILE_NAME = "scheduledTasks.json";

        public JsonFileScheduledTaskService(IWebHostEnvironment webHostEnvironment)
        {
            this.WebHostEnvironment = webHostEnvironment;
        }
        private IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get
            {
                return Path.Combine(WebHostEnvironment.WebRootPath, "data", JSON_FILE_NAME);
            }
        }

        /* Deserialize = Convert the JSON string to an C# list (array) */
        public List<ScheduledTask> GetScheduledTasks()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                string jsonString = jsonFileReader.ReadToEnd();
                return JsonSerializer.Deserialize<List<ScheduledTask>>(jsonString,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
            }
        }

        /* Add a new element to the JSON list */
        public void AddNewScheduledTask(string urlSlected, string cron_syntax_expression)
        {
            List<ScheduledTask> scheduledTasks = GetScheduledTasks();

            var lastIdQuery = scheduledTasks.MaxBy(x => x.Id);
            int lastId = lastIdQuery == null ? 0 : lastIdQuery.Id;
            scheduledTasks.Add(new ScheduledTask(++lastId, urlSlected, cron_syntax_expression));

            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                // Serialize = Convert the C# list (array) to an JSON string
                JsonSerializer.Serialize<List<ScheduledTask>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    scheduledTasks
                );
            }
        }

        /* Get the last Index (Id) found in the JSON list*/
        public int GetLastIndex()
        {
            List<ScheduledTask> scheduledTasks = GetScheduledTasks();
            var lastIdQuery = scheduledTasks.MaxBy(x => x.Id);
            int lastId = lastIdQuery == null ? 0 : lastIdQuery.Id;
            return lastId;
        }

        /* Get a specific url associated to an Index (id) */
        public string GetUrl(int index)
        {
            List<ScheduledTask> scheduledTasks = GetScheduledTasks();
            ScheduledTask task = scheduledTasks.Find(x => x.Id == index);
            if(task != null)
                return task.Url;
            return "";
        }

        /* Get a specific cron_syntax_expression associated to an Index (id) */
        public string GetCron_syntax(int index)
        {
            List<ScheduledTask> scheduledTasks = GetScheduledTasks();
            ScheduledTask task = scheduledTasks.Find(x => x.Id == index);
            if (task != null)
                return task.Cron_syntax;
            return "";
        }
    }
}
