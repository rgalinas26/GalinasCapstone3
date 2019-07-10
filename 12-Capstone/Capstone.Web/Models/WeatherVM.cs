using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class WeatherVM
    {
        public IList<ParkWeather> Weather { get; set; }
        public string Unit { get; set; }
    }
}
