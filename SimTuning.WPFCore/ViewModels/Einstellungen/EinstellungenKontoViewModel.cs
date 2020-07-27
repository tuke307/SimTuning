using API;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.WPFCore.Business;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimTuning.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenKontoViewModel : SimTuning.Core.ViewModels.Einstellungen.KontoViewModel
    {
        //private readonly MainWindowViewModel mainWindowViewModel;
        private SecureString Password;

        public EinstellungenKontoViewModel/*MainWindowViewModel mainWindowViewModel*/(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //this.mainWindowViewModel = mainWindowViewModel;

            //override Commands
            ConnectUserCommand = new MvxAsyncCommand(ConnectUser);
            RegisterSiteCommand = new MvxCommand(RegisterSite);

            PasswordChangedCommand = new MvxCommand<string>(PasswordChanged);
        }

        public override void Prepare(UserModel _user)
        {
            base.Prepare(_user);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        protected override void PasswordChanged(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            Password = passwordBox.SecurePassword;
        }

        protected new async Task ConnectUser()
        {
            var tuple = await API.API.UserLoginAsync(email: Email, password: Password);
            User.UserValid = tuple.Item1;
            User.LicenseValid = tuple.Item2;

            for (int i = 0; i < tuple.Item3.Count; i++)
            {
                //mainWindowViewModel.NotificationSnackbar.Enqueue(tuple.Item3[i]);
            }
        }

        protected override void RegisterSite()
        {
            Functions.GoToSite("https://tuke-productions.de/mein-konto/");
        }
    }
}