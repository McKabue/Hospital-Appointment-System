using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Hos.HELPERS
{
    [HubName("hoshub")]
    public class hosHub : Hub
    {
    }
}