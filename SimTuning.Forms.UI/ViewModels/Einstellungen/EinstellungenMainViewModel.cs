using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    public class EinstellungenMainViewModel : SimTuning.Core.ViewModels.Einstellungen.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public EinstellungenMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
        }

        public override void ViewAppearing()
        {
            _navigationService.Navigate<EinstellungenVehiclesViewModel, UserModel>(User);
            _navigationService.Navigate<EinstellungenKontoViewModel, UserModel>(User);
        }
    }
}