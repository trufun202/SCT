using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace SnowConeTycoon.Android
{
    public class WebBrowser
    {
        public static void OpenPage(string url)
        { 
            var uri = global::Android.Net.Uri.Parse(url);
            var intent = new Intent(Intent.ActionView, uri);
            Application.Context.StartActivity(intent);
        }
    }
}