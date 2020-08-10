// project=SimTuning.Forms.UI, file=AuslassTheorieViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.UI.ViewModels.Auslass
{
    public class AuslassTheorieViewModel : SimTuning.Core.ViewModels.Auslass.TheorieViewModel
    {
        public AuslassTheorieViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }
    }
}