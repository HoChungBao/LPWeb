
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LienPhatERP.BackgroundService;
using LienPhatERP.Controllers;

using LienPhatERP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using NCrontab;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Microsoft.AspNetCore.Http.Extensions;

namespace LienPhatERP.Scheduler
{
    public abstract class ScheduledProcessor : ScopedProcessor
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        
      
        protected abstract string Schedule { get; }
        protected abstract string ScheduleCS { get; }
        public ScheduledProcessor(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            
            _schedule = CrontabSchedule.Parse(Schedule);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
              
                if (now > _nextRun)
                {
                    await Process();
                  //  var urldomain = _config["UrlWeb:Host"];
                   // Configuration.GetSection("ApplicationSettings")
                  //  string apiRequest = string.Format("api/CommonApi/Sendmail/MEDIHUB1@23456");

                 //   var api = "https://localhost:44335/";
                 //   var apiLive = "http://erp.medihub.com.vn/";
                 //   var client = new RestClient(apiLive);
                 //   var req = new RestRequest(apiRequest, Method.GET) { RequestFormat = DataFormat.Json };

                 //   var rs = client.Execute<HttpContentResult<dynamic>>(req).Data;
                    //  typeof(CommonApiController).s;
                 //   _nextRun = _schedule.GetNextOccurrence(DateTime.Now);

                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
              
            }
            while (!stoppingToken.IsCancellationRequested);
        }
    }
}
