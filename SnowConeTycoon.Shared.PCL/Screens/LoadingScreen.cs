using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class LoadingScreen
    {
        int FrameTime = 0;
        int FrameTimeTotal = 50;
        int Frame = 1;

        public LoadingScreen()
        {
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {

        }

        public void Update(GameTime gameTime)
        {
            FrameTime += gameTime.ElapsedGameTime.Milliseconds;

            if (FrameTime > FrameTimeTotal)
            {
                FrameTime = 0;
                Frame++;

                if (Frame > 8)
                {
                    Frame = 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Defaults.Brown);
            spriteBatch.Draw(ContentHandler.Images["SupplyShop_Background"], Vector2.Zero, Color.White);
            spriteBatch.Draw(ContentHandler.Images["Loading_Frame"], new Rectangle((int)(Defaults.GraphicsWidth / 2), (int)(Defaults.GraphicsHeight / 2), ContentHandler.Images["Loading_Frame"].Width, ContentHandler.Images["Loading_Frame"].Height), null, Color.White, 0f, new Vector2((int)(ContentHandler.Images["Loading_Frame"].Width / 2), (int)(ContentHandler.Images["Loading_Frame"].Height / 2)), SpriteEffects.None, 1f);
            spriteBatch.Draw(ContentHandler.Images[$"Loading_Bar0{Frame}"], new Rectangle((int)(Defaults.GraphicsWidth / 2), (int)(Defaults.GraphicsHeight / 2), ContentHandler.Images[$"Loading_Bar0{Frame}"].Width, ContentHandler.Images[$"Loading_Bar0{Frame}"].Height), null, Color.White, 0f, new Vector2((int)(ContentHandler.Images[$"Loading_Bar0{Frame}"].Width / 2), (int)(ContentHandler.Images[$"Loading_Bar0{Frame}"].Height / 2)), SpriteEffects.None, 1f);
            spriteBatch.DrawString(Defaults.Font, "loading", new Vector2((int)(Defaults.GraphicsWidth / 2), (int)(Defaults.GraphicsHeight / 2)), Color.Brown, 0f, new Vector2((int)(Defaults.Font.MeasureString("loading").X / 2), (int)(Defaults.Font.MeasureString("loading").Y / 2)), 0.5f, SpriteEffects.None, 1f);
        }
    }
}
