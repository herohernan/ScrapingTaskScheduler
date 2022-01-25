using System.Text.Json;
using System.Text.Json.Serialization;

/* 
 * It is the C# representation of the JSON file called "scrapes.json",
 * the JSON file is stored in the path "<project>/wwwroot/data/scrapes.json"
 */

namespace ScrapingTaskScheduler.Models
{
    public class Scrape
    {
        public int Id { get; set; }

        [JsonPropertyName("url_source")]
        public string Url { get; set; }

        public DateTimeOffset Date { get; set; }    
        public string? header { get; set; }
        public string? first_1000_characters { get; set; }

        public Scrape()
        {
        }
        public Scrape(int newId, string newUrl, DateTimeOffset newDate, string? newHeader, string? newFirst_100_characters)
        {
            Id = newId;
            Url = newUrl;
            Date = newDate;
            header = newHeader;

            if (header == null)
                first_1000_characters = newFirst_100_characters;
            else
                first_1000_characters = null;
        }

        /* Serialize = Convert the C# Object to a JSON string */
        public override string ToString() => JsonSerializer.Serialize<Scrape>(this); 
    }
}