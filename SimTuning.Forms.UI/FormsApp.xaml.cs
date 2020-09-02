// project=SimTuning.Forms.UI, file=FormsApp.xaml.cs, creation=2020:6:28 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Plugin.DeviceInfo;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Resources;

    /// <summary>
    /// FormsApp.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Application" />
    public partial class FormsApp : Xamarin.Forms.Application
    {
        private readonly ResourceManager rm;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormsApp" /> class.
        /// </summary>
        public FormsApp()
        {
            this.InitializeComponent();

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

            using (var db = new DatabaseContext())
            {
                db.Database.EnsureCreated();
                db.Database.Migrate();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnStart()
        {
            base.OnStart();
        }
    }
}