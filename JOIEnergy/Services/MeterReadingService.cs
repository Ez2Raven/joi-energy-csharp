using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        public Dictionary<string, List<ElectricityReading>> MeterAssociatedReadings { get; set; }
        public MeterReadingService(Dictionary<string, List<ElectricityReading>> meterAssociatedReadings)
        {
            MeterAssociatedReadings = meterAssociatedReadings;
        }

        public List<ElectricityReading> GetReadings(string smartMeterId) {
            if (MeterAssociatedReadings.ContainsKey(smartMeterId)) {
                return MeterAssociatedReadings[smartMeterId];
            }
            return new List<ElectricityReading>();
        }
        
        /// <summary>
        /// Get readings based on a given date range
        /// </summary>
        /// <param name="smartMeterId">the user's smart meter id</param>
        /// <param name="fromDateTime">The earlier Date Time to begin search</param>
        /// <param name="toDateTime">The later Date Time to begin search</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public List<ElectricityReading> GetReadings(string smartMeterId, DateTime fromDateTime, DateTime toDateTime)
        {
            if (MeterAssociatedReadings.ContainsKey(smartMeterId))
            {
                var listOfReadings = MeterAssociatedReadings[smartMeterId];
                return listOfReadings.FindAll(x => x.Time >= fromDateTime && x.Time <= toDateTime);
            }
            else
            {
                throw new InvalidOperationException("Unrecognized smart meter device");
            }
        }

        public void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings) {
            if (!MeterAssociatedReadings.ContainsKey(smartMeterId)) {
                MeterAssociatedReadings.Add(smartMeterId, new List<ElectricityReading>());
            }
            MeterAssociatedReadings[smartMeterId] = electricityReadings;
        }
    }
}
