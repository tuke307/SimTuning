// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core;
    using SimTuning.Core.Services;
    using SimTuning.Forms.UI.Helpers;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Xamarin.Essentials;

    /// <summary>
    /// DynoDataViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.DataViewModel" />
    public class DynoDataViewModel : SimTuning.Core.ViewModels.Dyno.DataViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDataViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoDataViewModel(
            ILogger<DynoDataViewModel> logger,
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
            // Commands
            this.NewDynoCommand = new MvxCommand(this.NewDyno);
            this.DeleteDynoCommand = new MvxCommand(this.DeleteDyno);
            this.SaveDynoCommand = new MvxCommand(this.SaveDyno);

            this.ImportDynoCommand = new MvxAsyncCommand(this.ImportDyno);

            this.ShowSettingsCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<DynoRuntimeViewModel>());

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.DataViewModel.DeleteDyno" />
        protected new void DeleteDyno()
        {
            if (!base.DeleteDyno())
            {
                Functions.ShowSnackbarDialog("Fehler beim löschen");
            }
        }

        /// <inheritdoc />
        protected override async void ExportDyno()
        {
            try
            {
                base.ExportDyno();

                await Share.RequestAsync(new ShareFileRequest
                {
                    File = new ShareFile(GeneralSettings.DataExportArchivePath),
                }).ConfigureAwait(true);
            }
            catch (Exception)
            {
                Functions.ShowSnackbarDialog("Fehler beim exportieren");
            }
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.DataViewModel.ImportDyno(string)" />
        protected new async Task ImportDyno()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile("https://simtuning.tuke-productions.de/wp-content/uploads/DataExport.zip", SimTuning.Core.GeneralSettings.DataExportArchivePath);
            }

            await base.ImportDyno(SimTuning.Core.GeneralSettings.DataExportArchivePath).ConfigureAwait(true);
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.DataViewModel.NewDyno" />
        protected new void NewDyno()
        {
            if (!base.NewDyno())
            {
                Functions.ShowSnackbarDialog("Fehler beim erstellen");
            }
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.DataViewModel.SaveDyno" />
        protected new void SaveDyno()
        {
            if (!base.SaveDyno())
            {
                Functions.ShowSnackbarDialog("Fehler beim speichern");
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<DynoDataViewModel> _logger;

        /// <summary>
        /// Gets the show settings command.
        /// </summary>
        /// <value>The show settings command.</value>
        public IMvxAsyncCommand ShowSettingsCommand { get; private set; }

        #endregion Values
    }
}