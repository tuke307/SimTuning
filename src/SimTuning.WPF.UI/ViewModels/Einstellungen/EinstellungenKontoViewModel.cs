// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Services;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Messages;
    using System.Security;
    using System.Threading.Tasks;
    using System.Windows.Controls;

    /// <summary>
    /// WPF-spezifisches Einstellungen-Konto-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.KontoViewModel" />
    public class EinstellungenKontoViewModel : SimTuning.Core.ViewModels.Einstellungen.KontoViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenKontoViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenKontoViewModel(
            ILogger<EinstellungenKontoViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger,
            IBrowserService browserService)
            : base(logger, navigationService, messenger, browserService)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            // override Commands
            this.ConnectUserCommand = new MvxAsyncCommand(ConnectUser);

            this.PasswordChangedCommand = new MvxCommand<object>(PasswordChanged);
            OpenMenuCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenMenuViewModel>());

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc cref="Core.ViewModels.Einstellungen.KontoViewModel.ConnectUser" />
        protected new async Task ConnectUser()
        {
            //var result = await Login.UserLoginAsync(email: Email, password: Password).ConfigureAwait(true);
            //SimTuning.Core.UserSettings.User = result.Item1;
            //SimTuning.Core.UserSettings.Order = result.Item2;

            //foreach (var item in result.Item3) { _messenger.Publish(new ShowSnackbarMessage(this, item)); }

            base.ConnectUser();
        }

        /// <summary>
        /// Passwords the changed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        protected override void PasswordChanged(object parameter)
        {
            if (parameter is PasswordBox passwordBox)
            {
                this.Password = passwordBox.SecurePassword;
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<EinstellungenKontoViewModel> _logger;
        private SecureString Password;

        public MvxAsyncCommand OpenMenuCommand { get; set; }

        #endregion Values
    }
}