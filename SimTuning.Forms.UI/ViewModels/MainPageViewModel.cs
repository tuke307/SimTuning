using MvvmCross.Commands;
using MvvmCross.Navigation;
using SimTuning.Forms.UI.ViewModels.Home;

namespace SimTuning.Forms.UI.ViewModels
{
    public class MainPageViewModel : SimTuning.Core.ViewModels.MainPage
    {
        private readonly IMvxNavigationService _navigationService;

        public MainPageViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowHomeViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MenuViewModel>());
        }

        // MvvmCross Lifecycle

        // MVVM Properties

        // MVVM Commands

        public IMvxAsyncCommand ShowHomeViewModelCommand { get; private set; }
        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

        // Private methods
    }
}