using System;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Models;

namespace SnowConeTycoon.Shared.Services
{
    public interface IWeatherService
    {
        DayForecast GetForecast(int day);
    }
}
