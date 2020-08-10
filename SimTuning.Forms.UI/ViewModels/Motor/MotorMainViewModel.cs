// project=SimTuning.Forms.UI, file=MotorMainViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.Forms.UI.ViewModels.Motor
{
    public class MotorMainViewModel : SimTuning.Core.ViewModels.Motor.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private bool _firstTime = true;

        public MotorMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
        }

        public override void ViewAppearing()
        {
            if (_firstTime)
            {
                ShowInitialViewModels();
                _firstTime = false;
            }
        }

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                _navigationService.Navigate<MotorUmrechnungenViewModel>(),
                _navigationService.Navigate<MotorSteuerdiagrammViewModel>(),
                _navigationService.Navigate<MotorVerdichtungViewModel>(),
                _navigationService.Navigate<MotorHubraumViewModel>()
            };
            return Task.WhenAll(tasks);
        }
    }
}