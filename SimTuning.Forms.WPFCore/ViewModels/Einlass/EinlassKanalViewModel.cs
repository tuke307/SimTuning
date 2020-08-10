// project=SimTuning.Forms.WPFCore, file=EinlassKanalViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.WPFCore.ViewModels.Einlass
{
    public class EinlassKanalViewModel : SimTuning.Core.ViewModels.Einlass.KanalViewModel
    {
        public EinlassKanalViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}