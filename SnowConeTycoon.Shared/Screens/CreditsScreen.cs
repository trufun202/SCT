using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Backgrounds;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class CreditsScreen
    {
        IBackground background;
        List<string> Credits;
        int Y = 0;
        int YStep = 0;
        int YStepFast = -40;
        int YStepSlow = -10;
        int YMin = -5100;
        TimedEvent scrollEvent;

        public CreditsScreen()
        {
            Reset();
        }

        public void Reset()
        {
            background = new BackgroundCloudy();
            Credits = BuildCredits();
            Y = Defaults.GraphicsHeight - 150;
            YStep = YStepSlow;
            scrollEvent = new TimedEvent(50, () =>
            {
                Y += YStep;
            }, -1);
        }

        public bool IsDone()
        {
            if (Y <= YMin)
                return true;
            else
                return false;
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            if (previousTouchCollection.Count == 0 && currentTouchCollection.Count > 0)
            {
                YStep = YStepFast;
            }

            if (previousTouchCollection.Count > 0 && currentTouchCollection.Count == 0)
            {
                YStep = YStepSlow;
            }
        }

        public void Update(GameTime gameTime)
        {
            background.Update(gameTime);
            scrollEvent.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            int y = 0;

            foreach (var credit in Credits)
            {
                spriteBatch.DrawString(Defaults.Font, credit, new Vector2(Defaults.GraphicsWidth / 2, Y + y), Defaults.Brown, 0f, Defaults.Font.MeasureString(credit) / 2, 0.75f, SpriteEffects.None, 1f);
                y += 125;
            }

            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle(0, 0, Defaults.GraphicsWidth, 200), Color.Black);
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle(0, Defaults.GraphicsHeight - 200, Defaults.GraphicsWidth, 200), Color.Black);
        }

        private List<string> BuildCredits()
        {
            var credits = new List<string>();

            credits.Add("snow cone tycoon " + Defaults.VersionNumber);
            credits.Add("(c) 2019 chros games");
            credits.Add("chrosgames.com");
            credits.Add("");
            credits.Add("---------------------------------------------------");
            credits.Add("design and programming");
            credits.Add("---------------------------------------------------");
            credits.Add("chris bryant");
            credits.Add("");
            credits.Add("---------------------------------------------------");
            credits.Add("artwork");
            credits.Add("---------------------------------------------------");
            credits.Add("gamedevmarket.net");
            credits.Add("wowu");
            credits.Add("");
            credits.Add("---------------------------------------------------");
            credits.Add("audio");
            credits.Add("---------------------------------------------------");
            credits.Add("audiojungle.net");
            credits.Add("alexander blu");
            credits.Add("alexm76");
            credits.Add("artic net");
            credits.Add("audio roll");
            credits.Add("bant");
            credits.Add("endless stitch");
            credits.Add("game chest audio");
            credits.Add("kv sound");
            credits.Add("pasha striker");
            credits.Add("sound ideas");
            credits.Add("sun smile studio");
            credits.Add("tiba sound");
            credits.Add("");
            credits.Add("---------------------------------------------------");
            credits.Add("dedicated to, my children");
            credits.Add("---------------------------------------------------");
            credits.Add("lillian");
            credits.Add("lucas");
            credits.Add("");
            credits.Add("---------------------------------------------------");
            credits.Add("special thanks to");
            credits.Add("---------------------------------------------------");
            credits.Add("my wife, jennifer");
            credits.Add("and YOU, for playing!");

            return credits;
        }
    }
}
