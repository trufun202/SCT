using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class WatchAdModal
    {
        public bool Active = false;

        public WatchAdModal(double scaleX, double scaleY)
        {
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.FromNonPremultiplied(new Vector4(0, 0, 0, 0.75f)));
            spriteBatch.Draw(ContentHandler.Images["WatchAdModal"], new Rectangle((int)(Defaults.GraphicsWidth / 2), (int)(Defaults.GraphicsHeight / 2), ContentHandler.Images["WatchAdModal"].Width, ContentHandler.Images["WatchAdModal"].Height), null, Color.White, 0f, new Vector2((int)(ContentHandler.Images["WatchAdModal"].Width / 2), (int)(ContentHandler.Images["WatchAdModal"].Height / 2)), SpriteEffects.None, 1f);
        }
    }
}
