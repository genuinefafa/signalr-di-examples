using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Ninject;
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
            var kernel = new StandardKernel();
            var resolver = new NinjectSignalRDependencyResolver(kernel);

            kernel.Bind<Tick.Tick>().To<Tick.Tick>().InSingletonScope();
            kernel.Bind<IHubConnectionContext>().ToMethod(context => resolver.Resolve<IConnectionManager>().GetHubContext<TickHub>().Clients).WhenInjectedInto<Tick.Tick>();

            routes.MapHubs(new HubConfiguration() { Resolver = resolver });
        }
    }
}