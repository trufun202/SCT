﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Kids;
using SnowConeTycoon.Shared.Models;

namespace SnowConeTycoon.Shared.Handlers
{
    public static class KidHandler
    {
        public enum KidType
        {
            Boy,
            Girl
        };

        public static Dictionary<string, IKid> Boys;
        public static Dictionary<string, IKid> Girls;

        private static Dictionary<string, IKid> SelectedKids;
        public static KidType SelectedKidType;
        public static int SelectedKidIndex;
        private static string SelectedKid;

        public static void Init()
        {
            Boys = new Dictionary<string, IKid>();
            Girls = new Dictionary<string, IKid>();

            Boys.Add("Boy1", new Kid("Lucas", "BoyAvatar_05", "Eyes_Open3"));
            Boys.Add("Boy2", new Kid("Walter", "BoyAvatar_16", "Eyes_Open5"));
            Boys.Add("Boy3", new Kid("Chad", "BoyAvatar_02", "Eyes_Open1", true, UnlockMechanism.Purchase, 100));
            Boys.Add("Boy4", new Kid("Jake", "BoyAvatar_04", "Eyes_Open4", true, UnlockMechanism.Purchase, 200));
            Boys.Add("Boy5", new Kid("Victor", "BoyAvatar_22", "Eyes_Open3", true, UnlockMechanism.Sales, 25));
            Boys.Add("Boy6", new Kid("Trevor", "BoyAvatar_30", "Eyes_Open4", true, UnlockMechanism.Sales, 75));
            Boys.Add("Boy7", new Kid("Greyson", "BoyAvatar_26", "Eyes_Open2", true, UnlockMechanism.Sales, 125));
            Boys.Add("Boy8", new Kid("Tommy", "BoyAvatar_08", "Eyes_Open6", true, UnlockMechanism.Purchase, 400));
            Boys.Add("Boy9", new Kid("Jon", "BoyAvatar_15", "Eyes_Open3", true, UnlockMechanism.Sales, 175));
            Boys.Add("Boy10", new Kid("Bailey", "BoyAvatar_10", "Eyes_Open7", true, UnlockMechanism.Sales, 225));
            Boys.Add("Boy11", new Kid("Braiden", "BoyAvatar_11", "Eyes_Open6", true, UnlockMechanism.Sales, 275));
            Boys.Add("Boy12", new Kid("Eric", "BoyAvatar_14", "Eyes_Open1", true, UnlockMechanism.Sales, 325));
            Boys.Add("Boy13", new Kid("Thomas", "BoyAvatar_33", "Eyes_Open5", true, UnlockMechanism.Sales, 375));
            Boys.Add("Boy14", new Kid("Marshall", "BoyAvatar_20", "Eyes_Open6", true, UnlockMechanism.Sales, 425));
            Boys.Add("Boy15", new Kid("Kyle", "BoyAvatar_09", "Eyes_Open3", true, UnlockMechanism.Sales, 475));
            Boys.Add("Boy16", new Kid("Sam", "BoyAvatar_03", "Eyes_Open2", true, UnlockMechanism.Purchase, 800));
            Boys.Add("Boy17", new Kid("Kevin", "BoyAvatar_17", "Eyes_Open2", true, UnlockMechanism.Sales, 525));
            Boys.Add("Boy18", new Kid("Josh", "BoyAvatar_18", "Eyes_Open6", true, UnlockMechanism.Sales, 575));
            Boys.Add("Boy19", new Kid("Chaz", "BoyAvatar_19", "Eyes_Open3", true, UnlockMechanism.Sales, 625));
            Boys.Add("Boy20", new Kid("Shawn", "BoyAvatar_12", "Eyes_Open5", true, UnlockMechanism.Sales, 675));
            Boys.Add("Boy21", new Kid("Allen", "BoyAvatar_21", "Eyes_Open7", true, UnlockMechanism.Sales, 725));
            Boys.Add("Boy22", new Kid("Jay", "BoyAvatar_01", "Eyes_Open3", true, UnlockMechanism.Sales, 775));
            Boys.Add("Boy23", new Kid("Eddie", "BoyAvatar_23", "Eyes_Open5", true, UnlockMechanism.Sales, 825));
            Boys.Add("Boy24", new Kid("Ralph", "BoyAvatar_24", "Eyes_Open6", true, UnlockMechanism.Sales, 875));
            Boys.Add("Boy25", new Kid("Curtis", "BoyAvatar_25", "Eyes_Open4", true, UnlockMechanism.Sales, 925));
            Boys.Add("Boy26", new Kid("Craig", "BoyAvatar_07", "Eyes_Open5", true, UnlockMechanism.Purchase, 600));
            Boys.Add("Boy27", new Kid("Freddie", "BoyAvatar_27", "Eyes_Open1", true, UnlockMechanism.Sales, 975));
            Boys.Add("Boy28", new Kid("Scott", "BoyAvatar_28", "Eyes_Open3", true, UnlockMechanism.Sales, 1025));
            Boys.Add("Boy29", new Kid("Wayne", "BoyAvatar_29", "Eyes_Open4", true, UnlockMechanism.Sales, 1075));
            Boys.Add("Boy30", new Kid("Chris", "BoyAvatar_06", "Eyes_Open7", true, UnlockMechanism.Purchase, 1000));
            Boys.Add("Boy31", new Kid("Gus", "BoyAvatar_31", "Eyes_Open6", true, UnlockMechanism.Sales, 1125));
            Boys.Add("Boy32", new Kid("Melvin", "BoyAvatar_32", "Eyes_Open7", true, UnlockMechanism.Sales, 1175));
            Boys.Add("Boy33", new Kid("Ronald", "BoyAvatar_13", "Eyes_Open2", true, UnlockMechanism.Sales, 1225));
            Boys.Add("Boy34", new Kid("Caleb", "BoyAvatar_34", "Eyes_Open5", true, UnlockMechanism.Sales, 1275));
            Boys.Add("Boy35", new Kid("Peter", "BoyAvatar_35", "Eyes_Open7", true, UnlockMechanism.Sales, 1325));
            Boys.Add("Boy36", new Kid("Norbert", "BoyAvatar_36", "Eyes_Open5", true, UnlockMechanism.Sales, 1375));
            Boys.Add("Boy37", new Kid("Frank", "BoyAvatar_37", "Eyes_Open6", true, UnlockMechanism.Sales, 1425));
            Boys.Add("Boy38", new Kid("Phil", "BoyAvatar_38", "Eyes_Open5", true, UnlockMechanism.Sales, 1475));
            Boys.Add("Boy39", new Kid("Gomer", "BoyAvatar_39", "Eyes_Open2", true, UnlockMechanism.Sales, 640));
            Boys.Add("Boy40", new Kid("Carl", "BoyAvatar_40", "Eyes_Open7", true, UnlockMechanism.Sales, 1575));

            Girls.Add("Girl1", new Kid("Lily", "GirlAvatar_01", "Eyes_Open2"));
            Girls.Add("Girl2", new Kid("Shana", "GirlAvatar_15", "Eyes_Open1"));
            Girls.Add("Girl3", new Kid("Maya", "GirlAvatar_03", "Eyes_Open2", true, UnlockMechanism.Purchase, 100));
            Girls.Add("Girl4", new Kid("London", "GirlAvatar_04", "Eyes_Open6", true, UnlockMechanism.Purchase, 250));
            Girls.Add("Girl5", new Kid("Navea", "GirlAvatar_18", "Eyes_Open7", true, UnlockMechanism.Sales, 50));
            Girls.Add("Girl6", new Kid("Kaley", "GirlAvatar_26", "Eyes_Open4", true, UnlockMechanism.Sales, 100));
            Girls.Add("Girl7", new Kid("Beelz", "GirlAvatar_31", "Eyes_Open3", true, UnlockMechanism.Sales, 150));
            Girls.Add("Girl8", new Kid("Abby", "GirlAvatar_08", "Eyes_Open7", true, UnlockMechanism.Sales, 200));
            Girls.Add("Girl9", new Kid("Anita", "GirlAvatar_09", "Eyes_Open2", true, UnlockMechanism.Sales, 250));
            Girls.Add("Girl10", new Kid("Gloria", "GirlAvatar_22", "Eyes_Open4", true, UnlockMechanism.Sales, 300));
            Girls.Add("Girl11", new Kid("Lena", "GirlAvatar_11", "Eyes_Open3", true, UnlockMechanism.Sales, 350));
            Girls.Add("Girl12", new Kid("Mary", "GirlAvatar_25", "Eyes_Open2", true, UnlockMechanism.Sales, 400));
            Girls.Add("Girl13", new Kid("Clara", "GirlAvatar_02", "Eyes_Open3", true, UnlockMechanism.Purchase, 550));
            Girls.Add("Girl14", new Kid("Angie", "GirlAvatar_14", "Eyes_Open3", true, UnlockMechanism.Sales, 450));
            Girls.Add("Girl15", new Kid("Lyla", "GirlAvatar_35", "Eyes_Open3", true, UnlockMechanism.Sales, 500));
            Girls.Add("Girl16", new Kid("Abigail", "GirlAvatar_16", "Eyes_Open7", true, UnlockMechanism.Sales, 550));
            Girls.Add("Girl17", new Kid("Samantha", "GirlAvatar_36", "Eyes_Open3", true, UnlockMechanism.Sales, 600));
            Girls.Add("Girl18", new Kid("Jenny", "GirlAvatar_05", "Eyes_Open2", true, UnlockMechanism.Purchase, 600));
            Girls.Add("Girl19", new Kid("Queen Ella", "GirlAvatar_30", "Eyes_Open2", true, UnlockMechanism.Sales, 650));
            Girls.Add("Girl20", new Kid("Ellee", "GirlAvatar_33", "Eyes_Open3", true, UnlockMechanism.Sales, 700));
            Girls.Add("Girl21", new Kid("Rebecca", "GirlAvatar_21", "Eyes_Open3", true, UnlockMechanism.Sales, 750));
            Girls.Add("Girl22", new Kid("Marilyn", "GirlAvatar_06", "Eyes_Open4", true, UnlockMechanism.Purchase, 800));
            Girls.Add("Girl23", new Kid("Hannah", "GirlAvatar_23", "Eyes_Open3", true, UnlockMechanism.Sales, 800));
            Girls.Add("Girl24", new Kid("Miranda", "GirlAvatar_24", "Eyes_Open1", true, UnlockMechanism.Sales, 850));
            Girls.Add("Girl25", new Kid("Jamie", "GirlAvatar_12", "Eyes_Open4", true, UnlockMechanism.Sales, 900));
            Girls.Add("Girl26", new Kid("Lucy", "GirlAvatar_10", "Eyes_Open1", true, UnlockMechanism.Sales, 950));
            Girls.Add("Girl27", new Kid("Kendall", "GirlAvatar_27", "Eyes_Open5", true, UnlockMechanism.Sales, 1000));
            Girls.Add("Girl28", new Kid("Olive", "GirlAvatar_28", "Eyes_Open1", true, UnlockMechanism.Sales, 1050));
            Girls.Add("Girl29", new Kid("Sarah", "GirlAvatar_29", "Eyes_Open6", true, UnlockMechanism.Sales, 1100));
            Girls.Add("Girl30", new Kid("Tina", "GirlAvatar_19", "Eyes_Open3", true, UnlockMechanism.Sales, 1150));
            Girls.Add("Girl31", new Kid("Gabby", "GirlAvatar_07", "Eyes_Open3", true, UnlockMechanism.Sales, 1200));
            Girls.Add("Girl32", new Kid("Isabelle", "GirlAvatar_32", "Eyes_Open2", true, UnlockMechanism.Sales, 1250));
            Girls.Add("Girl33", new Kid("Donna", "GirlAvatar_20", "Eyes_Open1", true, UnlockMechanism.Sales, 1300));
            Girls.Add("Girl34", new Kid("Ana", "GirlAvatar_34", "Eyes_Open7", true, UnlockMechanism.Sales, 1350));
            Girls.Add("Girl35", new Kid("Tasha", "GirlAvatar_13", "Eyes_Open5", true, UnlockMechanism.Sales, 1400));
            Girls.Add("Girl36", new Kid("Lara", "GirlAvatar_17", "Eyes_Open2", true, UnlockMechanism.Sales, 1450));
            Girls.Add("Girl37", new Kid("Melva", "GirlAvatar_37", "Eyes_Open5", true, UnlockMechanism.Sales, 1500));
            Girls.Add("Girl38", new Kid("Savannah", "GirlAvatar_38", "Eyes_Open6", true, UnlockMechanism.Sales, 1550));
            Girls.Add("Girl39", new Kid("Jana", "GirlAvatar_39", "Eyes_Open5", true, UnlockMechanism.Purchase, 1200));
            Girls.Add("Girl40", new Kid("Kayley", "GirlAvatar_40", "Eyes_Open7", true, UnlockMechanism.Sales, 1600));

            SelectedKidType = KidType.Girl;
            SelectedKidIndex = 1;

            //SelectKid(KidType.Boy, Utilities.GetRandomInt(1, 20));
            //SelectKid(KidType.Girl, Utilities.GetRandomInt(1, 40));
        }

        public static List<int> GetBoyLocks()
        { 
            var locks = new List<int>();

            foreach(var boy in Boys)
            {
                locks.Add(boy.Value.IsLocked ? 1 : 0);
            }

            return locks;
        }

        public static List<int> GetGirlLocks()
        {
            var locks = new List<int>();

            foreach (var girl in Girls)
            {
                locks.Add(girl.Value.IsLocked ? 1: 0);
            }

            return locks;
        }

        public static void SetBoyLocks(List<int> locks)
        {
            int index = 0;

            foreach (var boy in Boys)
            {
                boy.Value.IsLocked = locks[index] == 1 ? true : false;
                index++;
            }
        }

        public static void SetGirlLocks(List<int> locks)
        {
            int index = 0;

            foreach (var girl in Girls)
            {
                girl.Value.IsLocked = locks[index] == 1 ? true : false;
                index++;
            }
        }

        public static void SelectKid(KidType type, int index)
        {
            SelectedKidIndex = index;

            switch (type)
            {
                case KidType.Boy:
                    SelectedKids = Boys;
                    SelectedKid = $"Boy{index}";
                    break;
                case KidType.Girl:
                    SelectedKids = Girls;
                    SelectedKid = $"Girl{index}";
                    break;
            }

            Player.KidType = type;
            Player.KidIndex = index;
        }

        public static IKid CurrentKid
        {
            get
            {
                return SelectedKids[SelectedKid];
            }
        }

        public static void Update(GameTime gameTime)
        {
            SelectedKids[SelectedKid].Update(gameTime);
        }

        public static void SetKidType(KidType type)
        {
            SelectedKidType = type;

            switch (SelectedKidType)
            {
                case KidType.Boy:
                    SelectedKids = Boys;
                    SelectedKid = $"Boy{SelectedKidIndex}";
                    break;
                case KidType.Girl:
                    SelectedKids = Girls;
                    SelectedKid = $"Girl{SelectedKidIndex}";
                    break;
            }
        }

        public static void Draw(SpriteBatch spriteBatch, int x, int y, bool facingAway = false)
        {
            SelectedKids[SelectedKid].Draw(spriteBatch, x, y, facingAway, null);
        }

        public static string GetKidName(KidType type, int index)
        {
            var kidType = "Boy";
            var kids = Boys;

            if (type == KidType.Girl)
            {
                kidType = "Girl";
                kids = Girls;
            }

            return kids[$"{kidType}{index}"].GetName();
        }

        public static void UpdateKid(KidType type, int index, GameTime gameTime)
        {
            var kidType = "Boy";
            var kids = Boys;

            if (type == KidType.Girl)
            {
                kidType = "Girl";
                kids = Girls;
            }

            kids[$"{kidType}{index}"].Update(gameTime);
        }

        public static void DrawKid(KidType type, int index, SpriteBatch spriteBatch, int x, int y, int? size, bool facingAway = false, bool isCustomer = false)
        {
            var kidType = "Boy";
            var kids = Boys;
            
            if (type == KidType.Girl)
            {
                kidType = "Girl";
                kids = Girls;
            }

            kids[$"{kidType}{index}"].Draw(spriteBatch, x, y, facingAway, size, isCustomer);
        }

        public static void UnlockKid(KidType type, int index)
        {
            var kidType = "Boy";
            var kids = Boys;

            if (type == KidType.Girl)
            {
                kidType = "Girl";
                kids = Girls;
            }

            kids[$"{kidType}{index}"].Unlock();
        }

        public static void MakeKidHappy(KidType type, int index)
        {
            var kidType = "Boy";
            var kids = Boys;

            if (type == KidType.Girl)
            {
                kidType = "Girl";
                kids = Girls;
            }

            kids[$"{kidType}{index}"].MakeHappy();
        }

        public static void MakeKidMad(KidType type, int index)
        {
            var kidType = "Boy";
            var kids = Boys;

            if (type == KidType.Girl)
            {
                kidType = "Girl";
                kids = Girls;
            }

            kids[$"{kidType}{index}"].MakeMad();
        }

        public static void MakeKidSad(KidType type, int index)
        {
            var kidType = "Boy";
            var kids = Boys;

            if (type == KidType.Girl)
            {
                kidType = "Girl";
                kids = Girls;
            }

            kids[$"{kidType}{index}"].MakeSad();
        }

        public static UnlockResult GetUnlock()
        {
            var result = new UnlockResult()
            {
                Unlocked = false
            };

            for (int i = 1; i <= Boys.Count; i++)
            {
                var boy = Boys["Boy" + i];
                var girl = Girls["Girl" + i];

                if (girl.IsLocked && girl.UnlockMechanism == UnlockMechanism.Sales && girl.UnlockPrice <= Player.SoldCount)
                {
                    result.Unlocked = true;
                    result.KidType = KidType.Girl;
                    result.KidIndex = i;
                    return result;
                }

                if (boy.IsLocked && boy.UnlockMechanism == UnlockMechanism.Sales && boy.UnlockPrice <= Player.SoldCount)
                {
                    result.Unlocked = true;
                    result.KidType = KidType.Boy;
                    result.KidIndex = i;
                    return result;
                }
            }

            return result;
        }
    }
}
