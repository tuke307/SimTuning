using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoRuntimeViewModel : SimTuning.Core.ViewModels.Dyno.RuntimeViewModel
    {
        public DynoRuntimeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxLocationWatcher locationWatcher)
            : base(logProvider, navigationService, locationWatcher)
        {
        }
    }
}