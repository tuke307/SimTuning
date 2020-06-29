using Data.Models;
using SimTuning.windows.Business;
using SimTuning.windows.ViewModels;
using System.Security;
using System.Windows.Controls;

namespace SimTuning.ViewModels.Einstellungen
{
    public class EinstellungenKontoViewModel : SimTuning.ViewModels.Einstellungen.KontoViewModel
    {
        private readonly MainWindowViewModel mainWindowViewModel;
        private SecureString Password;

        public EinstellungenKontoViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            //SimTuning.Business.Functions.LoginCredentials(out string _email, out SecureString _password);
            //Email = _email;

            ConnectUserCommand = new ActionCommand(ConnectUser);
            RegisterSiteCommand = new ActionCommand(RegisterSite);
            LoginSiteCommand = new ActionCommand(LoginSite);
            PasswordChangedCommand = new ActionCommand(PasswordChanged);
        }

        protected override void PasswordChanged(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            Password = passwordBox.SecurePassword;
        }

        protected override async void ConnectUser(object parameter)
        {
            var tuple = await API.API.UserLoginAsync(email: Email, password: Password);
            mainWindowViewModel.UserValid = tuple.Item1;
            mainWindowViewModel.LicenseValid = tuple.Item2;

            for (int i = 0; i < tuple.Item3.Count; i++)
                mainWindowViewModel.NotificationSnackbar.Enqueue(tuple.Item3[i]);
        }

        protected override void RegisterSite(object parameter)
        {
            Functions.GoToSite("https://tuke-productions.de/mein-konto/");
        }

        protected override void LoginSite(object parameter)
        {
            Functions.GoToSite("https://tuke-productions.de/mein-konto/");
        }
    }
}