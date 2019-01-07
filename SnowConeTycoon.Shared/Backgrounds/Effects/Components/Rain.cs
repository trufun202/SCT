using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Backgrounds.Effects.Components;

namespace SnowConeTycoon.Shared.Backgrounds.Effects
{
    public class Rain : IBackgroundEffect
    {
        private List<Raindrop> RainDrops = new List<Raindrop>();
        public Rain(int dropCount)
        {
            for (int i = 0; i < dropCount; i++)
            {
                RainDrops.Add(new Raindrop());
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var drop in RainDrops)
            {
                drop.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var drop in RainDrops)
            {
                drop.Update(gameTime);
            }
        }
    }
}
