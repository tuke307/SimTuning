using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    public class EinstellungenApplicationViewModel : SimTuning.Core.ViewModels.Einstellungen.ApplicationViewModel
    {
        public EinstellungenApplicationViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
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
        /// Prepares this instance.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion Methods
    }
}