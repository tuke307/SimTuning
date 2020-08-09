using API;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.Forms.WPFCore.Business;
using System.Security;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SimTuning.Forms.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenKontoViewModel : SimTuning.Core.ViewModels.Einstellungen.KontoViewModel
    {
        private SecureString Password;

        public EinstellungenKontoViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //override Commands
            ConnectUserCommand = new MvxAsyncCommand(ConnectUser);
            RegisterSiteCommand = new MvxCommand(RegisterSite);

            PasswordChangedCommand = new MvxCommand<object>(PasswordChanged);
        }

        public override void Prepare(UserModel _user)
        {
            base.Prepare(_user);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        protected override void PasswordChanged(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            if (passwordBox != null)
            {
                Password = passwordBox.SecurePassword;
            }
        }

        protected new async Task ConnectUser()
        {
            var result = await API.Login.UserLoginAsync(email: Email, password: Password);
            User = result.Item1;
            Functions.ShowSnackbarDialog(result.Item2);
        }

        protected override void RegisterSite()
        {
            Functions.GoToSite("https://tuke-productions.de/mein-konto/");
        }
    }
}