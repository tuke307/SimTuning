// Copyright (c) 2021 tuke productions. All rights reserved.
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using OxyPlot;
using SimTuning.Core.ModuleLogic;
using SimTuning.Data;
using SimTuning.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Tuning
{
    /// <summary>
    /// Diagnosis-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxViewModel" />
    public class DiagnosisViewModel : MvxViewModel
    {
        /// <summary> Initializes a new instance of the <see cref="DiagnosisViewModel"/>
        /// class. </summary> <param name="logger"><inheritdoc cref="ILogger"
        /// path="/summary/node()" /></param> <param name="navigationService"><inheritdoc
        /// cref="IMvxNavigationService" path="/summary/node()" /></param <param
        /// name="messenger"><inheritdoc cref="IMvxMessenger" path="/summary/node()"
        /// /></param>
        public DiagnosisViewModel(
            ILogger<DiagnosisViewModel> logger,
            IMvxNavigationService navigationService,
            IMvxMessenger messenger)
        {
            this._logger = logger;
            this._messenger = messenger;
        }

        #region Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns>Initilisierung.</returns>
        public override Task Initialize()
        {
            tunigLogic = new TuningLogic();

            tunigLogic.DefinePlot();
            tunigLogic.OriginalSeries();
            // RaisePropertyChanged("PlotTuning");

            Enable_Zoom = false;
            Enable_verschieben = false;
            Enable_Tracker = true;

            using (var db = new DatabaseContext())
            {
                try
                {
                    Tuning = LoadTuning(db.Tuning.Where(d => d.Active == true).First());
                }
                catch { }
            }

            return base.Initialize();
        }

        /// <summary>
        /// Prepares this instance. called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

        /// <summary>
        /// The Tuning Model to load.
        /// </summary>
        /// <param name="tuning">The tuning.</param>
        /// <returns>geladenes TuningModel.</returns>
        protected Data.Models.TuningModel LoadTuning(Data.Models.TuningModel tuning)
        {
            try
            {
                using (var data = new Data.DatabaseContext())
                {
                    // Vehicle+Dyno laden
                    return data.Tuning
                      .Where(v => v.Id == tuning.Id)
                      .Include(v => v.Vehicle)
                      .Include(v => v.Tuning)
                      .First();
                }
            }
            catch (Exception)
            {
                return tuning;
                throw;
            }
        }

        #endregion Methods

        #region Values

        protected readonly IMvxMessenger _messenger;
        private readonly ILogger<DiagnosisViewModel> _logger;
        private bool _enable_Tracker;
        private bool _enable_verschieben;
        private bool _enable_Zoom;
        private TuningModel _tuning;
        private TuningLogic tunigLogic;

        public bool Enable_Tracker
        {
            get => _enable_Tracker;
            set
            {
                SetProperty(ref _enable_Tracker, value);

                if (value == true) { tunigLogic.Controller_tracker_on(); }
                else { tunigLogic.Controller_tracker_off(); }
            }
        }

        public bool Enable_verschieben
        {
            get => _enable_verschieben;
            set
            {
                SetProperty(ref _enable_verschieben, value);

                if (value == true) { tunigLogic.Controller_pan_on(); }
                else { tunigLogic.Controller_pan_off(); }
            }
        }

        public bool Enable_Zoom
        {
            get => _enable_Zoom;
            set
            {
                SetProperty(ref _enable_Zoom, value);

                if (value == true) { tunigLogic.Controller_zoom_on(); }
                else { tunigLogic.Controller_zoom_off(); }
            }
        }

        public PlotModel PlotTuning
        {
            get { return tunigLogic.PlotTuning; }
        }

        public PlotController PlotTuningController
        {
            get { return tunigLogic.PlotTuningController; }
        }

        public TuningModel Tuning
        {
            get => _tuning;
            set { SetProperty(ref _tuning, value); }
        }

        #endregion Values
    }
}