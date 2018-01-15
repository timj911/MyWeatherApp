using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyWeatherApp.Droid
{
    [Activity(Label = "SplashScreen", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, Icon = "@mipmap/icon")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
  
            Thread.Sleep(3000);
            
            StartActivity(typeof(MainActivity));
        }
    }
}