using Data.Models;
using System.Security;
using System.Windows.Input;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.ViewModels.Einstellungen
{
    public class KontoViewModel : MvxViewModel
    {
        public KontoViewModel()
        {
            SimTuning.Business.Functions.GetLoginCredentials(out string _email, out SecureString _password);
            Email = _email;
        }

        public ICommand ConnectUserCommand { get; set; }
        public ICommand RegisterSiteCommand { get; set; }
        public ICommand LoginSiteCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        #region Commmands

        protected virtual void PasswordChanged(object parameter)
        {
        }

        protected virtual void ConnectUser(object parameter)
        {
        }

        protected virtual void RegisterSite(object parameter)
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