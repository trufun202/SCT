using System;
using Foundation;
using SnowConeTycoon.Shared;
using UIKit;

namespace SnowConeTycoon.iOS.iOS
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static iOSWrapperGame game;

        internal static void RunGame()
        {
            game = new iOSWrapperGame();
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}
