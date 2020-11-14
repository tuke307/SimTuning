// project=SimTuning.Core, file=KontoViewModel.cs, creation=2020:7:31 Copyright (c) 2020
// tuke productions. All rights reserved.
using Data.Models;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System.Security;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Einstellungen
{
    /// <summary>
    /// Einstellungen-Konto-ViewModel.
    /// </summary>

    public class KontoViewModel : MvxNavigationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KontoViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public KontoViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logProvider, navigationService)
        {
            this._messenger = messenger;

            this.RegisterSiteCommand = new MvxCommand(() => SimTuning.Core.Business.Functions.OpenSite(SimTuning.Core.WebsiteConstants.MyWebsite));
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

        public override void Prepare()
        {
            base.Prepare();
        }

        protected virtual void ConnectUser()
        {
            var message = new Core.Models.MvxUserReloadMessage(this);

            this._messenger.Publish(message);
        }

        protected virtual void PasswordChanged(object parameter)
        {
        }

        #endregion Methods

        #region Values

        #region Commands

        public IMvxAsyncCommand ConnectUserCommand { get; set; }

        public IMvxCommand PasswordChangedCommand { get; set; }

        public IMvxCommand RegisterSiteCommand { get; set; }

        #endregion Commands

        protected readonly IMvxMessenger _messenger;
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