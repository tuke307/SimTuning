// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core;
    using SimTuning.Forms.UI.Helpers;
    using SimTuning.Forms.UI.ViewModels.Auslass;
    using SimTuning.Forms.UI.ViewModels.Demo;
    using SimTuning.Forms.UI.ViewModels.Dyno;
    using SimTuning.Forms.UI.ViewModels.Einlass;
    using SimTuning.Forms.UI.ViewModels.Einstellungen;
    using SimTuning.Forms.UI.ViewModels.Home;
    using SimTuning.Forms.UI.ViewModels.Motor;
    using SimTuning.Forms.UI.ViewModels.Tuning;
    using System.IO;
    using System.Threading.Tasks;
    using Xamarin.Essentials;

    /// <summary>
    /// MenuViewModel.
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
        }

        #region Values

        private readonly ILogger<MenuViewModel> _logger;

        #endregion Values

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

            this.InitializeDatabase.ExecuteAsync();

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void ViewAppeared()
        {
            base.ViewAppeared();

            this.LoginUserCommand.Execute();
        }

        /// <inheritdoc />
        protected override async Task InitializeDatabaseAsync()
        {
            // da die GetPermission Funktionen teilweise zu lange dauern oder die funktion beenden wird der Task davor gestartet.
            _ = Task.Run(async () =>
            {
                // 5 sekunden warten => solange ist zeit um die permission zu geben.
                await Task.Delay(5000).ConfigureAwait(true);

                // since android 10, database has to be created at the first time
                if (!File.Exists(Data.DatabaseSettings.DatabasePath))
                {
                    var fs = File.Create(Data.DatabaseSettings.DatabasePath);
                    fs.Dispose();
                }

                await base.InitializeDatabaseAsync().ConfigureAwait(true);
            }
               ).ConfigureAwait(false);

            await Functions.GetPermission<Permissions.StorageRead>().ConfigureAwait(true);
            await Functions.GetPermission<Permissions.StorageWrite>().ConfigureAwait(true);
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        protected async Task LoginUser()
        {
            //var result = await API.Login.UserLoginAsync().ConfigureAwait(true);
            //SimTuning.Core.UserSettings.User = result.Item1;
            //SimTuning.Core.UserSettings.Order = result.Item2;

            //Functions.ShowSnackbarDialog(result.Item3);

            this.ReloadUserAsync();
        }

        /// <summary>
        /// Shows the dyno.
        /// </summary>
        private async Task ShowDyno()
        {
            if (UserSettings.LicenseValid)
            {
                await this._navigationService.Navigate<DynoDataViewModel>().ConfigureAwait(true);
            }
            else
            {
                await this._navigationService.Navigate<DemoMainViewModel>().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Shows the tuning.
        /// </summary>
        private async Task ShowTuning()
        {
            if (UserSettings.LicenseValid)
            {
                await this._navigationService.Navigate<TuningMainViewModel>().ConfigureAwait(true);
            }
            else
            {
                await this._navigationService.Navigate<DemoMainViewModel>().ConfigureAwait(true);
            }
        }

        #endregion Methods
    }
}