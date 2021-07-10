// project=SimTuning.Core, file=DiagnosisViewModel.cs, creation=2020:7:31 Copyright (c)
// 2021 tuke productions. All rights reserved.
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using OxyPlot;
using SimTuning.Core.ModuleLogic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Tuning
{
    /// <summary>
    /// Diagnosis-ViewModel.
    /// </summary>
    /// <seealso cref="MvvmCross.ViewModels.MvxNavigationViewModel" />
    public class DiagnosisViewModel : MvxNavigationViewModel
    {
        private TuningLogic tunigLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosisViewModel" /> class.
        /// </summary>
        /// <param name="logFactory">The log provider.</param>
        /// <param name="navigationService">The navigation service.</param>
        public DiagnosisViewModel(ILoggerFactory logFactory, IMvxNavigationService navigationService, IMvxMessenger messenger)
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
            tunigLogic = new TuningLogic();

            tunigLogic.DefinePlot();
            tunigLogic.OriginalSeries();
            //RaisePropertyChanged("PlotTuning");

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
                using (var Data = new Data.DatabaseContext())
                {
                    //Vehicle+Dyno laden
                    return Data.Tuning
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

        private bool _enable_Tracker;
        private bool _enable_verschieben;
        private bool _enable_Zoom;
        private TuningModel _tuning;

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