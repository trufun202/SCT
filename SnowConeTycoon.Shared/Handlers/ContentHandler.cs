using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace SnowConeTycoon.Shared.Handlers
{
    public static class ContentHandler
    {
        public static Dictionary<string, Texture2D> Images;
        public static Dictionary<string, SoundEffect> Sounds;
        public static Dictionary<string, Song> Songs;

        public static void PreInit(ContentManager content)
        {
            Images = new Dictionary<string, Texture2D>();

            ///////////////////////////////////////////
            //LOADING SCREEN
            ///////////////////////////////////////////
            Images.Add("Loading_Frame", content.Load<Texture2D>("Loading_Frame"));
            Images.Add("Loading_Bar01", content.Load<Texture2D>("Loading_Bar01"));
            Images.Add("Loading_Bar02", content.Load<Texture2D>("Loading_Bar02"));
            Images.Add("Loading_Bar03", content.Load<Texture2D>("Loading_Bar03"));
            Images.Add("Loading_Bar04", content.Load<Texture2D>("Loading_Bar04"));
            Images.Add("Loading_Bar05", content.Load<Texture2D>("Loading_Bar05"));
            Images.Add("Loading_Bar06", content.Load<Texture2D>("Loading_Bar06"));
            Images.Add("Loading_Bar07", content.Load<Texture2D>("Loading_Bar07"));
            Images.Add("Loading_Bar08", content.Load<Texture2D>("Loading_Bar08"));
            Images.Add("SupplyShop_Background", content.Load<Texture2D>("SupplyShop_Background"));
        }

        public static void Init(ContentManager content)
        {
            Sounds = new Dictionary<string, SoundEffect>();
            Songs = new Dictionary<string, Song>();

            //////////////////////////
            // BACKGROUNDS
            //////////////////////////
            Images.Add("TitleScreen_Background", content.Load<Texture2D>("TitleScreen_Background"));
            Images.Add("TitleScreen_Foreground", content.Load<Texture2D>("TitleScreen_Foreground"));
            Images.Add("Background_Hills", content.Load<Texture2D>("Background_Hills"));
            Images.Add("Background_HillsDark", content.Load<Texture2D>("Background_HillsDark"));
            Images.Add("Background_Clouds", content.Load<Texture2D>("Background_Clouds"));
            Images.Add("Background_ClearClouds", content.Load<Texture2D>("Background_ClearClouds"));
            Images.Add("Background_Sun", content.Load<Texture2D>("Background_Sun"));
            Images.Add("CharacterSelectScreen_Foreground", content.Load<Texture2D>("CharacterSelectScreen_Foreground"));

            //////////////////////////
            // AVATARS
            //////////////////////////
            for (int i = 1; i <= 40; i++)
            {
                var num = i.ToString("00");
                Images.Add($"BoyAvatar_{num}", content.Load<Texture2D>($"BoyAvatar_{num}"));
                Images.Add($"GirlAvatar_{num}", content.Load<Texture2D>($"GirlAvatar_{num}"));
            }

            //////////////////////////
            // EYES
            //////////////////////////
            Images.Add("Eyes_Closed1", content.Load<Texture2D>("Eyes_Closed1"));
            Images.Add("Eyes_Closed2", content.Load<Texture2D>("Eyes_Closed2"));
            Images.Add("Eyes_Open1", content.Load<Texture2D>("Eyes_Open1"));
            Images.Add("Eyes_Open2", content.Load<Texture2D>("Eyes_Open2"));
            Images.Add("Eyes_Open3", content.Load<Texture2D>("Eyes_Open3"));
            Images.Add("Eyes_Open4", content.Load<Texture2D>("Eyes_Open4"));
            Images.Add("Eyes_Open5", content.Load<Texture2D>("Eyes_Open5"));
            Images.Add("Eyes_Open6", content.Load<Texture2D>("Eyes_Open6"));
            Images.Add("Eyes_Open7", content.Load<Texture2D>("Eyes_Open7"));

            //////////////////////////
            // MOUTHS
            //////////////////////////
            Images.Add("mouth_happy1", content.Load<Texture2D>("mouth_happy1"));
            Images.Add("mouth_happy2", content.Load<Texture2D>("mouth_happy2"));
            Images.Add("mouth_happy3", content.Load<Texture2D>("mouth_happy3"));
            Images.Add("mouth_happy4", content.Load<Texture2D>("mouth_happy4"));
            Images.Add("mouth_happy5", content.Load<Texture2D>("mouth_happy5"));
            Images.Add("mouth_happy6", content.Load<Texture2D>("mouth_happy6"));
            Images.Add("mouth_happy7", content.Load<Texture2D>("mouth_happy7"));
            Images.Add("mouth_happy8", content.Load<Texture2D>("mouth_happy8"));
            Images.Add("mouth_happy9", content.Load<Texture2D>("mouth_happy9"));
            Images.Add("mouth_mad1", content.Load<Texture2D>("mouth_mad1"));
            Images.Add("mouth_mad2", content.Load<Texture2D>("mouth_mad2"));
            Images.Add("mouth_mad3", content.Load<Texture2D>("mouth_mad3"));
            Images.Add("mouth_mad4", content.Load<Texture2D>("mouth_mad4"));
            Images.Add("mouth_mad5", content.Load<Texture2D>("mouth_mad5"));
            Images.Add("mouth_mad6", content.Load<Texture2D>("mouth_mad6"));
            Images.Add("mouth_sad1", content.Load<Texture2D>("mouth_sad1"));
            Images.Add("mouth_sad2", content.Load<Texture2D>("mouth_sad2"));
            Images.Add("mouth_sad3", content.Load<Texture2D>("mouth_sad3"));
            Images.Add("mouth_sad4", content.Load<Texture2D>("mouth_sad4"));
            Images.Add("mouth_sad5", content.Load<Texture2D>("mouth_sad5"));
            Images.Add("mouth_sad6", content.Load<Texture2D>("mouth_sad6"));
            Images.Add("mouth_sad7", content.Load<Texture2D>("mouth_sad7"));
            Images.Add("mouth_sad8", content.Load<Texture2D>("mouth_sad8"));

            //////////////////////////
            // MISC
            //////////////////////////
            Images.Add("debugbox", content.Load<Texture2D>("debugbox"));
            Images.Add("WhiteDot", content.Load<Texture2D>("WhiteDot"));
            Images.Add("ArrowRight", content.Load<Texture2D>("ArrowRight"));
            Images.Add("ArrowRight2", content.Load<Texture2D>("ArrowRight2"));
            Images.Add("ArrowLeft", content.Load<Texture2D>("ArrowLeft"));
            Images.Add("Symbol_Male", content.Load<Texture2D>("Symbol_Male"));
            Images.Add("Symbol_Female", content.Load<Texture2D>("Symbol_Female"));
            Images.Add("IconGear", content.Load<Texture2D>("IconGear"));
            Images.Add("IconHome", content.Load<Texture2D>("IconHome"));
            Images.Add("IconSnowCone", content.Load<Texture2D>("IconSnowCone"));
            Images.Add("nps_detractor", content.Load<Texture2D>("nps_detractor"));
            Images.Add("nps_passive", content.Load<Texture2D>("nps_passive"));
            Images.Add("nps_promoter", content.Load<Texture2D>("nps_promoter"));
            Images.Add("nps_background", content.Load<Texture2D>("nps_background"));
            Images.Add("ThoughtBubble", content.Load<Texture2D>("ThoughtBubble"));
            Images.Add("ThoughtCloud", content.Load<Texture2D>("ThoughtCloud"));
            Images.Add("OpenForBusiness_Foreground", content.Load<Texture2D>("OpenForBusiness_Foreground"));
            Images.Add("ChrosGamesLogoNoCircle", content.Load<Texture2D>("ChrosGamesLogoNoCircle"));
            Images.Add("ChrosGamesLogoCircle", content.Load<Texture2D>("ChrosGamesLogoCircle"));
            Images.Add("particle", content.Load<Texture2D>("particle"));
            Images.Add("particle_ice", content.Load<Texture2D>("particle_ice"));
            Images.Add("lock", content.Load<Texture2D>("lock"));
            Images.Add("button_locked", content.Load<Texture2D>("button_locked"));
            Images.Add("button_unlock", content.Load<Texture2D>("button_unlock"));
            Images.Add("DailyBonus_Ice", content.Load<Texture2D>("DailyBonus_Ice"));
            Images.Add("DailyBonus_Circle", content.Load<Texture2D>("DailyBonus_Circle"));
            Images.Add("DailyBonus_Check", content.Load<Texture2D>("DailyBonus_Check"));
            Images.Add("DailyBonus_RedX", content.Load<Texture2D>("DailyBonus_RedX"));
            Images.Add("TitleScreen_Ice", content.Load<Texture2D>("TitleScreen_Ice"));
            Images.Add("AdReward_Modal", content.Load<Texture2D>("AdReward_Modal"));

            ///////////////////////////////////////////
            //DAY SETUP
            ///////////////////////////////////////////
            Images.Add("DaySetup_DayLabel", content.Load<Texture2D>("DaySetup_DayLabel"));
            Images.Add("DaySetup_ForecastLabel", content.Load<Texture2D>("DaySetup_ForecastLabel"));
            Images.Add("DaySetup_IconFlavor", content.Load<Texture2D>("DaySetup_IconFlavor"));
            Images.Add("DaySetup_IconFlyer", content.Load<Texture2D>("DaySetup_IconFlyer"));
            Images.Add("DaySetup_IconPrice", content.Load<Texture2D>("DaySetup_IconPrice"));
            Images.Add("DaySetup_IconCone", content.Load<Texture2D>("DaySetup_IconCone"));
            Images.Add("DaySetup_IconShop", content.Load<Texture2D>("DaySetup_IconShop"));
            Images.Add("DaySetup_InvCoins", content.Load<Texture2D>("DaySetup_InvCoins"));
            Images.Add("DaySetup_InvCones", content.Load<Texture2D>("DaySetup_InvCones"));
            Images.Add("DaySetup_Inventory", content.Load<Texture2D>("DaySetup_Inventory"));
            Images.Add("DaySetup_InvFlyers", content.Load<Texture2D>("DaySetup_InvFlyers"));
            Images.Add("DaySetup_InvIce", content.Load<Texture2D>("DaySetup_InvIce"));
            Images.Add("DaySetup_InvSyrup", content.Load<Texture2D>("DaySetup_InvSyrup"));
            Images.Add("DaySetup_LetsGo", content.Load<Texture2D>("DaySetup_LetsGo"));
            Images.Add("DaySetup_Back", content.Load<Texture2D>("DaySetup_Back"));
            Images.Add("DaySetup_NumControl", content.Load<Texture2D>("DaySetup_NumControl"));
            Images.Add("DaySetup_Paper", content.Load<Texture2D>("DaySetup_Paper"));
            Images.Add("DaySetup_Plus", content.Load<Texture2D>("DaySetup_Plus"));
            Images.Add("DaySetup_WatchAd", content.Load<Texture2D>("DaySetup_WatchAd"));
            Images.Add("Results_Rank", content.Load<Texture2D>("Results_Rank"));
            Images.Add("Results_Next", content.Load<Texture2D>("Results_Next"));

            ///////////////////////////////////////////
            //SUPPLY SHOP
            ///////////////////////////////////////////
            Images.Add("SupplyShop_Banner", content.Load<Texture2D>("SupplyShop_Banner"));
            Images.Add("SupplyShop_Checkout", content.Load<Texture2D>("SupplyShop_Checkout"));
            Images.Add("SupplyShop_Minus", content.Load<Texture2D>("SupplyShop_Minus"));
            Images.Add("SupplyShop_Paper", content.Load<Texture2D>("SupplyShop_Paper"));
            Images.Add("SupplyShop_Plus", content.Load<Texture2D>("SupplyShop_Plus"));

            ///////////////////////////////////////////
            //SOUNDS
            ///////////////////////////////////////////
            Sounds.Add("pop", content.Load<SoundEffect>("pop"));
            Sounds.Add("Swoosh", content.Load<SoundEffect>("Swoosh"));
            Sounds.Add("Game Coin", content.Load<SoundEffect>("Game Coin"));
            Sounds.Add("Cash Register Fast", content.Load<SoundEffect>("Cash Register Fast"));
            Sounds.Add("Magic Wand 1", content.Load<SoundEffect>("Magic Wand 1"));
            Sounds.Add("Ice_Cube", content.Load<SoundEffect>("Ice_Cube"));

            ///////////////////////////////////////////
            //SONGS
            ///////////////////////////////////////////
            Songs.Add("Song1", content.Load<Song>("Song1"));
            Songs.Add("Song2", content.Load<Song>("Song2"));
            Songs.Add("Song3", content.Load<Song>("Song3"));
        }
    }
}
