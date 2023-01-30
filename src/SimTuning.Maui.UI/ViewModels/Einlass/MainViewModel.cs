﻿// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.Extensions.Logging;
using SimTuning.Maui.UI.Services;

namespace SimTuning.Maui.UI.ViewModels.Einlass
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(
            ILogger<MainViewModel> logger,
            INavigationService navigationService)
        {
            this._logger = logger;
            this._navigationService = navigationService;
        }

        #region Methods



        #endregion Methods

        #region Values

        protected readonly INavigationService _navigationService;
        private readonly ILogger<MainViewModel> _logger;
        private int _einlassTabIndex;

        public int EinlassTabIndex
        {
            get => _einlassTabIndex;
            set => SetProperty(ref _einlassTabIndex, value);
        }

        #endregion Values
    }
}