// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Home
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Home-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Home.HomeViewModel" />
    public class HomeMainViewModel : SimTuning.Core.ViewModels.Home.HomeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeMainViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public HomeMainViewModel(
            ILogger<HomeMainViewModel> logger,
            IMvxNavigationService navigationService,
            IBrowserService browserService)
            : base(logger, navigationService, browserService)
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

        private readonly ILogger<HomeMainViewModel> _logger;

        #endregion Values
    }
}