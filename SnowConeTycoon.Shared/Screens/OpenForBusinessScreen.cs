using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Kids;
using SnowConeTycoon.Shared.Models;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class OpenForBusinessScreen
    {
        double ScaleX;
        double ScaleY;
        Form Form;
        Customer Customer;
        BusinessDayResult Results = new BusinessDayResult();
        SnowConeTycoonGame Game;
        int CoinDisplay = 0;
        FadeOutImage FastForwardImage;

        public OpenForBusinessScreen(SnowConeTycoonGame game, double scaleX, double scaleY)
        {
            Game = game;
            Customer = new Customer(game, this);
            ScaleX = scaleX;
            ScaleY = scaleY;
            Form = new Form(0, 0);
            FastForwardImage = new FadeOutImage("FastForwardIcon", new Vector2(Defaults.GraphicsWidth / 2, Defaults.GraphicsHeight / 2), 2000);
        }

        public void AddCoinDisplay(int count)
        {
            CoinDisplay += count;
        }

        public void Reset(BusinessDayResult results)
        {
            Customer = new Customer(Game, this);
            Customer.Reset(results);
            Customer.ResetScene();
            Customer.SetSpeed1x();
            Player.GameSpeed = GameSpeed.x1;
            Results = results;
            CoinDisplay = Player.CoinCount;
            FastForwardImage.Reset();
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection, GameTime gameTime)
        {
            Form.HandleInput(previousTouchCollection, currentTouchCollection, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            Customer.Update(gameTime);

            if (Player.GameSpeed == GameSpeed.x2)
            {
                FastForwardImage.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["OpenForBusiness_Foreground"], new Vector2(0, 0), Color.White);

            var kidName = $"{KidHandler.CurrentKid.GetName()}'s Snow Cones";

            spriteBatch.DrawString(Defaults.Font, kidName, new Vector2(Defaults.GraphicsWidth / 2, 470), Defaults.Cream, -0.06f, Defaults.Font.MeasureString(kidName) / 2, 1f, SpriteEffects.None, 1f);

            spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCones"], new Vector2(210, 1820), Color.White);
            spriteBatch.Draw(ContentHandler.Images["DaySetup_InvSyrup"], new Vector2(200, 2000), Color.White);

            spriteBatch.DrawString(Defaults.Font, Player.ConeCount.ToString(), new Vector2(400, 1790), Defaults.Cream);
            spriteBatch.DrawString(Defaults.Font, Player.SyrupCount.ToString(), new Vector2(400, 1990), Defaults.Cream);

            spriteBatch.Draw(ContentHandler.Images["DaySetup_IconPrice"], new Vector2(40, -15), Color.White);

            spriteBatch.DrawString(Defaults.Font, CoinDisplay.ToString(), new Vector2(218, 33), Defaults.Brown);
            spriteBatch.DrawString(Defaults.Font, CoinDisplay.ToString(), new Vector2(218, 37), Defaults.Brown);
            spriteBatch.DrawString(Defaults.Font, CoinDisplay.ToString(), new Vector2(222, 33), Defaults.Brown);
            spriteBatch.DrawString(Defaults.Font, CoinDisplay.ToString(), new Vector2(222, 37), Defaults.Brown);
            spriteBatch.DrawString(Defaults.Font, CoinDisplay.ToString(), new Vector2(220, 35), Defaults.Cream);
            Customer.Draw(spriteBatch);

            if (Player.GameSpeed == GameSpeed.x2)
            {
                FastForwardImage.Draw(spriteBatch);
            }
        }
    }
}
