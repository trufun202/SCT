using System;
using SnowConeTycoon.Shared.Enums;

namespace SnowConeTycoon.Shared.Services
{
    public interface IWeatherService
    {
        Forecast GetForecast(int day);
    }
}
