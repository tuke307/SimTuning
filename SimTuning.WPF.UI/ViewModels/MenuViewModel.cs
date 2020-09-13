﻿// project=SimTuning.WPF.UI, file=MenuViewModel.cs, creation=2020:9:2 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core;
    using SimTuning.Core.Models;
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
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MenuViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;

            this.ShowHomeCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<HomeMainViewModel>());

            this.ShowEinlassCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinlassMainViewModel>());

            this.ShowAuslassCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<AuslassMainViewModel>());

            this.ShowMotorCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<MotorMainViewModel>());

            this.ShowDynoCommand = new MvxAsyncCommand(this.ShowDyno);

            this.ShowTuningCommand = new MvxAsyncCommand(this.ShowTuning);

            this.ShowEinstellungenCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenMainViewModel>());

            this.LoginUserCommand = new MvxAsyncCommand(this.LoginUser);
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initialisierung.</returns>
        public override Task Initialize()
        {
            SimTuning.Core.GeneralSettings.FileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SimTuning"); // appdata-local-simtunig

            if (string.IsNullOrEmpty(Data.DatabaseSettings.DatabasePath))
            {
                Data.DatabaseSettings.DatabasePath = Path.Combine(SimTuning.Core.GeneralSettings.FileDirectory, Data.DatabaseSettings.DatabaseName);
            }

            if (!Directory.Exists(SimTuning.Core.GeneralSettings.FileDirectory))
            {
                Directory.CreateDirectory(SimTuning.Core.GeneralSettings.FileDirectory);
            }

            using (var db = new DatabaseContext())
            {
                db.Database.Migrate();
            }

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
        /// Logins the user.
        /// </summary>
        protected new async Task LoginUser()
        {
            var result = await API.Login.UserLoginAsync().ConfigureAwait(true);
            SimTuning.Core.UserSettings.User = result.Item1;
            SimTuning.Core.UserSettings.Order = result.Item2;
            SimTuning.Core.UserSettings.UserValid = result.Item3;
            SimTuning.Core.UserSettings.LicenseValid = result.Item4;

            Business.Functions.ShowSnackbarDialog(result.Item4);
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