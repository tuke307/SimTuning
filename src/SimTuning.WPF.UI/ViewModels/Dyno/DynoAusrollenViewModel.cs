// Copyright (c) 2021 tuke productions. All rights reserved.
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using SimTuning.Core.Services;
using SimTuning.Core.ViewModels.Dyno;
using SimTuning.WPF.UI.Dialog;
using SimTuning.WPF.UI.Messages;
using System.Threading.Tasks;
using System.Windows;

namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    /// <summary>
    /// DynoAusrollenViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.AusrollenViewModel" />
    public class DynoAusrollenViewModel : AusrollenViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoAusrollenViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="messenger">The messenger.</param>
        public DynoAusrollenViewModel(
            ILogger<DynoAusrollenViewModel> logger,
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
            this.RefreshPlotCommand = new MvxAsyncCommand(this.RefreshPlot);

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc />
        protected override async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }
            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(async () =>
                {
                    await base.RefreshPlot().ConfigureAwait(true);

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Überprüft ob wichtige Dyno-Audio-Daten vorhanden sind.
        /// </summary>
        private bool CheckDynoData()
        {
            if (this.Dyno == null)
            {
                _messenger.Publish(
                    new ShowSnackbarMessage(
                        this,
                        SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA")));

                return false;
            }

            if (this.Dyno.Geschwindigkeit == null)
            {
                _messenger.Publish(
                    new ShowSnackbarMessage(
                        this,
                        SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA")));

                return false;
            }

            return true;
        }

        #endregion Methods

        #region Values

        private readonly ILogger<DynoAusrollenViewModel> _logger;

        #endregion Values
    }
}