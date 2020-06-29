using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels.Einstellungen
{
    public class EinstellungenKontoViewModel : SimTuning.ViewModels.Einstellungen.KontoViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public EinstellungenKontoViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            ConnectUserCommand = new Command(ConnectUser);
            RegisterSiteCommand = new Command(RegisterSite);
        }

        protected override async void ConnectUser(object parameter)
        {
            var tuple = await API.API.UserLoginAsync(email: Email, password: SimTuning.Business.Converts.StringToSecureString(Password));
            mainWindowViewModel.UserValid = tuple.Item1;
            mainWindowViewModel.LicenseValid = tuple.Item2;

            for (int i = 0; i < tuple.Item3.Count; i++)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: tuple.Item3[i],
                                            msDuration: MaterialSnackbar.DurationLong);
            }
        }

        protected override void RegisterSite(object parameter)
        {
            Launcher.OpenAsync(new Uri("https://tuke-productions.de/mein-konto/"));
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
    }
}