// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core;
    using SimTuning.Core.Services;
    using SimTuning.Forms.UI.Helpers;
    using System;
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
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenVehiclesViewModel(
            ILogger<EinstellungenVehiclesViewModel> logger,
            IMvxNavigationService navigationService,
            IVehicleService vehicleService,
            IMvxMessenger messenger)
            : base(logger, navigationService, vehicleService, messenger)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.NewVehicleCommand = new MvxCommand(this.NewVehicle, this.CanExecute);
            this.DeleteVehicleCommand = new MvxCommand(this.DeleteVehicle, this.CanExecute);
            this.SaveVehicleCommand = new MvxCommand(this.SaveVehicle, this.SaveCanExecute);

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc />
        protected override bool CanExecute()
        {
            var ret = base.CanExecute();

            if (!ret)
            {
                Functions.ShowSnackbarDialog(SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "MES_PRO"));
            }

            return ret;
        }

        /// <inheritdoc cref="Core.ViewModels.Einstellungen.VehiclesViewModel.DeleteVehicle" />
        protected new void DeleteVehicle()
        {
            if (!base.DeleteVehicle())
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        /// <inheritdoc cref="Core.ViewModels.Einstellungen.VehiclesViewModel.NewVehicle" />
        protected new void NewVehicle()
        {
            if (!base.NewVehicle())
            {
                Functions.ShowSnackbarDialog("Fehler beim erstellen");
            }
        }

        /// <inheritdoc cref="Core.ViewModels.Einstellungen.VehiclesViewModel.SaveVehicle" />
        protected new void SaveVehicle()
        {
            if (!base.SaveVehicle())
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<EinstellungenVehiclesViewModel> _logger;

        #endregion Values
    }
}