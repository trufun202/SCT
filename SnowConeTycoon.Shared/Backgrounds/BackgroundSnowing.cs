using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Backgrounds
{
    public class BackgroundSnowing : IBackground
    {
        public BackgroundSnowing()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Defaults.DarkBlue);
            spriteBatch.Draw(ContentHandler.Images["Background_HillsDark"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.White);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
