using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class PricePlanService : IPricePlanService
    {
        public interface Debug { void Log(string s); };

        private readonly List<PricePlan> _pricePlans;
        private IMeterReadingService _meterReadingService;
        private IAccountService _accountService;

        public PricePlanService(List<PricePlan> pricePlan, IMeterReadingService meterReadingService, IAccountService accountService)
        {
            _pricePlans = pricePlan;
            _meterReadingService = meterReadingService;
            _accountService = accountService;
        }

        private decimal calculateAverageReading(List<ElectricityReading> electricityReadings)
        {
            var newSummedReadings = electricityReadings.Select(readings => readings.Reading).Aggregate((reading, accumulator) => reading + accumulator);

            return newSummedReadings / electricityReadings.Count();
        }

        private decimal calculateTimeElapsed(List<ElectricityReading> electricityReadings)
        {
            var first = electricityReadings.Min(reading => reading.Time);
            var last = electricityReadings.Max(reading => reading.Time);

            return (decimal)(last - first).TotalHours;
        }
        private decimal calculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan)
        {
            var average = calculateAverageReading(electricityReadings);
            var timeElapsed = calculateTimeElapsed(electricityReadings);
            var averagedCost = average/timeElapsed;
            return averagedCost * pricePlan.UnitRate;
        }

        public Dictionary<String, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(String smartMeterId)
        {
            List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId);

            if (!electricityReadings.Any())
            {
                return new Dictionary<string, decimal>();
            }
            return _pricePlans.ToDictionary(plan => plan.EnergySupplier.ToString(), plan => calculateCost(electricityReadings, plan));
        }

        public decimal GetConsumptionCostOfPreviousWeekElectricityReadings(string smartMeterId)
        {
            DateTime  toDate = DateTime.Now;
            DateTime fromDate = toDate.Subtract(new TimeSpan(7, 0, 0, 0));
            
            List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId, fromDate, toDate);

            var pricePlanId = _accountService.GetPricePlanIdForSmartMeterId(smartMeterId);

            var cost = calculateCost(electricityReadings, _pricePlans.Find(plan => plan.EnergySupplier == pricePlanId));
            return cost;
        }
    }
}
