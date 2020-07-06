using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace SimTuning.Forms.UI.ViewModels.Auslass
{
    public class AuslassMainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public AuslassMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}