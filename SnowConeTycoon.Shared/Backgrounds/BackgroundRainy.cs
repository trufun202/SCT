using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Backgrounds.Effects;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Backgrounds
{
    public class BackgroundRainy : IBackground
    {
        private Vector2 Paralax1Pos;
        private Vector2 Paralax2Pos;
        private int BackgroundWidth;
        private Vector2 Direction = new Vector2(-1, 0);
        private Vector2 Speed = new Vector2(30, 0);
        GameSpeed gameSpeed = GameSpeed.x1;

        public BackgroundRainy()
        {
            BackgroundWidth = ContentHandler.Images["Background_ClearClouds"].Width;
            Paralax1Pos = new Vector2(0, 870);
            Paralax2Pos = new Vector2(BackgroundWidth, 870);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Defaults.DarkBlue);
            spriteBatch.Draw(ContentHandler.Images["Background_ClearClouds"], Paralax1Pos, Color.White);
            spriteBatch.Draw(ContentHandler.Images["Background_ClearClouds"], Paralax2Pos, Color.White);
            spriteBatch.Draw(ContentHandler.Images["Background_HillsDark"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (gameSpeed == GameSpeed.x1 && Player.GameSpeed == GameSpeed.x2)
            {
                gameSpeed = Player.GameSpeed;
                Speed = new Vector2(300, 0);
            }
            else if (gameSpeed == GameSpeed.x2 && Player.GameSpeed == GameSpeed.x1)
            {
                gameSpeed = Player.GameSpeed;
                Speed = new Vector2(30, 0);
            }

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
        }
    }
}
