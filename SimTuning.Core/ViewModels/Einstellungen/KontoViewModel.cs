// project=SimTuning.Core, file=KontoViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
using Data.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Security;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Einstellungen
{
    /// <summary>
    /// Einstellungen-Konto-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel{SimTuning.Core.Models.UserModel}" />
    public class KontoViewModel : MvxNavigationViewModel<SimTuning.Core.Models.UserModel>
    {
        public SimTuning.Core.Models.UserModel User { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KontoViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public KontoViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            SimTuning.Core.Business.Functions.GetLoginCredentials(out string _email, out SecureString _password);
            this.Email = _email;

            return base.Initialize();
        }

        /// <summary>
        /// Prepares the specified user.
        /// </summary>
        /// <param name="_user">The user.</param>
        public override void Prepare(SimTuning.Core.Models.UserModel _user)
        {
            this.User = _user;
        }

        protected virtual void ConnectUser()
        {
        }

        protected virtual void LoginSite(object parameter)
        {
        }

        protected virtual void PasswordChanged(object parameter)
        {
        }

        protected virtual void RegisterSite()
        {
        }

        #endregion Methods

        #region Values

        #region Commands

        public IMvxAsyncCommand ConnectUserCommand { get; set; }

        public IMvxCommand PasswordChangedCommand { get; set; }

        public IMvxCommand RegisterSiteCommand { get; set; }

        #endregion Commands

        private string _email;

        private string _firstname;

        private string _lastname;

        public string Email
        {
            get => _email;
            set { SetProperty(ref _email, value); }
        }

        public string Firstname
        {
            get => _firstname;
            set { SetProperty(ref _firstname, value); }
        }

        public string Lastname
        {
            get => _lastname;
            set { SetProperty(ref _lastname, value); }
        }

        #endregion Values
    }
}