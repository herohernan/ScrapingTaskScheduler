using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScrapingTaskScheduler.Services;
using ScrapingTaskScheduler.Models;

namespace ScrapingTaskScheduler.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        // Quartz when the web page is refresh
        private QuartzService InitScheduler; 

        public List<ScheduledTask> ScheduledTasks { get; private set; } 
        public JsonFileScheduledTaskService ScheduledTaskService; 

        public IndexModel(
            ILogger<IndexModel> logger,
             QuartzService initScheduler,
            JsonFileScheduledTaskService scheduledTaskService)  
        {
            _logger = logger;
            InitScheduler = initScheduler;
            ScheduledTaskService = scheduledTaskService;

            InitScheduler.LaunchInitTasks();
        }

        public void OnGet()
        {
            
        }
    }
}