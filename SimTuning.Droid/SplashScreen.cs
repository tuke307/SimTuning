// project=SimTuning.Droid, file=SplashScreen.cs, creation=2020:7:1 Copyright (c) 2020
// tuke productions. All rights reserved.
using Android.App;
using Android.Content.PM;
using MvvmCross.Platforms.Android.Views;

namespace SimTuning.Droid
{
    [Activity(
         MainLauncher = true,
         Icon = "@mipmap/logo",
         Theme = "@style/Theme.Splash",
         NoHistory = true,
         ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}