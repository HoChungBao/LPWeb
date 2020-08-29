using GoogleMaps.LocationServices;
using LienPhatERP.BackgroundService;
using LienPhatERP.Helper;
using LienPhatERP.ViewModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LienPhatERP.Scheduler
{
    public abstract  class ScheduledUpdateLocation : ScopedProcessor
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private GoogleLocationService _googleLocation;

        protected abstract string Schedule { get; }
        protected abstract string ScheduleCS { get; }
        public ScheduledUpdateLocation(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _googleLocation = new GoogleLocationService("AIzaSyCv-VJuonOOmaXpJLsqenv0bUTskGAeBWQ&am");
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
                    //await Process();
                    ////var urldomain = _config["UrlWeb:Host"];
                    ////Configuration.GetSection("ApplicationSettings")
                    //  string apiRequest = string.Format("api/OutletApi/GetAddressOutlet");

                    //var api = "https://localhost:44335/";
                    //var apiLive = "http://lienphat.com.vn/";
                    //var client = new RestClient(api);
                    //var req = new RestRequest(apiRequest, Method.GET) { RequestFormat = DataFormat.Json };

                    //var rs = client.Execute<HttpContentResult<List<MOutletStoreApiModel>>> (req).Data;
                    //if (rs != null)
                    //{
                    //    rs.Data.ForEach(x =>
                    //    {
                    //        var location = _googleLocation.GetLatLongFromAddress(x.Address);
                    //        if (location != null)
                    //        {
                    //            x.Latitue = location.Latitude.ToString();
                    //            x.Longtitue = location.Longitude.ToString();
                    //        }
                    //    });
                    //    apiRequest = string.Format("api/OutletApi/UpdateLocationOutlet");
                    //    req = new RestRequest(apiRequest, Method.POST) { RequestFormat = DataFormat.Json };
                    //    req.AddParameter("model",Helper.JsonHelper.SerializeObject(rs.Data));
                    //    client.Execute(req);
                    //}
                    //_nextRun = _schedule.GetNextOccurrence(DateTime.Now);

                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay

            }
            while (!stoppingToken.IsCancellationRequested);
        }
    }
}
