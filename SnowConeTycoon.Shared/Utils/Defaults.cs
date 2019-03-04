using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnowConeTycoon.Shared.Utils
{
    public static class Defaults
    {
        public static bool DebugMode = false;
        public static int GraphicsWidth = 1536;
        public static int GraphicsHeight = 2732;
        public static SpriteFont Font;
        public const string ListenConnectionString = "Endpoint=sb://chrosgames.servicebus.windows.net/;SharedAccessKeyName=SnowConeTycoonDefaultPolicy;SharedAccessKey=QOOEZ1UbIsiR6kLlZYlN3DsRXdYMBAvvyHGEfDjK1xs=";
        public const string NotificationHubName = "SnowConeTycoon";
        public const string REWARD_ICE_COUNT = 5;

        public static Color SkyBlue
        {
            get
            {
                return Color.FromNonPremultiplied(170, 228, 250, 255);
            }
        }

        public static Color DarkBlue
        {
            get
            {
                return Color.FromNonPremultiplied(3, 38, 119, 255);
            }
        }

        public static Color Cream
        {
            get
            {
                return Color.FromNonPremultiplied(255, 243, 221, 255);
            }
        }

        public static Color Brown
        {
            get
            {
                return Color.FromNonPremultiplied(78, 38, 35, 255);
            }
        }
    }
}
