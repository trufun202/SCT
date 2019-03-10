using System;
namespace SnowConeTycoon.Shared.Models
{
    public class BusinessDayResult
    {
        public int PotentialCustomers { get; set; }
        public int SnowConesSold { get; set; }
        public int SnowConePrice { get; set; }
        public int NPSDetractors { get; set; }
        public int NPSPassives { get; set; }
        public int NPSPromoters { get; set; }
        public string DayQuote { get; set; }
        public int CoinsPrevious { get; set; }
        public int CoinsEarned { get; set; }
        public int SyrupPerSnowCone { get; set; }
    }
}