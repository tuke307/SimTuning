using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using SimTuning.Forms.UI;
using System.Collections.Generic;
using System.Reflection;

namespace SimTuning.Droid
{
    public class Setup : MvxFormsAndroidSetup<App, FormsApp>
    // No Splash Screen with this; MvxAndroidSetup<App>
    {
        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);

            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Messenger.Plugin>();
            pluginManager.EnsurePluginLoaded<MvvmCross.Plugin.Location.Fused.Plugin>();
        }
    }
}