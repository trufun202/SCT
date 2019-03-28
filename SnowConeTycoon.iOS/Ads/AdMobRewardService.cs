using System;
using Google.MobileAds;
using SnowConeTycoon.Shared;
using SnowConeTycoon.Shared.Utils;

public class AdMobRewardService : RewardBasedVideoAdDelegate
{
    private readonly SnowConeTycoonGame Game;
    public bool AdReady = false;

    public AdMobRewardService(SnowConeTycoonGame SnowConeGame)
    {
        Game = SnowConeGame;
        RewardBasedVideoAd.SharedInstance.Delegate = this;
    }

    public void Reset()
    {
        var request = Request.GetDefaultRequest();
        request.TestDevices = new[] { "2693502e8e082cb9db4c3b1124d2622026621f23" };
        RewardBasedVideoAd.SharedInstance.LoadRequest(request, "ca-app-pub-7775864718817628/9107262668");
    }

    public override void DidRewardUser(RewardBasedVideoAd rewardBasedVideoAd, AdReward reward)
    {
        if (AdReady)
        {
            AdReady = false;
            Game.AddReward(Defaults.REWARD_ICE_COUNT, Defaults.REWARD_COIN_COUNT);
        }
        Console.Write("ice rewarded");
    }

    public override void DidReceiveAd(RewardBasedVideoAd rewardBasedVideoAd)
    {
        AdReady = true;
        Console.WriteLine("Reward based video ad is received.");
    }

    public override void DidOpen(RewardBasedVideoAd rewardBasedVideoAd)
    {
        Console.WriteLine("Opened reward based video ad.");
    }

    public override void DidStartPlaying(RewardBasedVideoAd rewardBasedVideoAd)
    {
        Console.WriteLine("Reward based video ad started playing.");
    }

    public override void DidClose(RewardBasedVideoAd rewardBasedVideoAd)
    {
        AdReady = false;
        Game.CancelReward();
        Console.WriteLine("Reward based video ad is closed.");
    }

    public override void WillLeaveApplication(RewardBasedVideoAd rewardBasedVideoAd)
    {
        Console.WriteLine("Reward based video ad will leave application.");
    }
}