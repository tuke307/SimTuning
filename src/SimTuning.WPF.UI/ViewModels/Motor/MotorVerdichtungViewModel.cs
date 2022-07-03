// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Motor-Verdichtung-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.VerdichtungViewModel" />
    public class MotorVerdichtungViewModel : SimTuning.Core.ViewModels.Motor.VerdichtungViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorVerdichtungViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorVerdichtungViewModel(
            ILogger<MotorVerdichtungViewModel> logger,
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

        private readonly ILogger<MotorVerdichtungViewModel> _logger;

        #endregion Values
    }
}