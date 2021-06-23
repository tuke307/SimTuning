// project=SimTuning.WPF.UI, file=EinstellungenApplicationViewModel.cs, creation=2020:9:2
// Copyright (c) 2020 tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace SimTuning.WPF.UI.ViewModels.Einstellungen
{
    public class EinstellungenApplicationViewModel : SimTuning.Core.ViewModels.Einstellungen.ApplicationViewModel
    {
        public MvxAsyncCommand OpenMenuCommand { get; set; }

        public EinstellungenApplicationViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
                : base(logFactory, navigationService)
        {
            OpenMenuCommand = new MvxAsyncCommand(() => NavigationService.Navigate<EinstellungenMenuViewModel>());
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