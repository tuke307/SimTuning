// project=SimTuning.Forms.Droid, file=MainActivity.cs, creation=2020:7:1
// Copyright (c) 2020 tuke productions. All rights reserved.
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using MediaManager;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using SimTuning.Forms.UI;

namespace SimTuning.Forms.Droid
{
    [Activity(Label = "SimTuning",
            Theme = "@style/MainTheme",
            Icon = "@mipmap/logo",
            MainLauncher = true,
            ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
            LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : MvxFormsAppCompatActivity/*<MvxFormsAndroidSetup<App, FormsApp>, App, FormsApp>*/
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            //zusätzliche Controls
            XF.Material.Droid.Material.Init(this, bundle);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            CrossMediaManager.Current.Init(this);
        }
    }
}