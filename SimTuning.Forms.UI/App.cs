using MediaManager;
using MvvmCross;
using MvvmCross.IoC;
using SimTuning.Forms.UI.ViewModels;

namespace SimTuning.Forms.UI
{
    public class App : MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            //CreatableTypes()
            //   .EndingWith("Service")
            //   .AsInterfaces()
            //   .RegisterAsLazySingleton();
            Mvx.IoCProvider.RegisterSingleton(CrossMediaManager.Current);
            //CrossMediaManager.Current.Library.Providers.Add(new MediaItemProvider());

            RegisterAppStart<MainPageViewModel>();
        }
    }
}