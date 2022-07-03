// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using SimTuning.Core.Services;
using System.Security;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Einstellungen
{
    /// <summary>
    /// Einstellungen-Konto-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class KontoViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="KontoViewModel"/> class. </summary> <param name="logger"><inheritdoc cref="ILogger" path="/summary/node()" /></param> <param
        /// name="navigationService"><inheritdoc cref="IMvxNavigationService" path="/summary/node()" /></param <param name="messenger"><inheritdoc cref="IMvxMessenger" path="/summary/node()"
        /// /></param>
        public KontoViewModel(
            ILogger<KontoViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger,
            IBrowserService browserService)
        {
            this._logger = logger;
            this._messenger = messenger;
            this._navigationService = navigationService;
            this._browserService = browserService;
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.RegisterSiteCommand = new MvxCommand(() => _browserService.OpenBrowser(SimTuning.Core.WebsiteConstants.MyWebsite));

            SimTuning.Core.Helpers.Functions.GetLoginCredentials(out string _email, out SecureString _password);
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
            //var message = new Core.Models.MvxUserReloadMessage(this);

            //this._messenger.Publish(message);
        }

        protected virtual void PasswordChanged(object parameter)
        {
        }

        #endregion Methods

        #region Values

        #region Commands

        private readonly ILogger<KontoViewModel> _logger;

        public IMvxAsyncCommand ConnectUserCommand { get; set; }

        public IMvxCommand PasswordChangedCommand { get; set; }

        public IMvxCommand RegisterSiteCommand { get; set; }

        #endregion Commands

        protected readonly IMvxMessenger _messenger;
        protected readonly IMvxNavigationService _navigationService;
        private readonly IBrowserService _browserService;
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