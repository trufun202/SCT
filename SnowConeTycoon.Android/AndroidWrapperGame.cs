using System;
using Android.App;
using Android.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Android.Ads;
using SnowConeTycoon.Shared;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Android
{
    public class AndroidWrapperGame : Game
    {
        public static SnowConeTycoonGame SnowConeGame = new SnowConeTycoonGame();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        AlertDialog.Builder dialog;
        public static bool RewardAdLoaded
        {
            get
            {
                return SnowConeGame.RewardAdLoaded;
            }
            set
            {
                SnowConeGame.RewardAdLoaded = value;
            }
        }
        public static bool InterstitialAdLoaded
        {
            get
            {
                return SnowConeGame.InterstitialAdLoaded;
            }
            set
            {
                SnowConeGame.InterstitialAdLoaded = value;
            }
        }

        public AndroidWrapperGame(Activity1 activity)
        {
            dialog = new AlertDialog.Builder(activity);
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            SnowConeGame.Initialize(graphics);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            SnowConeGame.LoadContent(Content);
            AdController.InitRewardAd();
            AdController.InitRegularAd();
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            SnowConeGame.OnDeactivated();
            base.OnDeactivated(sender, args);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SnowConeGame.Update(gameTime);

            if (SnowConeGame.ShowRateGame)
            {
                SnowConeGame.ShowRateGame = false;

                // Request a review from the user
                ShowRateDialog();
            }
        }

        public void ShowRateDialog()
        {
            dialog.SetTitle("Enjoying Snow Cone Tycoon?  Please take a moment to rate it.");
            dialog.SetPositiveButton("Rate It", (senderAlert, args) => {
                SnowConeGame.SetPlayerRatedApp();
                WebBrowser.OpenPage("https://play.google.com/store/apps/details?id=12345");
            });

            dialog.SetNegativeButton("Not Now", (senderAlert, args) => {
            });

            dialog.Show();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (SnowConeGame.CurrentScreen == Shared.Enums.Screen.RewardAd)
            {
                spriteBatch.Begin();
                if (AdController.adRewardLoaded)
                {
                    AdController.ShowRewardAd();
                }

                if (AdController.adRewarded)
                {
                    SnowConeGame.CurrentScreen = Shared.Enums.Screen.DaySetup;
                    SnowConeGame.AddReward(Defaults.REWARD_ICE_COUNT, Defaults.REWARD_COIN_COUNT);
                }
                else if (AdController.adRewardCancelled)
                {
                    SnowConeGame.CurrentScreen = Shared.Enums.Screen.DaySetup;
                }

                spriteBatch.End();
            }
            else if (SnowConeGame.CurrentScreen == Shared.Enums.Screen.FullScreenAd)
            {
                spriteBatch.Begin();
                if (AdController.adRegularLoaded)
                {
                    AdController.ShowRegularAd();
                }

                if (AdController.adClosed)
                {
                    SnowConeGame.CurrentScreen = Shared.Enums.Screen.DaySetup;
                }

                spriteBatch.End();
            }
            else
            {
                SnowConeGame.Draw(graphics, spriteBatch, gameTime);
            }
        }
    }
}
