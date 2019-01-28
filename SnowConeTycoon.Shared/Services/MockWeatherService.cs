using System;
using SnowConeTycoon.Shared.Enums;

namespace SnowConeTycoon.Shared.Services
{
    public class MockWeatherService : IWeatherService
    {
        public MockWeatherService()
        {
        }

        public Forecast GetForecast(int day)
        {
            //todo write real weather service.  Maybe let the first 3 days be sunny/partly cloudy, then throw in some other weather.  Maybe follow a curve?  (Sine Wave, Bazier curve?)

            var rand = Utilities.GetRandomInt(0, 5);

            switch (rand)
            {
                case 0:
                    return Forecast.Cloudy;
                case 1:
                    return Forecast.PartlyCloudy;
                case 2:
                    return Forecast.Rain;
                case 3:
                    return Forecast.Snow;
                case 4:
                    return Forecast.Sunny;
                default:
                    return Forecast.Sunny;
            }
        }
    }
}
