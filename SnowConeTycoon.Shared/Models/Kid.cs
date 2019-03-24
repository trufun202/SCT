using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Particles;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Kids
{
    public class Kid : IKid
    {
        private KidState State;
        private Texture2D Image;
        private Texture2D Eyes;
        private Texture2D Mouth;
        private bool IsBlinking = false;
        private TimedEvent EyeClosingEvent;
        private TimedEvent EyeOpeningEvent;
        private int Size = 1100;
        private TimedEvent EmotionEvent;
        public string Name { get; set; }
        public bool IsLocked { get; set; }
        public int UnlockPrice { get; set; }
        private ParticleEmitter ParticleEmitter;
        private TimedEvent ParticleEvent;
        public UnlockMechanism UnlockMechanism { get; set; }
        private GameSpeed gameSpeed = GameSpeed.x1;

        public Kid(string name, string avatar, string eyes, bool locked = false, UnlockMechanism unlockMechanism = UnlockMechanism.None, int unlockValue = 0)
        {
            UnlockMechanism = unlockMechanism;
            IsLocked = locked;
            UnlockPrice = unlockValue;
            Name = name;
            Image = ContentHandler.Images[avatar];
            Eyes = ContentHandler.Images[eyes];
            //MakeHappy();
            MakeMad();

            EyeClosingEvent = new TimedEvent(3000, () => { IsBlinking = true; EyeOpeningEvent.Reset(); }, -1);
            EyeOpeningEvent = new TimedEvent(150, () => { IsBlinking = false; EyeClosingEvent.Reset(); }, -1);
            EmotionEvent = new TimedEvent(6000, () =>
            {
                switch (State)
                {
                    case KidState.Happy:
                        MakeHappy();
                        break;
                    case KidState.Mad:
                        MakeMad();
                        break;
                    case KidState.Sad:
                        MakeSad();
                        break;
                }
            }, -1);

            if (locked)
            {
                ParticleEmitter = new ParticleEmitter(500, (int)(Defaults.GraphicsWidth / 2), (int)(Defaults.GraphicsHeight / 2), 40, 1000);
                ParticleEmitter.Gravity = 20f;
                ParticleEmitter.Velocity = new Vector2(1350, 1350);
                ParticleEmitter.SetCircularPath(300);
            }
        }

        public void SetGameSpeedx1()
        {
            gameSpeed = GameSpeed.x1;
            EyeClosingEvent.TimeTotal = 3000;
            EyeOpeningEvent.TimeTotal = 150;
            EmotionEvent.TimeTotal = 6000;
        }

        public void SetGameSpeedx2()
        {
            gameSpeed = GameSpeed.x2;
            EyeClosingEvent.TimeTotal = 3000;
            EyeOpeningEvent.TimeTotal = 150;
            EmotionEvent.TimeTotal = 6000;
        }

        public void Unlock()
        {
            if (IsLocked)
            {
                MakeHappy();
                ParticleEmitter.FlowOn = true;
                ParticleEvent = new TimedEvent(1000, () =>
                {
                    ParticleEmitter.FlowOn = false;
                    IsLocked = false;
                },
                1);
            }
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y, bool facingAway, int? size, bool isCustomer = false)
        {
            if (!size.HasValue)
            {
                size = Size;
            }

            Color color = Color.White;

            if (IsLocked && !isCustomer)
            {
                color = Color.FromNonPremultiplied(new Vector4(0.2f, 0.2f, 0.2f, 1));
            }

            var effect = SpriteEffects.None;

            if (facingAway)
            {
                color = Color.Black;
                effect = SpriteEffects.FlipHorizontally;
            }

            spriteBatch.Draw(Image, new Rectangle(x, y, size.Value, size.Value), null, color, 0f, Vector2.Zero, effect, 1f);

            if (!IsBlinking)
            {
                spriteBatch.Draw(Eyes, new Rectangle(x, y, size.Value, size.Value), color);
            }
            else
            {
                spriteBatch.Draw(ContentHandler.Images["Eyes_Closed1"], new Rectangle(x, y, size.Value, size.Value), color);
            }

            spriteBatch.Draw(Mouth, new Rectangle(x, y, size.Value, size.Value), color);

            if (IsLocked && UnlockMechanism == UnlockMechanism.Purchase && !isCustomer)
            {
                spriteBatch.DrawString(Defaults.Font, UnlockPrice.ToString(), new Vector2((int)(x + (size.Value / 2)) - 85, (int)(y + (size.Value / 2)) + 150), Defaults.Cream, 0f, Defaults.Font.MeasureString(UnlockPrice.ToString()) / 2, 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_IconPrice"], new Rectangle((int)(x + (size.Value / 2)) + 85, (int)(y + (size.Value / 2) - 15 + 150), ContentHandler.Images["DaySetup_IconPrice"].Width, ContentHandler.Images["DaySetup_IconPrice"].Height), null, Color.White, 0f, new Vector2((int)(ContentHandler.Images["DaySetup_IconPrice"].Width / 2), (int)(ContentHandler.Images["DaySetup_IconPrice"].Height / 2)), SpriteEffects.None, 1f);
                //spriteBatch.Draw(ContentHandler.Images["lock"], new Rectangle((int)(x + (size.Value / 2)), (int)(y + (size.Value / 2) + 250), ContentHandler.Images["lock"].Width, ContentHandler.Images["lock"].Height), null, Color.White, 0f, new Vector2((int)(ContentHandler.Images["lock"].Width / 2), (int)(ContentHandler.Images["lock"].Height / 2)), SpriteEffects.None, 1f);
            }

            ParticleEmitter?.Draw(spriteBatch);
        }

        public string GetName()
        {
            if (IsLocked)
                return "???";

            return Name;
        }

        public void MakeHappy()
        {
            State = KidState.Happy;
            var mouthIndex = Utilities.GetRandomInt(1, 9);
            Mouth = ContentHandler.Images[$"mouth_happy{mouthIndex}"];
        }

        public void MakeMad()
        {
            State = KidState.Mad;
            var mouthIndex = Utilities.GetRandomInt(1, 6);
            Mouth = ContentHandler.Images[$"mouth_mad{mouthIndex}"];
        }

        public void MakeSad()
        {
            State = KidState.Sad;
            var mouthIndex = Utilities.GetRandomInt(1, 8);
            Mouth = ContentHandler.Images[$"mouth_sad{mouthIndex}"];
        }

        public void Update(GameTime gameTime)
        {
            if (Player.GameSpeed == GameSpeed.x1 && gameSpeed == GameSpeed.x2)
            {
                SetGameSpeedx1();
            }
            else if (Player.GameSpeed == GameSpeed.x2 && gameSpeed == GameSpeed.x1)
            {
                SetGameSpeedx2();
            }

            if (IsBlinking)
            {
                EyeOpeningEvent.Update(gameTime);
            }
            else
            {
                EyeClosingEvent.Update(gameTime);
            }

            EmotionEvent.Update(gameTime);

            ParticleEmitter?.Update(gameTime);
            ParticleEvent?.Update(gameTime);
        }
    }
}
