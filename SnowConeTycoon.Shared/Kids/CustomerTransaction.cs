using System;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;
using static SnowConeTycoon.Shared.Handlers.KidHandler;

namespace SnowConeTycoon.Shared.Kids
{
    public class CustomerTransaction
    {
        public bool MadePurchase { get; set; }
        public NPS NPS { get; set; }
        public KidType KidType { get; set; }
        public int KidIndex { get; set; }

        public CustomerTransaction()
        {        
        }

        public void ConfigureKid()
        {
            SetRandomKid();
            SetEmotion();
        }

        private void SetRandomKid()
        {
            KidType = Utilities.GetRandomInt(1, 100) <= 50 ? KidType.Boy : KidType.Girl;

            KidIndex = Utilities.GetRandomInt(1, 40);

            while(KidType == Player.KidType && KidIndex == Player.KidIndex)
            {
                KidIndex = Utilities.GetRandomInt(1, 40);
            }

            return;
        }

        private void SetEmotion()
        {
            if (MadePurchase)
            {
                switch (NPS)
                {
                    case NPS.Detractor:
                        KidHandler.MakeKidMad(KidType, KidIndex);
                        break;
                    case NPS.Passive:
                        KidHandler.MakeKidSad(KidType, KidIndex);
                        break;
                    case NPS.Promoter:
                        KidHandler.MakeKidHappy(KidType, KidIndex);
                        break;
                }
            }
            else
            {
                KidHandler.MakeKidSad(KidType, KidIndex);
            }
        }
    }
}
