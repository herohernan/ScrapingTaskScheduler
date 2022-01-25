using HtmlAgilityPack;
using ScrapySharp.Extensions;

/*
 * It is used to do make web scraping. For that we use the "ScrapySharp" complement (package)
 * (package installed using NuGet).
 * 
 * Scraping Framework containing :
 *   - a web client able to simulate a web browser.
 *   - an HtmlAgilityPack extension to select elements using css selector (like JQuery)
 */

namespace ScrapingTaskScheduler.Services
{
    public class ScrapySharpService
    {
        public ScrapySharpService(IWebHostEnvironment webHostEnvironment)
        {
            this.WebHostEnvironment = webHostEnvironment;
        }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public string? ScrapeTheHeader(string url)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlNode scrapedText;
            try {
                HtmlDocument document = htmlWeb.Load(url);
                scrapedText = document.DocumentNode.CssSelect("header").First(); 
            }
            catch (Exception) {
                return null; 
            }

            return scrapedText.InnerHtml;
        }

        public string? ScrapeTheFirst_1000_characters(string url)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            HtmlNode scrapedText;
            try {
                HtmlDocument document = htmlWeb.Load(url);
                scrapedText = document.DocumentNode.CssSelect("html").First();
            }
            catch (Exception) {
                return "ERROR :( = the url is not responding or the cron syntax expression is incorrect";
            }

            return scrapedText.InnerHtml.Substring(0, 1000);
        }
    }
}
