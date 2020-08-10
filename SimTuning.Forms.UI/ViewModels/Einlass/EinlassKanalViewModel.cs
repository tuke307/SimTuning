// project=SimTuning.Forms.UI, file=EinlassKanalViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.UI.ViewModels.Einlass
{
    public class EinlassKanalViewModel : SimTuning.Core.ViewModels.Einlass.KanalViewModel
    {
        public EinlassKanalViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}