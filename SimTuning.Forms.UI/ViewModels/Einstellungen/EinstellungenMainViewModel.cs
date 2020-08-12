// project=SimTuning.Forms.UI, file=EinstellungenMainViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.UI.ViewModels.Einstellungen
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using SimTuning.Core.Models;

    /// <summary>
    /// EinstellungenMainViewModel.
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

        /// <summary>
        /// Views the appearing.
        /// </summary>
        public override void ViewAppearing()
        {
            this._navigationService.Navigate<EinstellungenVehiclesViewModel, UserModel>(this.User);
            this._navigationService.Navigate<EinstellungenKontoViewModel, UserModel>(this.User);
        }
    }
}