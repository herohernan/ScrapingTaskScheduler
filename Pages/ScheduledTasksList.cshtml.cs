using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ScrapingTaskScheduler.Models;
using ScrapingTaskScheduler.Services;
using System.Diagnostics;

namespace ScrapingTaskScheduler.Pages
{
    public class ScheduledTasksListModel : PageModel
    {
        public List<ScheduledTask> ScheduledTasks { get; private set; }

        private readonly ILogger<ScheduledTasksListModel> _logger;
        public JsonFileScheduledTaskService ScheduledTaskService;
        public ScheduledTasksListModel(
            ILogger<ScheduledTasksListModel> logger,
            JsonFileScheduledTaskService scheduledTaskService)
        {
            _logger = logger;
            ScheduledTaskService = scheduledTaskService;
        }

        public void OnGet()
        {
            ScheduledTasks = ScheduledTaskService.GetScheduledTasks();
        }
    }
}
