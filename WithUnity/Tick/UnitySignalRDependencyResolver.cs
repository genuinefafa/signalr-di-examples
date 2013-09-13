using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;

namespace WithUnity.Tick
{
    public class UnitySignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IUnityContainer _container;

        public UnitySignalRDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            if (_container.IsRegistered(serviceType))
            {
                return _container.Resolve(serviceType);
            }
            return base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container.IsRegistered(serviceType))
            {
                return _container.ResolveAll(serviceType);
            }
            return base.GetServices(serviceType);
        }

        public override void Register(Type serviceType, Func<object> activator)
        {
            base.Register(serviceType, activator);
            Trace.WriteLine("Registering " + serviceType.ToString());
        }
        public override void Register(Type serviceType, IEnumerable<Func<object>> activators)
        {
            base.Register(serviceType, activators);
            Trace.WriteLine("Registering " + serviceType.ToString());
        }
    }
}