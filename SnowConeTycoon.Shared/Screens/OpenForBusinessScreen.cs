using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Kids;

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

            Form.Controls.Add(new Button(new Rectangle(100, 600, 136, 136),
                () =>
                {
                    //TODO Set speed to 1x
                    return true;
                },
                "", //TODO add slowdown sound
                scaleX,
                scaleY));
            Form.Controls.Add(new Button(new Rectangle(1300, 600, 136, 136),
                () =>
                {
                    //TODO Set speed to 2x
                    return true;
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
            spriteBatch.Draw(ContentHandler.Images["ArrowRight"], new Vector2(100, 600), Color.White);
            spriteBatch.Draw(ContentHandler.Images["ArrowRight2"], new Vector2(1300, 600), Color.White);
            Customer.Draw(spriteBatch);
        }
    }
}
