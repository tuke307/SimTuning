using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using SimTuning.Forms.UI;

namespace SimTuning.Forms.Droid
{
    [Activity(Label = "SimTuning",
            Theme = "@style/MyTheme",
            Icon = "@mipmap/logo",
            MainLauncher = true,
            ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
            LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<App, FormsApp>, App, FormsApp>
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            //zusätzliche Controls
            XF.Material.Droid.Material.Init(this, bundle);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            //SetContentView(Resource.Layout.TipView);
        }
    }
}