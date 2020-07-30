using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.WPF.Base.Business;
using SimTuning.WPF.Base.ViewModels.Home;

namespace SimTuning.WPF.Base.ViewModels
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
            //NotificationSnackbar = new SnackbarMessageQueue();
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            ShowMenuViewModelCommand.Execute();
            ShowHomeViewModelCommand.Execute();
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

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            Task.Run(() =>
            {
                var tuple = API.API.UserLoginAsync();
                User.UserValid = tuple.Result.Item1;
                User.LicenseValid = tuple.Result.Item2;

                //for (int i = 0; i < tuple.Result.Item3.Count; i++)
                //NotificationSnackbar.Enqueue(tuple.Result.Item3[i]);

                var result = DialogHost.Show(typeof(DialogLoadingView), "LoadingDialog", delegate (object sender, DialogOpenedEventArgs args)
                {
                    args.Session.Close(false);
                });
            });
        }

        //private SnackbarMessageQueue _notificationSnackbar;

        //public SnackbarMessageQueue NotificationSnackbar
        //{
        //    get => _notificationSnackbar;
        //    private set => SetProperty(ref _notificationSnackbar, value);
        //}
    }
}