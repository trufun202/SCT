using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class DaySetupScreen
    {
        int CurrentDay = 1;
        Forecast CurrentForecast = Forecast.Sunny;
        Vector2 PositionPaperStart = Vector2.Zero;
        Vector2 PositionPaperEnd = Vector2.Zero;
        Vector2 PositionPaper = Vector2.Zero;
        Vector2 PositionInvStart = Vector2.Zero;
        Vector2 PositionInvEnd = Vector2.Zero;
        Vector2 PositionInv = Vector2.Zero;
        bool AnimatingPaper = true;
        bool ShowingInventory = false;
        int TimePaper = 0;
        int TimePaperTotal = 500;
        int TimeInv = 0;
        int TimeInvTotal = 1000;
        double ScaleX;
        double ScaleY;
        Form form;
        ScaledImage DayImage;
        ScaledImage ForecastImage;
        ScaledImage LetsGoButton;
        public bool DoneAnimating = false;

        public DaySetupScreen(double scaleX, double scaleY)
        {
            ScaleX = scaleX;
            ScaleY = scaleY;
            DayImage = new ScaledImage("DaySetup_DayLabel", new Vector2(350, 200));
            ForecastImage = new ScaledImage("DaySetup_ForecastLabel", new Vector2(800, 200));
            LetsGoButton = new ScaledImage("DaySetup_LetsGo", new Vector2(1200, 2500), 1000);
        }

        public void Reset()
        {
            DoneAnimating = false;
            form = new Form(0, 0);
            form.Controls.Add(new NumberPicker("DaySetup_IconPrice", "price", new Vector2(250, 500), 1, 8, ScaleX, ScaleY, false));
            form.Controls.Add(new NumberPicker("DaySetup_IconFlavor", "flavor", new Vector2(250, 800), 1, 8, ScaleX, ScaleY, false));
            form.Controls.Add(new NumberPicker("DaySetup_IconFlyer", "flyers", new Vector2(250, 1100), 1, 8, ScaleX, ScaleY, false));
            AnimatingPaper = true;
            ShowingInventory = false;
            TimePaper = 0;
            TimeInv = 0;
            PositionPaperStart = new Vector2(0, -ContentHandler.Images["DaySetup_Paper"].Height);
            PositionPaper = PositionPaperStart;
            PositionInvStart = new Vector2(0, Defaults.GraphicsHeight - (ContentHandler.Images["DaySetup_Inventory"].Height * 2) - 100);
            PositionInv = PositionInvStart;
            PositionInvEnd = new Vector2(0, Defaults.GraphicsHeight - ContentHandler.Images["DaySetup_Inventory"].Height - 200);
            DayImage.Reset();
            ForecastImage.Reset();
            LetsGoButton.Reset();
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            form.HandleInput(previousTouchCollection, currentTouchCollection);
        }

        public void Update(GameTime gameTime)
        {
            form.Update(gameTime);

            if (AnimatingPaper)
            {
                TimePaper += gameTime.ElapsedGameTime.Milliseconds;
                var amt = TimePaper / (float)TimePaperTotal;

                PositionPaper = Vector2.SmoothStep(PositionPaperStart, PositionPaperEnd, amt);

                if (TimePaper >= TimePaperTotal)
                {
                    AnimatingPaper = false;
                    form.Reveil();
                }
            }
            else if (!ShowingInventory && form.IsVisible())
            {
                TimeInv += gameTime.ElapsedGameTime.Milliseconds;
                var amt = TimeInv / (float)TimeInvTotal;

                PositionInv = Vector2.SmoothStep(PositionInvStart, PositionInvEnd, amt);

                if (TimeInv >= TimeInvTotal)
                {
                    ShowingInventory = true;
                }
            }
            else if (ShowingInventory)
            {
                if (!DayImage.IsDoneAnimating())
                {
                    DayImage.Update(gameTime);
                }
                else if (!ForecastImage.IsDoneAnimating())
                {
                    ForecastImage.Update(gameTime);
                }
                else if (!LetsGoButton.IsDoneAnimating())
                {
                    LetsGoButton.Update(gameTime);
                }
                else if (LetsGoButton.IsDoneAnimating())
                {
                    DoneAnimating = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!AnimatingPaper)
            {
                spriteBatch.Draw(ContentHandler.Images["DaySetup_Inventory"], PositionInv, Color.White);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCoins"], new Vector2(PositionInv.X + 280, PositionInv.Y + 200), Color.White);
                spriteBatch.DrawString(Defaults.Font, "coins", new Vector2(PositionInv.X + 500, PositionInv.Y + 200), Color.White);
                spriteBatch.DrawString(Defaults.Font, "345", new Vector2(PositionInv.X + 1100, PositionInv.Y + 200), Color.White, 0f, new Vector2(Defaults.Font.MeasureString("345").X, 0), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_WatchAd"], new Vector2(PositionInv.X + 1135, PositionInv.Y + 215), Color.White);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCones"], new Vector2(PositionInv.X + 325, PositionInv.Y + 415), Color.White);
                spriteBatch.DrawString(Defaults.Font, "cones", new Vector2(PositionInv.X + 500, PositionInv.Y + 400), Color.White);
                spriteBatch.DrawString(Defaults.Font, "23", new Vector2(PositionInv.X + 1100, PositionInv.Y + 400), Color.White, 0f, new Vector2(Defaults.Font.MeasureString("23").X, 0), 1f, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvIce"], new Vector2(PositionInv.X + 300, PositionInv.Y + 600), Color.White);
                spriteBatch.DrawString(Defaults.Font, "ice", new Vector2(PositionInv.X + 500, PositionInv.Y + 600), Color.White);
                spriteBatch.DrawString(Defaults.Font, "35", new Vector2(PositionInv.X + 1100, PositionInv.Y + 600), Color.White, 0f, new Vector2(Defaults.Font.MeasureString("35").X, 0), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_Plus"], new Vector2(PositionInv.X + 1150, PositionInv.Y + 600), Color.White);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvSyrup"], new Vector2(PositionInv.X + 300, PositionInv.Y + 800), Color.White);
                spriteBatch.DrawString(Defaults.Font, "syrup", new Vector2(PositionInv.X + 500, PositionInv.Y + 800), Color.White);
                spriteBatch.DrawString(Defaults.Font, "12", new Vector2(PositionInv.X + 1100, PositionInv.Y + 800), Color.White, 0f, new Vector2(Defaults.Font.MeasureString("12").X, 0), 1f, SpriteEffects.None, 1f);
            }

            spriteBatch.Draw(ContentHandler.Images["DaySetup_Paper"], PositionPaper, Color.White);
            form.Draw(spriteBatch);

            if (ShowingInventory)
            {
                if (!DayImage.IsDoneAnimating())
                {
                    DayImage.Draw(spriteBatch);
                }
                else if (!ForecastImage.IsDoneAnimating())
                {
                    DayImage.Draw(spriteBatch);
                    spriteBatch.DrawString(Defaults.Font, $"day {CurrentDay}", DayImage.Position, Color.Black, -0.1f, Defaults.Font.MeasureString($"day {CurrentDay}") / 2, 0.6f, SpriteEffects.None, 1f);
                    ForecastImage.Draw(spriteBatch);
                }
                else
                {
                    DayImage.Draw(spriteBatch);
                    spriteBatch.DrawString(Defaults.Font, $"day {CurrentDay}", DayImage.Position, Color.Black, -0.1f, Defaults.Font.MeasureString($"day {CurrentDay}") / 2, 0.6f, SpriteEffects.None, 1f);
                    ForecastImage.Draw(spriteBatch);
                    var forecast = CurrentForecast.ToString().ToLower();

                    if (CurrentForecast == Forecast.PartlyCloudy)
                    {
                        forecast = "partly cloudy";
                    }

                    spriteBatch.DrawString(Defaults.Font, forecast, ForecastImage.Position, Color.Black, -0.1f, Defaults.Font.MeasureString(forecast) / 2, 0.6f, SpriteEffects.None, 1f);
                    LetsGoButton.Draw(spriteBatch);
                }
            }
        }
    }
}
