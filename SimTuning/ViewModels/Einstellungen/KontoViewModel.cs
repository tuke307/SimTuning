using Data.Models;
using System.Security;
using System.Windows.Input;

namespace SimTuning.ViewModels.Einstellungen
{
    public class KontoViewModel : BaseViewModel
    {
        //private readonly MainWindowViewModel mainWindowViewModel;

        public KontoViewModel(/*MainWindowViewModel mainWindowViewModel*/)
        {
            //this.mainWindowViewModel = mainWindowViewModel;

            SimTuning.Business.Functions.GetLoginCredentials(out string _email, out SecureString _password);
            Email = _email;

            //ConnectUserCommand = new ActionCommand(ConnectUser);
            //RegisterSiteCommand = new ActionCommand(RegisterSite);
            //LoginSiteCommand = new ActionCommand(LoginSite);
            //PasswordChangedCommand = new ActionCommand(PasswordChanged);
        }

        protected virtual void PasswordChanged(object parameter)
        {
            //var passwordBox = parameter as PasswordBox;
            //Password = passwordBox.SecurePassword;
        }

        public ICommand ConnectUserCommand { get; set; }
        public ICommand RegisterSiteCommand { get; set; }
        public ICommand LoginSiteCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        protected virtual void ConnectUser(object parameter)
        {
            //var tuple = await API.API.UserLoginAsync(email: Email, password: Password);
            //mainWindowViewModel.USER_VALID = tuple.Item1;
            //mainWindowViewModel.LICENSE_VALID = tuple.Item2;

            //for (int i = 0; i < tuple.Item3.Count; i++)
            //    mainWindowViewModel.NotificationSnackbar.Enqueue(tuple.Item3[i]);
        }

        protected virtual void RegisterSite(object parameter)
        {
            //Business.Functions.GoToSite("https://tuke-productions.de/mein-konto/");
        }

        protected virtual void LoginSite(object parameter)
        {
            //Business.Functions.GoToSite("https://tuke-productions.de/mein-konto/");
        }

        protected SettingsModel settings
        {
            get => Get<SettingsModel>();
            set => Set(value);
        }

        public string Email
        {
            get => Get<string>();
            set => Set(value);
        }

        public string firstname
        {
            get => Get<string>();
            set => Set(value);
        }

        public string lastname
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}