using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Backgrounds
{
    public class BackgroundSunny : IBackground
    {
        float SunRotation = 0f;
        Vector2 rotationStart = new Vector2(-1, -1);
        Vector2 rotationEnd = new Vector2(1, 1);
        int rotationTime = 0;
        int rotationTimeTotal = 8000;
        int rotationDirection = 1;
        GameSpeed gameSpeed = GameSpeed.x1;

        public BackgroundSunny()
        {
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Defaults.SkyBlue);
            spriteBatch.Draw(ContentHandler.Images["Background_Sun"], new Rectangle(250, 500, 4000, 4000), null, Color.White, SunRotation, new Vector2(ContentHandler.Images["Background_Sun"].Width / 2, ContentHandler.Images["Background_Sun"].Height / 2), SpriteEffects.None, 0);            
            spriteBatch.Draw(ContentHandler.Images["Background_Hills"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (gameSpeed == GameSpeed.x1 && Player.GameSpeed == GameSpeed.x2)
            {
                gameSpeed = Player.GameSpeed;
                rotationTimeTotal = 3000;
            }
            else if (gameSpeed == GameSpeed.x2 && Player.GameSpeed == GameSpeed.x1)
            {
                gameSpeed = Player.GameSpeed;
                rotationTimeTotal = 8000;
            }

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
