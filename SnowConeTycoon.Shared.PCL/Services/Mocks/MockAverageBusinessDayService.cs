using System;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Models;

namespace SnowConeTycoon.Shared.Services
{
    public class MockRainyBusinessDayService : IBusinessDayService
    {
        private DayQuoteService quoteService = new DayQuoteService();

        public MockRainyBusinessDayService()
        {
        }

        public BusinessDayResult CalculateDay(Forecast forecast, int cones, int syrup, int flyers, int price)
        {
            return new BusinessDayResult()
            {
                DayQuote = quoteService.GetQuote(OverallDayOpinion.WeatherRain),
                SnowConePrice = 1,
                SnowConesSold = 2,
                PotentialCustomers = 10,
                CoinsEarned = 2,
                CoinsPrevious = 0,
                NPSDetractors = 1,
                NPSPassives = 1,
                NPSPromoters = 0
            };
        }
    }
}
