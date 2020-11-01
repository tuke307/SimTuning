// project=SimTuning.Forms.UI, file=MainPageViewModel.cs, creation=2020:6:28 Copyright (c)
// 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Forms.UI.Business;
    using SimTuning.Forms.UI.ViewModels.Home;
    using System.Threading.Tasks;

    /// <summary>
    /// MainPage.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.MainPage" />
    public class MainPageViewModel : SimTuning.Core.ViewModels.MainPage
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MainPageViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;

            this.ShowHomeViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel>());
            this.ShowMenuViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MenuViewModel>());
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initialisierung.</returns>
        public override Task Initialize()
        {
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
        /// Views the appeared.
        /// </summary>
        public override void ViewAppeared()
        {
            base.ViewAppeared();

            this.ShowMenuViewModelCommand.Execute();
            this.ShowHomeViewModelCommand.Execute();

            // TODO: nicht der richtige platz
            ApplicationChanges.LoadColors();
        }
    }
}