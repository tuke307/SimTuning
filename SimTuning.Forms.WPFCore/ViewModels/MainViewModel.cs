// project=SimTuning.Forms.WPFCore, file=MainViewModel.cs, creation=2020:7:31 Copyright
// (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Forms.WPFCore.Business;
    using SimTuning.Forms.WPFCore.ViewModels.Home;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.MainPage" />
    public class MainViewModel : SimTuning.Core.ViewModels.MainPage
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
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
            ApplicationChanges.LoadColors();

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