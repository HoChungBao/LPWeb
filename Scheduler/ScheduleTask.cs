using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LienPhatERP.Scheduler
{
    public class ScheduleTask: ScheduledUpdateLocation
    {

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
        }

        protected override string Schedule => "*/30 * * * *";
        protected override string ScheduleCS => "* * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Processing starts here");
            return Task.CompletedTask;
        }
    }
}
