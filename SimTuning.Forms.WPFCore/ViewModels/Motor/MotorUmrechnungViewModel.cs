// project=SimTuning.Forms.WPFCore, file=MotorUmrechnungViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.WPFCore.ViewModels.Motor
{
    public class MotorUmrechnungViewModel : SimTuning.Core.ViewModels.Motor.UmrechnungViewModel
    {
        public MotorUmrechnungViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}