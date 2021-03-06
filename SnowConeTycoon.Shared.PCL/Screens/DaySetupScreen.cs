﻿using System;
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
        public Forecast CurrentForecast = Forecast.Sunny;
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
        ScaledImage TemperatureImage;
        ScaledImage LetsGoButton;
        ScaledImage BackButton;
        public bool DoneAnimating = false;
        int Temperature = 100;

        public DaySetupScreen(double scaleX, double scaleY)
        {
            ScaleX = scaleX;
            ScaleY = scaleY;
            DayImage = new ScaledImage("DaySetup_DayLabel", new Vector2(300, 200), 250);
            ForecastImage = new ScaledImage("DaySetup_ForecastLabel", new Vector2(750, 200), 250);
            TemperatureImage = new ScaledImage("DaySetup_DayLabel", new Vector2(1200, 200), 250);
            LetsGoButton = new ScaledImage("DaySetup_LetsGo", new Vector2(1200, 2500), 500);
            BackButton = new ScaledImage("DaySetup_Back", new Vector2(350, 2470), 500);
        }

        public void Reset(int currentDay, int temperature)
        {
            CurrentDay = currentDay;
            Temperature = temperature;
            DoneAnimating = false;
            form = new Form(0, 0);
            form.Controls.Add(new NumberPicker("DaySetup_IconFlavor", "syrup", new Vector2(250, 550), 0, 10, ScaleX, ScaleY, false));
            form.Controls.Add(new NumberPicker("DaySetup_IconFlyer", "flyers", new Vector2(250, 800), 0, 10, ScaleX, ScaleY, false));
            form.Controls.Add(new Label("---------------------------------", new Vector2(250, 1000), Defaults.Brown));
            form.Controls.Add(new NumberPicker("DaySetup_IconPrice", "price", new Vector2(250, 1200), 1, 10, ScaleX, ScaleY, false));

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
            TemperatureImage.Reset();
            LetsGoButton.Reset();
            BackButton.Reset();
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
                else if (!TemperatureImage.IsDoneAnimating())
                {
                    TemperatureImage.Update(gameTime);
                }
                else if (!LetsGoButton.IsDoneAnimating())
                {
                    LetsGoButton.Update(gameTime);
                    BackButton.Update(gameTime);
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
                spriteBatch.DrawString(Defaults.Font, "my supplies", new Vector2(PositionInv.X + 500, PositionInv.Y + 100), Defaults.Brown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

                var fontScale = 0.70f;

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvIce"], new Vector2(PositionInv.X + 300, PositionInv.Y + 250), Color.White);
                spriteBatch.DrawString(Defaults.Font, "ice", new Vector2(PositionInv.X + 500, PositionInv.Y + 250), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "6", new Vector2(PositionInv.X + 1100, PositionInv.Y + 250), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString("6").X, 0), fontScale, SpriteEffects.None, 1f);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_WatchAd"], new Vector2(PositionInv.X + 1135, PositionInv.Y + 250), Color.White);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCoins"], new Vector2(PositionInv.X + 290, PositionInv.Y + 400), Color.White);
                spriteBatch.DrawString(Defaults.Font, "coins", new Vector2(PositionInv.X + 500, PositionInv.Y + 400), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "345", new Vector2(PositionInv.X + 1100, PositionInv.Y + 400), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString("345").X, 0), fontScale, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCones"], new Vector2(PositionInv.X + 335, PositionInv.Y + 550), Color.White);
                spriteBatch.DrawString(Defaults.Font, "cones", new Vector2(PositionInv.X + 500, PositionInv.Y + 550), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "23", new Vector2(PositionInv.X + 1100, PositionInv.Y + 550), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString("23").X, 0), fontScale, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvSyrup"], new Vector2(PositionInv.X + 320, PositionInv.Y + 700), Color.White);
                spriteBatch.DrawString(Defaults.Font, "syrup", new Vector2(PositionInv.X + 500, PositionInv.Y + 700), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "35", new Vector2(PositionInv.X + 1100, PositionInv.Y + 700), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString("35").X, 0), fontScale, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvFlyers"], new Vector2(PositionInv.X + 320, PositionInv.Y + 850), Color.White);
                spriteBatch.DrawString(Defaults.Font, "flyers", new Vector2(PositionInv.X + 500, PositionInv.Y + 850), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "12", new Vector2(PositionInv.X + 1100, PositionInv.Y + 850), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString("12").X, 0), fontScale, SpriteEffects.None, 1f);
            }

            spriteBatch.Draw(ContentHandler.Images["DaySetup_Paper"], PositionPaper, Color.White);
            spriteBatch.DrawString(Defaults.Font, "snow cone setup", new Vector2(PositionPaper.X + 350, PositionPaper.Y + 325), Defaults.Brown);
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
                    spriteBatch.DrawString(Defaults.Font, $"day {CurrentDay}", DayImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString($"day {CurrentDay}") / 2, 0.6f, SpriteEffects.None, 1f);
                    ForecastImage.Draw(spriteBatch);
                }
                else if (!TemperatureImage.IsDoneAnimating())
                {
                    DayImage.Draw(spriteBatch);
                    spriteBatch.DrawString(Defaults.Font, $"{CurrentDay}", DayImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString($"day {CurrentDay}") / 2, 0.6f, SpriteEffects.None, 1f);
                    ForecastImage.Draw(spriteBatch);
                    var forecast = CurrentForecast.ToString().ToLower();

                    if (CurrentForecast == Forecast.PartlyCloudy)
                    {
                        forecast = "partly cloudy";
                    }

                    spriteBatch.DrawString(Defaults.Font, forecast, ForecastImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString(forecast) / 2, 0.6f, SpriteEffects.None, 1f);
                    TemperatureImage.Draw(spriteBatch);
                }
                else
                {
                    DayImage.Draw(spriteBatch);
                    spriteBatch.DrawString(Defaults.Font, $"day {CurrentDay}", DayImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString($"day {CurrentDay}") / 2, 0.6f, SpriteEffects.None, 1f);
                    ForecastImage.Draw(spriteBatch);
                    var forecast = CurrentForecast.ToString().ToLower();

                    if (CurrentForecast == Forecast.PartlyCloudy)
                    {
                        forecast = "partly cloudy";
                    }

                    spriteBatch.DrawString(Defaults.Font, forecast, ForecastImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString(forecast) / 2, 0.6f, SpriteEffects.None, 1f);
                    TemperatureImage.Draw(spriteBatch);
                    spriteBatch.DrawString(Defaults.Font, $"{Temperature}", TemperatureImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString($"{Temperature}") / 2, 0.6f, SpriteEffects.None, 1f);

                    var degrees = "          o";
                    spriteBatch.DrawString(Defaults.Font, degrees, TemperatureImage.Position, Defaults.Brown, -0.1f, new Vector2(Defaults.Font.MeasureString(degrees).X / 2, (Defaults.Font.MeasureString(degrees).Y / 2) + 50), 0.6f, SpriteEffects.None, 1f);
                    LetsGoButton.Draw(spriteBatch);
                    BackButton.Draw(spriteBatch);
                }
            }
        }
    }
}
