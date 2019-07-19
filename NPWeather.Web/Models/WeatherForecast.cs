using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class WeatherForecast
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int LowTempF { get; set; }
        public int LowTempC 
        {            
            get
            {
                return (int)((LowTempF - 32) * 0.5556);
            }
        }
        public int HighTempF { get; set; }
        public int HighTempC
        {
            get
            {
                return (int)((HighTempF - 32) * 0.5556);
            }
        }
        public string Forecast { get; set; }
    }
}
