// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Tuning
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Tuning-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Tuning.MainViewModel" />
    public class TuningMainViewModel : SimTuning.Core.ViewModels.Tuning.MainViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TuningMainViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public TuningMainViewModel(
            ILogger<TuningMainViewModel> logger,
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
            this._navigationService.Navigate<TuningDataViewModel>();
            this._navigationService.Navigate<TuningInputViewModel>();
            this._navigationService.Navigate<TuningDiagnosisViewModel>();

            this.TuningTabIndex = 0;
        }

        #endregion Methods

        #region Values

        private readonly ILogger<TuningMainViewModel> _logger;

        #endregion Values
    }
}