using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;
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
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle((int)Position.X, (int)Position.Y, 2, 8), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position.Y > Defaults.GraphicsHeight)
            {
                Position.Y = 0;
            }
        }
    }
}
