using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScrapingTaskScheduler.Models;
using ScrapingTaskScheduler.Services;

namespace ScrapingTaskScheduler.Pages
{
    public class ScrapesListModel : PageModel
    {
        public List<Scrape> Scrapes { get; private set; }

        private readonly ILogger<ScrapesListModel> _logger;
        public JsonFileScrapeService ScrapeService;
        public ScrapesListModel(
            ILogger<ScrapesListModel> logger,
            JsonFileScrapeService scrapeService)
        {
            _logger = logger;
            ScrapeService = scrapeService;
        }

        public void OnGet()
        {
            Scrapes = ScrapeService.GetScrapes();
        }
    }
}
