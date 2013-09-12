using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Barebone
{
    public class HubConfig
    {
        public static void RegisterHubs(RouteCollection routes)
        {
            routes.MapHubs();
        }
    }
}