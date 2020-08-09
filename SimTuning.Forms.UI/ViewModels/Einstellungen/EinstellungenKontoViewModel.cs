using API;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.Forms.UI.Business;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    public class EinstellungenKontoViewModel : SimTuning.Core.ViewModels.Einstellungen.KontoViewModel
    {
        public EinstellungenKontoViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            //override commands
            ConnectUserCommand = new MvxAsyncCommand(ConnectUser);
            RegisterSiteCommand = new MvxCommand(RegisterSite);
        }

        public override void Prepare(UserModel _user)
        {
            base.Prepare(_user);
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        protected new async Task ConnectUser()
        {
            var result = await API.API.UserLoginAsync(email: Email, password: Core.Business.Converts.StringToSecureString(Password)).ConfigureAwait(true);
            User = result.Item1;

            Functions.ShowSnackbarDialog(result.Item2);
        }

        protected override void RegisterSite()
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