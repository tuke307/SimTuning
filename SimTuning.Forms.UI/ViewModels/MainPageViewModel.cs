using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.Forms.UI.ViewModels.Home;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels
{
    public class MainPageViewModel : SimTuning.Core.ViewModels.MainPage
    {
        private readonly IMvxNavigationService _navigationService;

        public MainPageViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;

            this.ShowHomeViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel, UserModel>(User));
            this.ShowMenuViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MenuViewModel, UserModel>(User));
            this.LoginUserCommand = new MvxAsyncCommand(this.LoginUser);
        }

        protected new async Task LoginUser()
        {
            var tuple = await API.API.UserLoginAsync();
            User.UserValid = tuple.Item1;
            User.LicenseValid = tuple.Item2;

            foreach (var item in tuple.Item3)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: item).ConfigureAwait(false);
            }
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            ShowMenuViewModelCommand.Execute();
            ShowHomeViewModelCommand.Execute();
            LoginUserCommand.Execute();
        }
    }
}