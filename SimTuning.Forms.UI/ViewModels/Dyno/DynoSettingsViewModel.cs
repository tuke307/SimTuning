using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoSettingsViewModel : SimTuning.Core.ViewModels.Dyno.SettingsViewModel
    {
        public DynoSettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            ShowAccelerationCommand = new MvxAsyncCommand(async () => await this.NavigationService.Navigate<DynoRuntimeViewModel>());
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