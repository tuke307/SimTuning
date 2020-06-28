using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SimTuning.mobile.Views;
using SimTuning.mobile.Views.Auslass;
using SimTuning.mobile.Views.Demo;
using SimTuning.mobile.Views.Dyno;
using SimTuning.mobile.Views.Einlass;
using SimTuning.mobile.Views.Einstellungen;
using SimTuning.mobile.Views.Home;
using SimTuning.mobile.Views.Motor;
using SimTuning.mobile.Views.Tuning;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels
{
    public class MainWindowViewModel : SimTuning.ViewModels.MainWindow
    {
        //private ApplicationChanges settings = new ApplicationChanges();
        public INavigation Navigation { get; set; }

        public MainWindowViewModel(INavigation navigation)
        {
            //SelectedIndex = 0;
            Navigation = navigation;
            ChangeTabCommand = new Command(async () => await SetPageAsync());

            Application_load();
        }

        public ICommand ChangeTabCommand { get; set; }

        protected override async void Application_load()
        {
            //settings.LoadColors();
            //await Navigation.PushAsync(new Home_screen());

            var tuple = await API.API.UserLoginAsync();
            USER_VALID = tuple.Item1;
            LICENSE_VALID = tuple.Item2;

            for (int i = 0; i < tuple.Item3.Count; i++)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: tuple.Item3[i],
                                            msDuration: MaterialSnackbar.DurationLong);
            }
        }

        public MasterPageItem CurrentTab
        {
            get => Get<MasterPageItem>();
            set => Set(value);
        }

        //public bool USER_VALID
        //{
        //    get => Get<bool>();
        //    set => Set(value);
        //}

        //public bool LICENSE_VALID
        //{
        //    get => Get<bool>();
        //    set => Set(value);
        //}

        private async System.Threading.Tasks.Task SetPageAsync()
        {
            switch (CurrentTab.Title)
            {
                case "Home":
                    await Navigation.PushAsync(new Home_screen());
                    break;

                case "Einlass":
                    await Navigation.PushAsync(new Einlass_main());
                    break;

                case "Auslass":
                    await Navigation.PushAsync(new Auslass_main());
                    break;

                case "Motor":
                    await Navigation.PushAsync(new Motor_main());
                    break;

                case "Tuning":
                    if (LICENSE_VALID)
                        await Navigation.PushAsync(new TuningMainView());
                    else
                        await Navigation.PushAsync(new BuyPro());
                    break;

                case "Dyno":
                    if (LICENSE_VALID)
                        await Navigation.PushAsync(new Dyno_main());
                    else
                        await Navigation.PushAsync(new BuyPro());
                    break;

                case "Einstellungen":
                    await Navigation.PushAsync(new Einstellungen_main(this));
                    break;

                default:
                    break;
            }

            (Application.Current.MainPage as MasterDetailPage).IsPresented = false;
        }
    }
}