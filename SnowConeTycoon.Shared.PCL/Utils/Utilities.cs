using System;
using Microsoft.Xna.Framework;

namespace SnowConeTycoon.Shared
{
    public static class Utilities
    {
        public static Random rand = new Random(DateTime.Now.Millisecond);

        public static int GetRandomInt(int max)
        {
            return rand.Next(max);
        }

        public static int GetRandomInt(int min, int max)
        {
            return rand.Next(min, max);
        }

        public static Color GetRandomColor()
        {
            return Color.FromNonPremultiplied(GetRandomInt(256), GetRandomInt(256), GetRandomInt(256), 255);
        }
    }
}

