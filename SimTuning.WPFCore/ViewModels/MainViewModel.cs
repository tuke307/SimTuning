using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.WPFCore.Business;
using SimTuning.WPFCore.ViewModels.Home;

namespace SimTuning.WPFCore.ViewModels
{
    public class MainViewModel : SimTuning.Core.ViewModels.MainPage
    {
        private readonly ApplicationChanges settings = new ApplicationChanges();
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //SelectedIndex = 0;

            CloseMenuVis = false;
            OpenMenuVis = true;

            ButtonOpenMenu = new MvxCommand(ButtonOpenMenu_Click);
            ButtonCloseMenu = new MvxCommand(ButtonCloseMenu_Click);

            NotificationSnackbar = new SnackbarMessageQueue();

            _navigationService = navigationService;

            ShowHomeViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel, UserModel>(User));
            ShowMenuViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MenuViewModel, UserModel>(User));

            ShowMenuViewModelCommand.Execute();
            ShowHomeViewModelCommand.Execute();
        }

        public MvxCommand ButtonOpenMenu { get; set; }
        public MvxCommand ButtonCloseMenu { get; set; }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            settings.LoadColors();

            var tuple = API.API.UserLoginAsync();
            User.UserValid = tuple.Result.Item1;
            User.LicenseValid = tuple.Result.Item2;

            for (int i = 0; i < tuple.Result.Item3.Count; i++)
                NotificationSnackbar.Enqueue(tuple.Result.Item3[i]);

            return base.Initialize();
        }

        public void ButtonOpenMenu_Click()
        {
            CloseMenuVis = true;
            OpenMenuVis = false;
        }

        public void ButtonCloseMenu_Click()
        {
            CloseMenuVis = false;
            OpenMenuVis = true;
        }

        private bool _openMenuVis;

        public bool OpenMenuVis
        {
            get => _openMenuVis;
            set { SetProperty(ref _openMenuVis, value); }
        }

        private bool _closeMenuVis;

        public bool CloseMenuVis
        {
            get => _closeMenuVis;
            set { SetProperty(ref _closeMenuVis, value); }
        }

        private SnackbarMessageQueue _notificationSnackbar;

        public SnackbarMessageQueue NotificationSnackbar
        {
            get => _notificationSnackbar;
            private set => SetProperty(ref _notificationSnackbar, value);
        }
    }
}