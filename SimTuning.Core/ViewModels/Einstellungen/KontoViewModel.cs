using Data.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Security;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Einstellungen
{
    public class KontoViewModel : MvxNavigationViewModel<SimTuning.Core.Models.UserModel>
    {
        public SimTuning.Core.Models.UserModel User { get; protected set; }

        public KontoViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public IMvxAsyncCommand ConnectUserCommand { get; set; }
        public IMvxCommand RegisterSiteCommand { get; set; }
        public IMvxCommand PasswordChangedCommand { get; set; }

        public override void Prepare(SimTuning.Core.Models.UserModel _user)
        {
            // This is the first method to be called after construction

            User = _user;
        }

        public override Task Initialize()
        {
            SimTuning.Core.Business.Functions.GetLoginCredentials(out string _email, out SecureString _password);
            Email = _email;

            return base.Initialize();
        }

        #region Commmands

        protected virtual void PasswordChanged(object parameter)
        {
        }

        protected virtual void ConnectUser()
        {
        }

        protected virtual void RegisterSite()
        {
        }

        protected virtual void LoginSite(object parameter)
        {
        }

        #endregion Commmands

        #region Values

        protected SettingsModel settings;

        private string _email;

        public string Email
        {
            get => _email;
            set { SetProperty(ref _email, value); }
        }

        private string _firstname;

        public string Firstname
        {
            get => _firstname;
            set { SetProperty(ref _firstname, value); }
        }

        private string _lastname;

        public string Lastname
        {
            get => _lastname;
            set { SetProperty(ref _lastname, value); }
        }

        #endregion Values
    }
}