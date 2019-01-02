using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;
using static SnowConeTycoon.Shared.Handlers.KidHandler;

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
        private int Step = 5;
        private GameSpeed GameSpeed = GameSpeed.x1;

        public Customer()
        {
            Reset();
        }

        public void Reset()
        {
            sinWalkEvent = new TimedEvent(500,
            () =>
                {
                    YSinRadius += IsApproaching ? -5 : 5;
                },
                true);
            purchaseEvent = new TimedEvent(3000,
            () =>
            {
                IsPurchasing = false;
                IsLeaving = true;
            },
            true);
            thoughtEvent = new TimedEvent(500,
            () =>
            {
                ThoughtBubbleCount++;

                if (ThoughtBubbleCount > 3)
                    ThoughtBubbleCount = 3;
            },
            true);
            sinTime = 0;
            YSinRadius = YSinRadiusStart;
            Position.Y = Defaults.GraphicsHeight + ContentHandler.Images["BoyAvatar_01"].Height - 1400;
            Position.X = (int)(Defaults.GraphicsWidth / 2) - 300;
            IsApproaching = true;
            IsPurchasing = false;
            IsLeaving = false;
            KidType = Utilities.GetRandomInt(1, 2) == 1 ? KidType.Boy : KidType.Girl;
            KidIndex = Utilities.GetRandomInt(1, 40);
        }

        public bool SetSpeed1x()
        {
            if (GameSpeed == GameSpeed.x1)
                return false;

            YSinPeriod = 36f;
            sinWalkEvent.TimeTotal = 500;
            purchaseEvent.TimeTotal = 3000;
            thoughtEvent.TimeTotal = 500;
            Step = 5;
            GameSpeed = GameSpeed.x1;
            return true;
        }

        public bool SetSpeed2x()
        {
            if (GameSpeed == GameSpeed.x2)
                return false;

            sinWalkEvent.TimeTotal = 125;
            purchaseEvent.TimeTotal = 750;
            thoughtEvent.TimeTotal = 125;
            Step = 20;
            GameSpeed = GameSpeed.x2;
            return true;
        }

        public void Update(GameTime gameTime)
        {
            if (IsApproaching)
            {
                sinTime += gameTime.ElapsedGameTime.Milliseconds;
                Position.Y = (Position.Y - Step) + (float)Math.Sin(sinTime / YSinPeriod) * YSinRadius;
                sinWalkEvent.Update(gameTime);

                if (Position.Y < (Defaults.GraphicsHeight / 2) + 100)
                {
                    IsApproaching = false;
                    IsPurchasing = true;
                    purchaseEvent.Reset();
                    ThoughtBubbleCount = 1;
                    thoughtEvent.Reset();
                    var rand = Utilities.GetRandomInt(0, 100);

                    if (rand <= 30)
                    {
                        CurrentNPS = NPS.Detractor;
                        KidHandler.MakeKidMad(KidType, KidIndex);
                    }
                    else if (rand <= 60)
                    {
                        CurrentNPS = NPS.Passive;
                        KidHandler.MakeKidSad(KidType, KidIndex);
                    }
                    else
                    {
                        CurrentNPS = NPS.Promoter;
                        KidHandler.MakeKidHappy(KidType, KidIndex);
                    }
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

                    if (GameSpeed == GameSpeed.x2)
                    {
                        GameSpeed = GameSpeed.x1;
                        SetSpeed2x();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsApproaching || IsPurchasing)
            {
                KidHandler.DrawKid(KidType, KidIndex, spriteBatch, (int)Position.X, (int)Position.Y, 1300, true);

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
            else if (IsLeaving)
            {
                KidHandler.DrawKid(KidType, KidIndex, spriteBatch, (int)Position.X, (int)Position.Y, 1300, false);
            }
        }
    }
}
