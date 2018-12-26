using System;
using Microsoft.Xna.Framework;

namespace SnowConeTycoon.Shared.Utils
{
    public static class Defaults
    {
        public static bool DebugMode = false;
        public static int GraphicsWidth = 1536;
        public static int GraphicsHeight = 2732;

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
    }
}
