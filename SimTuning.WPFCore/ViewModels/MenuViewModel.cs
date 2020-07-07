using MvvmCross.Commands;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.WPFCore.ViewModels.Auslass;
using SimTuning.WPFCore.ViewModels.Dyno;
using SimTuning.WPFCore.ViewModels.Einlass;
using SimTuning.WPFCore.ViewModels.Einstellungen;
using SimTuning.WPFCore.ViewModels.Home;
using SimTuning.WPFCore.ViewModels.Motor;
using SimTuning.WPFCore.ViewModels.Tuning;

namespace SimTuning.WPFCore.ViewModels
{
    public class MenuViewModel : SimTuning.Core.ViewModels.Menu
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowHomeCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel, UserModel>(User));

            ShowEinlassCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinlassMainViewModel, UserModel>(User));

            ShowAuslassCommand = new MvxAsyncCommand(() => _navigationService.Navigate<AuslassMainViewModel, UserModel>(User));

            ShowMotorCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MotorMainViewModel, UserModel>(User));

            ShowDynoCommand = new MvxAsyncCommand(() => _navigationService.Navigate<DynoMainViewModel, UserModel>(User));

            ShowTuningCommand = new MvxAsyncCommand(() => _navigationService.Navigate<TuningMainViewModel, UserModel>(User));

            ShowEinstellungenCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenMainViewModel, UserModel>(User));
        }
    }
}