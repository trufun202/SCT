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

            Form.Controls.Add(new Button(new Rectangle(120, 1000, 136, 136),
                () =>
                {
                    return Customer.SetSpeed1x();
                },
                "", //TODO add slowdown sound
                scaleX,
                scaleY));
            Form.Controls.Add(new Button(new Rectangle(1320, 1000, 136, 136),
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

            spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCones"], new Vector2(210, 1820), Color.White);
            spriteBatch.Draw(ContentHandler.Images["DaySetup_InvIce"], new Vector2(185, 2020), Color.White);
            spriteBatch.Draw(ContentHandler.Images["DaySetup_InvSyrup"], new Vector2(200, 2220), Color.White);

            spriteBatch.DrawString(Defaults.Font, "24", new Vector2(400, 1790), Color.White);
            spriteBatch.DrawString(Defaults.Font, "4", new Vector2(400, 1990), Color.White);
            spriteBatch.DrawString(Defaults.Font, "16", new Vector2(400, 2210), Color.White);

            spriteBatch.Draw(ContentHandler.Images["DaySetup_IconPrice"], new Vector2(40, -15), Color.White);

            spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(218, 33), Color.Black);
            spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(218, 37), Color.Black);
            spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(222, 33), Color.Black);
            spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(222, 37), Color.Black);
            spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(220, 35), Color.White);

            spriteBatch.DrawString(Defaults.Font, "1x", new Vector2(100, 850), Color.White);
            spriteBatch.DrawString(Defaults.Font, "10x", new Vector2(1275, 850), Color.White);

            spriteBatch.Draw(ContentHandler.Images["ArrowRight"], new Vector2(120, 1000), Color.White);
            spriteBatch.Draw(ContentHandler.Images["ArrowRight2"], new Vector2(1320, 1000), Color.White);
            Customer.Draw(spriteBatch);
        }
    }
}
