using System;
namespace SnowConeTycoon.Shared
{
    public static class Player
    {
        public static int CoinCount { get; private set; }
        public static int ConeCount { get; private set; }
        public static int IceCount { get; private set; }
        public static int SyrupCount { get; private set; }

        public static void Reset()
        {
            CoinCount = 5;
            ConeCount = 10;
            IceCount = 0;
            SyrupCount = 3;
        }

        public static void AddCoins(int count)
        {
            CoinCount += count;
        }

        public static void AddCones(int count)
        {
            ConeCount += count;
        }

        public static void AddIce(int count)
        {
            IceCount += count;
        }

        public static void AddSyrup(int count)
        {
            SyrupCount += count;
        }
    }
}
