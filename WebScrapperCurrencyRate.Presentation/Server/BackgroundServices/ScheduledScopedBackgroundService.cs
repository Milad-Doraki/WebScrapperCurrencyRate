using Microsoft.Extensions.DependencyInjection;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebScrapperCurrencyRate.Presentation.Server.BackgroundServices
{
    public abstract class ScheduledScopedBackgroundService : ScopedBackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;

        protected abstract string ScheduleString { get; }

        public ScheduledScopedBackgroundService(IServiceScopeFactory serviceScopeFactory)
         : base(serviceScopeFactory)
        {
        }



        public override async Task ExecuteInScope(IServiceProvider serviceProvider, CancellationToken stoppingToken)
        { 
            _schedule = CrontabSchedule.Parse(ScheduleString, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
             
            do
            {
                var now = DateTime.Now;
                if (now > _nextRun)
                {
                    await ScheduledExecuteInScope(serviceProvider, stoppingToken);
                     
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(1000, stoppingToken); //1 second delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        public abstract Task ScheduledExecuteInScope(IServiceProvider serviceProvider, CancellationToken stoppingToken);
    }

}
