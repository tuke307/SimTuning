﻿// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI
{
    using MvvmCross.IoC;
    using SimTuning.Core.Models;

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
            XF.Material.Forms.Material.Init(this);

            this.InitializeComponent();

            XF.Material.Forms.Material.Use("Material.Configuration");

            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
            Sharpnado.MaterialFrame.Initializer.Initialize(loggerEnable: false, debugLogEnable: false);
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