using System;
using Google.MobileAds;
using SnowConeTycoon.Shared;
using SnowConeTycoon.Shared.Utils;
using UIKit;

public class AdMobInterstitialService : IInterstitialDelegate
{
    private SnowConeTycoonGame Game;
    private Interstitial interstitial;
    public bool AdLoaded = false;

    public AdMobInterstitialService(SnowConeTycoonGame SnowConeGame)
    {
        Game = SnowConeGame;
        LoadAd();
    }

    public void LoadAd()
    {
        interstitial = new Interstitial("ca-app-pub-7775864718817628/7572434112");
        interstitial.Delegate = this;
        interstitial.ScreenDismissed += Interstitial_ScreenDismissed;

        var request = Request.GetDefaultRequest();
        request.TestDevices = new[] { "2693502e8e082cb9db4c3b1124d2622026621f23" };
        interstitial.LoadRequest(request);
        AdLoaded = true;
    }

    void Interstitial_ScreenDismissed(object sender, EventArgs e)
    {
        AdLoaded = false;
        Game.InterstitialAdDone();
    }

    public IntPtr Handle => interstitial.Handle;

    public void Dispose()
    {
    }

    public void ShowAd()
    {
        if (interstitial.IsReady)
        {
            var viewController = GetVisibleViewController();
            interstitial.PresentFromRootViewController(viewController);
        }
    }

    UIViewController GetVisibleViewController()
    {
        var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

        if (rootController.PresentedViewController == null)
            return rootController;

        if (rootController.PresentedViewController is UINavigationController)
        {
            return ((UINavigationController)rootController.PresentedViewController).VisibleViewController;
        }

        if (rootController.PresentedViewController is UITabBarController)
        {
            return ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
        }

        return rootController.PresentedViewController;
    }
}