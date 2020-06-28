using SimTuning.Business;
using SimTuning.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace SimTuning.ViewModels
{
    public class MainWindow : BaseViewModel
    {
        //private readonly ApplicationChanges settings = new ApplicationChanges();

        public MainWindow()
        {
            // SelectedIndex = 0;
            Visibility_ButtonCloseMenu = false;
            //LoadingAnimation = false;

            //ButtonOpenMenu = new ActionCommand(ButtonOpenMenu_Click);
            //ButtonCloseMenu = new ActionCommand(ButtonCloseMenu_Click);
            Visibility_ButtonOpenMenu = true;

            //ButtonClose = new ActionCommand(CloseApplication);

            //NotificationSnackbar = new SnackbarMessageQueue();

            Application_load();
        }

        protected virtual void Application_load()
        {
            //settings.LoadColors();

            //var tuple = await API.API.UserLoginAsync();
            //USER_VALID = tuple.Item1;
            //LICENSE_VALID = tuple.Item2;

            //for (int i = 0; i < tuple.Item3.Count; i++)
            //    NotificationSnackbar.Enqueue(tuple.Item3[i]);
        }

        public bool Visibility_ButtonOpenMenu
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool Visibility_ButtonCloseMenu
        {
            get => Get<bool>();
            set => Set(value);
        }

        //public object HomeContent
        //{
        //    get => Get<object>();
        //    set => Set(value);
        //}

        //public bool LoadingAnimation
        //{
        //    get => Get<bool>();
        //    set => Set(value);
        //}

        public bool USER_VALID
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool LICENSE_VALID
        {
            get => Get<bool>();
            set => Set(value);
        }

        //public SnackbarMessageQueue NotificationSnackbar
        //{
        //    get => Get<SnackbarMessageQueue>();
        //    set => Set(value);
        //}

        //public int SelectedIndex
        //{
        //    get => Get<int>();
        //    set
        //    {
        //        Set(value);

        //        switch (SelectedIndex)
        //        {
        //            case 0:
        //                HomeContent = new Home_screen(this);
        //                break;

        //            case 1:
        //                //nichts da seperator
        //                break;

        //            case 2:
        //                HomeContent = new Einlass_main();
        //                break;

        //            case 3:
        //                HomeContent = new Auslass_main();
        //                break;

        //            case 4:
        //                HomeContent = new Motor_main();
        //                break;

        //            case 5:
        //                //nichts da seperator
        //                break;

        //            case 6:
        //                if (LICENSE_VALID)
        //                    HomeContent = new Tuning_main();
        //                else
        //                    HomeContent = new BuyPro();
        //                break;

        //            case 7:
        //                if (LICENSE_VALID)
        //                    HomeContent = new DynoMainView(this);
        //                else
        //                    HomeContent = new BuyPro();
        //                break;

        //            case 8:
        //                //nichts da seperator
        //                break;

        //            case 9:
        //                HomeContent = new Einstellungen_main(this);
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //}

        //public ICommand ButtonOpenMenu { get; set; }
        //public ICommand ButtonCloseMenu { get; set; }
        //public ICommand ButtonClose { get; set; }

        //public void ButtonOpenMenu_Click(object parameter)
        //{
        //    Visibility_ButtonCloseMenu = true;
        //    Visibility_ButtonOpenMenu = false;
        //}

        //public void ButtonCloseMenu_Click(object parameter)
        //{
        //    Visibility_ButtonCloseMenu = false;
        //    Visibility_ButtonOpenMenu = true;
        //}

        //public void CloseApplication(object parameter)
        //{
        //    Application.Current.Shutdown();
        //}
    }
}