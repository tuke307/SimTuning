// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    /// <summary>
    /// EinstellungenApplicationViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.ApplicationViewModel" />
    public class EinstellungenApplicationViewModel : SimTuning.Core.ViewModels.Einstellungen.ApplicationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="EinstellungenApplicationViewModel" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenApplicationViewModel(
            ILogger<EinstellungenApplicationViewModel> logger,
            IMvxNavigationService navigationService)
            : base(logger, navigationService)
        {
            this._logger = logger;
        }

        #region Methods

        /// <inheritdoc />
        public override Task Initialize()
        {
            OpenMenuCommand = new MvxAsyncCommand(() => _navigationService.Navigate<EinstellungenMenuViewModel>());

            return base.Initialize();
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion Methods

        #region Values

        private readonly ILogger<EinstellungenApplicationViewModel> _logger;

        public MvxAsyncCommand OpenMenuCommand { get; set; }

        #endregion Values
    }
}