using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace PDRSCalculationAPI.Hub
{
    [EnableCors("signalr")]
    public class ServerSignalR : Microsoft.AspNetCore.SignalR.Hub
    {


    }
}
