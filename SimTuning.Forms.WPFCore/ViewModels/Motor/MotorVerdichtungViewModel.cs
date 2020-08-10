// project=SimTuning.Forms.WPFCore, file=MotorVerdichtungViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.WPFCore.ViewModels.Motor
{
    public class MotorVerdichtungViewModel : SimTuning.Core.ViewModels.Motor.VerdichtungViewModel
    {
        public MotorVerdichtungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}