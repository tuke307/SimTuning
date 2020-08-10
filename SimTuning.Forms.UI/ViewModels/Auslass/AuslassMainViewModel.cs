// project=SimTuning.Forms.UI, file=AuslassMainViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.Forms.UI.ViewModels.Auslass
{
    public class AuslassMainViewModel : SimTuning.Core.ViewModels.Auslass.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        public AuslassMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
        }

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                _navigationService.Navigate<AuslassTheorieViewModel>(),
                _navigationService.Navigate<AuslassAnwendungViewModel>()
            };
            return Task.WhenAll(tasks);
        }

        public override void ViewAppearing()
        {
            if (_firstTime)
            {
                ShowInitialViewModels();
                _firstTime = false;
            }
        }
    }
}