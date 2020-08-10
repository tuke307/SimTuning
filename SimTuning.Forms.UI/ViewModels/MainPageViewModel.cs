// project=SimTuning.Forms.UI, file=MainPageViewModel.cs, creation=2020:6:28
// Copyright (c) 2020 tuke productions. All rights reserved.
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using SimTuning.Forms.UI.ViewModels.Home;
using XF.Material.Forms.UI.Dialogs;

namespace SimTuning.Forms.UI.ViewModels
{
    public class MainPageViewModel : SimTuning.Core.ViewModels.MainPage
    {
        private readonly IMvxNavigationService _navigationService;

        public MainPageViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;

            this.ShowHomeViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<HomeMainViewModel>());
            this.ShowMenuViewModelCommand = new MvxAsyncCommand(() => _navigationService.Navigate<MenuViewModel>());
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            ShowMenuViewModelCommand.Execute();
            ShowHomeViewModelCommand.Execute();
        }
    }
}