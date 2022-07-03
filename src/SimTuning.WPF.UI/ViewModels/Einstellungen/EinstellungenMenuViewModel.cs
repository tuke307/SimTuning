// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    /// <summary>
    /// EinstellungenMenuViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.MenuViewModel" />
    public class EinstellungenMenuViewModel : SimTuning.Core.ViewModels.Einstellungen.MenuViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenMenuViewModel" />
        /// class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenMenuViewModel(
            ILogger<EinstellungenMenuViewModel> logger,
            IMvxNavigationService navigationService)
            : base(logger, navigationService)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            this.OpenUpdateCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenUpdateViewModel>());

            this.OpenAussehenCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenAussehenViewModel>());
            this.OpenVehiclesCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenVehiclesViewModel>());
            this.OpenKontoCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenKontoViewModel>());
            this.OpenApplicationCommand = new MvxAsyncCommand(() => this._navigationService.Navigate<EinstellungenApplicationViewModel>());

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion Methods

        #region Values

        private readonly ILogger<EinstellungenMenuViewModel> _logger;

        public MvxAsyncCommand OpenUpdateCommand { get; set; }

        #endregion Values
    }
}