using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.ScreenTransitions
{
    public class FadeTransition : IScreenTransition
    {
        private Color CurrentFadeColor;
        private Color FadeColor;
        private bool FadingIn = true;
        private TimedEvent FadingInEvent;
        private TimedEvent FadingOutEvent;
        private int FadeTime = 500;
        private int FadeOutTime = 1000;
        public bool ShowingFade = false;

        public FadeTransition(Color fadeColor, OnTransitionDoneMethod method)
        {
            FadeColor = fadeColor;
            Reset(method);
        }

        public void Reset(OnTransitionDoneMethod method)
        {
            FadingIn = true;
            CurrentFadeColor = FadeColor;
            FadingInEvent = new TimedEvent(FadeTime, () => {
                FadingIn = false;
                method.Invoke();
            }, 1);
            FadingOutEvent = new TimedEvent(FadeOutTime, () => {
                ShowingFade = false;
            }, 1);

            if (method != null)
            {
                ShowingFade = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), CurrentFadeColor);
        }

        public void Update(GameTime gameTime)
        {
            if (ShowingFade)
            {
                if (FadingIn)
                {
                    FadingInEvent.Update(gameTime);
                    CurrentFadeColor = Color.Lerp(Color.Transparent, FadeColor, FadingInEvent.Time / (float)FadeTime);
                }
                else
                {
                    FadingOutEvent.Update(gameTime);
                    CurrentFadeColor = Color.Lerp(FadeColor, Color.Transparent, FadingOutEvent.Time / (float)FadeOutTime);
                }
            }
        }
    }
}
