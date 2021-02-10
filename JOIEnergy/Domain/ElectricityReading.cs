using System;
namespace JOIEnergy.Domain
{
    public class ElectricityReading
    {
        public DateTime Time { get; set; }
        
        /// <summary>
        /// Reading in kilowatts (kW)
        /// </summary>
        public Decimal Reading { get; set; }
    }
}
