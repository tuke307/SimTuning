using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    public class EinstellungenMainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public EinstellungenMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}