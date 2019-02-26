using System;
using System.IO.IsolatedStorage;
using static SnowConeTycoon.Shared.Handlers.KidHandler;

namespace SnowConeTycoon.Shared.Models
{
    public static class Player
    {
        public static KidType KidType { get; set; }
        public static int KidIndex { get; set; }
        public static int CoinCount { get; private set; }
        public static int ConeCount { get; private set; }
        public static int IceCount { get; private set; }
        public static int SyrupCount { get; private set; }
        public static int FlyerCount { get; private set; }
        public static int ConsecutiveDaysPlayed { get; private set; }
        public static DateTime DailyBonusLastReceived { get; set; }

        public static void Reset()
        {
            CoinCount = 10;
            ConeCount = 10;
            IceCount = 10;
            SyrupCount = 10;
            ConsecutiveDaysPlayed = 1;
            DailyBonusLastReceived = DateTime.Now;
        }

        public static GameData ToGameData()
        {
            return new GameData()
            {
                KidType = KidType,
                KidIndex = KidIndex,
                CoinCount = CoinCount,
                ConeCount = ConeCount,
                IceCount = IceCount,
                SyrupCount = SyrupCount,
                FlyerCount = FlyerCount,
                ConsecutiveDayCount = ConsecutiveDaysPlayed,
                LastPlayed = DateTime.Now.Date,
                DailyBonusLastReceived = DailyBonusLastReceived
            };
        }

        public static void FromGameData(GameData gameData)
        {
            KidType = gameData.KidType;
            KidIndex = gameData.KidIndex;
            CoinCount = gameData.CoinCount;
            ConeCount = gameData.ConeCount;
            IceCount = gameData.IceCount;
            SyrupCount = gameData.SyrupCount;
            FlyerCount = gameData.FlyerCount;
            ConsecutiveDaysPlayed = gameData.ConsecutiveDayCount;
            DailyBonusLastReceived = gameData.DailyBonusLastReceived;

            TimeSpan ts = DateTime.Now.Date - gameData.LastPlayed;

            if (ts.Days <= 1)
            {
                ConsecutiveDaysPlayed += ts.Days;
            }
            else
            {
                ConsecutiveDaysPlayed = 1;
            }
        }

        public static int GetIceEarned()
        {
            switch (ConsecutiveDaysPlayed)
            {
                case 2:
                    return 4;
                case 3:
                    return 6;
                case 4:
                    return 8;
                case 5:
                    return 10;
                default:
                    return 1;
            }
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

        public static void AddFlyer(int count)
        {
            FlyerCount += count;
        }

        public static void SetCoins(int count)
        {
            CoinCount = count;
        }

        public static void SetCones(int count)
        {
            ConeCount = count;
        }

        public static void SetIce(int count)
        {
            IceCount = count;
        }

        public static void SetSyrup(int count)
        {
            SyrupCount = count;
        }

        public static void SetFlyer(int count)
        {
            FlyerCount = count;
        }

        public static void SetConsecutiveDays(int count)
        {
            ConsecutiveDaysPlayed = count;
        }
    }
}
