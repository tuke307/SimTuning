using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Forms.UI.ViewModels.Auslass;
using SimTuning.Forms.UI.ViewModels.Demo;
using SimTuning.Forms.UI.ViewModels.Dyno;
using SimTuning.Forms.UI.ViewModels.Einlass;
using SimTuning.Forms.UI.ViewModels.Einstellungen;
using SimTuning.Forms.UI.ViewModels.Home;
using SimTuning.Forms.UI.ViewModels.Motor;
using SimTuning.Forms.UI.ViewModels.Tuning;
using SimTuning.Forms.UI.Views;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels
{
    public class MenuViewModel : SimTuning.Core.ViewModels.Menu
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowHomeCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel>());
            ShowEinlassCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinlassMainViewModel>());
            ShowAuslassCommand = new MvxAsyncCommand(() => _navigationService.Navigate<AuslassMainViewModel>());
            ShowMotorCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MotorMainViewModel>());
            ShowDynoCommand = new MvxAsyncCommand(() => _navigationService.Navigate<DynoMainViewModel>());
            ShowTuningCommand = new MvxAsyncCommand(() => _navigationService.Navigate<TuningMainViewModel>());
            ShowEinstellungenCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenMainViewModel>());

            ApplicationLoad();
        }

        public IMvxAsyncCommand ShowHomeCommand { get; set; }
        public IMvxAsyncCommand ShowEinlassCommand { get; set; }
        public IMvxAsyncCommand ShowAuslassCommand { get; set; }
        public IMvxAsyncCommand ShowMotorCommand { get; set; }
        public IMvxAsyncCommand ShowDynoCommand { get; set; }
        public IMvxAsyncCommand ShowTuningCommand { get; set; }
        public IMvxAsyncCommand ShowEinstellungenCommand { get; set; }

        protected new async void ApplicationLoad()
        {
            //var tuple = await API.API.UserLoginAsync().ConfigureAwait(true);
            //UserValid = tuple.Item1;
            //LicenseValid = tuple.Item2;

            //for (int i = 0; i < tuple.Item3.Count; i++)
            //{
            //    await MaterialDialog.Instance.SnackbarAsync(message: tuple.Item3[i],
            //                                msDuration: MaterialSnackbar.DurationLong).ConfigureAwait(false);
            //}
        }

        //private MasterPageItem _currentTab;

        //public MasterPageItem CurrentTab
        //{
        //    get => _currentTab;
        //    set => SetProperty(ref _currentTab, value);
        //}

        //private async System.Threading.Tasks.Task SetPageAsync()
        //{
        //    switch (CurrentTab.Title)
        //    {
        //        case "Home":
        //            await _navigationService.Navigate<HomeMainViewModel>();/* Navigation.PushAsync(new Home_screen());*/
        //            break;

        //        case "Einlass":
        //            await _navigationService.Navigate<EinlassMainViewModel>(); /* Navigation.PushAsync(new Einlass_main());*/
        //            break;

        //        case "Auslass":
        //            await _navigationService.Navigate<AuslassMainViewModel>(); /*Navigation.PushAsync(new Auslass_main());*/
        //            break;

        //        case "Motor":
        //            await _navigationService.Navigate<MotorMainViewModel>();/*Navigation.PushAsync(new Motor_main());*/
        //            break;

        //        case "Tuning":
        //            if (LicenseValid)
        //                await _navigationService.Navigate<TuningMainViewModel>(); /*Navigation.PushAsync(new TuningMainView());*/
        //            else
        //                await _navigationService.Navigate<DemoMainViewModel>(); /*Navigation.PushAsync(new BuyPro());*/
        //            break;

        //        case "Dyno":
        //            if (LicenseValid)
        //                await _navigationService.Navigate<DynoMainViewModel>(); /*Navigation.PushAsync(new Dyno_main());*/
        //            else
        //                await _navigationService.Navigate<DemoMainViewModel>(); /*Navigation.PushAsync(new BuyPro());*/
        //            break;

        //        case "Einstellungen":
        //            await _navigationService.Navigate<EinstellungenMainViewModel>(); /*Navigation.PushAsync(new Einstellungen_main(this));*/
        //            break;

        //        default:
        //            break;
        //    }

        //    //(Application.Current.MainPage as MasterDetailPage).IsPresented = false;
        //}
    }
}