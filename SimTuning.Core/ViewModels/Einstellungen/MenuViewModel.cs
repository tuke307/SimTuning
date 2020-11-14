namespace SimTuning.Core.ViewModels.Einstellungen
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using SimTuning.Core.Models;
    using System.Threading.Tasks;

    public class MenuViewModel : MvxNavigationViewModel
    {
        protected readonly IMvxNavigationService _navigationService;

        public MvxAsyncCommand OpenApplicationCommand { get; set; }

        public MvxAsyncCommand OpenAussehenCommand { get; set; }

        public MvxAsyncCommand OpenKontoCommand { get; set; }

        public MvxAsyncCommand OpenVehiclesCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MenuViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;
        }
    }
}