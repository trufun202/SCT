using System;
using System.IO.IsolatedStorage;
using SnowConeTycoon.Shared.Enums;
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
        public static int SoldCount { get; private set; }
        public static int ConsecutiveDaysPlayed { get; private set; }
        public static DateTime DailyBonusLastReceived { get; set; }
        public static int CurrentDay { get; set; }
        public static GameSpeed GameSpeed = GameSpeed.x1;

        public static void Reset()
        {
            CoinCount = 10;
            ConeCount = 10;
            IceCount = 10;
            SyrupCount = 10;
            ConsecutiveDaysPlayed = 1;
            CurrentDay = 1;
            DailyBonusLastReceived = DateTime.Now.Date;
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
                CurrentDay = CurrentDay,
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
            CurrentDay = gameData.CurrentDay;

            TimeSpan ts = DateTime.Now.Date - gameData.LastPlayed;

            if (ts.Days == 1)
            {
                ConsecutiveDaysPlayed++;
            }
            else if (ts.Days == 0)
            {
                //do nothing...because it's the same day
            }
            else
            {
                //it's been greater than 1 day since they played, so reset back to 1
                ConsecutiveDaysPlayed = 1;
            }

            if (ConsecutiveDaysPlayed > 5)
            {
                //cap them at 5 consecutive days played
                ConsecutiveDaysPlayed = 5;
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

        public static Rank GetRank()
        {
            if (SoldCount <= 10)
                return Rank.Lousy;
            else if (SoldCount <= 30)
                return Rank.Dabbling;
            else if (SoldCount <= 70)
                return Rank.Aspiring;
            else if (SoldCount <= 130)
                return Rank.Novice;
            else if (SoldCount <= 210)
                return Rank.Experienced;
            else if (SoldCount <= 310)
                return Rank.Skilled;
            else if (SoldCount <= 430)
                return Rank.Excellent;
            else if (SoldCount <= 570)
                return Rank.Professional;
            else if (SoldCount <= 730)
                return Rank.Veteran;
            else
                return Rank.Tycoon;
        }

        public static void AddSold(int count)
        {
            SoldCount += count;
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

            if (IceCount < 0)
                IceCount = 0;
        }

        public static void AddSyrup(int count)
        {
            SyrupCount += count;

            if (SyrupCount < 0)
                SyrupCount = 0;
        }

        public static void AddFlyer(int count)
        {
            FlyerCount += count;

            if (FlyerCount < 0)
                FlyerCount = 0;
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
