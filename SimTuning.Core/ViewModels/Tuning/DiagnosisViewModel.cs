using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using OxyPlot;
using SimTuning.Core.ModuleLogic;
using System;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels.Tuning
{
    public class DiagnosisViewModel : MvxNavigationViewModel
    {
        private TuningLogic tunigLogic;
        protected ResourceManager rm;

        public DiagnosisViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        /// <summary>
        /// Prepares this instance.
        /// called after construction.
        /// </summary>
        public override void Prepare()
        {
        }

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

            //messages
            this.rm = new ResourceManager(typeof(SimTuning.Core.resources));

            return base.Initialize();
        }

        #region Commands

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

        #endregion Commands

        #region Values

        private TuningModel _tuning;

        public TuningModel Tuning
        {
            get => _tuning;
            set { SetProperty(ref _tuning, value); }
        }

        public PlotModel PlotTuning
        {
            get { return tunigLogic.PlotTuning; }
        }

        public PlotController PlotTuningController
        {
            get { return tunigLogic.PlotTuningController; }
        }

        private bool _enable_Zoom;

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

        private bool _enable_verschieben;

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

        private bool _enable_Tracker;

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

        #endregion Values
    }
}