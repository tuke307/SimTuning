using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.mobile.ViewModels.Einstellungen
{
    public class Einstellungen_KontoViewModel : SimTuning.ViewModels.Einstellungen.KontoViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;

        public Einstellungen_KontoViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            //SimTuning.Business.Functions.LoginCredentials(out string _email, out SecureString _password);
            //Email = _email;

            ConnectUserCommand = new Command(ConnectUser);
            RegisterSiteCommand = new Command(RegisterSite);
            //PasswordChangedCommand = new Command(PasswordChanged);
        }

        //public ICommand ConnectUserCommand { get; set; }
        //public ICommand RegisterSiteCommand { get; set; }
        //public ICommand PasswordChangedCommand { get; set; }

        protected override async void ConnectUser(object parameter)
        {
            var tuple = await API.API.UserLoginAsync(email: Email, password: SimTuning.Business.Converts.StringToSecureString(Password));
            mainWindowViewModel.USER_VALID = tuple.Item1;
            mainWindowViewModel.LICENSE_VALID = tuple.Item2;

            for (int i = 0; i < tuple.Item3.Count; i++)
            {
                await MaterialDialog.Instance.SnackbarAsync(message: tuple.Item3[i],
                                            msDuration: MaterialSnackbar.DurationLong);
            }
        }

        //private void PasswordChanged(object parameter)
        //{
        //    var passwordBox = parameter as PasswordBox;
        //    password = passwordBox.SecurePassword;
        //}

        protected override void RegisterSite(object parameter)
        {
            Launcher.OpenAsync(new Uri("https://tuke-productions.de/mein-konto/"));
        }

        //private Data.Models.SettingsModel settings
        //{
        //    get => Get<Data.Models.SettingsModel>();
        //    set => Set(value);
        //}

        //public string Email
        //{
        //    get => Get<string>();
        //    set => Set(value);
        //}

        public string Password
        {
            get => Get<string>();
            set => Set(value);
        }

        //public string firstname
        //{
        //    get => Get<string>();
        //    set => Set(value);
        //}

        //public string lastname
        //{
        //    get => Get<string>();
        //    set => Set(value);
        //}
    }
}