// project=SimTuning.Forms.iOS, file=AppDelegate.cs, creation=2020:7:1 Copyright (c) 2020
// tuke productions. All rights reserved.
using Foundation;
using MvvmCross.Platforms.Ios.Core;
using SimTuning.Forms.UI;
using UIKit;

namespace SimTuning.Forms.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<MvxIosSetup<App>, App>
    {
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            XF.Material.iOS.Material.Init();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();

            return result;
        }
    }
}