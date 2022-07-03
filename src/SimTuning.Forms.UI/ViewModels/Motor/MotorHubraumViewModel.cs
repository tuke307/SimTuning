// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using System.Threading.Tasks;

    /// <summary>
    /// MotorHubraumViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.HubraumViewModel" />
    public class MotorHubraumViewModel : SimTuning.Core.ViewModels.Motor.HubraumViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorHubraumViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorHubraumViewModel(
            ILogger<MotorHubraumViewModel> logger,
            IMvxNavigationService navigationService,
            IVehicleService vehicleService)
            : base(logger, navigationService, vehicleService)
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

        #endregion Methods

        #region Values

        private readonly ILogger<MotorHubraumViewModel> _logger;

        #endregion Values
    }
}