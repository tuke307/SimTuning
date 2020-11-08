// project=SimTuning.Forms.Droid, file=Setup.cs, creation=2020:9:20 Copyright (c) 2020
// tuke productions. All rights reserved.
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Plugin;
using SimTuning.Forms.UI;

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
    }
}