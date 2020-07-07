using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenMainViewModel : SimTuning.Core.ViewModels.Einstellungen.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        public EinstellungenMainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                _navigationService.Navigate<EinstellungenAussehenViewModel>(),
                _navigationService.Navigate<EinstellungenUpdateViewModel>(),
                _navigationService.Navigate<EinstellungenVehiclesViewModel>(),
                _navigationService.Navigate<EinstellungenKontoViewModel>()
            };
            return Task.WhenAll(tasks);
        }

        public override void ViewAppearing()
        {
            if (_firstTime)
            {
                ShowInitialViewModels();
                _firstTime = false;
            }
        }
    }
}