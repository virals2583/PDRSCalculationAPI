using Microsoft.AspNetCore.SignalR;
using PDRSCalculationAPI.Entity;
using PDRSCalculationAPI.Hub;
using PDRSCalculationAPI.Repository;
using System.Threading.Tasks;

namespace PDRSCalculationAPI.Services
{
    public class ProcurementService
    {
        private ProcurementRepository _procurementRepository;
        public ProcurementService(PDRSDataContext dbContext)
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

        public async Task CalculateAndUpdate(Procurement procurement, double amount, int cnt)
        {            
            procurement.Progress = 20 * cnt;
            if (cnt == 5)
            {
                    procurement.Status = "Completed";
                    procurement.Amount = amount * 2;
            }
            await UpdateProcurement(procurement);
        }

        public async Task UpdateProcurement(Procurement procurement)
        {
            await _procurementRepository.UpdateProcurement(procurement);
        }
    }
}