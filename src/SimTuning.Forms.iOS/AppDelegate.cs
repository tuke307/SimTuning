// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.iOS
{
    using Foundation;
    using MvvmCross.Forms.Platforms.Ios.Core;
    using SimTuning.Forms.UI;
    using SimTuning.mobile.iOS;
    using UIKit;

    /// <summary>
    /// AppDelegate.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Platforms.Ios.Core.MvxFormsApplicationDelegate{SimTuning.mobile.iOS.Setup, SimTuning.Forms.UI.MvxApp, SimTuning.Forms.UI.FormsApp}" />
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxFormsApplicationDelegate<Setup, MvxApp, FormsApp>
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            XF.Material.iOS.Material.Init();
            MediaManager.CrossMediaManager.Current.Init();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();

            return result;
        }
    }
}