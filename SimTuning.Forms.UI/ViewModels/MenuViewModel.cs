// project=SimTuning.Forms.UI, file=MenuViewModel.cs, creation=2020:7:2
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using SimTuning.Forms.UI.Business;
using SimTuning.Forms.UI.ViewModels.Auslass;
using SimTuning.Forms.UI.ViewModels.Demo;
using SimTuning.Forms.UI.ViewModels.Dyno;
using SimTuning.Forms.UI.ViewModels.Einlass;
using SimTuning.Forms.UI.ViewModels.Einstellungen;
using SimTuning.Forms.UI.ViewModels.Home;
using SimTuning.Forms.UI.ViewModels.Motor;
using SimTuning.Forms.UI.ViewModels.Tuning;
using SimTuning.Forms.UI.Views;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels
{
    public class MenuViewModel : SimTuning.Core.ViewModels.Menu
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;

            ShowHomeCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel>());
            ShowEinlassCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinlassMainViewModel, UserModel>(User));
            ShowAuslassCommand = new MvxAsyncCommand(() => _navigationService.Navigate<AuslassMainViewModel, UserModel>(User));
            ShowMotorCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MotorMainViewModel, UserModel>(User));
            ShowDynoCommand = new MvxAsyncCommand(ShowDyno);
            ShowTuningCommand = new MvxAsyncCommand(ShowTuning);
            ShowEinstellungenCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenMainViewModel, UserModel>(User));
            this.LoginUserCommand = new MvxAsyncCommand(this.LoginUser);
        }

        protected new async Task LoginUser()
        {
            var result = await API.Login.UserLoginAsync();
            User = result.Item1;

            Functions.ShowSnackbarDialog(result.Item2);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            LoginUserCommand.Execute();
        }

        private async Task ShowDyno()
        {
            if (User.LicenseValid)
            {
                await _navigationService.Navigate<DynoMainViewModel, UserModel>(User).ConfigureAwait(true);
            }
            else
            {
                await _navigationService.Navigate<DemoMainViewModel>().ConfigureAwait(true);
            }
        }

        private async Task ShowTuning()
        {
            if (User.LicenseValid)
            {
                await _navigationService.Navigate<TuningMainViewModel, UserModel>(User).ConfigureAwait(true);
            }
            else
            {
                await _navigationService.Navigate<DemoMainViewModel>().ConfigureAwait(true);
            }
        }
    }
}