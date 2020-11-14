// project=SimTuning.WPF.UI, file=EinstellungenApplicationViewModel.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    public class EinstellungenMenuViewModel : SimTuning.Core.ViewModels.Einstellungen.MenuViewModel
    {
        public MvxAsyncCommand OpenUpdateCommand { get; set; }

        public EinstellungenMenuViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
                        : base(logProvider, navigationService)
        {
            this.OpenUpdateCommand = new MvxAsyncCommand(() => NavigationService.Navigate<EinstellungenUpdateViewModel>());

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
        /// Prepares the specified user.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        #endregion Methods
    }
}