﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkWeather
    {
        public string Park_Code { get; set; }
        public int FiveDayForecastValue { get; set; }
        public double LowTemp { get; set; }
        public double HighTemp { get; set; }
        public string Forecast { get; set; }
        public string Temperature_Unit { get; set; }
    }
}
