using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Kids;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class OpenForBusinessScreen
    {
        double ScaleX;
        double ScaleY;
        Form Form;
        Customer Customer;

        public OpenForBusinessScreen(double scaleX, double scaleY)
        {
            Customer = new Customer();
            ScaleX = scaleX;
            ScaleY = scaleY;
            Form = new Form(0, 0);

            Form.Controls.Add(new Button(new Rectangle(100, 1000, 136, 136),
                () =>
                {
                    return Customer.SetSpeed1x();
                },
                "", //TODO add slowdown sound
                scaleX,
                scaleY));
            Form.Controls.Add(new Button(new Rectangle(1300, 1000, 136, 136),
                () =>
                {
                    return Customer.SetSpeed2x();
                },
                "", //TODO add speedup sound
                scaleX,
                scaleY));
        }

        public void Reset()
        {
            Customer.Reset();
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            Form.HandleInput(previousTouchCollection, currentTouchCollection);
        }

        public void Update(GameTime gameTime)
        {
            Customer.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["OpenForBusiness_Foreground"], new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(Defaults.Font, "1x", new Vector2(100, 850), Color.White);
            spriteBatch.DrawString(Defaults.Font, "4x", new Vector2(1300, 850), Color.White);

            spriteBatch.Draw(ContentHandler.Images["ArrowRight"], new Vector2(100, 1000), Color.White);
            spriteBatch.Draw(ContentHandler.Images["ArrowRight2"], new Vector2(1300, 1000), Color.White);
            Customer.Draw(spriteBatch);
        }
    }
}
