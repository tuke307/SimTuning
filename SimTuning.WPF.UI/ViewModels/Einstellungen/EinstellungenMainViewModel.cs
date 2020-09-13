// project=SimTuning.WPF.UI, file=EinstellungenMainViewModel.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// WPF-spezifisches Einstellungen-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Einstellungen.MainViewModel" />
    public class EinstellungenMainViewModel : SimTuning.Core.ViewModels.Einstellungen.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EinstellungenMainViewModel" />
        /// class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public EinstellungenMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;
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
        /// Prepares the specified user.
        /// </summary>
        /// <param name="">The user.</param>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Views the appearing.
        /// </summary>
        public override void ViewAppearing()
        {
            this._navigationService.Navigate<EinstellungenAussehenViewModel>();
            this._navigationService.Navigate<EinstellungenUpdateViewModel>();
            this._navigationService.Navigate<EinstellungenVehiclesViewModel>();
            this._navigationService.Navigate<EinstellungenKontoViewModel>();
            this._navigationService.Navigate<EinstellungenApplicationViewModel>();

            this.EinstellungenTabIndex = 0;
        }

        #endregion Methods
    }
}