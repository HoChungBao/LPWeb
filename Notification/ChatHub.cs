using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LienPhatERP.Helper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using NPOI.HPSF;

namespace LienPhatERP.Notification
{
    public class ChatHub: Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, JsonHelper.SerializeObject(new { Id = 123, Value = 123456789 }));
        }
        public async Task SeenMessage(string message)
        {
            var id = getUserId();
            if (!string.IsNullOrEmpty(id))
            {
                //Remove
            }
        }

        public async Task Focus()
        {
            var id = getUserId();
            if (!string.IsNullOrEmpty(id))
            {
                await Clients.User(id).SendAsync("Focus", "true");
            }
        }
        public async Task Notification()
        {
            var id = getUserId();
            if (!string.IsNullOrEmpty(id))
            {
                await Clients.User(id).SendAsync("Notification", "true");
            }
        }
        public async Task ClickMessage(string idClass)
        {
            var id = getUserId();
            if (!string.IsNullOrEmpty(id))
            {
                await Clients.User(id).SendAsync("ClickMessage", idClass);
            }
        }
        public override async Task OnConnectedAsync()
        {
            UserConnect.ConnectId.Add(Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UserConnect.ConnectId.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        public string getUserId()
        {
            var claimsIdentity = Context.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    var userIdValue = userIdClaim.Value;
                    return userIdValue;
                }
                return "";
            }
            return "";
        }
    }
    public class UserConnect
    {
        public static List<string> ConnectId = new List<string>();
        //private readonly ConcurrentDictionary<string, string> _stocks = new ConcurrentDictionary<string, string>();

    }
}
