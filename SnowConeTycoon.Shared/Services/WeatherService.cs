using System;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Services;

namespace SnowConeTycoon.Shared.Services
{
    public class WeatherService : IWeatherService
    {
        private int TemperatureDeviation = 10;
        private int TemperatureOffset = 70;
        private float SinRadius = 50f;
        private float SinPeriod = 2f;

        public WeatherService()
        {
        }

        public DayForecast GetForecast(int day)
        {
            var dayForecast = new DayForecast();
            dayForecast.Temperature = (int)((float)Math.Sin((day + 5) / SinPeriod) * SinRadius) + Utilities.GetRandomInt(-TemperatureDeviation, TemperatureDeviation) + TemperatureOffset;

            if (dayForecast.Temperature > 107)
            {
                dayForecast.Temperature -= 40;
            }

            if (dayForecast.Temperature < 18)
            {
                dayForecast.Temperature += 60;
            }

            dayForecast.Forecast = GetForecastByTemperature(dayForecast.Temperature);

            return dayForecast;
        }

        private Forecast GetForecastByTemperature(int temperature)
        {
            if (temperature >= 90)
            {
                return Forecast.Sunny;
            }
            else if (temperature >= 80)
            {
                return Forecast.PartlyCloudy;
            }
            else if (temperature >= 70)
            {
                return Forecast.Cloudy;
            }
            else if (temperature >= 45)
            {
                return Forecast.Rain;
            }
            else
            {
                return Forecast.Snow;
            }
        }
    }
}
