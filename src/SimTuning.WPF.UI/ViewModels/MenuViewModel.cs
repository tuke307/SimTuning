// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels
{
    using MaterialDesignThemes.Wpf;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core;
    using SimTuning.WPF.UI.Messages;
    using SimTuning.WPF.UI.ViewModels.Auslass;
    using SimTuning.WPF.UI.ViewModels.Demo;
    using SimTuning.WPF.UI.ViewModels.Dyno;
    using SimTuning.WPF.UI.ViewModels.Einlass;
    using SimTuning.WPF.UI.ViewModels.Einstellungen;
    using SimTuning.WPF.UI.ViewModels.Home;
    using SimTuning.WPF.UI.ViewModels.Motor;
    using SimTuning.WPF.UI.ViewModels.Tuning;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Menu-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.MenuViewModel" />
    public class MenuViewModel : SimTuning.Core.ViewModels.MenuViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MenuViewModel(
            ILogger<MenuViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger)
            : base(logger, navigationService, messenger)
        {
            this._logger = logger;
            _token = messenger.Subscribe<ShowSnackbarMessage>(OnSnackbarMessage);
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.ShowHomeCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<HomeMainViewModel>());

            this.ShowEinlassCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinlassMainViewModel>());

            this.ShowAuslassCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<AuslassMainViewModel>());

            this.ShowMotorCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<MotorMainViewModel>());

            this.ShowDynoCommand = new MvxAsyncCommand(this.ShowDyno);

            this.ShowTuningCommand = new MvxAsyncCommand(this.ShowTuning);

            this.ShowEinstellungenCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenMenuViewModel>());

            this.LoginUserCommand = new MvxAsyncCommand(this.LoginUser);

            this.InitializeDatabase = new MvxAsyncCommand(this.InitializeDatabaseAsync);

            this.InitializeDatabase.Execute();

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void ViewAppeared()
        {
            base.ViewAppeared();

            this.LoginUserCommand.Execute();
        }

        /// <inheritdoc cref="Core.ViewModels.MenuViewModel.InitializeDatabaseAsync" />
        protected override async Task InitializeDatabaseAsync()
        {
            await base.InitializeDatabaseAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        protected new async Task LoginUser()
        {
            //var result = await API.Login.UserLoginAsync().ConfigureAwait(true);

            //SimTuning.Core.UserSettings.User = result.Item1; SimTuning.Core.UserSettings.Order = result.Item2;

            //if (result.Item3 != null) { foreach (var item in result.Item3) { _messenger.Publish(new ShowSnackbarMessage(this, item)); } }

            this.ReloadUserAsync();
        }

        /// <summary>
        /// Called when [snackbar message].
        /// </summary>
        /// <param name="message">The message.</param>
        private void OnSnackbarMessage(ShowSnackbarMessage message)
        {
            BoundMessageQueue.Enqueue(message.Message);
        }

        /// <summary>
        /// Shows the dyno.
        /// </summary>
        private async Task ShowDyno()
        {
            if (UserSettings.LicenseValid)
            {
                await _navigationService.Navigate<DynoMainViewModel>();
            }
            else
            {
                await _navigationService.Navigate<DemoMainViewModel>();
            }
        }

        /// <summary>
        /// Shows the tuning.
        /// </summary>
        private async Task ShowTuning()
        {
            if (UserSettings.LicenseValid)
            {
                await _navigationService.Navigate<TuningMainViewModel>();
            }
            else
            {
                await _navigationService.Navigate<DemoMainViewModel>();
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<MenuViewModel> _logger;
        private readonly MvxSubscriptionToken _token;

        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue();

        #endregion Values
    }
}