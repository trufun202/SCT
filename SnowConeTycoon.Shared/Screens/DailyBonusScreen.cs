using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class DailyBonusScreen
    {
        Vector2 PaperPosition;
        Vector2 PaperPositionStart;
        Vector2 PaperPositionEnd;
        int PaperTime = 0;
        int PaperTimeTotal = 2000;
        bool PaperDoneAnimating = false;
        int ShowingDayStats = 0;
        int DayStatTime = 0;
        int DayStatTimeTotal = 200;

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
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {

        }

        public void Update(GameTime gameTime)
        {
            if (!PaperDoneAnimating)
            {
                PaperTime += gameTime.ElapsedGameTime.Milliseconds;
                var amt = PaperTime / PaperTimeTotal;
                PaperPosition = Vector2.SmoothStep(PaperPositionStart, PaperPositionEnd, amt);

                if (PaperTime >= PaperTimeTotal)
                {
                    PaperDoneAnimating = true;
                }
            }
            else
            {
                if (ShowingDayStats < 5)
                {
                    DayStatTime += gameTime.ElapsedGameTime.Milliseconds;

                    if (DayStatTime >= DayStatTimeTotal)
                    {
                        ShowingDayStats++;
                        DayStatTime = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.FromNonPremultiplied(new Vector4(0,0,0, 0.5f)));
            spriteBatch.Draw(ContentHandler.Images["DaySetup_Paper"], PaperPosition, Color.White);
            spriteBatch.DrawString(Defaults.Font, "Daily Bonus", new Vector2(Defaults.GraphicsWidth / 2, PaperPosition.Y + 400), Defaults.Brown, 0f, Defaults.Font.MeasureString("Daily Bonus") / 2, 1f, SpriteEffects.None, 1f);
        }
    }
}
