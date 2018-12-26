using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnowConeTycoon.Shared.Backgrounds
{
    public interface IBackground
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
