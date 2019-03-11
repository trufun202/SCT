using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class AdRewardModal
    {
        public bool Active = false;
        Form form;
        bool PlayedSound = false;

        public AdRewardModal(double scaleX, double scaleY)
        {
            form = new Form(0, 0);
            form.Controls.Add(new Button(new Rectangle(1125, 1175, 200, 200),
            () =>
            {
                Active = false;
                return true;
            }, string.Empty, scaleX, scaleY));
            form.Ready = true;
        }

        public void Reset()
        {
            PlayedSound = false;
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            form.HandleInput(previousTouchCollection, currentTouchCollection);
        }

        public void Update(GameTime gameTime)
        {
            form.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!PlayedSound)
            {
                PlayedSound = true;
                ContentHandler.Sounds["Ice_Cube"].Play();
            }

            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.FromNonPremultiplied(new Vector4(0, 0, 0, 0.75f)));
            spriteBatch.Draw(ContentHandler.Images["AdReward_Modal"], new Rectangle((int)(Defaults.GraphicsWidth / 2), (int)(Defaults.GraphicsHeight / 2), ContentHandler.Images["AdReward_Modal"].Width, ContentHandler.Images["AdReward_Modal"].Height), null, Color.White, 0f, new Vector2((int)(ContentHandler.Images["AdReward_Modal"].Width / 2), (int)(ContentHandler.Images["AdReward_Modal"].Height / 2)), SpriteEffects.None, 1f);
            form.Draw(spriteBatch);
        }
    }
}
