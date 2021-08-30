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
        private ProcurementServices _procurementServices;
        private readonly IHubContext<ServerSignalR> _serverSignalR;        

        public ProcurementController(PDRSDataContext dbContext, IHubContext<ServerSignalR> serverSignalR)
        {
            _procurementServices = new ProcurementServices(dbContext);
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
            await _procurementServices.Calculate(_serverSignalR, procurementAmount.Amount);
        }
    }
}