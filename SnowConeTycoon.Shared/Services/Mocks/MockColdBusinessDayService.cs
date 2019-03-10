using System;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Models;

namespace SnowConeTycoon.Shared.Services
{
    public class MockColdBusinessDayService : IBusinessDayService
    {
        private DayQuoteService quoteService = new DayQuoteService();

        public MockColdBusinessDayService()
        {
        }

        public BusinessDayResult CalculateDay(DayForecast forecast, int cones, int syrup, int flyers, int price)
        {
            return new BusinessDayResult()
            {
                DayQuote = quoteService.GetQuote(OverallDayOpinion.WeatherCold, 1),
                SnowConePrice = 1,
                SnowConesSold = 1,
                PotentialCustomers = 6,
                CoinsEarned = 1,
                CoinsPrevious = 0,
                NPSDetractors = 0,
                NPSPassives = 1,
                NPSPromoters = 0
            };
        }
    }
}
