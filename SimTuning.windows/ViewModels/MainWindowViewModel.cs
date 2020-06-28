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
            Visibility_ButtonCloseMenu = false;
            LoadingAnimation = false;

            ButtonOpenMenu = new ActionCommand(ButtonOpenMenu_Click);
            ButtonCloseMenu = new ActionCommand(ButtonCloseMenu_Click);
            Visibility_ButtonOpenMenu = true;

            ButtonClose = new ActionCommand(CloseApplication);

            NotificationSnackbar = new SnackbarMessageQueue();
            //NotificationSnackbar.IgnoreDuplicate = true;
            //Application_load();
        }

        protected override async void Application_load()
        {
            settings.LoadColors();

            var tuple = await API.API.UserLoginAsync();
            USER_VALID = tuple.Item1;
            LICENSE_VALID = tuple.Item2;

            for (int i = 0; i < tuple.Item3.Count; i++)
                NotificationSnackbar.Enqueue(tuple.Item3[i]);
        }


        public object HomeContent
        {
            get => Get<object>();
            set => Set(value);
        }

        public bool LoadingAnimation
        {
            get => Get<bool>();
            set => Set(value);
        }


        public SnackbarMessageQueue NotificationSnackbar
        {
            get => Get<SnackbarMessageQueue>();
            set => Set(value);
        }

        public int SelectedIndex
        {
            get => Get<int>();
            set
            {
                Set(value);

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
                        if (LICENSE_VALID)
                            HomeContent = new TuningMainView(this);
                        else
                            HomeContent = new BuyProView();
                        break;

                    case 7:
                        if (LICENSE_VALID)
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

        public ICommand ButtonOpenMenu { get; set; }
        public ICommand ButtonCloseMenu { get; set; }
        public ICommand ButtonClose { get; set; }

        public void ButtonOpenMenu_Click(object parameter)
        {
            Visibility_ButtonCloseMenu = true;
            Visibility_ButtonOpenMenu = false;
        }

        public void ButtonCloseMenu_Click(object parameter)
        {
            Visibility_ButtonCloseMenu = false;
            Visibility_ButtonOpenMenu = true;
        }

        public void CloseApplication(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}