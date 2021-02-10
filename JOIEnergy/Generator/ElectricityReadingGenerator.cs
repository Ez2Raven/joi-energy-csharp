using System;
using System.Collections.Generic;
using JOIEnergy.Domain;

namespace JOIEnergy.Generator
{
    public class ElectricityReadingGenerator
    {
        public ElectricityReadingGenerator()
        {

        }
        
        /// <summary>
        /// Generates n number of <see cref="ElectricityReading"/> with a <see cref="Random"/> amount of kilowatts
        /// in decrement of 10 secs, sorted in time ascending order.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public List<ElectricityReading> Generate(int number)
        {
            var readings = new List<ElectricityReading>();
            var random = new Random();
            for (int i = 0; i < number; i++)
            {
                var reading = (decimal)random.NextDouble();
                var electricityReading = new ElectricityReading
                {
                    Reading = reading,
                    Time = DateTime.Now.AddSeconds(-i * 10)
                };
                readings.Add(electricityReading);
            }
            readings.Sort((reading1, reading2) => reading1.Time.CompareTo(reading2.Time));
            return readings;
        }
    }
}
