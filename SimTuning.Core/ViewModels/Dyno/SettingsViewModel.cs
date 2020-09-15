using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace SimTuning.Core.ViewModels.Dyno
{
    public class SettingsViewModel : MvxNavigationViewModel
    {
        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
        }
    }
}