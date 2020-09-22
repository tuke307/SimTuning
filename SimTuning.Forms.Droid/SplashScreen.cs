// project=SimTuning.Droid, file=SplashScreen.cs, creation=2020:7:1 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using MvvmCross.Forms.Platforms.Android.Views;
    using MvvmCross.Platforms.Android.Views;
    using SimTuning.Forms.Droid;
    using SimTuning.Forms.UI;
    using System.Threading.Tasks;

    /// <summary>
    /// SplashScreen.
    /// </summary>
    /// <seealso cref="MvvmCross.Platforms.Android.Views.MvxSplashScreenActivity" />
    [Activity(
         Label = "SimTuning",
         MainLauncher = true,
         NoHistory = true,
         Icon = "@mipmap/logo",
         Theme = "@style/AppTheme.Splash",
         ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxFormsSplashScreenActivity<Setup, App, FormsApp>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SplashScreen" /> class.
        /// </summary>
        public SplashScreen()
        //: base(Resource.Layout.SplashScreen)
        {
        }

        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(MainActivity));
            return Task.CompletedTask;
        }
    }
}