using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Utils;
using static SnowConeTycoon.Shared.Handlers.KidHandler;

namespace SnowConeTycoon.Shared.Screens.Modals
{
    public class UnlockModal
    {
        public bool Active = false;
        private KidType KidType = KidType.Boy;
        private int KidIndex = 1;
        private Form form;

        public UnlockModal(double scaleX, double scaleY)
        {
            form = new Form(0, 0);
            form.Controls.Add(new Button(new Rectangle(1300, 650, 200, 200), () =>
            {
                Active = false;
                return true;
            }, "pop", scaleX, scaleY));
            form.Controls.Add(new Button(new Rectangle(900, 2200, 550, 200), () =>
            {
                KidHandler.SelectKid(KidType, KidIndex);
                Active = false;
                return true;
            }, "pop", scaleX, scaleY));
        }

        public void SetKid(KidType kidType, int kidIndex)
        {
            KidType = kidType;
            KidIndex = kidIndex;
            Active = true;
            form.Ready = true;
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection, GameTime gameTime)
        {
            form.HandleInput(previousTouchCollection, currentTouchCollection, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            KidHandler.UpdateKid(KidType, KidIndex, gameTime);
            form.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["WhiteDot"], new Rectangle(0, 0, Defaults.GraphicsWidth, Defaults.GraphicsHeight), Color.FromNonPremultiplied(new Vector4(0, 0, 0, 0.75f)));
            spriteBatch.Draw(ContentHandler.Images["UnlockModal"], new Rectangle((int)(Defaults.GraphicsWidth / 2), (int)(Defaults.GraphicsHeight / 2), ContentHandler.Images["UnlockModal"].Width, ContentHandler.Images["UnlockModal"].Height), null, Color.White, 0f, new Vector2((int)(ContentHandler.Images["UnlockModal"].Width / 2), (int)(ContentHandler.Images["UnlockModal"].Height / 2)), SpriteEffects.None, 1f);

            var kidName = KidHandler.GetKidName(KidType, KidIndex);

            spriteBatch.DrawString(Defaults.Font, kidName, new Vector2((int)(Defaults.GraphicsWidth / 2), (int)(Defaults.GraphicsHeight / 2) - 400), Defaults.Brown, 0f, Defaults.Font.MeasureString(kidName) / 2, 1.25f, SpriteEffects.None, 1f);
            KidHandler.DrawKid(KidType, KidIndex, spriteBatch, 210, 741, null);

            form.Draw(spriteBatch);
        }
    }
}
