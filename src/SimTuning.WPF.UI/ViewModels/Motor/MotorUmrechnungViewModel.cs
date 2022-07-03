// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Motor-Umrechnung-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.UmrechnungViewModel" />
    public class MotorUmrechnungViewModel : SimTuning.Core.ViewModels.Motor.UmrechnungViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorUmrechnungViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorUmrechnungViewModel(
            ILogger<MotorUmrechnungViewModel> logger,
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

        private readonly ILogger<MotorUmrechnungViewModel> _logger;

        #endregion Values
    }
}