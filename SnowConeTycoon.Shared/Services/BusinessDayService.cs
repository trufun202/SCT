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
                CoinsPrevious = Player.CoinCount,
                SyrupPerSnowCone = syrup
            };

            results.PotentialCustomers = (int)(forecast.Temperature / (double)4);

            var rankLeadsMin = 0;
            var rankLeadsMax = 1;

            switch (Player.GetRank())
            {
                case Rank.Lousy:
                    rankLeadsMin = 1;
                    rankLeadsMax = 4;
                    break;
                case Rank.Dabbling:
                    rankLeadsMin = 2;
                    rankLeadsMax = 6;
                    break;
                case Rank.Aspiring:
                    rankLeadsMin = 3;
                    rankLeadsMax = 8;
                    break;
                case Rank.Novice:
                    rankLeadsMin = 4;
                    rankLeadsMax = 10;
                    break;
                case Rank.Experienced:
                    rankLeadsMin = 5;
                    rankLeadsMax = 12;
                    break;
                case Rank.Skilled:
                    rankLeadsMin = 6;
                    rankLeadsMax = 14;
                    break;
                case Rank.Excellent:
                    rankLeadsMin = 7;
                    rankLeadsMax = 16;
                    break;
                case Rank.Professional:
                    rankLeadsMin = 8;
                    rankLeadsMax = 18;
                    break;
                case Rank.Veteran:
                    rankLeadsMin = 10;
                    rankLeadsMax = 20;
                    break;
                case Rank.Tycoon:
                    rankLeadsMin = 12;
                    rankLeadsMax = 24;
                    break;
            }

            results.PotentialCustomers += Utilities.GetRandomInt(rankLeadsMin, rankLeadsMax) + flyers;  //each flyer gives you 1 guaranteed lead

            var basePurchaseMin = 0f;
            var basePurchaseMax = 0f;
            var idealSyrup = 0;
            var idealPrice = 0;

            Player.AddFlyer(-flyers);

            results.DayQuote = string.Empty;

            switch (forecast.Forecast)
            {
                case Forecast.Sunny:
                    basePurchaseMin = 0.8f;
                    basePurchaseMax = 0.9f;
                    idealSyrup = Utilities.GetRandomInt(3, 4);
                    idealPrice = Utilities.GetRandomInt(3, 5);
                    break;
                case Forecast.Cloudy:
                    basePurchaseMin = 0.6f;
                    basePurchaseMax = 0.7f;
                    idealSyrup = Utilities.GetRandomInt(2, 3);
                    idealPrice = Utilities.GetRandomInt(1, 3);
                    break;
                case Forecast.PartlyCloudy:
                    basePurchaseMin = 0.7f;
                    basePurchaseMax = 0.8f;
                    idealSyrup = Utilities.GetRandomInt(2, 4);
                    idealPrice = Utilities.GetRandomInt(2, 4);
                    break;
                case Forecast.Rain:
                    basePurchaseMin = 0.2f;
                    basePurchaseMax = 0.4f;
                    idealSyrup = Utilities.GetRandomInt(1, 2);
                    idealPrice = Utilities.GetRandomInt(1, 2);
                    break;
                case Forecast.Snow:
                    basePurchaseMin = 0.0f;
                    basePurchaseMax = 0.1f;
                    idealSyrup = Utilities.GetRandomInt(1, 2);
                    idealPrice = Utilities.GetRandomInt(1, 1);
                    break;
            }

            var potentialSoldMin = (int)(results.PotentialCustomers * basePurchaseMin);
            var potentialSoldMax = (int)(results.PotentialCustomers * basePurchaseMax);

            var priceDiff = price - idealPrice;  //3 - 5 = -2

            if (price <= idealPrice || priceDiff == 1)
            {
                //a little pricey
                potentialSoldMin = (int)(potentialSoldMin * 0.90f);
                potentialSoldMax = (int)(potentialSoldMax * 0.90f);
            }
            else if (priceDiff == 2)
            {
                //pretty pricey
                potentialSoldMin = (int)(potentialSoldMin * 0.7f);
                potentialSoldMax = (int)(potentialSoldMax * 0.7f);
            }
            else if (priceDiff == 3)
            {
                //quite pricey
                potentialSoldMin = (int)(potentialSoldMin * 0.4f);
                potentialSoldMax = (int)(potentialSoldMax * 0.4f);
            }
            else
            {
                //way too damn expensive!
                potentialSoldMin = (int)(potentialSoldMin * 0.25f);
                potentialSoldMax = (int)(potentialSoldMax * 0.25f);
            }

            results.SnowConesSold = Utilities.GetRandomInt(potentialSoldMin, potentialSoldMax);

            if (results.SnowConesSold > Player.ConeCount)
            {
                results.SnowConesSold = Player.ConeCount;
            }

            if (results.SnowConesSold * syrup > Player.SyrupCount)
            {
                results.SnowConesSold = (int)(Player.SyrupCount / (double)syrup);
            }

            switch (forecast.Forecast)
            {
                case Forecast.Rain:
                    results.OverallDayOpinion = OverallDayOpinion.WeatherRain;
                    break;
                case Forecast.Snow:
                    results.OverallDayOpinion = OverallDayOpinion.WeatherCold;
                    break;
                default:
                    break;
            }

            var syrupDiff = Math.Abs(syrup - idealSyrup);

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
                results.OverallDayOpinion = OverallDayOpinion.Perfect;
            }
            else if (syrupDiff == 1)
            {
                //a little off, you'll have some passives
                promoterMin = 0.6f;
                promoterMax = 0.8f;
                passiveMin = 0.2f;
                passiveMax = 0.5f;
                results.OverallDayOpinion = OverallDayOpinion.JustOkay;
            }
            else if (syrupDiff == 2)
            {
                //pretty off, you'll have some detractors
                promoterMin = 0.1f;
                promoterMax = 0.3f;
                passiveMin = 0.2f;
                passiveMax = 0.4f;
                results.OverallDayOpinion = OverallDayOpinion.TooPlain;
            }
            else if (syrupDiff >= 3)
            {
                //yuck!  You're way off
                promoterMin = 0.0f;
                promoterMax = 0.1f;
                passiveMin = 0.2f;
                passiveMax = 0.3f; 
                results.OverallDayOpinion = OverallDayOpinion.TooSweet;
            }

            results.DayQuote = quoteService.GetQuote(results.OverallDayOpinion, results.SnowConesSold);
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
