﻿// project=SimTuning.WPF.UI, file=EinstellungenVehiclesViewModel.cs, creation=2020:7:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Models;
    using SimTuning.WPF.UI.Business;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Einstellungen-Update-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.VehiclesViewModel" />
    public class EinstellungenVehiclesViewModel : SimTuning.Core.ViewModels.Einstellungen.VehiclesViewModel
    {
        private bool firstTime = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenVehiclesViewModel"
        /// /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenVehiclesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.NewVehicleCommand = new MvxCommand(NewVehicle, CanExecute);
            this.DeleteVehicleCommand = new MvxCommand(DeleteVehicle, CanExecute);
            this.SaveVehicleCommand = new MvxCommand(SaveVehicle, CanExecute);
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
        /// <param name="_user">The user.</param>
        public override void Prepare(UserModel _user)
        {
            base.Prepare(_user);
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
            if (!this.User.LicenseValid)
            {
                if (!firstTime)
                {
                    Functions.ShowSnackbarDialog("Kaufe die Pro Version um Presets zu ändern");
                }
            }

            return this.User.LicenseValid;
        }

        #endregion Methods
    }
}