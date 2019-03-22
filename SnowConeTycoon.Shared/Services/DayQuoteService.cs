using System;
using System.Collections.Generic;
using SnowConeTycoon.Shared.Enums;

namespace SnowConeTycoon.Shared.Services
{
    public class DayQuoteService
    {
        private Dictionary<OverallDayOpinion, List<string>> Quotes = new Dictionary<OverallDayOpinion, List<string>>();

        public DayQuoteService()
        {
            var perfectQuotes = new List<string>();
            perfectQuotes.Add("yummy yummy!");
            perfectQuotes.Add("tasty!");
            perfectQuotes.Add("sooo goooood!");
            perfectQuotes.Add("mmmmmm!");

            var justOkayQuotes = new List<string>();
            justOkayQuotes.Add("meh.");
            justOkayQuotes.Add("not bad.");
            justOkayQuotes.Add("blah...");
            justOkayQuotes.Add("nothing special.");

            var tooSweetQuotes = new List<string>();
            tooSweetQuotes.Add("yuck. too sweet!");
            tooSweetQuotes.Add("too much syrup!");
            tooSweetQuotes.Add("barf-o-rama!");
            tooSweetQuotes.Add("go easy on the syrup.");

            var tooPlainQuotes = new List<string>();
            tooPlainQuotes.Add("no flavor!");
            tooPlainQuotes.Add("needs more syrup.");
            tooPlainQuotes.Add("tastes like ice...");
            tooPlainQuotes.Add("more syrup, please!");

            var rainQuotes = new List<string>();
            rainQuotes.Add("too rainy a snow cone.");
            rainQuotes.Add("what a blah day.");
            rainQuotes.Add("not in the mood a snow cone");
            rainQuotes.Add("too wet to go outside!");

            var snowQuotes = new List<string>();
            snowQuotes.Add("brrrr!!");
            snowQuotes.Add("too cold for a snow cone!");
            snowQuotes.Add(" *teeth chattering* ");
            snowQuotes.Add("snow cones? are you crazy!?");

            Quotes.Add(OverallDayOpinion.Perfect, perfectQuotes);
            Quotes.Add(OverallDayOpinion.JustOkay, justOkayQuotes);
            Quotes.Add(OverallDayOpinion.TooSweet, tooSweetQuotes);
            Quotes.Add(OverallDayOpinion.TooPlain, tooPlainQuotes);
            Quotes.Add(OverallDayOpinion.WeatherRain, rainQuotes);
            Quotes.Add(OverallDayOpinion.WeatherCold, snowQuotes);
        }

        public string GetQuote(OverallDayOpinion opinion, int soldCount)
        {
            if (soldCount == 0)
                return "...";

            var quotes = Quotes[opinion];

            return quotes[Utilities.GetRandomInt(0, quotes.Count - 1)];
        }
    }
}