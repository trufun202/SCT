using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class DailyBonusScreen
    {
        Vector2 PaperPosition;
        Vector2 PaperPositionStart;
        Vector2 PaperPositionEnd;
        int PaperTime = 0;
        int PaperTimeTotal = 500;
        bool PaperDoneAnimating = false;
        int ShowingDayStats = 0;
        int DayStatTime = 0;
        int DayStatTimeTotal = 200;
        ScaledImage EarnedCheckImage;
        bool PlayedDing = false;

        public DailyBonusScreen()
        {
            Reset();
        }

        public void Reset()
        {
            PaperPositionStart = new Vector2(0, ContentHandler.Images["DaySetup_Paper"].Height * -1);
            PaperPositionEnd = new Vector2(0, (Defaults.GraphicsHeight / 2) - (ContentHandler.Images["DaySetup_Paper"].Height / 2));
            PaperTime = 0;
            PaperDoneAnimating = false;
            ShowingDayStats = 0;
            DayStatTime = 0;
            EarnedCheckImage = new ScaledImage("DailyBonus_Check", new Vector2((Defaults.GraphicsWidth / 2) - 370, PaperPositionEnd.Y + 480 + (Player.ConsecutiveDaysPlayed * 150)));
            PlayedDing = false;
        }

        public bool ScreenHasLoaded()
        {
            if (EarnedCheckImage != null && EarnedCheckImage.IsDoneAnimating())
            {
                return true;
            }

            return false;
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {

        }

        public void Update(GameTime gameTime)
        {
            if (!PaperDoneAnimating)
            {
                PaperTime += gameTime.ElapsedGameTime.Milliseconds;
                var amt = PaperTime / (float)PaperTimeTotal;
                PaperPosition = Vector2.SmoothStep(PaperPositionStart, PaperPositionEnd, amt);

                if (PaperTime >= PaperTimeTotal)
                {
                    PaperDoneAnimating = true;
                }
            }
            else
            {
                if (ShowingDayStats < 6)
                {
                    DayStatTime += gameTime.ElapsedGameTime.Milliseconds;

                    if (DayStatTime >= DayStatTimeTotal)
                    {
                        ShowingDayStats++;
                        DayStatTime = 0;
                        ContentHandler.Sounds["Swoosh"].Play();
                    }
                }
            }

            if (ShowingDayStats > 5)
            {
                EarnedCheckImage.Update(gameTime);
            }

            if (EarnedCheckImage.IsDoneAnimating())
            {
                if (!PlayedDing)
                {
                    PlayedDing = true;
                    ContentHandler.Sounds["Ding"].Play();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.FromNonPremultiplied(new Vector4(0,0,0, 0.75f)));
            spriteBatch.Draw(ContentHandler.Images["DaySetup_Paper"], PaperPosition, Color.White);
            spriteBatch.DrawString(Defaults.Font, "daily bonus", new Vector2(Defaults.GraphicsWidth / 2, PaperPosition.Y + 300), Defaults.Brown, 0f, Defaults.Font.MeasureString("daily bonus") / 2, 1f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(Defaults.Font, "consecutive\n      days", new Vector2((Defaults.GraphicsWidth / 2) - 300, PaperPosition.Y + 450), Defaults.Brown, 0f, Defaults.Font.MeasureString("consecutive\n      days") / 2, 0.5f, SpriteEffects.None, 1f);
            spriteBatch.Draw(ContentHandler.Images["DailyBonus_Ice"], new Vector2((Defaults.GraphicsWidth / 2) + 250, PaperPosition.Y + 375), Color.White);
            spriteBatch.DrawString(Defaults.Font, "--------------------------------", new Vector2(Defaults.GraphicsWidth / 2, PaperPosition.Y + 550), Defaults.Brown, 0f, Defaults.Font.MeasureString("--------------------------------") / 2, 1f, SpriteEffects.None, 1f);

            if (ShowingDayStats > 0)
            {
                spriteBatch.Draw(ContentHandler.Images["DailyBonus_Circle"], new Vector2((Defaults.GraphicsWidth / 2) - 450, PaperPosition.Y + 600), Color.White);

                if (Player.ConsecutiveDaysPlayed > 1)
                {
                    spriteBatch.Draw(ContentHandler.Images["DailyBonus_Check"], new Vector2((Defaults.GraphicsWidth / 2) - 440, PaperPosition.Y + 570), Color.White);
                }

                spriteBatch.DrawString(Defaults.Font, "1                   1", new Vector2((Defaults.GraphicsWidth / 2) - 250, PaperPosition.Y + 600), Defaults.Brown);
            }

            if (ShowingDayStats > 1)
            {
                spriteBatch.Draw(ContentHandler.Images["DailyBonus_Circle"], new Vector2((Defaults.GraphicsWidth / 2) - 450, PaperPosition.Y + 750), Color.White);

                if (Player.ConsecutiveDaysPlayed > 2)
                {
                    spriteBatch.Draw(ContentHandler.Images["DailyBonus_Check"], new Vector2((Defaults.GraphicsWidth / 2) - 440, PaperPosition.Y + 720), Color.White);
                }

                spriteBatch.DrawString(Defaults.Font, "2                   4", new Vector2((Defaults.GraphicsWidth / 2) - 250, PaperPosition.Y + 750), Defaults.Brown);
            }

            if (ShowingDayStats > 2)
            {
                spriteBatch.Draw(ContentHandler.Images["DailyBonus_Circle"], new Vector2((Defaults.GraphicsWidth / 2) - 450, PaperPosition.Y + 900), Color.White);

                if (Player.ConsecutiveDaysPlayed > 3)
                {
                    spriteBatch.Draw(ContentHandler.Images["DailyBonus_Check"], new Vector2((Defaults.GraphicsWidth / 2) - 440, PaperPosition.Y + 870), Color.White);
                }

                spriteBatch.DrawString(Defaults.Font, "3                   6", new Vector2((Defaults.GraphicsWidth / 2) - 250, PaperPosition.Y + 900), Defaults.Brown);
            }

            if (ShowingDayStats > 3)
            {
                spriteBatch.Draw(ContentHandler.Images["DailyBonus_Circle"], new Vector2((Defaults.GraphicsWidth / 2) - 450, PaperPosition.Y + 1050), Color.White);

                if (Player.ConsecutiveDaysPlayed > 4)
                {
                    spriteBatch.Draw(ContentHandler.Images["DailyBonus_Check"], new Vector2((Defaults.GraphicsWidth / 2) - 440, PaperPosition.Y + 1020), Color.White);
                }

                spriteBatch.DrawString(Defaults.Font, "4                   8", new Vector2((Defaults.GraphicsWidth / 2) - 250, PaperPosition.Y + 1050), Defaults.Brown);
            }

            if (ShowingDayStats > 4)
            {
                spriteBatch.Draw(ContentHandler.Images["DailyBonus_Circle"], new Vector2((Defaults.GraphicsWidth / 2) - 450, PaperPosition.Y + 1200), Color.White);

                if (Player.ConsecutiveDaysPlayed > 5)
                {
                    spriteBatch.Draw(ContentHandler.Images["DailyBonus_Check"], new Vector2((Defaults.GraphicsWidth / 2) - 440, PaperPosition.Y + 1170), Color.White);
                }

                spriteBatch.DrawString(Defaults.Font, "5                  10", new Vector2((Defaults.GraphicsWidth / 2) - 250, PaperPosition.Y + 1200), Defaults.Brown);
            }

            if (ShowingDayStats > 5)
            {
                EarnedCheckImage.Draw(spriteBatch);
                spriteBatch.DrawString(Defaults.Font, "see you tomorrow to keep your streak!", new Vector2((Defaults.GraphicsWidth / 2), PaperPosition.Y + 1400), Defaults.Brown, 0f, Defaults.Font.MeasureString("see you tomorrow to keep your streak!") / 2, 0.5f, SpriteEffects.None, 1f);
            }

            if (EarnedCheckImage.IsDoneAnimating())
            {
                spriteBatch.Draw(ContentHandler.Images["DailyBonus_RedX"], new Vector2((Defaults.GraphicsWidth / 2) + 500, PaperPosition.Y + 100), Color.White);
            }
        }
    }
}
