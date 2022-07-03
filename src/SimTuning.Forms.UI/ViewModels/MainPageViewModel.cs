// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using SimTuning.Forms.UI.ViewModels.Home;
    using System.Threading.Tasks;

    /// <summary>
    /// MainPage.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.MainPageViewModel" />
    public class MainPageViewModel : SimTuning.Core.ViewModels.MainPageViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainPageViewModel(
            ILogger<MainPageViewModel> logger,
            IMvxNavigationService navigationService)
            : base(logger, navigationService)
        {
            this._logger = logger;
        }

        #region Values

        private readonly ILogger<MainPageViewModel> _logger;

        #endregion Values

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.ShowHomeViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel>());
            this.ShowMenuViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MenuViewModel>());

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <inheritdoc />
        public override void ViewAppeared()
        {
            base.ViewAppeared();

            this.ShowMenuViewModelCommand.Execute();
            this.ShowHomeViewModelCommand.Execute();
        }

        #endregion Methods
    }
}