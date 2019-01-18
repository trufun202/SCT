using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Shared.Screens
{
    public class SupplyShopScreen
    {
        Vector2 PositionBannerStart = Vector2.Zero;
        Vector2 PositionBannerEnd = Vector2.Zero;
        Vector2 PositionBanner = Vector2.Zero;
        Vector2 PositionPaperStart = Vector2.Zero;
        Vector2 PositionPaperEnd = Vector2.Zero;
        Vector2 PositionPaper = Vector2.Zero;
        Vector2 PositionInvStart = Vector2.Zero;
        Vector2 PositionInvEnd = Vector2.Zero;
        Vector2 PositionInv = Vector2.Zero;
        bool AnimatingBanner = false;
        bool AnimatingPaper = true;
        bool ShowingInventory = false;
        int TimeBanner = 0;
        int TimeBannerTotal = 500;
        int TimePaper = 0;
        int TimePaperTotal = 500;
        int TimeInv = 0;
        int TimeInvTotal = 1000;
        double ScaleX;
        double ScaleY;
        Form form;
        ScaledImage CheckoutButton;
        public bool DoneAnimating = false;

        public SupplyShopScreen(double scaleX, double scaleY)
        {
            ScaleX = scaleX;
            ScaleY = scaleY;
            CheckoutButton = new ScaledImage("SupplyShop_Checkout", new Vector2(1200, 2500), 500);
            Reset();
        }

        public void Reset()
        {
            DoneAnimating = false;
            form = new Form(0, 0);
            form.Spacing = 10;
            form.Controls.Add(new NumberPicker("DaySetup_IconPrice", "cones", new Vector2(250, 450), 1, 8, ScaleX, ScaleY, false));
            form.Controls.Add(new NumberPicker("DaySetup_IconFlavor", "syrup", new Vector2(250, 725), 1, 8, ScaleX, ScaleY, false));
            form.Controls.Add(new NumberPicker("DaySetup_IconFlyer", "flyers", new Vector2(250, 1000), 1, 8, ScaleX, ScaleY, false));
            form.Controls.Add(new Label("------------------------------------", new Vector2(250, 1175), Defaults.Brown));
            AnimatingBanner = false;
            AnimatingPaper = true;
            ShowingInventory = false;
            TimeBanner = 0;
            TimePaper = 0;
            TimeInv = 0;
            PositionBannerStart = new Vector2(85, -ContentHandler.Images["SupplyShop_Banner"].Height);
            PositionBannerEnd = new Vector2(85, 60);
            PositionBanner = PositionBannerStart;
            PositionPaperStart = new Vector2(0, -ContentHandler.Images["SupplyShop_Paper"].Height);
            PositionPaper = PositionPaperStart;
            PositionInvStart = new Vector2(0, Defaults.GraphicsHeight - (ContentHandler.Images["DaySetup_Inventory"].Height * 2) - 100);
            PositionInv = PositionInvStart;
            PositionInvEnd = new Vector2(0, Defaults.GraphicsHeight - ContentHandler.Images["DaySetup_Inventory"].Height - 200);
            CheckoutButton.Reset();
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            form.HandleInput(previousTouchCollection, currentTouchCollection);
        }

        public void Update(GameTime gameTime)
        {
            form.Update(gameTime);

            if (AnimatingPaper)
            {
                TimePaper += gameTime.ElapsedGameTime.Milliseconds;
                var amt = TimePaper / (float)TimePaperTotal;

                PositionPaper = Vector2.SmoothStep(PositionPaperStart, PositionPaperEnd, amt);

                if (TimePaper >= TimePaperTotal)
                {
                    AnimatingBanner = true;
                    AnimatingPaper = false;
                    form.Reveil();
                }
            }
            else if (AnimatingBanner)
            {
                TimeBanner += gameTime.ElapsedGameTime.Milliseconds;
                var amt = TimeBanner / (float)TimeBannerTotal;

                PositionBanner = Vector2.SmoothStep(PositionBannerStart, PositionBannerEnd, amt);

                if (TimeBanner >= TimeBannerTotal)
                {
                    AnimatingBanner = false;
                }
            }
            else if (!ShowingInventory && form.IsVisible())
            {
                TimeInv += gameTime.ElapsedGameTime.Milliseconds;
                var amt = TimeInv / (float)TimeInvTotal;

                PositionInv = Vector2.SmoothStep(PositionInvStart, PositionInvEnd, amt);

                if (TimeInv >= TimeInvTotal)
                {
                    ShowingInventory = true;
                }
            }
            else if (ShowingInventory)
            {
                if (!CheckoutButton.IsDoneAnimating())
                {
                    CheckoutButton.Update(gameTime);
                }
                else if (CheckoutButton.IsDoneAnimating())
                {
                    DoneAnimating = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!AnimatingBanner && !AnimatingPaper)
            {
                spriteBatch.Draw(ContentHandler.Images["DaySetup_Inventory"], PositionInv, Color.White);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCoins"], new Vector2(PositionInv.X + 280, PositionInv.Y + 200), Color.White);
                spriteBatch.DrawString(Defaults.Font, "coins", new Vector2(PositionInv.X + 500, PositionInv.Y + 200), Defaults.Cream);
                spriteBatch.DrawString(Defaults.Font, "345", new Vector2(PositionInv.X + 1100, PositionInv.Y + 200), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString("345").X, 0), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_WatchAd"], new Vector2(PositionInv.X + 1135, PositionInv.Y + 215), Color.White);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCones"], new Vector2(PositionInv.X + 325, PositionInv.Y + 415), Color.White);
                spriteBatch.DrawString(Defaults.Font, "cones", new Vector2(PositionInv.X + 500, PositionInv.Y + 400), Defaults.Cream);
                spriteBatch.DrawString(Defaults.Font, "23", new Vector2(PositionInv.X + 1100, PositionInv.Y + 400), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString("23").X, 0), 1f, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvIce"], new Vector2(PositionInv.X + 300, PositionInv.Y + 600), Color.White);
                spriteBatch.DrawString(Defaults.Font, "ice", new Vector2(PositionInv.X + 500, PositionInv.Y + 600), Defaults.Cream);
                spriteBatch.DrawString(Defaults.Font, "35", new Vector2(PositionInv.X + 1100, PositionInv.Y + 600), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString("35").X, 0), 1f, SpriteEffects.None, 1f);
                spriteBatch.Draw(ContentHandler.Images["DaySetup_Plus"], new Vector2(PositionInv.X + 1150, PositionInv.Y + 600), Color.White);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvSyrup"], new Vector2(PositionInv.X + 300, PositionInv.Y + 800), Color.White);
                spriteBatch.DrawString(Defaults.Font, "syrup", new Vector2(PositionInv.X + 500, PositionInv.Y + 800), Defaults.Cream);
                spriteBatch.DrawString(Defaults.Font, "12", new Vector2(PositionInv.X + 1100, PositionInv.Y + 800), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString("12").X, 0), 1f, SpriteEffects.None, 1f);
            }

            spriteBatch.Draw(ContentHandler.Images["SupplyShop_Paper"], PositionPaper, Color.White);
            spriteBatch.Draw(ContentHandler.Images["SupplyShop_Banner"], PositionBanner, Color.White);
            form.Draw(spriteBatch);

            if (ShowingInventory)
            {
                CheckoutButton.Draw(spriteBatch);
            }
        }
    }
}
