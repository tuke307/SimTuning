// project=SimTuning.WPF.UI, file=EinstellungenApplicationViewModel.cs, creation=2020:8:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    public class EinstellungenApplicationViewModel : SimTuning.Core.ViewModels.Einstellungen.ApplicationViewModel
    {
        public EinstellungenApplicationViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
: base(logProvider, navigationService)
        {
        }
    }
}