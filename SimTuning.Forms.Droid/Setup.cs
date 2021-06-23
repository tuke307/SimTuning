// project=SimTuning.Forms.Droid, file=Setup.cs, creation=2020:10:19 Copyright (c) 2021
// tuke productions. All rights reserved.
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using SimTuning.Forms.UI;
using SimTuning.Forms.UI.Services;

namespace SimTuning.Forms.Droid
{
    public class Setup : MvxFormsAndroidSetup<MvxApp, FormsApp>
    // No Splash Screen with this; MvxAndroidSetup<App>
    {
        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);

            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.WebBrowser.Platforms.Android.Plugin>();
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Location.Fused.Plugin>();
        }

        protected override IMvxApplication CreateApp()
        {
            return CreateApp();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
            Mvx.IoCProvider.RegisterSingleton<IThemeService>(() => new ThemeService());
        }
    }
}