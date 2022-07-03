// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using SimTuning.WPF.UI.Services;
    using SimTuning.WPF.UI.ViewModels.Home;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.MainPageViewModel" />
    public class MainViewModel : SimTuning.Core.ViewModels.MainPageViewModel
    {
        private readonly IThemeService _themeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="logger">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainViewModel(
            ILogger<MainViewModel> logger,
            IMvxNavigationService navigationService,
            IThemeService themeService)
            : base(logger, navigationService)
        {
            this._logger = logger;
            this._themeService = themeService;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.ShowHomeViewModelCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<HomeMainViewModel>());
            this.ShowMenuViewModelCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<MenuViewModel>());

            _themeService.UpdateTheme((MaterialDesignThemes.Wpf.BaseTheme)ColorSettings.Theme);
            _themeService.UpdatePrimary((MaterialDesignColors.PrimaryColor)ColorSettings.Primary);
            _themeService.UpdateSecondary((MaterialDesignColors.SecondaryColor)ColorSettings.Secondary);

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
            base.ViewAppearing();

            this.ShowMenuViewModelCommand.Execute();
            this.ShowHomeViewModelCommand.Execute();
        }

        #endregion Methods

        #region Values

        private readonly ILogger<MainViewModel> _logger;

        #endregion Values
    }
}