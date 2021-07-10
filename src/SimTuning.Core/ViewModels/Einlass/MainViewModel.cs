﻿// project=SimTuning.Core, file=MainViewModel.cs, creation=2020:7:31 Copyright (c) 2021
// tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Einlass
{
    /// <summary>
    /// Einlass-Main-ViewModel.
    /// </summary>

    public class MainViewModel : MvxNavigationViewModel
    {
        public MainViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService)
            : base(logFactory, navigationService)
        {
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
        }

        #endregion Methods

        #region Values

        private int _einlassTabIndex;

        public int EinlassTabIndex
        {
            get => _einlassTabIndex;
            set { SetProperty(ref _einlassTabIndex, value); }
        }

        #endregion Values
    }
}