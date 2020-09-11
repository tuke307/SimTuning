// project=SimTuning.Droid, file=SplashScreen.cs, creation=2020:7:1 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Droid
{
    using Android.App;
    using Android.Content.PM;
    using MvvmCross.Platforms.Android.Views;

    /// <summary>
    /// SplashScreen.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Android.Views.MvxSplashScreenActivity" />
    [Activity(
         MainLauncher = true,
         Icon = "@mipmap/logo",
         Theme = "@style/Theme.Splash",
         NoHistory = true,
         ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SplashScreen" /> class.
        /// </summary>
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}