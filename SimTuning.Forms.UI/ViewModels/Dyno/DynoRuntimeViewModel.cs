using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Location;
using SimTuning.Forms.UI.Business;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoRuntimeViewModel : SimTuning.Core.ViewModels.Dyno.RuntimeViewModel
    {
        public DynoRuntimeViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IMvxLocationWatcher locationWatcher)
            : base(logProvider, navigationService, locationWatcher)
        {
            ShowAudioCommand = new MvxAsyncCommand(async () => await NavigationService.Navigate<DynoAudioViewModel>());
            CloseCommand = new MvxAsyncCommand(async () => await NavigationService.Close(this));

            Functions.CheckAndRequestLocationWhenInUsePermission();
            Functions.CheckAndRequestMicrophonePermission();
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion Methods
    }
}