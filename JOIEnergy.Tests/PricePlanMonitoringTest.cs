using System.Collections.Generic;
using JOIEnergy.Domain;
using JOIEnergy.Enums;
using JOIEnergy.Services;
using Xunit;
namespace JOIEnergy.Tests

{
    public class PricePlanMonitoringTest
    {
        
        public PricePlanMonitoringTest()
        {
            
        }

        [Fact]
        public void ShouldCalculateCostForLastWeekConsumption()
        {
            // Arrange
            var pricePlans = new List<PricePlan>();
            var meterReadingService = new MeterReadingService(new Dictionary<string, List<ElectricityReading>>());
            var accountService = new AccountService(new Dictionary<string, Supplier>());
            string smartMeterId = "smart-meter-id";
            
            PricePlanService pricePlanService = new PricePlanService(pricePlans, meterReadingService, accountService);
        
            // Act
            var actualPrice =
                pricePlanService.GetConsumptionCostOfPreviousWeekElectricityReadings(smartMeterId);
            
            //Asset
            decimal expectedPrice = 1200M;
            Assert.Equal(expectedPrice, actualPrice);
        }

        [Fact]
        public void ShouldThrowInvalidOperationExceptionForNonExistenceSmartMeterId()
        {
            var pricePlans = new List<PricePlan>();
            var meterReadingService = new MeterReadingService(new Dictionary<string, List<ElectricityReading>>());
            var accountService = new AccountService(new Dictionary<string, Supplier>());
            string smartMeterId = "smart-meter-id";
            
        }
    }
}