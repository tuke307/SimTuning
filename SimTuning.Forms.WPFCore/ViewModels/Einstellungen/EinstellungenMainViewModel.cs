// project=SimTuning.Forms.WPFCore, file=EinstellungenMainViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels.Einstellungen
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
        /// <param name="_user">The user.</param>
        public override void Prepare(SimTuning.Core.Models.UserModel _user)
        {
            base.Prepare(_user);
        }

        /// <summary>
        /// Views the appearing.
        /// </summary>
        public override void ViewAppearing()
        {
            this._navigationService.Navigate<EinstellungenAussehenViewModel, UserModel>(User);
            this._navigationService.Navigate<EinstellungenUpdateViewModel>();
            this._navigationService.Navigate<EinstellungenVehiclesViewModel, UserModel>(User);
            this._navigationService.Navigate<EinstellungenKontoViewModel, UserModel>(User);
            this._navigationService.Navigate<EinstellungenApplicationViewModel, UserModel>(User);

            this.EinstellungenTabIndex = 0;
        }

        #endregion Methods
    }
}