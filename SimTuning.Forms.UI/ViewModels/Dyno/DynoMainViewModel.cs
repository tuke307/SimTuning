using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoMainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public DynoMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}