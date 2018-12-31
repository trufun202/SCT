using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Kids;

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

            Boys.Add("Boy1", new Kid("Jay", "BoyAvatar_01", "Eyes_Open3"));
            Boys.Add("Boy2", new Kid("Chad", "BoyAvatar_02", "Eyes_Open1"));
            Boys.Add("Boy3", new Kid("Sam", "BoyAvatar_03", "Eyes_Open2", true));
            Boys.Add("Boy4", new Kid("Jake", "BoyAvatar_04", "Eyes_Open4"));
            Boys.Add("Boy5", new Kid("Lucas", "BoyAvatar_05", "Eyes_Open3"));
            Boys.Add("Boy6", new Kid("Chris", "BoyAvatar_06", "Eyes_Open7"));
            Boys.Add("Boy7", new Kid("Craig", "BoyAvatar_07", "Eyes_Open5"));
            Boys.Add("Boy8", new Kid("Tommy", "BoyAvatar_08", "Eyes_Open6"));
            Boys.Add("Boy9", new Kid("Kyle", "BoyAvatar_09", "Eyes_Open3"));
            Boys.Add("Boy10", new Kid("Bailey", "BoyAvatar_10", "Eyes_Open7"));
            Boys.Add("Boy11", new Kid("Braiden", "BoyAvatar_11", "Eyes_Open6"));
            Boys.Add("Boy12", new Kid("Shawn", "BoyAvatar_12", "Eyes_Open5"));
            Boys.Add("Boy13", new Kid("Ronald", "BoyAvatar_13", "Eyes_Open2"));
            Boys.Add("Boy14", new Kid("Eric", "BoyAvatar_14", "Eyes_Open1"));
            Boys.Add("Boy15", new Kid("Jon", "BoyAvatar_15", "Eyes_Open3"));
            Boys.Add("Boy16", new Kid("Walter", "BoyAvatar_16", "Eyes_Open5"));
            Boys.Add("Boy17", new Kid("Kevin", "BoyAvatar_17", "Eyes_Open2"));
            Boys.Add("Boy18", new Kid("Josh", "BoyAvatar_18", "Eyes_Open6"));
            Boys.Add("Boy19", new Kid("Chaz", "BoyAvatar_19", "Eyes_Open3"));
            Boys.Add("Boy20", new Kid("Marshall", "BoyAvatar_20", "Eyes_Open6"));
            Boys.Add("Boy21", new Kid("Allen", "BoyAvatar_21", "Eyes_Open7"));
            Boys.Add("Boy22", new Kid("Victor", "BoyAvatar_22", "Eyes_Open3"));
            Boys.Add("Boy23", new Kid("Eddie", "BoyAvatar_23", "Eyes_Open5"));
            Boys.Add("Boy24", new Kid("Ralph", "BoyAvatar_24", "Eyes_Open6"));
            Boys.Add("Boy25", new Kid("Curtis", "BoyAvatar_25", "Eyes_Open4"));
            Boys.Add("Boy26", new Kid("Greyson", "BoyAvatar_26", "Eyes_Open2"));
            Boys.Add("Boy27", new Kid("Freddie", "BoyAvatar_27", "Eyes_Open1"));
            Boys.Add("Boy28", new Kid("Scott", "BoyAvatar_28", "Eyes_Open3"));
            Boys.Add("Boy29", new Kid("Wayne", "BoyAvatar_29", "Eyes_Open4"));
            Boys.Add("Boy30", new Kid("Trevor", "BoyAvatar_30", "Eyes_Open4"));
            Boys.Add("Boy31", new Kid("Gus", "BoyAvatar_31", "Eyes_Open6"));
            Boys.Add("Boy32", new Kid("Melvin", "BoyAvatar_32", "Eyes_Open7"));
            Boys.Add("Boy33", new Kid("Thomas", "BoyAvatar_33", "Eyes_Open5"));
            Boys.Add("Boy34", new Kid("Caleb", "BoyAvatar_34", "Eyes_Open5"));
            Boys.Add("Boy35", new Kid("Peter", "BoyAvatar_35", "Eyes_Open7"));
            Boys.Add("Boy36", new Kid("Norbert", "BoyAvatar_36", "Eyes_Open5"));
            Boys.Add("Boy37", new Kid("Frank", "BoyAvatar_37", "Eyes_Open6"));
            Boys.Add("Boy38", new Kid("Phil", "BoyAvatar_38", "Eyes_Open5"));
            Boys.Add("Boy39", new Kid("Gomer", "BoyAvatar_39", "Eyes_Open2"));
            Boys.Add("Boy40", new Kid("Carl", "BoyAvatar_40", "Eyes_Open7"));

            Girls.Add("Girl1", new Kid("Lilly", "GirlAvatar_01", "Eyes_Open2"));
            Girls.Add("Girl2", new Kid("Clara", "GirlAvatar_02", "Eyes_Open3"));
            Girls.Add("Girl3", new Kid("Clara", "GirlAvatar_03", "Eyes_Open2"));
            Girls.Add("Girl4", new Kid("London", "GirlAvatar_04", "Eyes_Open6"));
            Girls.Add("Girl5", new Kid("Jennfer", "GirlAvatar_05", "Eyes_Open2"));
            Girls.Add("Girl6", new Kid("Marilyn", "GirlAvatar_06", "Eyes_Open4"));
            Girls.Add("Girl7", new Kid("Gabby", "GirlAvatar_07", "Eyes_Open3"));
            Girls.Add("Girl8", new Kid("Abby", "GirlAvatar_08", "Eyes_Open7"));
            Girls.Add("Girl9", new Kid("Anita", "GirlAvatar_09", "Eyes_Open2"));
            Girls.Add("Girl10", new Kid("Lucy", "GirlAvatar_10", "Eyes_Open1"));
            Girls.Add("Girl11", new Kid("Mary", "GirlAvatar_11", "Eyes_Open3"));
            Girls.Add("Girl12", new Kid("Jamie", "GirlAvatar_12", "Eyes_Open4"));
            Girls.Add("Girl13", new Kid("Tasha", "GirlAvatar_13", "Eyes_Open5"));
            Girls.Add("Girl14", new Kid("Angie", "GirlAvatar_14", "Eyes_Open3"));
            Girls.Add("Girl15", new Kid("Shana", "GirlAvatar_15", "Eyes_Open1"));
            Girls.Add("Girl16", new Kid("Abigail", "GirlAvatar_16", "Eyes_Open7"));
            Girls.Add("Girl17", new Kid("Lara", "GirlAvatar_17", "Eyes_Open2"));
            Girls.Add("Girl18", new Kid("Navea", "GirlAvatar_18", "Eyes_Open7"));
            Girls.Add("Girl19", new Kid("Tina", "GirlAvatar_19", "Eyes_Open3"));
            Girls.Add("Girl20", new Kid("Donna", "GirlAvatar_20", "Eyes_Open1"));
            Girls.Add("Girl21", new Kid("Rebecca", "GirlAvatar_21", "Eyes_Open3"));
            Girls.Add("Girl22", new Kid("Gloria", "GirlAvatar_22", "Eyes_Open4"));
            Girls.Add("Girl23", new Kid("Hannah", "GirlAvatar_23", "Eyes_Open3"));
            Girls.Add("Girl24", new Kid("Miranda", "GirlAvatar_24", "Eyes_Open1"));
            Girls.Add("Girl25", new Kid("Penelope", "GirlAvatar_25", "Eyes_Open2"));
            Girls.Add("Girl26", new Kid("Kaley", "GirlAvatar_26", "Eyes_Open4"));
            Girls.Add("Girl27", new Kid("Kendall", "GirlAvatar_27", "Eyes_Open5"));
            Girls.Add("Girl28", new Kid("Olive", "GirlAvatar_28", "Eyes_Open1"));
            Girls.Add("Girl29", new Kid("Sarah", "GirlAvatar_29", "Eyes_Open6"));
            Girls.Add("Girl30", new Kid("Queen Ella", "GirlAvatar_30", "Eyes_Open2"));
            Girls.Add("Girl31", new Kid("Beelz", "GirlAvatar_31", "Eyes_Open3"));
            Girls.Add("Girl32", new Kid("Isabelle", "GirlAvatar_32", "Eyes_Open2"));
            Girls.Add("Girl33", new Kid("Ellee", "GirlAvatar_33", "Eyes_Open3"));
            Girls.Add("Girl34", new Kid("Ana", "GirlAvatar_34", "Eyes_Open7"));
            Girls.Add("Girl35", new Kid("Lyla", "GirlAvatar_35", "Eyes_Open3"));
            Girls.Add("Girl36", new Kid("Samantha", "GirlAvatar_36", "Eyes_Open3"));
            Girls.Add("Girl37", new Kid("Melva", "GirlAvatar_37", "Eyes_Open5"));
            Girls.Add("Girl38", new Kid("Savannah", "GirlAvatar_38", "Eyes_Open6"));
            Girls.Add("Girl39", new Kid("Jana", "GirlAvatar_39", "Eyes_Open5"));
            Girls.Add("Girl40", new Kid("Kayley", "GirlAvatar_40", "Eyes_Open7"));

            SelectedKidType = KidType.Girl;
            SelectKid(KidType.Girl, 1);
            //SelectKid(KidType.Boy, Utilities.GetRandomInt(1, 20));
            //SelectKid(KidType.Girl, Utilities.GetRandomInt(1, 40));
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
            SelectedKids[SelectedKid].Draw(spriteBatch, x, y, facingAway);
        }

        public static void DrawKid(KidType type, int index, SpriteBatch spriteBatch, int x, int y, bool facingAway = false)
        {
            var kidType = "Boy";
            var kids = Boys;
            
            if (type == KidType.Girl)
            {
                kidType = "Girl";
                kids = Girls;
            }

            kids[$"{kidType}{index}"].Draw(spriteBatch, x, y, facingAway);
        }
    }
}
