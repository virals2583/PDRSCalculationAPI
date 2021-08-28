using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PDRSCalculationAPI.Hub;
using PDRSCalculationAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDRSCalculationAPI.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyCorsPolicy")]
    public class ProcurementController : ControllerBase
    {
        private readonly IHubContext<ServerSignalR> _serverSignalR;        
        public ProcurementController(IHubContext<ServerSignalR> serverSignalR)
        {            
            _serverSignalR = serverSignalR;
        }

        [Route("GetStatus")]
        [HttpGet]
        public ProcurementStatusViewModel GetStatus()
        {
            var procurementStatusViewModel = new ProcurementStatusViewModel
            {
                Status = "Not Started",
                Progress = 0,
                Amount = 0
            };

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Status")))
            {                
                HttpContext.Session.SetString("Status", "In Progress");
            }

            var status = HttpContext.Session.GetString("Status");
            //procurementStatusViewModel = HttpContext.Session.Get<ProcurementStatusViewModel>("Status");
            return procurementStatusViewModel;
        }

        [Route("Calculate")]        
        [HttpGet]
        public async Task Calculate()
        {            
            HttpContext.Session.SetString("Status", "Completed");
            await _serverSignalR.Clients.All.SendAsync("UpdateStatus");            
        }
    }
}
