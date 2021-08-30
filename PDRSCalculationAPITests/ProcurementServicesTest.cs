using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PDRSCalculationAPI.Entity;
using PDRSCalculationAPI.Services;
using System.Threading.Tasks;

namespace PDRSCalculationAPITests
{
    public class ProcurementServicesTest
    {
        public PDRSDataContext dbContext;
        public ProcurementService procurementService;        
        
        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<PDRSDataContext>()
                                   .UseSqlServer(@"Server=VIRALSHAH\SQLEXPRESS;Database=PDRS;Trusted_Connection=True")
                                   .Options;
            dbContext = new PDRSDataContext(contextOptions);
            procurementService = new ProcurementService(dbContext);            
        }

        [Test]
        public async Task GetFirstProcurementTest()
        {
            // Arrange
            var procurement = await procurementService.GetFirstProcurement();

            // Assert
            Assert.AreEqual(procurement.Id, 1);
        }


        public interface IClientContract
        {
            void broadcastMessage(string name, string message);
        }
        [TestCase(5)]
        [TestCase(25)]
        [TestCase(20.65)]
        [Test]
        public async Task CalculateAndUpdate(double amount)
        {
            using (var dbTransaction = dbContext.Database.BeginTransaction())
            {
                // Arrange
                var procurement = await procurementService.GetFirstProcurement();

                // Act
                await procurementService.CalculateAndUpdate(procurement, amount, 1);

                // Assert
                Assert.AreEqual(procurement.Amount, procurement.Amount);

                // Act
                await procurementService.CalculateAndUpdate(procurement, amount, 5);

                // Assert
                Assert.AreEqual(procurement.Amount, amount * 2);
                Assert.AreEqual(procurement.Progress, 100);
                Assert.AreEqual(procurement.Status, "Completed");

                dbTransaction.Rollback();
            }
        }
    }
}