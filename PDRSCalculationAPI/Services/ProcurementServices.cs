using Microsoft.AspNetCore.SignalR;
using PDRSCalculationAPI.Entity;
using PDRSCalculationAPI.Hub;
using PDRSCalculationAPI.Repository;
using System.Threading.Tasks;

namespace PDRSCalculationAPI.Services
{
    public class ProcurementServices
    {
        private ProcurementRepository _procurementRepository;
        public ProcurementServices(PDRSDataContext dbContext)
        {
            _procurementRepository = new ProcurementRepository(dbContext);
        }

        public async Task<Procurement> GetFirstProcurement()
        {
            var procurement = await _procurementRepository.GetFirstProcurement();
            if (procurement == null)
            {
                procurement = new Procurement
                {
                    Id = 1,
                    Status = "Ïn Progress",
                    Progress = 0,
                    Amount = 0
                };
                await _procurementRepository.AddProcurement(procurement);
            }
            return procurement;
        }

        public async Task Calculate(IHubContext<ServerSignalR> serverSignalR, double amount)
        {
            var procurement = await GetFirstProcurement();
            try
            {
                for (var cnt = 1; cnt <= 5; cnt++)
                {
                    procurement.Progress = 20 * cnt;
                    if (cnt == 5)
                    {
                        procurement.Status = "Completed";
                        procurement.Amount = amount * 2;
                    }
                    await _procurementRepository.UpdateProcurement(procurement);
                    await serverSignalR.Clients.All.SendAsync("UpdateStatus");
                    await Task.Delay(5000);
                }                                    
            }
            catch
            {
                procurement.Status = "Failed";
                await _procurementRepository.UpdateProcurement(procurement);
                await serverSignalR.Clients.All.SendAsync("UpdateStatus");
            }
        }
    }
}