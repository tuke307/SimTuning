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
        Theme = "@style/MainTheme",
        Icon = "@mipmap/logo",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<App, FormsApp>, App, FormsApp>
    {
        /// <summary>
        /// To be added.
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="permissions"></param>
        /// <param name="grantResults"></param>
        /// <remarks>
        /// Portions of this page are modifications based on work created and shared by
        /// the <format type="text/html"><a
        /// href="https://developers.google.com/terms/site-policies" title="Android Open
        /// Source Project">Android Open Source Project</a></format> and used according to
        /// terms described in the  <format type="text/html"><a
        /// href="https://creativecommons.org/licenses/by/2.5/" title="Creative Commons
        /// 2.5 Attribution License">Creative Commons 2.5 Attribution
        /// License.</a></format>
        /// </remarks>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // zusätzliche Controls
            XF.Material.Droid.Material.Init(this, bundle);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            CrossMediaManager.Current.Init(this);
        }
    }
}