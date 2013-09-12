using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using WithNinject.Tick;

namespace WithNinject
{
    public class HubConfig
    {
        public static void RegisterHubs(RouteCollection routes)
        {
            GlobalHost.DependencyResolver.Register(typeof(TickHub), () => new TickHub(Tick.Tick.Instance));
            routes.MapHubs();
        }
    }
}