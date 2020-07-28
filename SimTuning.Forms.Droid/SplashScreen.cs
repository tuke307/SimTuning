using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Platforms.Android.Views;
using SimTuning.Forms.UI;

namespace SimTuning.Forms.Droid
{
    [Activity(
         MainLauncher = true,
         Icon = "@mipmap/logo",
         Theme = "@style/MainTheme.Splash",
         NoHistory = true,
         ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxFormsSplashScreenActivity<Setup, App, FormsApp>
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(MainActivity));
            return Task.CompletedTask;
        }
    }
}