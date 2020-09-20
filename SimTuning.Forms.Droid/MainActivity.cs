// project=SimTuning.Forms.Droid, file=MainActivity.cs, creation=2020:7:1 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using MediaManager;
    using MvvmCross.Forms.Platforms.Android.Core;
    using MvvmCross.Forms.Platforms.Android.Views;
    using SimTuning.Forms.UI;

    /// <summary>
    /// MainActivity.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Platforms.Android.Views.MvxFormsAppCompatActivity" />
    [Activity(
        Label = "SimTuning",
        Theme = "@style/AppTheme",
        Icon = "@mipmap/logo",
        //MainLauncher = true, for splashscreen
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : MvxFormsAppCompatActivity

    // No Splash Screen with this; MvxFormsAppCompatActivity<MvxFormsAndroidSetup<App,
    // FormsApp>, App, FormsApp>
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // Permission
            Xamarin.Essentials.Platform.Init(this, bundle);

            // zusätzliche Controls
            XF.Material.Droid.Material.Init(this, bundle);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            CrossMediaManager.Current.Init(this);
        }
    }
}