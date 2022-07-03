// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core;
    using SimTuning.Core.Services;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Messages;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Einstellungen-Update-ViewModel.
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
            this.NewVehicleCommand = new MvxCommand(NewVehicle, CanExecute);
            this.DeleteVehicleCommand = new MvxCommand(DeleteVehicle, CanExecute);
            this.SaveVehicleCommand = new MvxCommand(SaveVehicle, SaveCanExecute);
            OpenMenuCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenMenuViewModel>());

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc />
        public override void ViewAppeared()
        {
            base.ViewAppeared();

            this.firstTime = false;
        }

        /// <inheritdoc />
        protected override bool CanExecute()
        {
            var ret = base.CanExecute();

            if (!ret)
            {
                if (!firstTime)
                {
                    _messenger.Publish(new ShowSnackbarMessage(this, "Kaufe die Pro Version um Presets zu ändern"));
                }
            }

            return ret;
        }

        /// <inheritdoc cref="Core.ViewModels.Einstellungen.VehiclesViewModel.DeleteVehicle" />
        protected new void DeleteVehicle()
        {
            if (!base.DeleteVehicle())
            {
                _messenger.Publish(new ShowSnackbarMessage(this, "Fehler beim löschen"));
            }
        }

        /// <inheritdoc cref="Core.ViewModels.Einstellungen.VehiclesViewModel.NewVehicle" />
        protected new void NewVehicle()
        {
            if (!base.NewVehicle())
            {
                _messenger.Publish(new ShowSnackbarMessage(this, "Fehler beim erstellen"));
            }
        }

        /// <inheritdoc cref="Core.ViewModels.Einstellungen.VehiclesViewModel.SaveVehicle" />
        protected new void SaveVehicle()
        {
            if (!base.SaveVehicle())
            {
                _messenger.Publish(new ShowSnackbarMessage(this, "Fehler beim speichern"));
            }
        }

        #endregion Methods

        #region Values

        protected bool firstTime = true;
        private readonly ILogger<EinstellungenVehiclesViewModel> _logger;

        public MvxAsyncCommand OpenMenuCommand { get; set; }

        #endregion Values
    }
}