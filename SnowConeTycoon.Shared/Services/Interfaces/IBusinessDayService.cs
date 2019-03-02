using System;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Models;

namespace SnowConeTycoon.Shared.Services
{
    public interface IBusinessDayService
    {
        BusinessDayResult CalculateDay(DayForecast forecast, int cones, int syrup, int flyers, int price);
    }
}
