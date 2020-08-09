using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels
{
    public class Menu : MvxNavigationViewModel
    {
        public Menu(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            User = new UserModel();
        }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        public MvxCommand ButtonOpenMenu { get; set; }
        public MvxCommand ButtonCloseMenu { get; set; }
        public IMvxAsyncCommand ShowHomeCommand { get; set; }
        public IMvxAsyncCommand ShowEinlassCommand { get; set; }
        public IMvxAsyncCommand ShowAuslassCommand { get; set; }
        public IMvxAsyncCommand ShowMotorCommand { get; set; }
        public IMvxAsyncCommand ShowDynoCommand { get; set; }
        public IMvxAsyncCommand ShowTuningCommand { get; set; }
        public IMvxAsyncCommand ShowEinstellungenCommand { get; set; }
        public IMvxAsyncCommand LoginUserCommand { get; protected set; }

        #region Values

        public bool LicenseValid
        {
            get
            {
                return this.User.LicenseValid;
            }
        }

        public bool UserValid
        {
            get
            {
                return this.User.UserValid;
            }
        }

        private UserModel _user;

        public UserModel User
        {
            get => _user;
            set
            {
                this.SetProperty(ref _user, value);

                this.RaisePropertyChanged(() => UserValid);
                this.RaisePropertyChanged(() => LicenseValid);
            }
        }

        #endregion Values

        protected virtual void LoginUser()
        {
        }
    }
}