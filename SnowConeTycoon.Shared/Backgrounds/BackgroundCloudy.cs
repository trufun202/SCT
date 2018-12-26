using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Backgrounds
{
    public class BackgroundCloudy : IBackground
    {
        private Vector2 Paralax1Pos;
        private Vector2 Paralax2Pos;
        private int BackgroundWidth;
        private Vector2 Direction = new Vector2(-1, 0);
        private Vector2 Speed = new Vector2(30, 0);

        public BackgroundCloudy()
        {
            BackgroundWidth = ContentHandler.Images["Background_Clouds"].Width;
            Paralax1Pos = new Vector2(0, 870);
            Paralax2Pos = new Vector2(BackgroundWidth, 870);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Defaults.SkyBlue);
            spriteBatch.Draw(ContentHandler.Images["Background_Clouds"], Paralax1Pos, Color.White);
            spriteBatch.Draw(ContentHandler.Images["Background_Clouds"], Paralax2Pos, Color.White);
            spriteBatch.Draw(ContentHandler.Images["Background_Hills"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if(Paralax1Pos.X < -BackgroundWidth)
            {
                Paralax1Pos.X = Paralax2Pos.X + BackgroundWidth;
            }

            if (Paralax2Pos.X < -BackgroundWidth)
            {
                Paralax2Pos.X = Paralax1Pos.X + BackgroundWidth;
            }

            Paralax1Pos += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Paralax2Pos += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
