// project=SimTuning.Forms.WPFCore, file=EinstellungenMainViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.Forms.WPFCore.ViewModels.Einstellungen
{
    public class EinstellungenMainViewModel : SimTuning.Core.ViewModels.Einstellungen.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public EinstellungenMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
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
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            return base.Initialize();
        }

        public override void ViewAppearing()
        {
            _navigationService.Navigate<EinstellungenAussehenViewModel, UserModel>(User);
            _navigationService.Navigate<EinstellungenUpdateViewModel>();
            _navigationService.Navigate<EinstellungenVehiclesViewModel, UserModel>(User);
            _navigationService.Navigate<EinstellungenKontoViewModel, UserModel>(User);

            EinstellungenTabIndex = 0;
        }
    }
}