// project=SimTuning.Forms.UI, file=DynoMainViewModel.cs, creation=2020:6:30
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Logging;
using MvvmCross.Navigation;
using SimTuning.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimTuning.Forms.UI.ViewModels.Dyno
{
    public class DynoMainViewModel : SimTuning.Core.ViewModels.Dyno.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public DynoMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare(UserModel _user)
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
            ShowInitialViewModels();
        }

        private Task ShowInitialViewModels()
        {
            var tasks = new List<Task>
            {
                _navigationService.Navigate<DynoDataViewModel>(),
                _navigationService.Navigate<DynoAudioViewModel>(),
                _navigationService.Navigate<DynoSpectrogramViewModel>(),
                _navigationService.Navigate<DynoDiagnosisViewModel>()
            };
            return Task.WhenAll(tasks);
        }
    }
}