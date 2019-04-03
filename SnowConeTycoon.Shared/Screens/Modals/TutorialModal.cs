using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens.Modals
{
    public class TutorialModal
    {
        public bool Active = false;
        private TutorialType TutorialType = TutorialType.DaySetup;
        private Vector2 StartPosition = Vector2.Zero;
        private Vector2 EndPosition = Vector2.Zero;
        private Vector2 Position = Vector2.Zero;
        public int Step = 1;
        private int Direction = 1;
        private int Time = 0;
        private int TimeTotal = 750;
        private Vector2 PaperPosition = Vector2.Zero;
        private string PaperText = string.Empty;
        private bool PlayedSound = false;
        private SpriteEffects ArrowEffects = SpriteEffects.None;
        private int TutorialCount = 0;
        private int TutorialCountTotal = 2;

        public TutorialModal(double scaleX, double scaleY)
        {
        }

        public void Show(TutorialType type, int step)
        {
            Position = StartPosition;
            Active = true;
            PlayedSound = false;
            TutorialType = type;
            Step = step;

            switch (TutorialType)
            {
                case TutorialType.DaySetup:
                    switch (step)
                    {
                        case 1:
                            ArrowEffects = SpriteEffects.None;
                            StartPosition = new Vector2(1000, 300);
                            EndPosition = new Vector2(1100, 200);
                            PaperPosition = new Vector2(Defaults.GraphicsWidth / 2, (Defaults.GraphicsHeight / 2));
                            PaperText = "tap here to get\nmore supplies\n\nyou'll need cones,\nsyrup, and flyers\n\nflyers bring in more\ncustomers.";
                            break;
                        case 2:
                            ArrowEffects = SpriteEffects.None;
                            StartPosition = new Vector2(400, 500);
                            EndPosition = new Vector2(500, 400);
                            PaperPosition = new Vector2(Defaults.GraphicsWidth / 2, (Defaults.GraphicsHeight / 2) + 100);
                            PaperText = "the day count,\nweather forecast and\ntemperature help you\nmake the perfect\nsnow cone!\n\nkids like extra syrup\non hot days and less\non cold days.";
                            break;
                        case 3:
                            ArrowEffects = SpriteEffects.FlipVertically;
                            StartPosition = new Vector2(900, 2000);
                            EndPosition = new Vector2(1000, 2100);
                            PaperPosition = new Vector2(Defaults.GraphicsWidth / 2, (Defaults.GraphicsHeight / 2) - 100);
                            PaperText = "opening your snow\n\ncone stand will cost\n\nyou one ice.";
                            break;
                        case 4:
                            ArrowEffects = SpriteEffects.None;
                            StartPosition = new Vector2(750, 1900);
                            EndPosition = new Vector2(850, 1800);
                            PaperPosition = new Vector2(Defaults.GraphicsWidth / 2, (Defaults.GraphicsHeight / 2) - 300);
                            PaperText = "tap here to watch\n\nan ad and earn more\n\ncoins and ice!";
                            break;
                        default:
                            Active = false;

                            TutorialCount++;

                            if (TutorialCount >= TutorialCountTotal)
                            {
                                Player.IsFirstTimePlaying = false;
                            }
                            break;
                    }
                    break;
                case TutorialType.SupplyShop:
                    break;
                case TutorialType.OpenForBusiness:
                    break;
                case TutorialType.Results:
                    break;
            }
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            if (previousTouchCollection.Count == 0 && currentTouchCollection.Count > 0)
            {
                Step++;
                Show(TutorialType, Step);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Active)
            {
                Time += gameTime.ElapsedGameTime.Milliseconds;

                if (Time >= TimeTotal)
                {
                    Direction *= -1;
                    Time = 0;
                }

                var amt = Time / (float)TimeTotal;

                if (Direction == 1)
                {
                    Position = Vector2.SmoothStep(StartPosition, EndPosition, amt);
                }
                else
                {
                    Position = Vector2.SmoothStep(EndPosition, StartPosition, amt);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!PlayedSound)
            {
                PlayedSound = true;
                ContentHandler.Sounds["Ding"].Play();
            }

            var paperScale = 7;
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.FromNonPremultiplied(new Vector4(0, 0, 0, 0.75f)));
            spriteBatch.Draw(ContentHandler.Images["Tutorial_Arrow"], new Rectangle((int)Position.X, (int)Position.Y, ContentHandler.Images["Tutorial_Arrow"].Width, ContentHandler.Images["Tutorial_Arrow"].Height), null, Color.White, 0f, Vector2.Zero, ArrowEffects, 1f);
            spriteBatch.Draw(ContentHandler.Images["Tutorial_Paper"], new Rectangle((int)PaperPosition.X, (int)PaperPosition.Y, ContentHandler.Images["Tutorial_Paper"].Width * paperScale, ContentHandler.Images["Tutorial_Paper"].Height * paperScale), null, Color.White, 0f, new Vector2(ContentHandler.Images["Tutorial_Paper"].Width / 2, ContentHandler.Images["Tutorial_Paper"].Height / 2), SpriteEffects.None, 1f);
            spriteBatch.DrawString(Defaults.Font, PaperText, new Vector2(PaperPosition.X, PaperPosition.Y), Defaults.Brown, 0.1f, Defaults.Font.MeasureString(PaperText) / 2, 0.62f, SpriteEffects.None, 1f);
        }
    }
}
