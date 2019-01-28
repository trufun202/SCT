using System;
using SnowConeTycoon.Shared.Enums;

namespace SnowConeTycoon.Shared.Services
{
    public interface IBusinessDayService
    {
        BusinessDayResult CalculateDay(Forecast forecast, int cones, int syrup, int flyers, int price);
    }
}
