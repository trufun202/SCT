using System;
using SnowConeTycoon.Shared.Enums;

namespace SnowConeTycoon.Shared.Models
{
    public class DayForecast
    {
        public Forecast Forecast { get; set; }
        public int Temperature { get; set; }

        public DayForecast()
        {
        }
    }
}
