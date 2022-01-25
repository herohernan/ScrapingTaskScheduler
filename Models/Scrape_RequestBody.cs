/* 
 * It is the C# representation of a "Request Body Format" (JSON) used in ScrapesController,
 * The client send the JSON as part of the POST Api request
 */

namespace ScrapingTaskScheduler.Models
{
    public class Scrape_RequestBody
    {
        public string UrlSelected { get; set; }
        public string? HeaderScraped { get; set; }
        public string? First1000Char_Scraped { get; set; }
    }
}
