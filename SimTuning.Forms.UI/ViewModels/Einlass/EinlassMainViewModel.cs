// project=SimTuning.Forms.UI, file=EinlassMainViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.Forms.UI.ViewModels.Einlass
{
    public class EinlassMainViewModel : SimTuning.Core.ViewModels.Einlass.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        public EinlassMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
        }

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                _navigationService.Navigate<EinlassKanalViewModel>(),
                _navigationService.Navigate<EinlassVergaserViewModel>()
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