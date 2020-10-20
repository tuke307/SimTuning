// project=SimTuning.Forms.iOS, file=AppDelegate.cs, creation=2020:7:7 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Forms.iOS
{
    using Foundation;
    using MvvmCross.Forms.Platforms.Ios.Core;
    using SimTuning.Forms.UI;
    using UIKit;

    /// <summary>
    /// AppDelegate.
    /// </summary>
    /// <seealso cref="MvvmCross.Forms.Platforms.Ios.Core.MvxFormsApplicationDelegate{MvvmCross.Forms.Platforms.Ios.Core.MvxFormsIosSetup{SimTuning.Forms.UI.App, SimTuning.Forms.UI.FormsApp}, SimTuning.Forms.UI.App, SimTuning.Forms.UI.FormsApp}" />
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<App, FormsApp>, App, FormsApp>
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            XF.Material.iOS.Material.Init();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();

            return result;
        }
    }
}