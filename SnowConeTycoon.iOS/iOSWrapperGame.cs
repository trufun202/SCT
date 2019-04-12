using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared;
using Google.MobileAds;
using UIKit;
using StoreKit;

namespace SnowConeTycoon.iOS
{
    public class iOSWrapperGame : Game
    {
        AdMobRewardService AdMobRewardService;
        AdMobInterstitialService AdMobInterstitialService;
        public SnowConeTycoonGame SnowConeGame = new SnowConeTycoonGame();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public iOSWrapperGame()
        {
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
            AdMobRewardService = new AdMobRewardService(SnowConeGame);
            AdMobInterstitialService = new AdMobInterstitialService(SnowConeGame);
            AdMobRewardService.Reset();
            AdMobInterstitialService.LoadAd();
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
                SKStoreReviewController.RequestReview();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (SnowConeGame.CurrentScreen == Shared.Enums.Screen.RewardAd)
            {
                spriteBatch.Begin();

                if (!AdMobRewardService.AdReady)
                {
                    AdMobRewardService.Reset();
                }

                if (RewardBasedVideoAd.SharedInstance.IsReady)
                {
                    RewardBasedVideoAd.SharedInstance.PresentFromRootViewController(Services.GetService(typeof(UIViewController)) as UIViewController);
                }

                spriteBatch.End();
            }
            else if (SnowConeGame.CurrentScreen == Shared.Enums.Screen.FullScreenAd)
            {
                spriteBatch.Begin();

                if (!AdMobInterstitialService.AdLoaded)
                {
                    AdMobInterstitialService.LoadAd();
                }

                AdMobInterstitialService.ShowAd();

                spriteBatch.End();
            }
            else
            {
                SnowConeGame.Draw(graphics, spriteBatch, gameTime);
            }
        }
    }
}
