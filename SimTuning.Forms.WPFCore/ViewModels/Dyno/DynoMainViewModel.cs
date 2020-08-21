﻿// project=SimTuning.Forms.WPFCore, file=DynoMainViewModel.cs, creation=2020:7:31
// Copyright (c) 2020 tuke productions. All rights reserved.
namespace SimTuning.Forms.WPFCore.ViewModels.Dyno
{
    using MvvmCross;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using MvvmCross.Views;
    using System.Threading.Tasks;

    /// <summary>
    /// Dyno-Main-ViewModel.
    /// </summary>
    /// <seealso cref="SimTuning.Core.ViewModels.Dyno.MainViewModel" />
    public class DynoMainViewModel : SimTuning.Core.ViewModels.Dyno.MainViewModel
    {
        private readonly IMvxViewModelLoader _mvxViewModelLoader;
        private readonly IMvxViewsContainer _mvxViewsContainer;
        private readonly IMvxNavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynoMainViewModel" /> class.
        /// </summary>
        /// <param name="logProvider">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DynoMainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
            : base(logProvider, navigationService)
        {
            this._navigationService = navigationService;

            _mvxViewsContainer = Mvx.IoCProvider.Resolve<IMvxViewsContainer>();
            _mvxViewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
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
            this._navigationService.Navigate<DynoDataViewModel>();
            this._navigationService.Navigate<DynoAudioViewModel>();
            this._navigationService.Navigate<DynoSpectrogramViewModel>();
            this._navigationService.Navigate<DynoDiagnosisViewModel>();

            var presenter = Mvx.IoCProvider.Resolve<IMvxViewsContainer>(); // or inject with IoC
            //var current = presenter.;

            this.DynoTabIndex = 0;
        }

        #endregion Methods
    }
}