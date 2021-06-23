// project=SimTuning.Forms.UI, file=EinstellungenMainViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using System.Threading.Tasks;

    /// <summary>
    /// EinstellungenMainViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.MainViewModel" />
    public class EinstellungenMenuViewModel : SimTuning.Core.ViewModels.Einstellungen.MenuViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenMenuViewModel" />
        /// class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenMenuViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
            this._navigationService = navigationService;

            this.OpenAussehenCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenAussehenViewModel>());
            this.OpenVehiclesCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenVehiclesViewModel>());
            this.OpenKontoCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenKontoViewModel>());
            this.OpenApplicationCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenApplicationViewModel>());
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