using System;
using System.Collections.Generic;
using System.Text;

namespace SnowConeTycoon.Shared.Enums
{
    public enum Screen
    {
        Loading,
        Logo,
        Title,
        CharacterSelect,
        DaySetup,
        SupplyShop,
        OpenForBusiness,
        Results,
        RewardAd,
        FullScreenAd,
        Credits
    }

    public enum Degrees
    {
        Fahrenheit,
        Celsius
    }

    public enum TutorialType
    {
        DaySetup,
        SupplyShop,
        OpenForBusiness,
        Results
    }

    public enum SupplyShopResult
    {
        Success,
        NotEnoughCoins
    }

    public enum OverallDayOpinion
    {
        Perfect,
        JustOkay,
        TooSweet,
        TooPlain,
        WeatherRain,
        WeatherCold
    }

    public enum KidState
    {
        Happy,
        Mad,
        Sad
    }

    public enum Forecast
    {
        Sunny,
        Cloudy,
        PartlyCloudy,
        Rain,
        Snow
    }

    public enum NPS
    {
        Detractor,
        Passive,
        Promoter
    }

    public enum GameSpeed
    {
        x1,
        x2
    }

    public enum Align
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public enum ParticleMovementPath
    {
        None,
        Circle
    }

    public enum UnlockMechanism
    {
        None,
        Purchase,
        Sales
    }

    public enum Rank
    {
        Lousy,
        Dabbling,
        Aspiring,
        Novice,
        Experienced,
        Skilled,
        Excellent,
        Professional,
        Veteran,
        Tycoon
    }
}
