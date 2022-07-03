// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Dyno
{
    using MaterialDesignThemes.Wpf;
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Core.Models;
    using SimTuning.Core.Services;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Dialog;
    using SimTuning.WPF.UI.Messages;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// WPF-spezifisches Dyno-Diagnose-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel" />
    public class DynoDiagnosisViewModel : SimTuning.Core.ViewModels.Dyno.DiagnosisViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynoDiagnosisViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoDiagnosisViewModel(
            ILogger<DynoDiagnosisViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger,
            IVehicleService vehicleService)
            : base(logger, navigationService, messenger, vehicleService)
        {
            this._logger = logger;
            _token = _messenger.Subscribe<MvxReloaderMessage>(this.ReloadData);
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.RefreshPlotCommand = new MvxAsyncCommand(RefreshPlot);

            // datensatz checken CheckDynoData();

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc cref="Core.ViewModels.Dyno.DiagnosisViewModel.RefreshPlot" />
        protected new async Task RefreshPlot()
        {
            if (!this.CheckDynoData())
            {
                return;
            }

            await DialogHost.Show(new DialogLoadingView(), "DialogLoading", (object sender, DialogOpenedEventArgs args) =>
            {
                Task.Run(async () =>
                {
                    base.RefreshPlot();

                    Application.Current.Dispatcher.Invoke(() => args.Session.Close());
                });
            }).ConfigureAwait(true);
        }

        /// <summary>
        /// Überprüft ob wichtige Dyno-Audio-Daten vorhanden sind.
        /// </summary>
        private bool CheckDynoData()
        {
            if (Dyno == null)
            {
                _messenger.Publish(
                    new ShowSnackbarMessage(
                        this,
                        SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA")));

                return false;
            }
            else { return true; }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<DynoDiagnosisViewModel> _logger;
        private readonly MvxSubscriptionToken _token;

        #endregion Values
    }
}