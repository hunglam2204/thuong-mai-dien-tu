using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace TMDT_Web.signalr.hubs
{
    public class ChatHub : Hub
    {
        public void send(string name,string avatar, string message)
        {
            Clients.All.addNewMessageToPage(name,avatar, message);
        }
    }
}