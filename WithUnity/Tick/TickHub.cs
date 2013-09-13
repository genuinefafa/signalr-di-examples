using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WithUnity.Tick
{
    public class TickHub : Hub
    {
        private readonly Tick _tick;
        public TickHub(Tick tick) { _tick = tick; }
    }
}