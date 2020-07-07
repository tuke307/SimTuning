using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using SimTuning.Forms.UI.ViewModels.Auslass;
using SimTuning.Forms.UI.ViewModels.Demo;
using SimTuning.Forms.UI.ViewModels.Dyno;
using SimTuning.Forms.UI.ViewModels.Einlass;
using SimTuning.Forms.UI.ViewModels.Einstellungen;
using SimTuning.Forms.UI.ViewModels.Home;
using SimTuning.Forms.UI.ViewModels.Motor;
using SimTuning.Forms.UI.ViewModels.Tuning;
using SimTuning.Forms.UI.Views;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels
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

            //ApplicationLoad();
        }
    }
}