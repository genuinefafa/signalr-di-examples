using System;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Practices.Unity;
using WithUnity.Tick;

namespace WithUnity
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            Func<IUnityContainer, object> func = delegate(IUnityContainer c)
            {
                var conMan = resolver.Resolve<IConnectionManager>();
                return new Tick.Tick(conMan.GetHubContext<TickHub>().Clients);
            };

            container.RegisterType<Tick.Tick, Tick.Tick>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(func)
            );

            container.RegisterType<TickHub, TickHub>();

        }

        private static UnitySignalRDependencyResolver resolver;

        public static void RegisterHubs(RouteCollection routes)
        {
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            hubConfiguration.EnableJavaScriptProxies = true;
            resolver  = new UnitySignalRDependencyResolver(UnityConfig.GetConfiguredContainer());
            hubConfiguration.Resolver = resolver;
            // esto tiene que estar antes de las otras routes como lo dice la documentación
            // @see http://www.asp.net/signalr/overview/hubs-api/hubs-api-guide-server#route
            RouteTable.Routes.MapHubs(hubConfiguration);

        }
    }
}
