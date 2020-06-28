using Data;
using Plugin.DeviceInfo;
using SimTuning.mobile.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.Resources.Typography;

namespace SimTuning.mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //XF.Material.Forms.Material.Init(this);
            XF.Material.Forms.Material.Init(this, "Material.Configuration");

            SimTuning.Constants.Platform = CrossDeviceInfo.Current.Platform;

            switch (SimTuning.Constants.Platform)
            {
                case Plugin.DeviceInfo.Abstractions.Platform.iOS:
                    Data.Constants.DatabasePath = Path.Combine(SimTuning.Constants.FileDirectory, Data.Constants.DatabaseName);
                    break;

                case Plugin.DeviceInfo.Abstractions.Platform.Android:
                    Data.Constants.DatabasePath = Path.Combine(SimTuning.Constants.FileDirectory, Data.Constants.DatabaseName);
                    break;

                default:
                    throw new NotImplementedException("Platform not supported");
            }

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}