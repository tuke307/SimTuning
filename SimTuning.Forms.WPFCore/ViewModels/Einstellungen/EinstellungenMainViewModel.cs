using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.Forms.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenMainViewModel : SimTuning.Core.ViewModels.Einstellungen.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        public EinstellungenMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare(SimTuning.Core.Models.UserModel _user)
        {
            base.Prepare(_user);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewAppearing()
        {
            _navigationService.Navigate<EinstellungenAussehenViewModel, UserModel>(User);
            _navigationService.Navigate<EinstellungenUpdateViewModel>();
            _navigationService.Navigate<EinstellungenVehiclesViewModel, UserModel>(User);
            _navigationService.Navigate<EinstellungenKontoViewModel, UserModel>(User);

            EinstellungenTabIndex = 0;
        }
    }
}