using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    public class TuningMainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public TuningMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}