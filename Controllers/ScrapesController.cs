using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrapingTaskScheduler.Models;
using ScrapingTaskScheduler.Services;

/*
 * An API communication controller to get and post the "Scrapes" List
 * 
 * The "Scrapes" List is stored in the path "<project>/wwwroot/data/Scrapes.json"
 * 
 * ------------------------------------------------------------------------
 * Useful documentation:
 * 
 * Route path for the API: "<url>:port/api/scrapes" or "<url>/api/scrapes" 
 * 
 *     <RootPath>/api/Scraping/get = get the list of Web Scrapes 
 * ------------------------------------------------------------------------
 */

namespace ScrapingTaskScheduler.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ScrapesController : ControllerBase
    {
        public ScrapesController(JsonFileScrapeService scrapeService)
        {
            this.ScrapeService = scrapeService;
        }
        private JsonFileScrapeService ScrapeService { get; }

        [Route("Get")]
        [HttpGet]
        public List<Scrape> Get()
        {
            return ScrapeService.GetScrapes();
        }
    }
}