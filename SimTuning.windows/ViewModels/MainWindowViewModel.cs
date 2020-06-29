using MaterialDesignThemes.Wpf;
using SimTuning.windows.Business;
using SimTuning.windows.Views.Auslass;
using SimTuning.windows.Views.Demo;
using SimTuning.windows.Views.Dyno;
using SimTuning.windows.Views.Einlass;
using SimTuning.windows.Views.Einstellungen;
using SimTuning.windows.Views.Home;
using SimTuning.windows.Views.Motor;
using SimTuning.windows.Views.Tuning;
using System.Windows;
using System.Windows.Input;

namespace SimTuning.windows.ViewModels
{
    public class MainWindowViewModel : SimTuning.ViewModels.MainWindow
    {
        private readonly ApplicationChanges settings = new ApplicationChanges();

        public MainWindowViewModel()
        {
            SelectedIndex = 0;
            LoadingAnimation = false;
            CloseMenuVis = false;
            OpenMenuVis = true;

            ButtonOpenMenu = new ActionCommand(ButtonOpenMenu_Click);
            ButtonCloseMenu = new ActionCommand(ButtonCloseMenu_Click);

            ButtonClose = new ActionCommand(CloseApplication);

            NotificationSnackbar = new SnackbarMessageQueue();
        }

        public ICommand ButtonOpenMenu { get; set; }
        public ICommand ButtonCloseMenu { get; set; }
        public ICommand ButtonClose { get; set; }

        protected override async void Application_load()
        {
            settings.LoadColors();

            var tuple = await API.API.UserLoginAsync();
            UserValid = tuple.Item1;
            LicenseValid = tuple.Item2;

            for (int i = 0; i < tuple.Item3.Count; i++)
                NotificationSnackbar.Enqueue(tuple.Item3[i]);
        }

        private object _homeContent;

        public object HomeContent
        {
            get => _homeContent;
            set => SetProperty(ref _homeContent, value);
        }

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

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                SetProperty(ref _selectedIndex, value);

                switch (SelectedIndex)
                {
                    case 0:
                        HomeContent = new Home_screen(this);
                        break;

                    case 1:
                        //nichts da seperator
                        break;

                    case 2:
                        HomeContent = new EinlassMainView();
                        break;

                    case 3:
                        HomeContent = new AuslassMainView();
                        break;

                    case 4:
                        HomeContent = new MotorMainView();
                        break;

                    case 5:
                        //nichts da seperator
                        break;

                    case 6:
                        if (LicenseValid)
                            HomeContent = new TuningMainView(this);
                        else
                            HomeContent = new BuyProView();
                        break;

                    case 7:
                        if (LicenseValid)
                            HomeContent = new DynoMainView(this);
                        else
                            HomeContent = new BuyProView();
                        break;

                    case 8:
                        //nichts da seperator
                        break;

                    case 9:
                        HomeContent = new EinstellungenMainView(this);
                        break;

                    default:
                        break;
                }
            }
        }

        public void ButtonOpenMenu_Click(object parameter)
        {
            CloseMenuVis = true;
            OpenMenuVis = false;
        }

        public void ButtonCloseMenu_Click(object parameter)
        {
            CloseMenuVis = false;
            OpenMenuVis = true;
        }

        public void CloseApplication(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}