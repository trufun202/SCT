using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using SnowConeTycoon.Shared.Animations;
using SnowConeTycoon.Shared.Enums;
using SnowConeTycoon.Shared.Forms;
using SnowConeTycoon.Shared.Handlers;
using SnowConeTycoon.Shared.Models;
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
        ScaledImage BackButton;
        public bool DoneAnimating = false;
        int CheckoutTotal = 0;

        public SupplyShopScreen(double scaleX, double scaleY)
        {
            ScaleX = scaleX;
            ScaleY = scaleY;
            CheckoutButton = new ScaledImage("SupplyShop_Checkout", new Vector2(1200, 2500), 500);
            BackButton = new ScaledImage("DaySetup_Back", new Vector2(350, 2470), 500);
            Reset();
        }

        public bool IsReady()
        {
            return DoneAnimating;
        }

        public void Reset()
        {
            DoneAnimating = false;
            form = new Form(0, 0);
            form.Spacing = 10;
            form.Controls.Add(new NumberPicker("DaySetup_IconCone", "cones", new Vector2(250, 450), 0, 8, ScaleX, ScaleY, false));
            form.Controls.Add(new NumberPicker("DaySetup_IconFlavor", "syrup", new Vector2(250, 725), 0, 8, ScaleX, ScaleY, false));
            form.Controls.Add(new NumberPicker("DaySetup_IconFlyer", "flyers", new Vector2(250, 1000), 0, 8, ScaleX, ScaleY, false));
            form.Controls.Add(new Label("------------------------------------", new Vector2(250, 1175), Defaults.Brown));
            form.Controls.Add(new Label("total", new Vector2(450, 1290), Defaults.Brown));
            form.Controls.Add(new LabelWithImage(CheckoutTotal.ToString(), new Vector2(1120, 1350), Defaults.Brown, "DaySetup_IconPrice", Align.Right, 50, -120));
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
            BackButton.Reset();
        }

        public void HandleInput(TouchCollection previousTouchCollection, TouchCollection currentTouchCollection)
        {
            form.HandleInput(previousTouchCollection, currentTouchCollection);
            UpdateCheckoutTotal();
        }

        private void UpdateCheckoutTotal()
        {
            var total = 0;

            foreach (var control in form.Controls)
            {
                if (control is NumberPicker)
                {
                    total += ((NumberPicker)control).Value;
                }
            }

            if (total != CheckoutTotal)
            {
                CheckoutTotal = total;

                var totalLabel = form.Controls.Where(c => c is LabelWithImage).ToList()[0] as LabelWithImage;

                totalLabel.SetText(CheckoutTotal.ToString(), true);
            }
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
                    BackButton.Update(gameTime);
                }
                else if (CheckoutButton.IsDoneAnimating())
                {
                    DoneAnimating = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ContentHandler.Images["SupplyShop_Background"], Vector2.Zero, Color.White);

            if (!AnimatingBanner && !AnimatingPaper)
            {
                spriteBatch.Draw(ContentHandler.Images["DaySetup_Inventory"], PositionInv, Color.White);

                spriteBatch.DrawString(Defaults.Font, "my supplies", new Vector2(PositionInv.X + 500, PositionInv.Y + 100), Defaults.Brown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

                var fontScale = 0.70f;

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCoins"], new Vector2(PositionInv.X + 290, PositionInv.Y + 250), Color.White);
                spriteBatch.DrawString(Defaults.Font, "coins", new Vector2(PositionInv.X + 500, PositionInv.Y + 250), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.CoinCount.ToString(), new Vector2(PositionInv.X + 1250, PositionInv.Y + 250), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.CoinCount.ToString()).X, 0), fontScale, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvCones"], new Vector2(PositionInv.X + 335, PositionInv.Y + 400), Color.White);
                spriteBatch.DrawString(Defaults.Font, "cones", new Vector2(PositionInv.X + 500, PositionInv.Y + 400), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.ConeCount.ToString(), new Vector2(PositionInv.X + 1250, PositionInv.Y + 400), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.ConeCount.ToString()).X, 0), fontScale, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvSyrup"], new Vector2(PositionInv.X + 320, PositionInv.Y + 530), Color.White);
                spriteBatch.DrawString(Defaults.Font, "syrup", new Vector2(PositionInv.X + 500, PositionInv.Y + 550), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.SyrupCount.ToString(), new Vector2(PositionInv.X + 1250, PositionInv.Y + 550), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.SyrupCount.ToString()).X, 0), fontScale, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvFlyers"], new Vector2(PositionInv.X + 320, PositionInv.Y + 700), Color.White);
                spriteBatch.DrawString(Defaults.Font, "flyers", new Vector2(PositionInv.X + 500, PositionInv.Y + 700), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.FlyerCount.ToString(), new Vector2(PositionInv.X + 1250, PositionInv.Y + 700), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.FlyerCount.ToString()).X, 0), fontScale, SpriteEffects.None, 1f);

                spriteBatch.Draw(ContentHandler.Images["DaySetup_InvIce"], new Vector2(PositionInv.X + 300, PositionInv.Y + 850), Color.White);
                spriteBatch.DrawString(Defaults.Font, "ice", new Vector2(PositionInv.X + 500, PositionInv.Y + 850), Defaults.Cream, 0f, Vector2.Zero, fontScale, SpriteEffects.None, 1f);
                spriteBatch.DrawString(Defaults.Font, Player.IceCount.ToString(), new Vector2(PositionInv.X + 1250, PositionInv.Y + 850), Defaults.Cream, 0f, new Vector2(Defaults.Font.MeasureString(Player.IceCount.ToString()).X, 0), fontScale, SpriteEffects.None, 1f);
            }

            spriteBatch.Draw(ContentHandler.Images["SupplyShop_Paper"], PositionPaper, Color.White);
            spriteBatch.Draw(ContentHandler.Images["SupplyShop_Banner"], PositionBanner, Color.White);
            form.Draw(spriteBatch);

            if (ShowingInventory)
            {
                CheckoutButton.Draw(spriteBatch);
                BackButton.Draw(spriteBatch);
            }
        }
    }
}
