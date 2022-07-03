// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using SimTuning.Data;
    using System.Threading.Tasks;

    /// <summary>
    /// Menu-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class MenuViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="MenuViewModel"/> class. </summary> <param name="logger"><inheritdoc cref="ILogger" path="/summary/node()" /></param> <param
        /// name="navigationService"><inheritdoc cref="IMvxNavigationService" path="/summary/node()" /></param <param name="messenger"><inheritdoc cref="IMvxMessenger" path="/summary/node()"
        /// /></param>
        public MenuViewModel(
            ILogger<MenuViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger)
        {
            this._logger = logger;
            this._navigationService = navigationService;
            this._messenger = messenger;
            this._token = messenger.Subscribe<MvxUserReloadMessage>(this.ReloadUserAsync);
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        /// <summary>
        /// Initializes the database asynchronous.
        /// </summary>
        protected virtual async Task InitializeDatabaseAsync()
        {
            using (var db = new DatabaseContext())
            {
                await db.Database.MigrateAsync().ConfigureAwait(true);
                await db.Database.EnsureCreatedAsync().ConfigureAwait(true);
            }
        }

        protected virtual void ReloadUserAsync(MvxUserReloadMessage mvxUserReloadMessage = null)
        {
            this.RaisePropertyChanged(() => this.LicenseValid).ConfigureAwait(true);
            this.RaisePropertyChanged(() => this.UserValid).ConfigureAwait(true);
        }

        #endregion Methods

        #region Values

        #region Commands

        private readonly ILogger<MenuViewModel> _logger;

        public MvxCommand ButtonCloseMenu { get; set; }

        public MvxCommand ButtonOpenMenu { get; set; }

        public IMvxAsyncCommand InitializeDatabase { get; protected set; }

        public IMvxAsyncCommand LoginUserCommand { get; protected set; }

        public IMvxAsyncCommand ShowAuslassCommand { get; set; }

        public IMvxAsyncCommand ShowDynoCommand { get; set; }

        public IMvxAsyncCommand ShowEinlassCommand { get; set; }

        public IMvxAsyncCommand ShowEinstellungenCommand { get; set; }

        public IMvxAsyncCommand ShowHomeCommand { get; set; }

        public IMvxAsyncCommand ShowMotorCommand { get; set; }

        public IMvxAsyncCommand ShowTuningCommand { get; set; }

        #endregion Commands

        protected readonly IMvxMessenger _messenger;
        protected readonly IMvxNavigationService _navigationService;
        protected readonly MvxSubscriptionToken _token;

        public bool LicenseValid
        {
            get => UserSettings.LicenseValid;
        }

        public bool UserValid
        {
            get => UserSettings.UserValid;
        }

        #endregion Values
    }
}