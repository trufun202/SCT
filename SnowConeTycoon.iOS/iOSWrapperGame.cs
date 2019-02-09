using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Shared;
using Google.MobileAds;
using UIKit;

namespace SnowConeTycoon.iOS
{
    public class iOSWrapperGame : Game
    {
        AdMobService AdMobService;
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
            AdMobService = new AdMobService();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            SnowConeGame.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (SnowConeGame.CurrentScreen == Shared.Enums.Screen.RewardAd)
            {
                spriteBatch.Begin();

                if (RewardBasedVideoAd.SharedInstance.IsReady)
                {
                    RewardBasedVideoAd.SharedInstance.PresentFromRootViewController(Services.GetService(typeof(UIViewController)) as UIViewController);
                }

                spriteBatch.End();
            }
            else if (SnowConeGame.CurrentScreen == Shared.Enums.Screen.FullScreenAd)
            {
                spriteBatch.Begin();
                spriteBatch.End();
            }
            else
            {
                SnowConeGame.Draw(graphics, spriteBatch, gameTime);
            }
        }
    }
}
