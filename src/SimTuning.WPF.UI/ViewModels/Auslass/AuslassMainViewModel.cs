// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Auslass
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Auslass-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Auslass.MainViewModel" />
    public class AuslassMainViewModel : SimTuning.Core.ViewModels.Auslass.MainViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuslassMainViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public AuslassMainViewModel(
            ILogger<AuslassMainViewModel> logger,
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
            this._navigationService.Navigate<AuslassTheorieViewModel>();
            this._navigationService.Navigate<AuslassAnwendungViewModel>();

            this.AuslassTabIndex = 0;
        }

        #endregion Methods

        #region Values

        private readonly ILogger<AuslassMainViewModel> _logger;

        #endregion Values
    }
}