// project=SimTuning.Core, file=Menu.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels
{
    using System.Threading.Tasks;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;

    /// <summary>
    /// Menu-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class Menu : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public Menu(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.User = new UserModel();
        }

        #region Methods

        /// <summary>
        /// Prepares this instance.
        /// called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        protected virtual void LoginUser()
        {
        }

        #endregion Methods

        #region Values

        #region Commands

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

        #endregion Commands

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
    }
}