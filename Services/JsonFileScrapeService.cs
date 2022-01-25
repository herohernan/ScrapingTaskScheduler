using System.Text.Json;
using ScrapingTaskScheduler.Models;

/* 
 * It is a service to pick file up (JSON file called "scrapes.json"), 
 * turn into a C# list (array) and add new information to it 
 * 
 * The JSON file is stored in the path "<project>/wwwroot/data/scrapes.json"
 */

namespace ScrapingTaskScheduler.Services
{
    public class JsonFileScrapeService
    {
        string JSON_FILE_NAME = "scrapes.json";

        public JsonFileScrapeService(IWebHostEnvironment webHostEnvironment)
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

        /* Get the Scrape list from the JSON file*/
        public List<Scrape> GetScrapes()
        {
            try {
                List<Scrape> scrapes = Deserialize();
                return scrapes;
            }
            catch (IOException) {
                return null;
            }
        }
        /* Deserialize = Convert the JSON string to an C# list (array) */
        private List<Scrape> Deserialize()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                string jsonString = jsonFileReader.ReadToEnd();
                return JsonSerializer.Deserialize<List<Scrape>>(jsonString,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                jsonFileReader.Close();
            }
        }

        /* Add a new Scrape to the JSON list */
        public void AddNewScrape(string urlSelected, string? headerScraped, string? first100Char_Scraped)
        {
            List<Scrape> scrapes = GetScrapes();

            var lastIdQuery = scrapes.MaxBy(x => x.Id);
            int lastId = lastIdQuery == null ? 0 : lastIdQuery.Id;
            scrapes.Add(new Scrape(++lastId, urlSelected, DateTime.Now, headerScraped, first100Char_Scraped));

            try {
                Serialize(scrapes);
            }
            catch (IOException) {
            }
        }
        /* Serialize = Convert the C# list (array) to an JSON string */
        private void Serialize(List<Scrape> scrapes)
        {
            using (var outputStream = File.OpenWrite(JsonFileName))
            {
                // 
                JsonSerializer.Serialize<List<Scrape>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    scrapes);
                outputStream.Close();
            }
        }

    }
}