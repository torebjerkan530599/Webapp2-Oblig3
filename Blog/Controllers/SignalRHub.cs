using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Blog.Controllers
{
    public class SignalRHub : Hub
    {
        public async Task NewPostNotify(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveNewPost", user, message);
        }
    }
}
