using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SnowConeTycoon.Android.Ads;
using SnowConeTycoon.Shared;
using SnowConeTycoon.Shared.Utils;

namespace SnowConeTycoon.Android
{
    public class AndroidWrapperGame : Game
    {
        public SnowConeTycoonGame SnowConeGame = new SnowConeTycoonGame();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public AndroidWrapperGame(Activity1 activity)
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
            AdController.InitRegularAd();
            AdController.InitRewardAd();
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
                    SnowConeGame.AddIce(Defaults.REWARD_ICE_COUNT);
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
