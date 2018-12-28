using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Forms
{
    public class Button : IFormControl
    {
        public delegate bool OnTappedMethod();

        public Rectangle Bounds { get; set; }
        private Rectangle TrueBounds;
        private OnTappedMethod Method;
        private string Sound;
        private Microsoft.Xna.Framework.Rectangle rectangle;
        private Color DebugColor = Utilities.GetRandomColor();
        public bool Visible { get; set; }

        public Button(Rectangle bounds, OnTappedMethod method, string onTappedSound, double scaleX, double scaleY)
        {
            Visible = true;
            TrueBounds = bounds;
            Bounds = new Rectangle((int)(bounds.X * scaleX), (int)(bounds.Y * scaleY), (int)(bounds.Width * scaleX), (int)(bounds.Height * scaleY));
            Method = method;
            Sound = onTappedSound;
        }

        public Button(Microsoft.Xna.Framework.Rectangle rectangle)
        {
            this.rectangle = rectangle;
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            if (Visible)
            {
                if (currentTouchCollection.Count > 0)
                {
                    if ((currentTouchCollection[0].State == TouchLocationState.Moved || currentTouchCollection[0].State == TouchLocationState.Pressed)
                        && (previousTouchCollection.Count == 0))
                    {
                        if (Bounds.Contains((int)currentTouchCollection[0].Position.X, (int)currentTouchCollection[0].Position.Y))
                        {
                            if (Method.Invoke() && !string.IsNullOrWhiteSpace(Sound))
                            {
                                ContentHandler.Sounds[Sound].Play();
                            }
                        }
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Defaults.DebugMode)
            {
                spriteBatch.Draw(ContentHandler.Images["debugbox"], TrueBounds, DebugColor);
            }
        }
    }
}
