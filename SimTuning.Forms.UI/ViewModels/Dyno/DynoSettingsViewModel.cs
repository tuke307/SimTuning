﻿using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoSettingsViewModel : SimTuning.Core.ViewModels.Dyno.SettingsViewModel
    {
        public DynoSettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            ShowAccelerationCommand = new MvxAsyncCommand(async () => await this.NavigationService.Navigate<DynoRuntimeViewModel>());
        }
    }
}