using System;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Models;

namespace SnowConeTycoon.Shared.Services
{
    public class MockAverageBusinessDayService : IBusinessDayService
    {
        private DayQuoteService quoteService = new DayQuoteService();

        public MockAverageBusinessDayService()
        {
        }

        public BusinessDayResult CalculateDay(Forecast forecast, int cones, int syrup, int flyers, int price)
        {
            return new BusinessDayResult()
            {
                DayQuote = quoteService.GetQuote(OverallDayOpinion.JustOkay),
                SnowConePrice = 2,
                SnowConesSold = 2,
                PotentialCustomers = 5,
                CoinsEarned = 4,
                CoinsPrevious = 0,
                NPSDetractors = 1,
                NPSPassives = 2,
                NPSPromoters = 1
            };
        }
    }
}
