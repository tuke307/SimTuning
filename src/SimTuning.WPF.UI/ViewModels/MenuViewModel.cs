// project=SimTuning.WPF.UI, file=MenuViewModel.cs, creation=2020:9:2 Copyright (c) 2021
// tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core;
    using SimTuning.WPF.UI.ViewModels.Auslass;
    using SimTuning.WPF.UI.ViewModels.Demo;
    using SimTuning.WPF.UI.ViewModels.Dyno;
    using SimTuning.WPF.UI.ViewModels.Einlass;
    using SimTuning.WPF.UI.ViewModels.Einstellungen;
    using SimTuning.WPF.UI.ViewModels.Home;
    using SimTuning.WPF.UI.ViewModels.Motor;
    using SimTuning.WPF.UI.ViewModels.Tuning;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Menu-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Menu" />
    public class MenuViewModel : SimTuning.Core.ViewModels.Menu
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MenuViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logFactory, navigationService, messenger)
        {
            this._navigationService = navigationService;

            this.ShowHomeCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<HomeMainViewModel>());

            this.ShowEinlassCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinlassMainViewModel>());

            this.ShowAuslassCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<AuslassMainViewModel>());

            this.ShowMotorCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<MotorMainViewModel>());

            this.ShowDynoCommand = new MvxAsyncCommand(this.ShowDyno);

            this.ShowTuningCommand = new MvxAsyncCommand(this.ShowTuning);

            this.ShowEinstellungenCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenMenuViewModel>());

            this.LoginUserCommand = new MvxAsyncCommand(this.LoginUser);

            this.InitializeDatabase = new MvxAsyncCommand(this.InitializeDatabaseAsync);
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initialisierung.</returns>
        public override Task Initialize()
        {
            this.InitializeDatabase.Execute();

            return base.Initialize();
        }

        /// <summary>
        /// Views the appeared.
        /// </summary>
        public override void ViewAppeared()
        {
            base.ViewAppeared();

            this.LoginUserCommand.Execute();
        }

        /// <summary>
        /// Initializes the database asynchronous.
        /// </summary>
        protected override async Task InitializeDatabaseAsync()
        {
            await base.InitializeDatabaseAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        protected new async Task LoginUser()
        {
            var result = await API.Login.UserLoginAsync().ConfigureAwait(true);
            SimTuning.Core.UserSettings.User = result.Item1;
            SimTuning.Core.UserSettings.Order = result.Item2;

            Business.Functions.ShowSnackbarDialog(result.Item3);

            this.ReloadUserAsync();
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
    }
}