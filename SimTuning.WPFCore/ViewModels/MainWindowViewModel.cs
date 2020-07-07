using MaterialDesignThemes.Wpf;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.WPFCore.Business;
using SimTuning.WPFCore.ViewModels.Home;

namespace SimTuning.WPFCore.ViewModels
{
    public class MainWindowViewModel : SimTuning.Core.ViewModels.MainPage
    {
        private readonly ApplicationChanges settings = new ApplicationChanges();
        private readonly IMvxNavigationService _navigationService;

        public MainWindowViewModel(IMvxNavigationService navigationService)
        {
            //SelectedIndex = 0;
            LoadingAnimation = false;
            CloseMenuVis = false;
            OpenMenuVis = true;

            ButtonOpenMenu = new MvxCommand(ButtonOpenMenu_Click);
            ButtonCloseMenu = new MvxCommand(ButtonCloseMenu_Click);

            NotificationSnackbar = new SnackbarMessageQueue();

            _navigationService = navigationService;

            ShowHomeViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel, UserModel>(User));
            ShowMenuViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MenuViewModel, UserModel>(User));

            ShowMenuViewModelCommand.Execute();
            //ShowHomeViewModelCommand.Execute();
        }

        public MvxCommand ButtonOpenMenu { get; set; }
        public MvxCommand ButtonCloseMenu { get; set; }

        protected override async void ApplicationLoad()
        {
            settings.LoadColors();

            var tuple = await API.API.UserLoginAsync();
            User.UserValid = tuple.Item1;
            User.LicenseValid = tuple.Item2;

            for (int i = 0; i < tuple.Item3.Count; i++)
                NotificationSnackbar.Enqueue(tuple.Item3[i]);
        }

        //private object _homeContent;

        //public object HomeContent
        //{
        //    get => _homeContent;
        //    set => SetProperty(ref _homeContent, value);
        //}

        private bool _loadingAnimation;

        public bool LoadingAnimation
        {
            get => _loadingAnimation;
            set => SetProperty(ref _loadingAnimation, value);
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

        //private int _selectedIndex;

        //public int SelectedIndex
        //{
        //    get => _selectedIndex;
        //    set
        //    {
        //        SetProperty(ref _selectedIndex, value);

        //        switch (SelectedIndex)
        //        {
        //            case 0:
        //                HomeContent = new Home_screen(this);
        //                break;

        //            case 1:
        //                //nichts da seperator
        //                break;

        //            case 2:
        //                HomeContent = new EinlassMainView();
        //                break;

        //            case 3:
        //                HomeContent = new AuslassMainView();
        //                break;

        //            case 4:
        //                HomeContent = new MotorMainView();
        //                break;

        //            case 5:
        //                //nichts da seperator
        //                break;

        //            case 6:
        //                if (LicenseValid)
        //                    HomeContent = new TuningMainView(this);
        //                else
        //                    HomeContent = new BuyProView();
        //                break;

        //            case 7:
        //                if (LicenseValid)
        //                    HomeContent = new DynoMainView(this);
        //                else
        //                    HomeContent = new BuyProView();
        //                break;

        //            case 8:
        //                //nichts da seperator
        //                break;

        //            case 9:
        //                HomeContent = new EinstellungenMainView(this);
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //}

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
    }
}