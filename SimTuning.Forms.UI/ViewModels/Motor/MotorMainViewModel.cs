using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace SimTuning.Forms.UI.ViewModels.Motor
{
    public class MotorMainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MotorMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}