using System;
using static SnowConeTycoon.Shared.Handlers.KidHandler;

namespace SnowConeTycoon.Shared.Models
{
    public class UnlockResult
    {
        public bool Unlocked { get; set; }
        public KidType KidType { get; set; }
        public int KidIndex { get; set; }
    }
}
