using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Backgrounds.Effects.Components
{
    public class Raindrop : IBackgroundEffectComponent
    {
        private Vector2 Position;
        private Vector2 Direction = new Vector2(0, 1);
        private int Speed = 0;
        private int SpeedMin = 750;
        private int SpeedMax = 2000;
        GameSpeed gameSpeed = GameSpeed.x1;

        public Raindrop()
        {
            Position = new Vector2()
            {
                X = Utilities.GetRandomInt(0, Defaults.GraphicsWidth),
                Y = Utilities.GetRandomInt(0, Defaults.GraphicsHeight)
            };

            Speed = Utilities.GetRandomInt(SpeedMin, SpeedMax);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle((int)Position.X, (int)Position.Y, 4, 32), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (gameSpeed == GameSpeed.x1 && Player.GameSpeed == GameSpeed.x2)
            {
                gameSpeed = Player.GameSpeed;
                SpeedMin = 7500;
                SpeedMax = 20000;
                Speed = Utilities.GetRandomInt(SpeedMin, SpeedMax);
            }
            else if (gameSpeed == GameSpeed.x2 && Player.GameSpeed == GameSpeed.x1)
            {
                gameSpeed = Player.GameSpeed;
                SpeedMin = 750;
                SpeedMax = 2000;
                Speed = Utilities.GetRandomInt(SpeedMin, SpeedMax);
            }

            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position.Y > Defaults.GraphicsHeight)
            {
                Position.Y = 0;
            }
        }
    }
}
