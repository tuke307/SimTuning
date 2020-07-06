using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace SimTuning.Forms.UI.ViewModels.Einlass
{
    public class EinlassMainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public EinlassMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}