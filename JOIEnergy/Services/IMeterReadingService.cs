using System;
using System.Collections.Generic;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public interface IMeterReadingService
    {
        List<ElectricityReading> GetReadings(string smartMeterId);

        List<ElectricityReading> GetReadings(string smartMeterId, DateTime fromDateTime, DateTime toDateTime);
        
        void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings);
    }
}