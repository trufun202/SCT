using System;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Models;

namespace SnowConeTycoon.Shared.Services
{
    public class MockWeatherService : IWeatherService
    {
        public MockWeatherService()
        {
        }

        public DayForecast GetForecast(int day)
        {
            var dayForecast = new DayForecast();

            var rand = Utilities.GetRandomInt(0, 5);

            switch (rand)
            {
                case 0:
                    dayForecast.Forecast = Forecast.Cloudy;
                    dayForecast.Temperature = 75;
                    break;
                case 1:
                    dayForecast.Forecast = Forecast.PartlyCloudy;
                    dayForecast.Temperature = 85;
                    break;
                case 2:
                    dayForecast.Forecast = Forecast.Rain;
                    dayForecast.Temperature = 65;
                    break;
                case 3:
                    dayForecast.Forecast = Forecast.Snow;
                    dayForecast.Temperature = 55;
                    break;
                case 4:
                    dayForecast.Forecast = Forecast.Sunny;
                    dayForecast.Temperature = 105;
                    break;
                default:
                    dayForecast.Forecast = Forecast.Sunny;
                    dayForecast.Temperature = 105;
                    break;
            }

            return dayForecast;
        }
    }
}
