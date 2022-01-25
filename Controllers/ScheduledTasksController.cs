using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrapingTaskScheduler.Models;
using ScrapingTaskScheduler.Services;

/*
 * An API communication controller to get and post the "ScheduledTask" List
 * 
 * The "ScheduledTask" List is stored in the path "<project>/wwwroot/data/scheduledTasks.json"
 * 
 * -------------------------------------------------------------------------------------
 * Useful documentation:
 * 
 * Route path for the API: "<url>:port/api/scheduledtasks" or "<url>/api/scheduledtasks"
 * 
 *     <RootPath>/api/ScheduledTasks/get  = get the list of Scheduled Tasks
 *     <RootPath>/api/ScheduledTasks/post = add a new Scheduled Task to the list.                      
 *                       
 *                       Warning: the scheduled task takes effect when the website is refreshed
 *                       Inputs: It needs a JSON body format, like the next example 
 *                       
 *                           {
 *                               "UrlSelected" : "https://www.efecty.com.co",
 *                               "CronSyntax" : "0/20 * * ? * * *"
 *                           }
 *                           
 *     <RootPath>/api/ScheduledTasks/post2 = it's a GET api instruction that emulate a Post of new register   
 *                        
 *                        Warning: the scheduled task takes effect when the website is refreshed
 *                        Inputs: It needs a query list, like the next example
 *                          
 *                           ?urlSelected=www.ejemeplo.com&cronSyntax=0 0 4 ? * * *
 *                           
 * -------------------------------------------------------------------------------------
 */

namespace ScrapingTaskScheduler.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]
    public class ScheduledTasksController : ControllerBase
    {
        public ScheduledTasksController(
            JsonFileScheduledTaskService scheduledTaskService)
        {
            this.ScheduledTaskService = scheduledTaskService;
        }
        private JsonFileScheduledTaskService ScheduledTaskService { get; }

        [Route("Get")]
        [HttpGet]
        public List<ScheduledTask> Get()
        {
            return ScheduledTaskService.GetScheduledTasks();
        }

        [Route("Post")]
        [HttpPost]
        /* It needs a json request body format */
        public ActionResult Post(
            [FromBody] ScheduleTask_RequestBody requestBody)
        {
            ScheduledTaskService.AddNewScheduledTask(requestBody.UrlSelected, requestBody.CronSyntax);
            return Ok();
        }

        [Route("Post2")]
        [HttpGet]
        /* Use the GET api instruction to emulate a Post of new register */
        public ActionResult Post(
            [FromQuery] string urlSelected,
            [FromQuery] string cronSyntax)
        {
            ScheduledTaskService.AddNewScheduledTask(urlSelected, cronSyntax);
            return Ok();
        }
    }
}
