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
    public class ResultsScreen
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
        ScaledImage DayImage;
        ScaledImage ForecastImage;
        ScaledImage NextButton;
        BlindOpenImage NPSBackground;
        public bool DoneAnimating = false;
        PulseImage DetractorImage;
        PulseImage PassiveImage;
        PulseImage PromoterImage;
        PulseImage RankImage;
        bool ShowingDetractors = false;
        bool ShowingPassives = false;
        bool ShowingPromoters = false;
        bool ShowingRank = false;
        int DetractorCount = 0;
        public int DetractorTotal = 2;
        int PassiveCount = 0;
        public int PassiveTotal = 5;
        int PromoterCount = 0;
        public int PromoterTotal = 13;
        int TickTime = 0;
        int TickTimeTotal = 25;
        bool NPSTickerDone = false;
        bool RankDone = false;
        public int PotentialCustomerCount = 30;
        public int SnowConesSold = 20;
        public int SnowConeRate = 1;
        public int CoinsPrevious = 50;
        public int CoinsEarned = 20;

        public ResultsScreen(double scaleX, double scaleY)
        {
            ScaleX = scaleX;
            ScaleY = scaleY;
            DayImage = new ScaledImage("DaySetup_DayLabel", new Vector2(350, 200), 250);
            ForecastImage = new ScaledImage("DaySetup_ForecastLabel", new Vector2(800, 200), 250);
            NextButton = new ScaledImage("Results_Next", new Vector2(1200, 2500), 500);
            NPSBackground = new BlindOpenImage("nps_background", new Vector2((Defaults.GraphicsWidth / 2) + 20, ContentHandler.Images["DaySetup_Paper"].Height - 400), 250);
            DetractorImage = new PulseImage("nps_detractor", new Vector2((Defaults.GraphicsWidth / 2) - 310, ContentHandler.Images["DaySetup_Paper"].Height - 450), 1, 2.75f, 2.25f, 250);
            PassiveImage = new PulseImage("nps_passive", new Vector2((Defaults.GraphicsWidth / 2) + 20, ContentHandler.Images["DaySetup_Paper"].Height - 460), 1, 2.5f, 2f, 250);
            PromoterImage = new PulseImage("nps_promoter", new Vector2((Defaults.GraphicsWidth / 2) + 350, ContentHandler.Images["DaySetup_Paper"].Height - 460), 1, 2.5f, 2f, 250);
            RankImage = new PulseImage("Results_Rank", new Vector2((Defaults.GraphicsWidth / 2) + 275, ContentHandler.Images["DaySetup_Paper"].Height + 610), 0.5f, 1.25f, 1f, 250);
        }

        public void Reset()
        {
            DoneAnimating = false;
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
            NextButton.Reset();
            NPSBackground.Reset();
            ShowingDetractors = false;
            ShowingPassives = false;
            ShowingPromoters = false;
            ShowingRank = false;
            RankDone = false;
            DetractorImage.Reset();
            PassiveImage.Reset();
            PromoterImage.Reset();
            RankImage.Reset();
            DetractorCount = 0;
            PassiveCount = 0;
            PromoterCount = 0;
            NPSTickerDone = false;
            TickTime = 0;
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (AnimatingPaper)
            {
                TimePaper += gameTime.ElapsedGameTime.Milliseconds;
                var amt = TimePaper / (float)TimePaperTotal;

                PositionPaper = Vector2.SmoothStep(PositionPaperStart, PositionPaperEnd, amt);

                if (TimePaper >= TimePaperTotal)
                {
                    AnimatingPaper = false;
                }
            }
            else if (!ShowingInventory)
            {
                NPSBackground.Update(gameTime);

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
                else if (!DetractorImage.IsDoneAnimating())
                {
                    ShowingDetractors = true;
                    DetractorImage.Update(gameTime);
                }
                else if (!PassiveImage.IsDoneAnimating())
                {
                    ShowingPassives = true;
                    PassiveImage.Update(gameTime);
                }
                else if (!PromoterImage.IsDoneAnimating())
                {
                    ShowingPromoters = true;
                    PromoterImage.Update(gameTime);
                }
                else if (!NPSTickerDone)
                {
                    TickTime += gameTime.ElapsedGameTime.Milliseconds;

                    if (TickTime >= TickTimeTotal)
                    {
                        TickTime = 0;
                        DetractorCount = DetractorCount < DetractorTotal ? DetractorCount + 1 : DetractorCount;
                        PassiveCount = PassiveCount < PassiveTotal ? PassiveCount + 1 : PassiveCount;
                        PromoterCount = PromoterCount < PromoterTotal ? PromoterCount + 1 : PromoterCount;

                        if (DetractorCount == DetractorTotal
                            && PassiveCount == PassiveTotal
                            && PromoterCount == PromoterTotal)
                        {
                            NPSTickerDone = true;
                        }
                    }
                }
                else if (!RankImage.IsDoneAnimating())
                {
                    ShowingRank = true;
                    RankImage.Update(gameTime);
                }
                else if (RankDone)
                {
                    NextButton.Update(gameTime);
                }
                else if (NextButton.IsDoneAnimating())
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

                spriteBatch.Draw(ContentHandler.Images["DaySetup_IconPrice"], new Rectangle((int)PositionInv.X + 1125, (int)PositionInv.Y + 160, (int)(ContentHandler.Images["DaySetup_IconPrice"].Width * 0.75), (int)(ContentHandler.Images["DaySetup_IconPrice"].Height * 0.75)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "previous", new Vector2((int)PositionInv.X + 400, (int)PositionInv.Y + 200), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, CoinsPrevious.ToString(), new Vector2((int)PositionInv.X + 1000, (int)PositionInv.Y + 200), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_IconPrice"], new Rectangle((int)PositionInv.X + 1125, (int)PositionInv.Y + 310, (int)(ContentHandler.Images["DaySetup_IconPrice"].Width * 0.75), (int)(ContentHandler.Images["DaySetup_IconPrice"].Height * 0.75)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "earned", new Vector2((int)PositionInv.X + 400, (int)PositionInv.Y + 350), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, CoinsEarned.ToString(), new Vector2((int)PositionInv.X + 1000, (int)PositionInv.Y + 350), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "-------------------------------------", new Vector2((int)PositionInv.X + 400, (int)PositionInv.Y + 450), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_IconPrice"], new Rectangle((int)PositionInv.X + 1125, (int)PositionInv.Y + 510, (int)(ContentHandler.Images["DaySetup_IconPrice"].Width * 0.75), (int)(ContentHandler.Images["DaySetup_IconPrice"].Height * 0.75)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "total", new Vector2((int)PositionInv.X + 400, (int)PositionInv.Y + 550), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, (CoinsPrevious + CoinsEarned).ToString(), new Vector2((int)PositionInv.X + 1000, (int)PositionInv.Y + 550), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, "rank:", new Vector2(PositionInv.X + 400, PositionInv.Y + 700), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                //spriteBatch.Draw(ContentHandler.Images["Results_Rank"], new Vector2(PositionInv.X + 750, PositionInv.Y + 670), Color.White);
            }

            spriteBatch.Draw(ContentHandler.Images["DaySetup_Paper"], PositionPaper, Color.White);
            spriteBatch.DrawString(Defaults.Font, "results", new Vector2(PositionPaper.X + 600, PositionPaper.Y + 300), Color.Brown);
            spriteBatch.DrawString(Defaults.Font, "potential customers:", new Vector2(PositionPaper.X + 300, PositionPaper.Y + 550), Color.Brown, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(Defaults.Font, PotentialCustomerCount.ToString(), new Vector2(PositionPaper.X + 1200, PositionPaper.Y + 550), Color.Brown, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(Defaults.Font, "snow cones sold:", new Vector2(PositionPaper.X + 260, PositionPaper.Y + 700), Color.Brown, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(Defaults.Font, SnowConesSold.ToString() + " @ " + SnowConeRate, new Vector2(PositionPaper.X + 990, PositionPaper.Y + 700), Color.Brown, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 1f);
            spriteBatch.Draw(ContentHandler.Images["DaySetup_IconPrice"], new Rectangle((int)PositionPaper.X + 930 + (int)Defaults.Font.MeasureString(SnowConesSold.ToString() + " @ " + SnowConeRate).X, (int)PositionPaper.Y + 655, (int)(ContentHandler.Images["DaySetup_IconPrice"].Width * 0.75), (int)(ContentHandler.Images["DaySetup_IconPrice"].Height * 0.75)), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);

            if (!AnimatingPaper)
            {
                NPSBackground.Draw(spriteBatch);
            }

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

                    if (ShowingDetractors)
                    {
                        DetractorImage.Draw(spriteBatch);
                    }

                    if (ShowingPassives)
                    {
                        PassiveImage.Draw(spriteBatch);
                    }

                    if (ShowingPromoters)
                    {
                        PromoterImage.Draw(spriteBatch);
                    }

                    if (ShowingRank)
                    {
                        RankImage.Draw(spriteBatch);

                        if (RankImage.IsDoneAnimating())
                        {
                            spriteBatch.DrawString(Defaults.Font, "novice", new Vector2(PositionInv.X + 800, PositionInv.Y + 680), Color.White, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, 1f);
                            RankDone = true;
                        }
                    }

                    if (ShowingDetractors && ShowingPassives && ShowingPromoters && PromoterImage.IsDoneAnimating())
                    {
                        spriteBatch.DrawString(Defaults.Font, DetractorCount.ToString(), new Vector2((Defaults.GraphicsWidth / 2) - 310, ContentHandler.Images["DaySetup_Paper"].Height - 270), Color.White, 0f, Defaults.Font.MeasureString(DetractorCount.ToString()) / 2, 1f, SpriteEffects.None, 1f);
                        spriteBatch.DrawString(Defaults.Font, PassiveCount.ToString(), new Vector2((Defaults.GraphicsWidth / 2) + 20, ContentHandler.Images["DaySetup_Paper"].Height - 270), Color.White, 0f, Defaults.Font.MeasureString(PassiveCount.ToString()) / 2, 1f, SpriteEffects.None, 1f);
                        spriteBatch.DrawString(Defaults.Font, PromoterCount.ToString(), new Vector2((Defaults.GraphicsWidth / 2) + 350, ContentHandler.Images["DaySetup_Paper"].Height - 270), Color.White, 0f, Defaults.Font.MeasureString(PromoterCount.ToString()) / 2, 1f, SpriteEffects.None, 1f);
                    }

                    if (NPSTickerDone && RankDone)
                    {
                        NextButton.Draw(spriteBatch);
                    }
                }
            }
        }
    }
}
