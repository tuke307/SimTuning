using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.WPFCore.ViewModels.Auslass;
using SimTuning.WPFCore.ViewModels.Demo;
using SimTuning.WPFCore.ViewModels.Dyno;
using SimTuning.WPFCore.ViewModels.Einlass;
using SimTuning.WPFCore.ViewModels.Einstellungen;
using SimTuning.WPFCore.ViewModels.Home;
using SimTuning.WPFCore.ViewModels.Motor;
using SimTuning.WPFCore.ViewModels.Tuning;
using System.Threading.Tasks;

namespace SimTuning.WPFCore.ViewModels
{
    public class MenuViewModel : SimTuning.Core.ViewModels.Menu
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;

            LoadingAnimation = false;
        }

        public override Task Initialize()
        {
            ShowHomeCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel, UserModel>(User));

            ShowEinlassCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinlassMainViewModel, UserModel>(User));

            ShowAuslassCommand = new MvxAsyncCommand(() => _navigationService.Navigate<AuslassMainViewModel, UserModel>(User));

            ShowMotorCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MotorMainViewModel, UserModel>(User));

            ShowDynoCommand = new MvxAsyncCommand(ShowDyno);

            ShowTuningCommand = new MvxAsyncCommand(ShowTuning);

            ShowEinstellungenCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenMainViewModel, UserModel>(User));

            return base.Initialize();
        }

        private bool _loadingAnimation;

        public bool LoadingAnimation
        {
            get => _loadingAnimation;
            set => SetProperty(ref _loadingAnimation, value);
        }

        private async Task ShowDyno()
        {
            if (User.LicenseValid)
            {
                await _navigationService.Navigate<DynoMainViewModel, UserModel>(User);
            }
            else
            {
                await _navigationService.Navigate<DemoMainViewModel>();
            }
        }

        private async Task ShowTuning()
        {
            if (User.LicenseValid)
            {
                await _navigationService.Navigate<TuningMainViewModel, UserModel>(User);
            }
            else
            {
                await _navigationService.Navigate<DemoMainViewModel>();
            }
        }
    }
}