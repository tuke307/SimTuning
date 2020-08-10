// project=SimTuning.Forms.WPFCore, file=TuningMainViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimTuning.Forms.WPFCore.ViewModels.Tuning
{
    public class TuningMainViewModel : SimTuning.Core.ViewModels.Tuning.MainViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public TuningMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
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
            _navigationService.Navigate<TuningDataViewModel>();
            _navigationService.Navigate<TuningInputViewModel>();
            _navigationService.Navigate<TuningDiagnosisViewModel>();

            TuningTabIndex = 0;
        }
    }
}