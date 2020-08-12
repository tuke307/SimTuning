// project=SimTuning.Forms.UI, file=EinstellungenVehiclesViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Models;
    using SimTuning.Forms.UI.Business;
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    /// <summary>
    /// EinstellungenVehiclesViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.VehiclesViewModel" />
    public class EinstellungenVehiclesViewModel : SimTuning.Core.ViewModels.Einstellungen.VehiclesViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenVehiclesViewModel"
        /// /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenVehiclesViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this.NewVehicleCommand = new MvxCommand(this.NewVehicle, this.CanExecute);
            this.DeleteVehicleCommand = new MvxCommand(this.DeleteVehicle, this.CanExecute);
            this.SaveVehicleCommand = new MvxCommand(this.SaveVehicle, this.CanExecute);
        }

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
        /// Deletes the vehicle.
        /// </summary>
        protected override void DeleteVehicle()
        {
            try
            {
                base.DeleteVehicle();
            }
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
                Functions.ShowSnackbarDialog(this.rm.GetString("MES_PRO", CultureInfo.CurrentCulture));
            }

            return this.User.LicenseValid;
        }
    }
}