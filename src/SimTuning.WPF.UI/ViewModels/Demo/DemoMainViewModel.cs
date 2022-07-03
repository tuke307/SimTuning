// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Demo
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Services;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Demo-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Demo.DemoMainViewModel" />
    public class DemoMainViewModel : SimTuning.Core.ViewModels.Demo.DemoMainViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DemoMainViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DemoMainViewModel(
            ILogger<DemoMainViewModel> logger,
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

        private readonly ILogger<DemoMainViewModel> _logger;

        #endregion Values
    }
}