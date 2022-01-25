using Quartz;
using Quartz.Spi;

/*
 * Class imported from the Quartz.net examples.
 * It is useful to allow the creation of a constructor method inside the model: "QuartzJob.cs"
 */

namespace ScrapingTaskScheduler.Services
{
    public class QuartzSingletonService : IJobFactory
    {
        private readonly IServiceScope _serviceScope;
        public QuartzSingletonService(IServiceProvider serviceProvider)
        {
            _serviceScope = serviceProvider.CreateScope();
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceScope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
        }
    }
}