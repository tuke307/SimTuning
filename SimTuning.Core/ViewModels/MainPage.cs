using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using SimTuning.Core.Models;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels
{
    public class MainPage : MvxNavigationViewModel
    {
        public MainPage(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            User = new UserModel();
        }

        public IMvxAsyncCommand ShowHomeViewModelCommand { get; protected set; }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; protected set; }
        public IMvxAsyncCommand LoginUserCommand { get; protected set; }

        public override void Prepare()
        {
            // This is the first method to be called after construction
        }

        public override Task Initialize()
        {
            // Async initialization

            return base.Initialize();
        }

        protected virtual void LoginUser()
        {
        }

        #region Values

        private UserModel _user;

        public UserModel User
        {
            get => _user;
            set { SetProperty(ref _user, value); }
        }

        #endregion Values
    }
}