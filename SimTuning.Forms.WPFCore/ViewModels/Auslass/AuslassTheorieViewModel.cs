// project=SimTuning.Forms.WPFCore, file=AuslassTheorieViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.WPFCore.ViewModels.Auslass
{
    public class AuslassTheorieViewModel : SimTuning.Core.ViewModels.Auslass.TheorieViewModel
    {
        public AuslassTheorieViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}