// project=SimTuning.Forms.UI, file=MenuViewModel.cs, creation=2020:7:2 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core;
    using SimTuning.Core.Models;
    using SimTuning.Forms.UI.Business;
    using SimTuning.Forms.UI.ViewModels.Auslass;
    using SimTuning.Forms.UI.ViewModels.Demo;
    using SimTuning.Forms.UI.ViewModels.Dyno;
    using SimTuning.Forms.UI.ViewModels.Einlass;
    using SimTuning.Forms.UI.ViewModels.Einstellungen;
    using SimTuning.Forms.UI.ViewModels.Home;
    using SimTuning.Forms.UI.ViewModels.Motor;
    using SimTuning.Forms.UI.ViewModels.Tuning;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Xamarin.Essentials;

    /// <summary>
    /// MenuViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Menu" />
    public class MenuViewModel : SimTuning.Core.ViewModels.Menu
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MenuViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxMessenger messenger)
            : base(logProvider, navigationService, messenger)
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
        public override Task Initialize()
        {
            this.InitializeDatabase.ExecuteAsync();

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
            await Functions.GetPermission<Permissions.StorageRead>().ConfigureAwait(true);
            await Functions.GetPermission<Permissions.StorageWrite>().ConfigureAwait(true);

            // android: "/data/user/0/com.tuke_productions.SimTuning/files/"
            SimTuning.Core.GeneralSettings.FileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            if (string.IsNullOrEmpty(Data.DatabaseSettings.DatabasePath))
            {
                Data.DatabaseSettings.DatabasePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, Data.DatabaseSettings.DatabaseName);
            }

            // since android 10, database has to be created at the first time
            if (!File.Exists(Data.DatabaseSettings.DatabasePath))
            {
                var fs = File.Create(Data.DatabaseSettings.DatabasePath);
                fs.Dispose();
            }

            await base.InitializeDatabaseAsync().ConfigureAwait(true);
        }

        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        protected async Task LoginUser()
        {
            var result = await API.Login.UserLoginAsync().ConfigureAwait(true);
            SimTuning.Core.UserSettings.User = result.Item1;
            SimTuning.Core.UserSettings.Order = result.Item2;

            Functions.ShowSnackbarDialog(result.Item3);

            this.ReloadUserAsync();
        }

        /// <summary>
        /// Shows the dyno.
        /// </summary>
        private async Task ShowDyno()
        {
            if (UserSettings.LicenseValid)
            {
                await this._navigationService.Navigate</*DynoMainViewModel*/DynoDataViewModel>().ConfigureAwait(true);
            }
            else
            {
                //await this._navigationService.Navigate</*DynoMainViewModel*/DynoDataViewModel>().ConfigureAwait(true);
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