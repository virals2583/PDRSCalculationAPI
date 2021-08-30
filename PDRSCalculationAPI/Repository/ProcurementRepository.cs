using Microsoft.EntityFrameworkCore;
using PDRSCalculationAPI.Entity;
using System.Threading.Tasks;

namespace PDRSCalculationAPI.Repository
{    
    public class ProcurementRepository
    {
        private PDRSDataContext _dbContext;
        public ProcurementRepository(PDRSDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Procurement> GetFirstProcurement()
        {
            return await _dbContext.Procurements.FirstOrDefaultAsync();
        }

        public async Task AddProcurement(Procurement procurement)
        {
            await _dbContext.Procurements.AddAsync(procurement);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProcurement(Procurement procurement)
        {
            _dbContext.Procurements.Update(procurement);
            await _dbContext.SaveChangesAsync();
        }
    }
}