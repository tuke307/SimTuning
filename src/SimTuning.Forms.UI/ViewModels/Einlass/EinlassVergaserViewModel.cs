// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Einlass
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using System.Threading.Tasks;

    /// <summary>
    /// EinlassVergaserViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einlass.VergaserViewModel" />
    public class EinlassVergaserViewModel : SimTuning.Core.ViewModels.Einlass.VergaserViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinlassVergaserViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinlassVergaserViewModel(
            ILogger<EinlassVergaserViewModel> logger,
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

        private readonly ILogger<EinlassVergaserViewModel> _logger;

        #endregion Values
    }
}