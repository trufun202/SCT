using CoreGraphics;
using Google.MobileAds;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UIKit;

namespace SnowConeTycoon.iOS.Ads
{
    public class AdMobHelper
    {
        private readonly UIViewController ViewController;
        private readonly string AdUnitID;
        private readonly CGPoint Location;
        private BannerView adView;
        private bool isAddOnScreen = false;
        public bool AdsAreBeingReceived { get; private set; }

        public AdMobHelper(GameServiceContainer services, string adUnitID, Vector2 locationOnScreen)
        {
            ViewController = services.GetService(typeof(UIViewController)) as UIViewController;
            AdUnitID = adUnitID;
            Location = new CGPoint(locationOnScreen.X, locationOnScreen.Y);
        }

        /// <summary>
        /// Render the add onto the device's screen.
        /// </summary>
        public void AddBanner()
        {
            // Setup your BannerView, review AdSizeCons class for more Ad sizes. 
            adView = new BannerView(size: AdSizeCons.Banner, origin: Location)
            {
                AdUnitID = AdUnitID,
                RootViewController = ViewController
            };

            // Wire AdReceived event to know when the Ad is ready to be displayed
            adView.AdReceived += (object sender, EventArgs e) =>
            {
                if (!isAddOnScreen)
                    ViewController.View.AddSubview(adView);
                isAddOnScreen = true;
                AdsAreBeingReceived = true;
            };

            adView.ReceiveAdFailed += (object sender, BannerViewErrorEventArgs e) =>
            {
                throw new Exception(e.Error.DebugDescription);
                //throw new Exception(e.Error.Description); Might be more helpful??
            };

            Request request = Request.GetDefaultRequest();
            request.TestDevices = new[] { "GAD_SIMULATOR_ID", "kGADSimulatorID" };

            adView.LoadRequest(request);
        }
    }
}