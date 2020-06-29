using MvvmCross.Navigation;
using SimTuning.mobile.ViewModels.Auslass;
using SimTuning.mobile.ViewModels.Demo;
using SimTuning.mobile.ViewModels.Dyno;
using SimTuning.mobile.ViewModels.Einlass;
using SimTuning.mobile.ViewModels.Einstellungen;
using SimTuning.mobile.ViewModels.Home;
using SimTuning.mobile.ViewModels.Motor;
using SimTuning.mobile.ViewModels.Tuning;
using SimTuning.mobile.Views;
using SimTuning.mobile.Views.Auslass;
using SimTuning.mobile.Views.Demo;
using SimTuning.mobile.Views.Dyno;
using SimTuning.mobile.Views.Einlass;
using SimTuning.mobile.Views.Einstellungen;
using SimTuning.mobile.Views.Home;
using SimTuning.mobile.Views.Motor;
using SimTuning.mobile.Views.Tuning;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels
{
    public class MainWindowViewModel : SimTuning.ViewModels.MainWindow
    {
        private readonly IMvxNavigationService _navigationService;

        public MainWindowViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            ChangeTabCommand = new Command(async () => await SetPageAsync());

            Application_load();
        }

        public ICommand ChangeTabCommand { get; set; }

        protected override async void Application_load()
        {
            var tuple = await API.API.UserLoginAsync();
            UserValid = tuple.Item1;
            LicenseValid = tuple.Item2;

            for (int i = 0; i < tuple.Item3.Count; i++)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: tuple.Item3[i],
                                            msDuration: MaterialSnackbar.DurationLong);
            }
        }

        private MasterPageItem _currentTab;

        public MasterPageItem CurrentTab
        {
            get => _currentTab;
            set => SetProperty(ref _currentTab, value);
        }

        private async System.Threading.Tasks.Task SetPageAsync()
        {
            switch (CurrentTab.Title)
            {
                case "Home":
                    await _navigationService.Navigate<HomeMainViewModel>();/* Navigation.PushAsync(new Home_screen());*/
                    break;

                case "Einlass":
                    await _navigationService.Navigate<EinlassMainViewModel>(); /* Navigation.PushAsync(new Einlass_main());*/
                    break;

                case "Auslass":
                    await _navigationService.Navigate<AuslassMainViewModel>(); /*Navigation.PushAsync(new Auslass_main());*/
                    break;

                case "Motor":
                    await _navigationService.Navigate<MotorMainViewModel>();/*Navigation.PushAsync(new Motor_main());*/
                    break;

                case "Tuning":
                    if (LicenseValid)
                        await _navigationService.Navigate<TuningMainViewModel>(); /*Navigation.PushAsync(new TuningMainView());*/
                    else
                        await _navigationService.Navigate<DemoMainViewModel>(); /*Navigation.PushAsync(new BuyPro());*/
                    break;

                case "Dyno":
                    if (LicenseValid)
                        await _navigationService.Navigate<DynoMainViewModel>(); /*Navigation.PushAsync(new Dyno_main());*/
                    else
                        await _navigationService.Navigate<DemoMainViewModel>(); /*Navigation.PushAsync(new BuyPro());*/
                    break;

                case "Einstellungen":
                    await _navigationService.Navigate<EinstellungenMainViewModel>(); /*Navigation.PushAsync(new Einstellungen_main(this));*/
                    break;

                default:
                    break;
            }

            //(Application.Current.MainPage as MasterDetailPage).IsPresented = false;
        }
    }
}