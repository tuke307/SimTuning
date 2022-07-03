﻿// Copyright (c) 2021 tuke productions. All rights reserved.
namespace SimTuning.Core.ViewModels.Dyno
{
    using Microsoft.Extensions.Logging;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.Plugin.Messenger;
    using MvvmCross.ViewModels;
    using OxyPlot;
    using SimTuning.Core.ModuleLogic;
    using SimTuning.Core.Services;
    using SimTuning.Data;
    using SimTuning.Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// BeschleunigungViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class GeschwindigkeitViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see
        /// cref="GeschwindigkeitViewModel"/> class. </summary> <param
        /// name="logger"><inheritdoc cref="ILogger" path="/summary/node()" /></param>
        /// <param name="navigationService"><inheritdoc cref="IMvxNavigationService"
        /// path="/summary/node()" /></param <param name="messenger"><inheritdoc
        /// cref="IMvxMessenger" path="/summary/node()" /></param>
        public GeschwindigkeitViewModel(
            ILogger<GeschwindigkeitViewModel> logger,
            IMvxNavigationService navigationService,
            IVehicleService vehicleService,
            IMvxMessenger messenger)
        {
            this._logger = logger;
            this._navigationService = navigationService;
            this._vehicleService = vehicleService;
            this._messenger = messenger;
        }

        #region Values

        protected readonly IMvxMessenger _messenger;
        protected readonly IMvxNavigationService _navigationService;
        protected readonly IVehicleService _vehicleService;
        private readonly ILogger<GeschwindigkeitViewModel> _logger;
        private DynoModel _dyno;

        /// <summary>
        /// Gets or sets the dyno.
        /// </summary>
        /// <value>The dyno.</value>
        public DynoModel Dyno
        {
            get => _dyno;
            set => SetProperty(ref _dyno, value);
        }

        public PlotModel PlotGeschwindigkeit
        {
            get => DynoLogic.PlotGeschwindigkeit;
        }

        public MvxAsyncCommand RefreshPlotCommand { get; set; }

        public MvxAsyncCommand ShowAusrollenCommand { get; set; }

        #endregion Values

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            this.RefreshPlotCommand = new MvxAsyncCommand(this.RefreshPlot);

            this.ReloadData();

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
            base.Prepare();
        }

        /// <summary>
        /// Reloads the data.
        /// </summary>
        public void ReloadData()
        {
            try
            {
                this.Dyno = _vehicleService.RetrieveOneActive();
            }
            catch (Exception exc)
            {
                _logger.LogError("Fehler bei ReloadData: ", exc);
            }
        }

        /// <summary>
        /// Aktualisiert den Beschleunigungs-Graphen.
        /// </summary>
        /// <returns></returns>
        protected virtual async Task RefreshPlot()
        {
            try
            {
                DynoLogic.GetGeschwindigkeitsGraphFitted(this.Dyno?.Geschwindigkeit.ToList());

                await this.RaisePropertyChanged(() => PlotGeschwindigkeit).ConfigureAwait(true);
            }
            catch (Exception exc)
            {
                _logger.LogError("Fehler bei RefreshPlot: ", exc);
            }
        }

        #endregion Methods
    }
}