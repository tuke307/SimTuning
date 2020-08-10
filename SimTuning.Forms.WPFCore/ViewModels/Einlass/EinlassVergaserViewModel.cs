// project=SimTuning.Forms.WPFCore, file=EinlassVergaserViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.WPFCore.ViewModels.Einlass
{
    public class EinlassVergaserViewModel : SimTuning.Core.ViewModels.Einlass.VergaserViewModel
    {
        public EinlassVergaserViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}