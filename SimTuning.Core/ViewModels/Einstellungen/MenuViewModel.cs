namespace SimTuning.Core.ViewModels.Einstellungen
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;

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
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public MenuViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
            this._navigationService = navigationService;
        }
    }
}