using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnowConeTycoon.Shared.Backgrounds.Effects.Components
{
    public class Snow : IBackgroundEffect
    {
        private List<SnowFlake> SnowFlakes = new List<SnowFlake>();

        public Snow(int dropCount)
        {
            for (int i = 0; i < dropCount; i++)
            {
                SnowFlakes.Add(new SnowFlake());
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var flake in SnowFlakes)
            {
                flake.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var flake in SnowFlakes)
            {
                flake.Update(gameTime);
            }
        }
    }
}
