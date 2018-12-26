using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;

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
        public Kid(string name, string avatar, string eyes, bool locked = false)
        {
            IsLocked = locked;
            Name = name;
            Image = ContentHandler.Images[avatar];
            Eyes = ContentHandler.Images[eyes];
            MakeHappy();

            EyeClosingEvent = new TimedEvent(3000, () => { IsBlinking = true; EyeOpeningEvent.Reset(); }, true);
            EyeOpeningEvent = new TimedEvent(150, () => { IsBlinking = false; EyeClosingEvent.Reset(); }, true);
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
            }, true);
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            Color color = Color.White;

            if (IsLocked)
            {
                color = Color.Black;
            }

            spriteBatch.Draw(Image, new Rectangle(x, y, Size, Size), color);

            if (!IsBlinking)
            {
                spriteBatch.Draw(Eyes, new Rectangle(x, y, Size, Size), color);
            }
            else
            {
                spriteBatch.Draw(ContentHandler.Images["Eyes_Closed1"], new Rectangle(x, y, Size, Size), color);
            }

            spriteBatch.Draw(Mouth, new Rectangle(x, y, Size, Size), color);
        }

        public void MakeHappy()
        {
            var mouthIndex = Utilities.GetRandomInt(1, 9);
            Mouth = ContentHandler.Images[$"mouth_happy{mouthIndex}"];
        }

        public void MakeMad()
        {
            var mouthIndex = Utilities.GetRandomInt(1, 6);
            Mouth = ContentHandler.Images[$"mouth_mad{mouthIndex}"];
        }

        public void MakeSad()
        {
            var mouthIndex = Utilities.GetRandomInt(1, 8);
            Mouth = ContentHandler.Images[$"mouth_sad{mouthIndex}"];
        }

        public void Update(GameTime gameTime)
        {
            if (IsBlinking)
            {
                EyeOpeningEvent.Update(gameTime);
            }
            else
            {
                EyeClosingEvent.Update(gameTime);
            }

            EmotionEvent.Update(gameTime);
        }
    }
}
