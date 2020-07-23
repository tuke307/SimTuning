using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenMainViewModel : SimTuning.Core.ViewModels.Einstellungen.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        public EinstellungenMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
        }

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                _navigationService.Navigate<EinstellungenAussehenViewModel, UserModel>(User),
                _navigationService.Navigate<EinstellungenUpdateViewModel>(),
                _navigationService.Navigate<EinstellungenVehiclesViewModel, UserModel>(User),
                _navigationService.Navigate<EinstellungenKontoViewModel, UserModel>(User)
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