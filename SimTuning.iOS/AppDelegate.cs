using Foundation;
using MvvmCross.Platforms.Ios.Core;
using SimTuning.Forms.UI;
using UIKit;

namespace SimTuning.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for
    // launching the User Interface of the application, as well as listening (and
    // optionally responding) to application events from iOS.
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<MvxIosSetup<App>, App>
    {
        // class-level declarations

        public override UIWindow Window
        {
            get;
            set;
        }

        /// <summary>
        /// Finisheds the launching.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="launchOptions">The launch options.</param>
        /// <returns></returns>
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            var result = base.FinishedLaunching(application, launchOptions);

            return result;
        }
    }
}