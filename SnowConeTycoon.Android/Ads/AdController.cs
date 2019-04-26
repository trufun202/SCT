using Android.Gms.Ads;
using Android.Gms.Ads.Reward;
using SnowConeTycoon.Shared;

/// <summary>
/// For Ads to work 
/// NuGet Xamarin.GooglePlayServices.Ads.Lite
/// Only then can ads be added to the project
/// 
/// AndroidManifest.xml File
/// add these lines
//  <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
//    <uses-permission android:name="android.permission.INTERNET" />
//  <!-- Below MUST be put into the manifest to allow the ad to react to the user-->
//    <activity android:name="com.google.android.gms.ads.AdActivity"
//    android:configChanges="keyboard|keyboardHidden|orientation|screenLayout|uiMode|screenSize|smallestScreenSize" />
///
/// </summary>

namespace SnowConeTycoon.Android.Ads
{
    public static class AdController
    {
        public static AndroidWrapperGame Game;
        public static InterstitialAd interstitialHandle = null;
        public static IRewardedVideoAd rewardHandle = null;

        // APP ID This is a test use yours from Admob
        private static string appID = "ca-app-pub-7775864718817628~3850330467";
        // AD ID's This is a test use yours from Admob
        public static string fullScreenAdId = "ca-app-pub-7775864718817628/5244847777";
        public static string rewardAdId = "ca-app-pub-7775864718817628/9223034506"; // reward full page ad

        public static bool adClosed = false;
        public static bool adRegularLoaded = false;
        public static bool adRewardLoaded = false;
        public static bool adRewarded = false;
        public static bool adRewardCancelled = false;
        public static bool youMadeMoney = false;

        public static SnowConeTycoonGame SnowConeGame;

        /*********************************************************************************************
         * Function Name : InitAdRegularAd
         * Description : Initialize a regular full page ad
         * if you need to change the adID, just set
         * interstitialRegular.AdUnitId to point to another adIDx
         * ******************************************************************************************/
        public static void InitRegularAd()
        {
            MobileAds.Initialize(AndroidWrapperGame.Activity, appID);                                               // initialize the ads        
            interstitialHandle = new InterstitialAd((Activity1)AndroidWrapperGame.Activity);
            interstitialHandle.AdUnitId = fullScreenAdId;                                                     // adID string, can repoint this on the fly
            interstitialHandle.AdListener = null;
            ListeningRegular listening = new ListeningRegular();
            interstitialHandle.AdListener = listening;
            interstitialHandle.LoadAd(new AdRequest.Builder().AddTestDevice("11938541a98ab081").Build());
        }
        /*********************************************************************************************
         * Function Name : ShowRegularAd
         * Description : display the ad
         * ******************************************************************************************/
        public static void ShowRegularAd()
        {
            if (adRegularLoaded)
            {
                adRegularLoaded = false;
                adRewarded = false;
                interstitialHandle.Show();
            }
        }
        /*********************************************************************************************
         * Function Name : InitRewardAd
         * Description : Initialize a reward Ad
         * ******************************************************************************************/
        public static void InitRewardAd()
        {
            ListeningReward listening = new ListeningReward();                                          // create a pointer to our listen class      
            rewardHandle = MobileAds.GetRewardedVideoAdInstance(AndroidWrapperGame.Activity);                        // initialize the handle      
            rewardHandle.UserId = appID;                                                              // set the App ID      
            rewardHandle.RewardedVideoAdListener = listening;                                           // point to the rewards Listen class      
            rewardHandle.LoadAd(rewardAdId, new AdRequest.Builder().AddTestDevice(AdRequest.DeviceIdEmulator).AddTestDevice("11938541a98ab081").Build());                                 // load the first one      
        }
        /*********************************************************************************************
         * Function Name : ShowRewardAd
         * Description : display the ad
         * ******************************************************************************************/
        public static void ShowRewardAd()
        {
            if (adRewardLoaded)                                                                          // we ready?        
            {
                adRewardLoaded = false;                                                                   // reset triggers        
                adRewarded = false;
                adRewardCancelled = false;
                rewardHandle.Show();                                                                      // show the ad        
            }
        }
    }
    /*********************************************************************************************
      * Class Name : ListeningRegular
      * Description : Listening class for the regualr ad
      * ******************************************************************************************/
    internal class ListeningRegular : AdListener
    {
        public override void OnAdLoaded()
        {
            AndroidWrapperGame.InterstitialAdLoaded = true;
            AdController.adRegularLoaded = true;
            base.OnAdLoaded();
        }
        public override void OnAdClosed()
        {
            AdController.adClosed = true;
            // load the next ad ready to display
            AdController.interstitialHandle.LoadAd(new AdRequest.Builder().Build());
            base.OnAdClosed();
        }
        public override void OnAdOpened()
        {
            base.OnAdOpened();
        }

        public override void OnAdFailedToLoad(int errorCode)
        {
            base.OnAdFailedToLoad(errorCode);
        }
    }
    /*********************************************************************************************
      * Function Name : ListeningReward
      * Description : Listening class for the reward ad
      * ******************************************************************************************/
    internal class ListeningReward : AdListener, IRewardedVideoAdListener
    {
        public ListeningReward()
        {
            // constructor, up to you if you need to do anything in here
        }
        public void OnRewarded(IRewardItem reward)
        {
            // you can use the reward variable to handle your own rewards
            AdController.adRewarded = true;
        }
        public void OnRewardedVideoAdClosed()
        {
            AdController.adRewardCancelled = true;
            // load the next ad ready to display
            AdController.rewardHandle.LoadAd(AdController.rewardAdId, new AdRequest.Builder().Build());
        }
        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
                AdController.adRewardCancelled = true;
            // error message if you want will be ignored otherwise
        }
        public void OnRewardedVideoAdLeftApplication()
        {
            // ad was clicked on so you just made money
            AdController.youMadeMoney = true;
        }
        public void OnRewardedVideoAdLoaded()
        {
            // ad was loaded
            AdController.adRewardLoaded = true;
            AndroidWrapperGame.RewardAdLoaded = true;
        }
        public void OnRewardedVideoAdOpened()
        {
            // ad opening
        }
        public void OnRewardedVideoStarted()
        {
            //ad running
        }
    }
}