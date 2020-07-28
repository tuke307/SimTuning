using Plugin.DeviceInfo;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;

namespace SimTuning.Forms.UI
{
    public partial class FormsApp : Xamarin.Forms.Application
    {
        private readonly ResourceManager rm;

        public FormsApp()
        {
            InitializeComponent();

            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            XF.Material.Forms.Material.Init(this, "Material.Configuration");

            SimTuning.Core.Constants.Platform = CrossDeviceInfo.Current.Platform;

            switch (SimTuning.Core.Constants.Platform)
            {
                case Plugin.DeviceInfo.Abstractions.Platform.iOS:
                    Data.Constants.DatabasePath = Path.Combine(SimTuning.Core.Constants.FileDirectory, Data.Constants.DatabaseName);
                    break;

                case Plugin.DeviceInfo.Abstractions.Platform.Android:
                    Data.Constants.DatabasePath = Path.Combine(SimTuning.Core.Constants.FileDirectory, Data.Constants.DatabaseName);
                    break;

                default:
                    throw new NotImplementedException(message: rm.GetString("ERR_NOSUPPORT", CultureInfo.CurrentCulture));
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}