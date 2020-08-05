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
            _navigationService = navigationService;

            ShowHomeViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel, UserModel>(User));
            ShowMenuViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MenuViewModel, UserModel>(User));
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            ShowMenuViewModelCommand.Execute();
            ShowHomeViewModelCommand.Execute();

            //Task.Run(() =>
            //{
            //    var tuple = API.API.UserLoginAsync();
            //    User.UserValid = tuple.Result.Item1;
            //    User.LicenseValid = tuple.Result.Item2;

            //    Functions.ShowSnackbarDialog(tuple.Result.Item3);
            //});
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