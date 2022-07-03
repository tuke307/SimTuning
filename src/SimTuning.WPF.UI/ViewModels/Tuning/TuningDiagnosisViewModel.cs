// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Tuning
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.WPF.UI.Business;
    using SimTuning.WPF.UI.Messages;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Tuning-Diagnose-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Tuning.DiagnosisViewModel" />
    public class TuningDiagnosisViewModel : SimTuning.Core.ViewModels.Tuning.DiagnosisViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TuningDiagnosisViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TuningDiagnosisViewModel(
            ILogger<TuningDiagnosisViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger)
            : base(logger, navigationService, messenger)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Überprüft ob wichtige Tuning-Audio-Daten vorhanden sind.
        /// </summary>
        private bool CheckTuningData()
        {
            if (Tuning == null)
            {
                _messenger.Publish(
                    new ShowSnackbarMessage(
                        this,
                        SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA")));

                return false;
            }
            else { return true; }
        }

        /// <summary>
        /// Saves the tuning.
        /// </summary>
        private void SaveTuning()
        {
            if (!CheckTuningData())
                return;
        }

        #endregion Methods

        #region Values

        private readonly ILogger<TuningDiagnosisViewModel> _logger;

        #endregion Values
    }
}