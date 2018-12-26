using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Backgrounds
{
    public class BackgroundPartlyCloudy : IBackground
    {
        private Vector2 Paralax1Pos;
        private Vector2 Paralax2Pos;
        private int BackgroundWidth;
        private Vector2 Direction = new Vector2(-1, 0);
        private Vector2 Speed = new Vector2(30, 0);
        float SunRotation = 0f;
        Vector2 rotationStart = new Vector2(-1, -1);
        Vector2 rotationEnd = new Vector2(1, 1);
        int rotationTime = 0;
        int rotationTimeTotal = 8000;
        int rotationDirection = 1;

        public BackgroundPartlyCloudy()
        {
            BackgroundWidth = ContentHandler.Images["Background_Clouds"].Width;
            Paralax1Pos = new Vector2(0, 870);
            Paralax2Pos = new Vector2(BackgroundWidth, 870);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Defaults.SkyBlue);
            spriteBatch.Draw(ContentHandler.Images["Background_Sun"], new Rectangle(250, 500, 4000, 4000), null, Color.White, SunRotation, new Vector2(ContentHandler.Images["Background_Sun"].Width / 2, ContentHandler.Images["Background_Sun"].Height / 2), SpriteEffects.None, 0);
            spriteBatch.Draw(ContentHandler.Images["Background_Clouds"], Paralax1Pos, Color.White);
            spriteBatch.Draw(ContentHandler.Images["Background_Clouds"], Paralax2Pos, Color.White);
            spriteBatch.Draw(ContentHandler.Images["Background_Hills"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (Paralax1Pos.X < -BackgroundWidth)
            {
                Paralax1Pos.X = Paralax2Pos.X + BackgroundWidth;
            }

            if (Paralax2Pos.X < -BackgroundWidth)
            {
                Paralax2Pos.X = Paralax1Pos.X + BackgroundWidth;
            }

            Paralax1Pos += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Paralax2Pos += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            rotationTime += gameTime.ElapsedGameTime.Milliseconds;

            if (rotationTime >= rotationTimeTotal)
            {
                rotationTime = 0;
                rotationDirection *= -1;
            }

            var amount = rotationTime / (float)rotationTimeTotal;
            var resultVector = Vector2.SmoothStep(rotationStart, rotationEnd, amount);

            SunRotation = (float)(resultVector.X * rotationDirection);
        }
    }
}
