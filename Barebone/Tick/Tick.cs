using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Barebone.Tick
{
    public class Tick
    {
        private readonly static Lazy<Tick> _instance = new Lazy<Tick>(
            () => new Tick(GlobalHost.ConnectionManager.GetHubContext<TickHub>().Clients));

        private IHubConnectionContext _clients;
        private Timer _timer;

        private Tick(IHubConnectionContext clients) { 
            _clients = clients;
            _timer = new Timer(SendTick, null, 1000, 1000);
        }

        public static Tick Instance { get { return _instance.Value; } }

        private void SendTick(object state)
        {
            _clients.All.tick();
        }

    }
}