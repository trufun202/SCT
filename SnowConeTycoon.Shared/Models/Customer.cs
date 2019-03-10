using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Particles;
using SnowConeTycoon.Shared.Utils;
using static SnowConeTycoon.Shared.Handlers.KidHandler;
using SnowConeTycoon.Shared.Extensions;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Screens;

namespace SnowConeTycoon.Shared.Kids
{
    public class Customer
    {
        private bool IsApproaching = true;
        private bool IsPurchasing = false;
        private bool IsLeaving = false;
        private KidType KidType = KidType.Girl;
        private int KidIndex = 1;
        private Vector2 Position = Vector2.Zero;
        private int sinTime = 0;
        private float YSinRadiusStart = 30f;
        private float YSinRadius = 30f;
        private float YSinPeriod = 36f;
        private TimedEvent sinWalkEvent;
        private TimedEvent purchaseEvent;
        private TimedEvent thoughtEvent;
        private int ThoughtBubbleCount = 1;
        private NPS CurrentNPS = NPS.Promoter;
        private int Step = 15;
        private BezierCurveImage coinImage;
        private bool ShowingCoin = false;
        private bool AnimatingCoin = false;
        private TimedEvent ShowingCoinEvent;
        private Vector2 CoinScaleStart = new Vector2(0, 1);
        private Vector2 CoinScaleEnd = new Vector2(0, 3);
        private int CoinScaleTime = 0;
        private int CoinScaleTimeTotal = 250;
        private int CoinScaleDirection = 1;
        private Vector2 CoinScale = new Vector2(0, 1);
        private bool CoinHasScaled = false;
        private ParticleEmitter ParticleEmitter;
        private ParticleEmitter ParticleCircleEmitter;
        private BusinessDayResult Results = new BusinessDayResult();
        private List<CustomerTransaction> Transactions = new List<CustomerTransaction>();
        private int TransactionIndex = 0;
        private SnowConeTycoonGame Game;
        private TimedEvent SpeedUpEvent;
        private TimedEvent DayDoneEvent;
        private OpenForBusinessScreen Screen;

        public Customer(SnowConeTycoonGame game, OpenForBusinessScreen screen)
        {
            Game = game;
            Screen = screen;
            ResetScene();
        }

        private void BuildTransactions(BusinessDayResult results)
        {
            Results = results;

            Transactions.Clear();
            TransactionIndex = 0;

            var purchaseCount = 0;
            var promoterCount = 0;
            var passiveCount = 0;
            var detractorCount = 0;

            for (int i = 0; i < results.PotentialCustomers; i++)
            {
                var transaction = new CustomerTransaction();

                if (purchaseCount < results.SnowConesSold)
                {
                    transaction.MadePurchase = true;

                    if (promoterCount < results.NPSPromoters)
                    {
                        transaction.NPS = NPS.Promoter;
                        promoterCount++;
                    }
                    else if (passiveCount < results.NPSPassives)
                    {
                        transaction.NPS = NPS.Passive;
                        passiveCount++;
                    }
                    else if (detractorCount < results.NPSDetractors)
                    {
                        transaction.NPS = NPS.Detractor;
                        detractorCount++;
                    }

                    purchaseCount++;
                }
                else
                {
                    transaction.MadePurchase = false;
                }

                transaction.ConfigureKid();
                Transactions.Add(transaction);
            }

            Transactions.Shuffle();
        }

        public void ResetScene()
        {
            ParticleEmitter = new ParticleEmitter(100, 0, 0, 30, 3000);
            ParticleCircleEmitter = new ParticleEmitter(5000, 0, 0, 40, 700);
            ParticleCircleEmitter.Velocity = new Vector2(350, 350);
            SpeedUpEvent = new TimedEvent(10000,
            () =>
            {
                SetSpeed2x();
                ContentHandler.Sounds["Swoosh"].Play();
            }, 1);
            DayDoneEvent = new TimedEvent(20000,
            () =>
            {
                Game.GoToResultsScreen(Results);
            }, 1);
            SetSpeed1x();
        }

        public void Reset(BusinessDayResult results)
        {
            BuildTransactions(results);
            TransactionIndex = 0;
            Reset();
        }

        private void Reset()
        {
            sinWalkEvent = new TimedEvent(500,
            () =>
            {
                YSinRadius += IsApproaching ? -5 : 5;
            },
                -1);
            purchaseEvent = new TimedEvent(2000,
            () =>
            {
                IsPurchasing = false;
                IsLeaving = true;
            },
            -1);
            thoughtEvent = new TimedEvent(500,
            () =>
            {
                ThoughtBubbleCount++;

                if (ThoughtBubbleCount > 3)
                {
                    ThoughtBubbleCount = 3;
                    switch (CurrentNPS)
                    {
                        case NPS.Detractor:
                            KidHandler.CurrentKid.MakeMad();
                            KidHandler.MakeKidMad(KidType, KidIndex);
                            break;
                        case NPS.Passive:
                            KidHandler.CurrentKid.MakeSad();
                            KidHandler.MakeKidSad(KidType, KidIndex);
                            break;
                        case NPS.Promoter:
                            KidHandler.CurrentKid.MakeHappy();
                            KidHandler.MakeKidHappy(KidType, KidIndex);
                            break;
                    }
                }
            },
            -1);
            sinTime = 0;
            YSinRadius = YSinRadiusStart;
            Position.Y = Defaults.GraphicsHeight + ContentHandler.Images["BoyAvatar_01"].Height - 1400;
            Position.X = (int)(Defaults.GraphicsWidth / 2) - 300;
            IsApproaching = true;
            IsPurchasing = false;
            IsLeaving = false;


            KidType = Transactions[TransactionIndex].KidType;
            KidIndex = Transactions[TransactionIndex].KidIndex;
            if (Player.GameSpeed == GameSpeed.x1)
            {
                coinImage = new BezierCurveImage("DaySetup_IconPrice", 0, 0);
            }
            ShowingCoinEvent = new TimedEvent(1000,
            () =>
            {
                if (Player.GameSpeed == GameSpeed.x1)
                {
                    AnimatingCoin = true;
                    ContentHandler.Sounds["Magic Wand 1"].Play();
                }

                ParticleCircleEmitter.FlowOn = false;
                IsPurchasing = true;
                purchaseEvent.Reset();
                ThoughtBubbleCount = 1;
                thoughtEvent.Reset();
                CurrentNPS = Transactions[TransactionIndex].NPS;
            },
            1);
            ShowingCoin = false;
            CoinScale = new Vector2(0, 1);
            CoinScaleTime = 0;
            CoinScaleDirection = 1;
            CoinHasScaled = false;
        }

        public bool SetSpeed1x()
        {
            if (Player.GameSpeed == GameSpeed.x1)
                return false;

            Reset();
            YSinPeriod = 36f;
            sinWalkEvent.TimeTotal = 500;
            purchaseEvent.TimeTotal = 2000;
            thoughtEvent.TimeTotal = 500;
            Step = 15;
            Player.GameSpeed = GameSpeed.x1;
            CoinScaleTimeTotal = 250;
            AnimatingCoin = false;
            ParticleCircleEmitter.Reset();
            ParticleEmitter.Reset();
            return true;
        }

        public bool SetSpeed2x()
        {
            if (Player.GameSpeed == GameSpeed.x2)
                return false;

            Reset();
            sinWalkEvent.TimeTotal = 50;
            purchaseEvent.TimeTotal = 100;
            thoughtEvent.TimeTotal = 10;
            ShowingCoinEvent.TimeTotal = 50;
            Step = 48;
            Player.GameSpeed = GameSpeed.x2;
            CoinScaleTimeTotal = 25;
            AnimatingCoin = false;
            return true;
        }

        public void Update(GameTime gameTime)
        {
            SpeedUpEvent.Update(gameTime);
            DayDoneEvent.Update(gameTime);

            if (IsApproaching)
            {
                sinTime += gameTime.ElapsedGameTime.Milliseconds;
                Position.Y = (Position.Y - Step) + (float)Math.Sin(sinTime / YSinPeriod) * YSinRadius;
                sinWalkEvent.Update(gameTime);

                if (Position.Y < (Defaults.GraphicsHeight / 2) + 100)
                {
                    IsApproaching = false;

                    if (Transactions[TransactionIndex].MadePurchase)
                    {
                        ShowingCoin = true;
                        coinImage = new BezierCurveImage("DaySetup_IconPrice", (int)Position.X + 660, (int)Position.Y);
                        ParticleCircleEmitter.FlowOn = true;
                        ParticleCircleEmitter.Position = new Vector2((int)Position.X + 660, (int)Position.Y);
                        ContentHandler.Sounds["Game Coin"].Play();
                        ShowingCoinEvent.Reset();
                    }
                    else
                    {
                        IsLeaving = true;
                    }

                    AnimatingCoin = false;
                }
            }
            else if (IsPurchasing)
            {
                purchaseEvent.Update(gameTime);
                thoughtEvent.Update(gameTime);
            }
            else if (IsLeaving)
            {
                sinTime += gameTime.ElapsedGameTime.Milliseconds;
                Position.Y = (Position.Y + Step) + (float)Math.Sin(sinTime / YSinPeriod) * YSinRadius;
                sinWalkEvent.Update(gameTime);

                if (Position.Y > Defaults.GraphicsHeight - 700)
                {
                    Reset();

                    TransactionIndex++;

                    if (TransactionIndex >= Transactions.Count - 1)
                    {
                        Game.GoToResultsScreen(Results);
                    }

                    if (Player.GameSpeed == GameSpeed.x2)
                    {
                        Player.GameSpeed = GameSpeed.x1;
                        SetSpeed2x();
                    }
                }
            }

            if (Player.GameSpeed == GameSpeed.x1)
            {
                ParticleEmitter.Update(gameTime);
                ParticleCircleEmitter.Update(gameTime);
            }

            if (ShowingCoin)
            {
                ShowingCoinEvent.Update(gameTime);

                if (!CoinHasScaled)
                {
                    CoinScaleTime += gameTime.ElapsedGameTime.Milliseconds;

                    var amt = CoinScaleTime / (float)CoinScaleTimeTotal;

                    if (CoinScaleDirection == 1)
                    {
                        CoinScale = Vector2.SmoothStep(CoinScaleStart, CoinScaleEnd, amt);
                    }
                    else
                    {
                        CoinScale = Vector2.SmoothStep(CoinScaleEnd, CoinScaleStart, amt);
                    }

                    if (CoinScaleTime >= CoinScaleTimeTotal)
                    {
                        CoinScaleDirection *= -1;
                        CoinScaleTime = 0;

                        if (CoinScaleDirection == 1)
                        {
                            if (Player.GameSpeed == GameSpeed.x2)
                            {
                                Screen.AddCoinDisplay(Results.SnowConePrice);
                                ContentHandler.Sounds["Cash Register Fast"].Play();
                            }

                            CoinHasScaled = true;
                        }
                    }
                }
            }

            if (AnimatingCoin)
            {
                coinImage.Update(gameTime);

                if (Player.GameSpeed == GameSpeed.x1)
                {
                    ParticleEmitter.FlowOn = true;
                    ParticleEmitter.Position = coinImage.Position;

                    if (coinImage.IsDoneAnimating())
                    {
                        ShowingCoin = false;
                        AnimatingCoin = false;
                        ParticleEmitter.FlowOn = false;
                        Screen.AddCoinDisplay(Results.SnowConePrice);
                        ContentHandler.Sounds["Cash Register Fast"].Play();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Player.GameSpeed == GameSpeed.x1)
            {
                ParticleEmitter.Draw(spriteBatch);
                ParticleCircleEmitter.Draw(spriteBatch);
            }

            if (IsApproaching || IsPurchasing || (ShowingCoin && Player.GameSpeed == GameSpeed.x1) || AnimatingCoin)
            {
                var facingAway = true;

                if (Player.GameSpeed == GameSpeed.x2)
                    facingAway = false;

                KidHandler.DrawKid(KidType, KidIndex, spriteBatch, (int)Position.X, (int)Position.Y, 1300, facingAway, true);

                if (ShowingCoin || AnimatingCoin)
                {
                    coinImage.Draw(spriteBatch, CoinScale.Y);
                }

                if (IsPurchasing)
                {
                    if (ThoughtBubbleCount >= 1)
                    {
                        spriteBatch.Draw(ContentHandler.Images["ThoughtBubble"], new Rectangle(800, 2200, 40, 40), Color.White);
                    }

                    if (ThoughtBubbleCount >= 2)
                    {
                        spriteBatch.Draw(ContentHandler.Images["ThoughtBubble"], new Rectangle(700, 2100, 80, 80), Color.White);
                    }

                    if (ThoughtBubbleCount >= 3)
                    {
                        spriteBatch.Draw(ContentHandler.Images["ThoughtCloud"], new Vector2(100, 1600), Color.White);

                        var nps = "nps_promoter";

                        switch(CurrentNPS)
                        {
                            case NPS.Passive:
                                nps = "nps_passive";
                                break;
                            case NPS.Promoter:
                                nps = "nps_promoter";
                                break;
                            case NPS.Detractor:
                                nps = "nps_detractor";
                                break;
                        }
                        spriteBatch.Draw(ContentHandler.Images[nps], new Rectangle(350, 1750, 256, 256), Color.White);
                    }
                }
            }
            else
            {
                KidHandler.DrawKid(KidType, KidIndex, spriteBatch, (int)Position.X, (int)Position.Y, 1300, false, true);
            }
        }
    }
}
