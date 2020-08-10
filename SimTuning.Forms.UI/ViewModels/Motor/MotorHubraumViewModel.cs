// project=SimTuning.Forms.UI, file=MotorHubraumViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.UI.ViewModels.Motor
{
    public class MotorHubraumViewModel : SimTuning.Core.ViewModels.Motor.HubraumViewModel
    {
        public MotorHubraumViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}