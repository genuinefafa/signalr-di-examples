using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace WithNinject.Tick
{
    public class Tick
    {
        private IHubConnectionContext _clients;
        private Timer _timer;

        public Tick(IHubConnectionContext clients) { 
            _clients = clients;
            _timer = new Timer(SendTick, null, 1000, 1000);
        }

        private void SendTick(object state)
        {
            _clients.All.tick();
        }

    }
}