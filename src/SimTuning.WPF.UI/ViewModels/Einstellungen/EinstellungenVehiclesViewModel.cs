﻿// project=SimTuning.WPF.UI, file=EinstellungenVehiclesViewModel.cs, creation=2020:9:2
// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using SimTuning.Core;
    using SimTuning.WPF.UI.Business;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Einstellungen-Update-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.VehiclesViewModel" />
    public class EinstellungenVehiclesViewModel : SimTuning.Core.ViewModels.Einstellungen.VehiclesViewModel
    {
        private bool firstTime = true;

        public MvxAsyncCommand OpenMenuCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenVehiclesViewModel"
        /// /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenVehiclesViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
            this.NewVehicleCommand = new MvxCommand(NewVehicle, CanExecute);
            this.DeleteVehicleCommand = new MvxCommand(DeleteVehicle, CanExecute);
            this.SaveVehicleCommand = new MvxCommand(SaveVehicle, CanExecute);
            OpenMenuCommand = new MvxAsyncCommand(() => NavigationService.Navigate<EinstellungenMenuViewModel>());
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
        /// Prepares the specified user.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Views the appeared.
        /// </summary>
        public override void ViewAppeared()
        {
            base.ViewAppeared();
            this.firstTime = false;
        }

        /// <summary>
        /// Deletes the vehicle.
        /// </summary>
        protected override void DeleteVehicle()
        {
            try
            {
                base.DeleteVehicle();
            }
            catch
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        /// <summary>
        /// Creates new vehicle.
        /// </summary>
        protected override void NewVehicle()
        {
            try
            {
                base.NewVehicle();
            }
            catch
            {
                Functions.ShowSnackbarDialog("Fehler beim erstellen");
            }
        }

        /// <summary>
        /// Saves the vehicle.
        /// </summary>
        protected override void SaveVehicle()
        {
            try
            {
                base.SaveVehicle();
            }
            catch
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        /// <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        private bool CanExecute()
        {
            if (!UserSettings.LicenseValid)
            {
                if (!firstTime)
                {
                    Functions.ShowSnackbarDialog("Kaufe die Pro Version um Presets zu ändern");
                }
            }

            return UserSettings.LicenseValid;
        }

        #endregion Methods
    }
}