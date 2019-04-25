using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Screens.Modals;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class DaySetupScreen
    {
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
        NumberPicker syrupPicker;
        NumberPicker flyerPicker;
        NumberPicker pricePicker;
        Label lineLabel;
        AdRewardModal adRewardModal;
        TutorialModal tutorialModal;

        public int SyrupCount
        {
            get
            {
                return syrupPicker.Value;
            }
        }

        public int FlyerCount
        {
            get
            {
                return flyerPicker.Value;
            }
        }

        public int Price
        {
            get
            {
                return pricePicker.Value;
            }
        }

        public DaySetupScreen(double scaleX, double scaleY)
        {
            adRewardModal = new AdRewardModal(scaleX, scaleY);
            ScaleX = scaleX;
            ScaleY = scaleY;
            DayImage = new ScaledImage("DaySetup_DayLabel", new Vector2(300, 200), 250);
            ForecastImage = new ScaledImage("DaySetup_ForecastLabel", new Vector2(750, 200), 250);
            TemperatureImage = new ScaledImage("DaySetup_DayLabel", new Vector2(1200, 200), 250);
            LetsGoButton = new ScaledImage("DaySetup_LetsGo", new Vector2(1200, 2500), 500);
            BackButton = new ScaledImage("DaySetup_Back", new Vector2(350, 2470), 500);
            syrupPicker = new NumberPicker("DaySetup_IconFlavor", "squirts", new Vector2(250, 550), 0, 6, ScaleX, ScaleY, false);
            lineLabel = new Label("---------------------------------", new Vector2(250, 750), Defaults.Brown);
            flyerPicker = new NumberPicker("DaySetup_IconFlyer", "flyers", new Vector2(250, 950), 0, 10, ScaleX, ScaleY, false);
            pricePicker = new NumberPicker("DaySetup_IconPrice", "price", new Vector2(250, 1200), 1, 6, ScaleX, ScaleY, false);
            tutorialModal = new TutorialModal(scaleX, scaleY);
        }

        public void Reset(int temperature)
        {
            Temperature = temperature;
            DoneAnimating = false;
            form = new Form(0, 0);
            form.Controls.Add(syrupPicker);
            form.Controls.Add(lineLabel);
            form.Controls.Add(flyerPicker);
            form.Controls.Add(pricePicker);

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
            syrupPicker.Visible = false;
            flyerPicker.Visible = false;
            pricePicker.Visible = false;
            lineLabel.Visible = false;
            syrupPicker.Value = 0;
            flyerPicker.Value = 0;
            pricePicker.Value = 1;
            ResetPickerMax();

            if (Player.IsFirstTimePlaying)
            {
                tutorialModal.Show(TutorialType.DaySetup, 1);
            }
        }

        public void ResetPickerMax()
        {
            syrupPicker.Max = Player.SyrupCount < 6 ? Player.SyrupCount : 6;
            flyerPicker.Max = Player.FlyerCount < 30 ? Player.FlyerCount : 30;
        }

        public void ShowIceReward()
        {
            adRewardModal.Reset();
            adRewardModal.Active = true;
        }

        public bool IsReady()
        {
            return TemperatureImage.IsDoneAnimating() && !tutorialModal.Active;
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection, GameTime gameTime)
        {
            form?.HandleInput(previousTouchCollection, currentTouchCollection, gameTime);

            if (adRewardModal.Active)
            {
                adRewardModal.HandleInput(previousTouchCollection, currentTouchCollection, gameTime);
            }

            if (tutorialModal.Active)
            {
                tutorialModal.HandleInput(previousTouchCollection, currentTouchCollection);
            }
        }

        public void Update(GameTime gameTime)
        {
            form?.Update(gameTime);

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

            if (adRewardModal.Active)
            {
                adRewardModal.Update(gameTime);
            }

            if (tutorialModal.Active)
            {
                tutorialModal.Update(gameTime);
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
                spriteBatch.DrawString(Defaults.Font, Player.IceCount.ToString(), new Vector2(PositionInv.X + 1100, PositionInv.Y + 250), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.IceCount.ToString()).X, 0), fontScale, SpriteEffects.None, 1f);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_WatchAd"], new Vector2(PositionInv.X + 1135, PositionInv.Y + 250), Color.White);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCoins"], new Vector2(PositionInv.X + 290, PositionInv.Y + 400), Color.White);
                spriteBatch.DrawString(Defaults.Font, "coins", new Vector2(PositionInv.X + 500, PositionInv.Y + 400), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(PositionInv.X + 1100, PositionInv.Y + 400), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.CoinCount.ToString()).X, 0), fontScale, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCones"], new Vector2(PositionInv.X + 335, PositionInv.Y + 550), Color.White);
                spriteBatch.DrawString(Defaults.Font, "cones", new Vector2(PositionInv.X + 500, PositionInv.Y + 550), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.ConeCount.ToString(), new Vector2(PositionInv.X + 1100, PositionInv.Y + 550), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.ConeCount.ToString()).X, 0), fontScale, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvSyrup"], new Vector2(PositionInv.X + 320, PositionInv.Y + 700), Color.White);
                spriteBatch.DrawString(Defaults.Font, "syrup", new Vector2(PositionInv.X + 500, PositionInv.Y + 700), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.SyrupCount.ToString(), new Vector2(PositionInv.X + 1100, PositionInv.Y + 700), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.SyrupCount.ToString()).X, 0), fontScale, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvFlyers"], new Vector2(PositionInv.X + 320, PositionInv.Y + 850), Color.White);
                spriteBatch.DrawString(Defaults.Font, "flyers", new Vector2(PositionInv.X + 500, PositionInv.Y + 850), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.FlyerCount.ToString(), new Vector2(PositionInv.X + 1100, PositionInv.Y + 850), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.FlyerCount.ToString()).X, 0), fontScale, SpriteEffects.None, 1f);
            }

            spriteBatch.Draw(ContentHandler.Images["DaySetup_Paper"], PositionPaper, Color.White);
            spriteBatch.DrawString(Defaults.Font, "snow cone setup", new Vector2(PositionPaper.X + 350, PositionPaper.Y + 325), Defaults.Brown);
            form?.Draw(spriteBatch);

            if (ShowingInventory)
            {
                if (!DayImage.IsDoneAnimating())
                {
                    DayImage.Draw(spriteBatch);
                }
                else if (!ForecastImage.IsDoneAnimating())
                {
                    DayImage.Draw(spriteBatch);
                    spriteBatch.DrawString(Defaults.Font, $"day {Player.CurrentDay}", DayImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString($"day {Player.CurrentDay}") / 2, 0.6f, SpriteEffects.None, 1f);
                    ForecastImage.Draw(spriteBatch);
                }
                else if (!TemperatureImage.IsDoneAnimating())
                {
                    DayImage.Draw(spriteBatch);
                    spriteBatch.DrawString(Defaults.Font, $"day {Player.CurrentDay}", DayImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString($"day {Player.CurrentDay}") / 2, 0.6f, SpriteEffects.None, 1f);
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
                    spriteBatch.DrawString(Defaults.Font, $"day {Player.CurrentDay}", DayImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString($"day {Player.CurrentDay}") / 2, 0.6f, SpriteEffects.None, 1f);
                    ForecastImage.Draw(spriteBatch);
                    var forecast = CurrentForecast.ToString().ToLower();

                    if (CurrentForecast == Forecast.PartlyCloudy)
                    {
                        forecast = "partly cloudy";
                    }

                    spriteBatch.DrawString(Defaults.Font, forecast, ForecastImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString(forecast) / 2, 0.6f, SpriteEffects.None, 1f);
                    TemperatureImage.Draw(spriteBatch);

                    var temp = Temperature;
                    var symbol = "F";

                    if (Player.Degrees == Degrees.Celsius)
                    {
                        temp = Utilities.ConvertToCelsius(Temperature);
                        symbol = "C";
                    }

                    spriteBatch.DrawString(Defaults.Font, $"{temp}     {symbol}", TemperatureImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString($"{temp}     {symbol}") / 2, 0.6f, SpriteEffects.None, 1f);

                    var degrees = "    o";
                    spriteBatch.DrawString(Defaults.Font, degrees, TemperatureImage.Position, Defaults.Brown, -0.1f, new Vector2(Defaults.Font.MeasureString(degrees).X / 2, (Defaults.Font.MeasureString(degrees).Y / 2) + 50), 0.5f, SpriteEffects.None, 1f);
                    LetsGoButton.Draw(spriteBatch);
                    BackButton.Draw(spriteBatch);

                    spriteBatch.Draw(ContentHandler.Images["DaySetup_IconShop"], new Vector2(1380, 40), Color.White);

                    if (tutorialModal.Active)
                    {
                        tutorialModal.Draw(spriteBatch);
                    }
                }
            }

            if (tutorialModal.Active)
            {
                switch(tutorialModal.Step)
                {
                    case 1:
                        spriteBatch.Draw(ContentHandler.Images["DaySetup_IconShop"], new Vector2(1380, 40), Color.White);
                        break;
                    case 2:
                        DayImage.Draw(spriteBatch);
                        spriteBatch.DrawString(Defaults.Font, $"day {Player.CurrentDay}", DayImage.Position, Defaults.Brown, -0.1f, Defaults.Font.MeasureString($"day {Player.CurrentDay}") / 2, 0.6f, SpriteEffects.None, 1f);
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
                        break;
                    case 3:
                        LetsGoButton.Draw(spriteBatch);
                        break;
                    case 4:
                        spriteBatch.Draw(ContentHandler.Images["DaySetup_WatchAd"], new Vector2(PositionInv.X + 1135, PositionInv.Y + 250), Color.White);
                        break;
                }
            }

            if (adRewardModal.Active)
            {
                adRewardModal.Draw(spriteBatch);
            }
        }
    }
}
