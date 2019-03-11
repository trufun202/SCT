using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class LogoScreen
    {
        bool AnimatingLogo = false;
        bool AnimatingCircle = false;
        ScaledImage ChrosGamesLogo;
        int CircleFadeTime = 0;
        int CircleFadeTimeTotal = 1500;
        Color CircleFadeColor = Color.Transparent;
        int CircleHeight = 0;
        int CircleWidth = 0;
        TimedEvent delayEvent;

        public LogoScreen()
        {
            CircleWidth = ContentHandler.Images["ChrosGamesLogoCircle"].Width;
            CircleHeight = ContentHandler.Images["ChrosGamesLogoCircle"].Height;

            ChrosGamesLogo = new ScaledImage("ChrosGamesLogoNoCircle", new Vector2((int)(Defaults.GraphicsWidth / 2), (int)(Defaults.GraphicsHeight / 2)), 500);

            delayEvent = new TimedEvent(500,
            () =>
                {
                    AnimatingLogo = true;
                },
                1);
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {

        }

        public void Update(GameTime gameTime)
        {
            delayEvent.Update(gameTime);

            if (AnimatingLogo)
            {
                ChrosGamesLogo.Update(gameTime);

                if (ChrosGamesLogo.IsDoneAnimating())
                {
                    AnimatingLogo = false;
                    AnimatingCircle = true;
                    ContentHandler.Sounds["ChrosGame_Logo"].Play();
                }
            }
            else if (AnimatingCircle)
            {
                CircleFadeTime += gameTime.ElapsedGameTime.Milliseconds;
                CircleFadeColor = Color.Lerp(Color.Transparent, Color.White, CircleFadeTime / (float)CircleFadeTimeTotal);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.White);

            if (AnimatingLogo || AnimatingCircle)
            {
                spriteBatch.Draw(ContentHandler.Images["ChrosGamesLogoCircle"], new Rectangle((int)(Defaults.GraphicsWidth / (float)2), (int)(Defaults.GraphicsHeight / (float)2), Defaults.GraphicsWidth, Defaults.GraphicsWidth), null, CircleFadeColor, 0f, new Vector2(CircleWidth / (float)2, CircleHeight / (float)2), SpriteEffects.None, 1f);
                ChrosGamesLogo.Draw(spriteBatch);
            }
        }
    }
}
