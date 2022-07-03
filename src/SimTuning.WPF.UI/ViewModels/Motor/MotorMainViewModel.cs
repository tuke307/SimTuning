// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Motor
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Motor-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Motor.MainViewModel" />
    public class MotorMainViewModel : SimTuning.Core.ViewModels.Motor.MainViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MotorMainViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MotorMainViewModel(
            ILogger<MotorMainViewModel> logger,
            IMvxNavigationService navigationService)
            : base(logger, navigationService)
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

        /// <inheritdoc />
        public override void ViewAppearing()
        {
            this._navigationService.Navigate<MotorUmrechnungViewModel>();
            this._navigationService.Navigate<MotorSteuerdiagrammViewModel>();
            this._navigationService.Navigate<MotorVerdichtungViewModel>();
            this._navigationService.Navigate<MotorHubraumViewModel>();

            this.MotorTabIndex = 0;
        }

        #endregion Methods

        #region Values

        private readonly ILogger<MotorMainViewModel> _logger;

        #endregion Values
    }
}