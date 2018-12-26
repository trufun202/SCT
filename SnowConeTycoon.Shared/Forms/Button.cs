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

        private Rectangle Bounds;
        private Rectangle TrueBounds;
        private OnTappedMethod Method;
        private string Sound;
        private Microsoft.Xna.Framework.Rectangle rectangle;
        private Color DebugColor = Utilities.GetRandomColor();

        public Button(Rectangle bounds, OnTappedMethod method, string onTappedSound, double scaleX, double scaleY)
        {
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
