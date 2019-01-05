using System;
namespace SnowConeTycoon.Shared
{
    public static class Player
    {
        public static int CoinCount { get; set; }
        public static int ConeCount { get; set; }
        public static int IceCount { get; set; }
        public static int SyrupCount { get; set; }

        public static void Reset()
        {
            CoinCount = 5;
            ConeCount = 10;
            IceCount = 0;
            SyrupCount = 3;
        }
    }
}
