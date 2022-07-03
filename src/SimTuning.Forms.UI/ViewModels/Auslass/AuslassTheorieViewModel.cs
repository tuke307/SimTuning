// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Auslass
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using System.Threading.Tasks;

    /// <summary>
    /// AuslassTheorieViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Auslass.TheorieViewModel" />
    public class AuslassTheorieViewModel : SimTuning.Core.ViewModels.Auslass.TheorieViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuslassTheorieViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AuslassTheorieViewModel(
            ILogger<AuslassTheorieViewModel> logger,
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

        private readonly ILogger<AuslassTheorieViewModel> _logger;

        #endregion Values
    }
}