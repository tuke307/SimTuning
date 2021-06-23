// project=SimTuning.WPF.UI, file=MainViewModel.cs, creation=2020:9:2 Copyright (c) 2020
// tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using SimTuning.WPF.UI.Models;
    using SimTuning.WPF.UI.ViewModels.Home;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.MainPage" />
    public class MainViewModel : SimTuning.Core.ViewModels.MainPage
    {
        private readonly IMvxNavigationService _navigationService;

        private readonly IThemeService _themeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, IThemeService themeService)
            : base(logFactory, navigationService)
        {
            this._themeService = themeService;
            this._navigationService = navigationService;

            this.ShowHomeViewModelCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<HomeMainViewModel>());
            this.ShowMenuViewModelCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<MenuViewModel>());
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            _themeService.UpdateTheme((MaterialDesignThemes.Wpf.BaseTheme)ColorSettings.Theme);
            _themeService.UpdatePrimary((MaterialDesignColors.PrimaryColor)ColorSettings.Primary);
            _themeService.UpdateSecondary((MaterialDesignColors.SecondaryColor)ColorSettings.Secondary);

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// When view is appearing.
        /// </summary>
        public override void ViewAppearing()
        {
            base.ViewAppearing();

            this.ShowMenuViewModelCommand.Execute();
            this.ShowHomeViewModelCommand.Execute();
        }

        #endregion Methods
    }
}