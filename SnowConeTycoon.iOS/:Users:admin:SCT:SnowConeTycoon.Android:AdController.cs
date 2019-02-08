using Android.Gms.Ads;
using Android.Gms.Ads.Reward;

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

namespace AdController
{
  public static class AdController
  {
    public static InterstitialAd interstitialHandle = null;
    public static IRewardedVideoAd rewardHandle = null;


    // APP ID This is a test use yours from Admob
    private static string appID = "ca-app-pub-3940256099942544~3347511713";
    // AD ID's This is a test use yours from Admob
    public static string adID1 = "ca-app-pub-3940256099942544/1033173712"; // standard full page ad
    public static string adID2 = "ca-app-pub-3940256099942544/5224354917"; // reward full page ad

    public static bool adRegularLoaded = false;
    public static bool adRewardLoaded = false;
    public static bool adRewarded = false;
    public static bool youMadeMoney = false;

    /*********************************************************************************************
     * Function Name : InitAdRegularAd
     * Description : Initialize a regular full page ad
     * if you need to change the adID, just set
     * interstitialRegular.AdUnitId to point to another adIDx
     * ******************************************************************************************/
    public static void InitRegularAd()
    {
      MobileAds.Initialize(Game1.Activity,appID);                                               // initialize the ads        
      interstitialHandle = new InterstitialAd((Activity1)Game1.Activity);
      interstitialHandle.AdUnitId = adID1;                                                     // adID string, can repoint this on the fly
      interstitialHandle.AdListener = null;
      ListeningRegular listening = new ListeningRegular();
      interstitialHandle.AdListener = listening;
      interstitialHandle.LoadAd(new AdRequest.Builder().Build());
    }
    /*********************************************************************************************
     * Function Name : ShowRegularAd
     * Description : display the ad
     * ******************************************************************************************/
    public static void ShowRegularAd()
    {
      if(adRegularLoaded)
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
      rewardHandle = MobileAds.GetRewardedVideoAdInstance(Game1.Activity);                        // initialize the handle      
      rewardHandle.UserId = appID;                                                                // set the App ID      
      rewardHandle.RewardedVideoAdListener = listening;                                           // point to the rewards Listen class      
      rewardHandle.LoadAd(adID2,new AdRequest.Builder().Build());                                 // load the first one      
    }
    /*********************************************************************************************
     * Function Name : ShowRewardAd
     * Description : display the ad
     * ******************************************************************************************/
    public static void ShowRewardAd()
    {
      if(adRewardLoaded)                                                                          // we ready?        
      {
        adRewardLoaded = false;                                                                   // reset triggers        
        adRewarded = false;
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
      AdController.adRegularLoaded = true;
      base.OnAdLoaded();
    }
    public override void OnAdClosed()
    {
      // load the next ad ready to display
      AdController.interstitialHandle.LoadAd(new AdRequest.Builder().Build());
      base.OnAdClosed();
    }
    public override void OnAdOpened()
    {
      base.OnAdOpened();
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
      // load the next ad ready to display
      AdController.rewardHandle.LoadAd(AdController.adID2,new AdRequest.Builder().Build());
    }
    public void OnRewardedVideoAdFailedToLoad(int errorCode)
    {
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