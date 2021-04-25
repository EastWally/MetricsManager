using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class Weather
    {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }

        public Weather(DateTime date, int temperature)
        {
            Date = date;
            Temperature = temperature;
        }

        public override string ToString()
        {
            return Date + " " + Temperature;
        }
    }
}
