using System;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Services
{
    public class BusinessDayService : IBusinessDayService
    {
        DayQuoteService quoteService;

        public BusinessDayService()
        {
            quoteService = new DayQuoteService();
        }

        public BusinessDayResult CalculateDay(DayForecast forecast, int cones, int syrup, int flyers, int price)
        {
            var results = new BusinessDayResult()
            {
                SnowConePrice = price,
                CoinsPrevious = Player.CoinCount
            };

            results.PotentialCustomers = (int)(forecast.Temperature / (double)2);

            var rankLeadsMin = 0;
            var rankLeadsMax = 1;

            switch (Player.GetRank())
            {
                case Rank.Lousy:
                    rankLeadsMin = 0;
                    rankLeadsMax = 1;
                    break;
                case Rank.Dabbling:
                    rankLeadsMin = 1;
                    rankLeadsMax = 4;
                    break;
                case Rank.Aspiring:
                    rankLeadsMin = 2;
                    rankLeadsMax = 6;
                    break;
                case Rank.Novice:
                    rankLeadsMin = 3;
                    rankLeadsMax = 8;
                    break;
                case Rank.Experienced:
                    rankLeadsMin = 4;
                    rankLeadsMax = 10;
                    break;
                case Rank.Skilled:
                    rankLeadsMin = 5;
                    rankLeadsMax = 12;
                    break;
                case Rank.Excellent:
                    rankLeadsMin = 6;
                    rankLeadsMax = 14;
                    break;
                case Rank.Professional:
                    rankLeadsMin = 7;
                    rankLeadsMax = 16;
                    break;
                case Rank.Veteran:
                    rankLeadsMin = 8;
                    rankLeadsMax = 18;
                    break;
                case Rank.Tycoon:
                    rankLeadsMin = 10;
                    rankLeadsMax = 20;
                    break;
            }

            results.PotentialCustomers += Utilities.GetRandomInt(rankLeadsMin, rankLeadsMax);

            var soldCount = flyers;  //as a base, they'll get 1 purchase per flyer
            var basePurchaseMin = 0f;
            var basePurchaseMax = 0f;
            var idealSyrup = 0;

            results.DayQuote = string.Empty;

            switch (forecast.Forecast)
            {
                case Forecast.Sunny:
                    basePurchaseMin = 0.8f;
                    basePurchaseMax = 0.9f;
                    idealSyrup = Utilities.GetRandomInt(3, 4);
                    break;
                case Forecast.Cloudy:
                    basePurchaseMin = 0.6f;
                    basePurchaseMax = 0.7f;
                    idealSyrup = Utilities.GetRandomInt(2, 3);
                    break;
                case Forecast.PartlyCloudy:
                    basePurchaseMin = 0.7f;
                    basePurchaseMax = 0.8f;
                    idealSyrup = Utilities.GetRandomInt(2, 4);
                    break;
                case Forecast.Rain:
                    basePurchaseMin = 0.2f;
                    basePurchaseMax = 0.4f;
                    idealSyrup = Utilities.GetRandomInt(1, 2);
                    results.DayQuote = quoteService.GetQuote(OverallDayOpinion.WeatherRain);
                    break;
                case Forecast.Snow:
                    basePurchaseMin = 0.0f;
                    basePurchaseMax = 0.1f;
                    idealSyrup = Utilities.GetRandomInt(1, 2);
                    results.DayQuote = quoteService.GetQuote(OverallDayOpinion.WeatherCold);
                    break;
            }

            var potentialSoldMin = (int)(results.PotentialCustomers * basePurchaseMin);
            var potentialSoldMax = (int)(results.PotentialCustomers * basePurchaseMax);

            results.SnowConesSold = Utilities.GetRandomInt(potentialSoldMin, potentialSoldMax);

            var syrupDiff = syrup - idealSyrup;

            var promoterMin = 0f;
            var promoterMax = 0f;
            var passiveMin = 0f;
            var passiveMax = 0f;

            if (syrupDiff == 0)
            {
                //spot on, you'll have happy customers
                promoterMin = 0.9f;
                promoterMax = 1f;
                passiveMin = 0f;
                passiveMax = 0.1f;
                results.DayQuote = quoteService.GetQuote(OverallDayOpinion.Perfect);
            }
            else if (syrupDiff == 1)
            {
                //a little off, you'll have some passives
                promoterMin = 0.6f;
                promoterMax = 0.8f;
                passiveMin = 0.2f;
                passiveMax = 0.5f;
                results.DayQuote = quoteService.GetQuote(OverallDayOpinion.JustOkay);
            }
            else if (syrupDiff == 2)
            {
                //pretty off, you'll have some detractors
                promoterMin = 0.1f;
                promoterMax = 0.3f;
                passiveMin = 0.2f;
                passiveMax = 0.4f;
                results.DayQuote = quoteService.GetQuote(OverallDayOpinion.TooPlain);
            }
            else if (syrupDiff >= 3)
            {
                //yuck!  You're way off
                promoterMin = 0.0f;
                promoterMax = 0.1f;
                passiveMin = 0.2f;
                passiveMax = 0.3f;
                results.DayQuote = quoteService.GetQuote(OverallDayOpinion.TooSweet);
            }

            results.NPSPromoters = Utilities.GetRandomInt((int)(results.SnowConesSold * promoterMin), (int)(results.SnowConesSold * promoterMax));
            results.NPSPassives = Utilities.GetRandomInt((int)(results.SnowConesSold * passiveMin), (int)(results.SnowConesSold * passiveMax));
            results.NPSDetractors = results.SnowConesSold - results.NPSPromoters - results.NPSPassives;

            if (results.NPSPromoters < 0)
            {
                results.NPSPromoters = 0;
            }

            if (results.NPSPassives < 0)
            {
                results.NPSPassives = 0;
            }

            if (results.NPSDetractors < 0)
            {
                results.NPSDetractors = 0;
            }

            results.CoinsEarned = results.SnowConesSold * results.SnowConePrice;

            return results;
        }
    }
}
