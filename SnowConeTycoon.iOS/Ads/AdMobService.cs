﻿using System;
using Google.MobileAds;
using SnowConeTycoon.Shared;

public class AdMobService : RewardBasedVideoAdDelegate
{
    private SnowConeTycoonGame Game;
    private const int ICE_REWARD_COUNT = 5;

    public AdMobService(SnowConeTycoonGame SnowConeGame)
    {
        Game = SnowConeGame;
        RewardBasedVideoAd.SharedInstance.Delegate = this;

        var request = Request.GetDefaultRequest();
        request.TestDevices = new[] { "2693502e8e082cb9db4c3b1124d2622026621f23" };
        RewardBasedVideoAd.SharedInstance.LoadRequest(request, "ca-app-pub-7775864718817628/9107262668");
    }

    public override void DidRewardUser(RewardBasedVideoAd rewardBasedVideoAd, AdReward reward)
    {
        Game.AddIce(ICE_REWARD_COUNT);
        Console.Write("ice rewarded");
    }

    public override void DidReceiveAd(RewardBasedVideoAd rewardBasedVideoAd)
    {
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
        Console.WriteLine("Reward based video ad is closed.");
    }

    public override void WillLeaveApplication(RewardBasedVideoAd rewardBasedVideoAd)
    {
        Console.WriteLine("Reward based video ad will leave application.");
    }
}