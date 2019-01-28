using System;
using SnowConeTycoon.Shared.Enums;

namespace SnowConeTycoon.Shared.Services
{
    public class MockPerfectBusinessDayService : IBusinessDayService
    {
        private DayQuoteService quoteService = new DayQuoteService();

        public MockPerfectBusinessDayService()
        {
        }

        public BusinessDayResult CalculateDay(Forecast forecast, int cones, int syrup, int flyers, int price)
        {
            return new BusinessDayResult()
            {
                DayQuote = quoteService.GetQuote(OverallDayOpinion.Perfect),
                SnowConePrice = 2,
                SnowConesSold = 20,
                PotentialCustomers = 20,
                CoinsEarned = 40,
                CoinsPrevious = 0,
                NPSDetractors = 0,
                NPSPassives = 0,
                NPSPromoters = 20
            };
        }
    }
}
