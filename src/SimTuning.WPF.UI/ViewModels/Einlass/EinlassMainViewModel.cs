// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einlass
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using System.Threading.Tasks;

    /// <summary>
    /// Einlass-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einlass.MainViewModel" />
    public class EinlassMainViewModel : SimTuning.Core.ViewModels.Einlass.MainViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinlassMainViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinlassMainViewModel(
            ILogger<EinlassMainViewModel> logger,
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
            this._navigationService.Navigate<EinlassKanalViewModel>();
            this._navigationService.Navigate<EinlassVergaserViewModel>();

            this.EinlassTabIndex = 0;
        }

        #endregion Methods

        #region Values

        private readonly ILogger<EinlassMainViewModel> _logger;

        #endregion Values
    }
}