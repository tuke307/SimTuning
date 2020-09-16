// project=SimTuning.Forms.UI, file=FormsApp.xaml.cs, creation=2020:6:28 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI
{
    using MvvmCross.IoC;

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

            MvxIoCProvider.Instance.RegisterSingleton<Plugin.Settings.Abstractions.ISettings>(Plugin.Settings.CrossSettings.Current);
            XF.Material.Forms.Material.Init(this, "Material.Configuration");
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