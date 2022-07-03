// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Tuning
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using SimTuning.Forms.UI.Helpers;
    using System.Threading.Tasks;

    /// <summary>
    /// TuningInputViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Tuning.InputViewModel" />
    public class TuningInputViewModel : SimTuning.Core.ViewModels.Tuning.InputViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TuningInputViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TuningInputViewModel(
            ILogger<TuningInputViewModel> logger,
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
        /// Checks the tuning data.
        /// </summary>
        /// <returns></returns>
        private bool CheckTuningData()
        {
            if (this.Tuning == null)
            {
                Functions.ShowSnackbarDialog(SimTuning.Core.Helpers.Functions.GetLocalisedRes(typeof(SimTuning.Core.resources), "ERR_NODATA"));

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Saves the tuning.
        /// </summary>
        private void SaveTuning()
        {
            if (!this.CheckTuningData())
            {
                return;
            }
        }

        #endregion Methods

        #region Values

        private readonly ILogger<TuningInputViewModel> _logger;

        #endregion Values
    }
}