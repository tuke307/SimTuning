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
        /// <summary>
        /// Initializes a new instance of the <see cref="FormsApp" /> class.
        /// </summary>
        public FormsApp()
        {
            this.InitializeComponent();

            XF.Material.Forms.Material.Init(this, "Material.Configuration");

            // android: "/data/user/0/com.tuke_productions.SimTuning/files/"
            SimTuning.Core.Constants.FileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Data.Constants.DatabasePath = Path.Combine(SimTuning.Core.Constants.FileDirectory, Data.Constants.DatabaseName);

            if (!Directory.Exists(SimTuning.Core.Constants.FileDirectory))
            {
                Directory.CreateDirectory(SimTuning.Core.Constants.FileDirectory);
            }

            // fix with android 10
            if (!File.Exists(Constants.DatabasePath)) File.Create(Constants.DatabasePath);

            using (var db = new DatabaseContext())
            {
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