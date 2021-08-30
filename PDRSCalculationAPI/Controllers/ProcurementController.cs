using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PDRSCalculationAPI.Entity;
using PDRSCalculationAPI.Hub;
using PDRSCalculationAPI.Models;
using PDRSCalculationAPI.Services;
using System.Threading.Tasks;

namespace PDRSCalculationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsPolicy")]
    public class ProcurementController : ControllerBase
    {
        private ProcurementService _procurementServices;
        private readonly IHubContext<ServerSignalR> _serverSignalR;        

        public ProcurementController(PDRSDataContext dbContext, IHubContext<ServerSignalR> serverSignalR)
        {
            _procurementServices = new ProcurementService(dbContext);
            _serverSignalR = serverSignalR;
        }

        [Route("GetStatus")]
        [HttpGet]
        public async Task<Procurement> GetStatus()
        {
            return await _procurementServices.GetFirstProcurement();
        }

        [Route("Calculate")]        
        [HttpPost]
        public async Task Calculate(ProcurementAmount procurementAmount)
        {
            var procurement = await _procurementServices.GetFirstProcurement();
            try
            {                
                for (var cnt = 1; cnt <= 5; cnt++)
                {
                    await _procurementServices.CalculateAndUpdate(procurement, procurementAmount.Amount, cnt);
                    await _serverSignalR.Clients.All.SendAsync("UpdateStatus");
                    await Task.Delay(5000);
                }
            }
            catch
            {
                procurement.Status = "Failed";
                await _procurementServices.UpdateProcurement(procurement);
            }
        }
    }
}