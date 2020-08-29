using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LienPhatERP.Services
{
    //public class DataRefreshService: IHostedService
    //{      
    //    private readonly IServiceProvider _provider;

    //    public DataRefreshService(IServiceProvider provider)
    //    {       
    //        _provider = provider;

    //    }
    //    protected override async  Task ExecuteAsync(CancellationToken cancellationToken)
    //    {
    //        while (!cancellationToken.IsCancellationRequested)
    //        {
    //            using (IServiceScope scope = _provider.CreateScope())
    //            {
    //                //  var context = scope.ServiceProvider.GetRequiredService<IOrderService>();
    //             scope.ServiceProvider.GetRequiredService<IEmailSender>();
                   
    //                var _emailSender = scope.ServiceProvider.GetRequiredService<IScheduleSendmailJob>();
    //                _emailSender.SendMailContractAsync();
    //            }          
    //            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

    //        }
    //    }
    //}
}
