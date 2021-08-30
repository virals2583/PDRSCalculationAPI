using Microsoft.AspNetCore.Cors;

namespace PDRSCalculationAPI.Hub
{
    [EnableCors("signalr")]
    public class ServerSignalR : Microsoft.AspNetCore.SignalR.Hub
    {


    }
}
