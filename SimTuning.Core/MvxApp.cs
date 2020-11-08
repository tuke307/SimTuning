using MediaManager;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core
{
    /// <summary>
    /// BASE Application.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxApplication" />
    public class MvxApp : MvxApplication
    {
        /// <summary>
        /// Any initialization steps that can be done in the background.
        /// </summary>
        public override void Initialize()
        {
            MvxIoCProvider.Instance.RegisterSingleton(Plugin.Settings.CrossSettings.Current);

            MvxIoCProvider.Instance.RegisterSingleton(CrossMediaManager.Current);

            base.Initialize();
        }

        /// <summary>
        /// If the application is restarted (eg primary activity on Android can be
        /// restarted) this method will be called before Startup is called again.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
        }

        /// <summary>
        /// Do any UI bound startup actions here.
        /// </summary>
        public override Task Startup()
        {
            return base.Startup();
        }
    }
}