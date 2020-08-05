using System;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.Forms.WPFCore.Business;
using SimTuning.Forms.WPFCore.ViewModels.Home;

namespace SimTuning.Forms.WPFCore.ViewModels
{
    public class MainViewModel : SimTuning.Core.ViewModels.MainPage
    {
        private readonly ApplicationChanges settings = new ApplicationChanges();
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;

            this.ShowHomeViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel, UserModel>(User));
            this.ShowMenuViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MenuViewModel, UserModel>(User));
            this.LoginUserCommand = new MvxAsyncCommand(this.LoginUser);
        }

        protected new async Task LoginUser()
        {
            var tuple = await API.API.UserLoginAsync();
            User.UserValid = tuple.Item1;
            User.LicenseValid = tuple.Item2;

            Functions.ShowSnackbarDialog(tuple.Item3);
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            ShowMenuViewModelCommand.Execute();
            ShowHomeViewModelCommand.Execute();
            LoginUserCommand.Execute();
        }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            settings.LoadColors();

            return base.Initialize();
        }
    }
}