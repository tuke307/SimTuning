using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.WPF.Base.ViewModels.Auslass
{
    public class AuslassTheorieViewModel : SimTuning.Core.ViewModels.Auslass.TheorieViewModel
    {
        public AuslassTheorieViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}