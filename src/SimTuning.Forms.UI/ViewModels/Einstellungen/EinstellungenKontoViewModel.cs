// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Services;
    using SimTuning.Forms.UI.Helpers;
    using System.Threading.Tasks;

    /// <summary>
    /// EinstellungenKontoViewModel.
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
            // override commands
            this.ConnectUserCommand = new MvxAsyncCommand(this.ConnectUser);

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
            //var result = await API.Login.UserLoginAsync(email: this.Email, password: Core.Converters.Converts.StringToSecureString(this.Password)).ConfigureAwait(true);
            //SimTuning.Core.UserSettings.User = result.Item1;
            //SimTuning.Core.UserSettings.Order = result.Item2;

            //Functions.ShowSnackbarDialog(result.Item3);

            base.ConnectUser();
        }

        #endregion Methods

        #region Values

        private readonly ILogger<EinstellungenKontoViewModel> _logger;
        private string _password;

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        #endregion Values
    }
}