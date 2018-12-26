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

        public static void Init(ContentManager content)
        {
            Images = new Dictionary<string, Texture2D>();
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
            Images.Add("ArrowLeft", content.Load<Texture2D>("ArrowLeft"));
            Images.Add("Symbol_Male", content.Load<Texture2D>("Symbol_Male"));
            Images.Add("Symbol_Female", content.Load<Texture2D>("Symbol_Female"));

            ///////////////////////////////////////////
            //SOUNDS
            ///////////////////////////////////////////
            Sounds.Add("pop", content.Load<SoundEffect>("pop"));
            Sounds.Add("Swoosh", content.Load<SoundEffect>("Swoosh"));

            ///////////////////////////////////////////
            //SONGS
            ///////////////////////////////////////////
            Songs.Add("Song1", content.Load<Song>("Song1"));
            Songs.Add("Song2", content.Load<Song>("Song2"));
            Songs.Add("Song3", content.Load<Song>("Song3"));
        }
    }
}
