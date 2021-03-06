﻿using System;
using System.Collections.Generic;
using SnowConeTycoon.Shared.Enums;
using static SnowConeTycoon.Shared.Handlers.KidHandler;

namespace SnowConeTycoon.Shared.Models
{
    [Serializable]
    public class GameData
    {
        public KidType KidType { get; set; }
        public int KidIndex { get; set; }
        public int CoinCount { get; set; }
        public int ConeCount { get; set; }
        public int IceCount { get; set; }
        public int SyrupCount { get; set; }
        public int FlyerCount { get; set; }
        public int ConsecutiveDayCount { get; set; }
        public DateTime LastPlayed { get; set; }
        public DateTime DailyBonusLastReceived { get; set; }
        public int CurrentDay { get; set; }
        public int SoldCount { get; set; }
        public List<int> BoyLocks { get; set; }
        public List<int> GirlLocks { get; set; }
        public bool RatedApp { get; set; }
        public Degrees Degrees { get; set; }
        public bool MusicEnabled { get; set; }
        public bool SoundEnabled { get; set; }
    }
}
